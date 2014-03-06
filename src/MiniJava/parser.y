%{
	public CompilerContext CompilerContext { get; protected set; }
	protected ClassTree CurrentClass { get; set; }
	public IDictionary<String, ClassTree> Classes
	{
		get { return CompilerContext.Classes; }
	}
%}
%token Assert Boolean Class Else Extends False If Int Length Main New Null Out PrintLn Public Return Static System This True Void While
%token <IntValue> IntLiteral
%token <StringValue> Identifier
%token EQ NE GE LE OR AND DIM

%type <StringValue> opt_extends 
%type <ExprValue> expr opt_expr logical_or_expr logical_and_expr cmp_expr rel_expr term_expr factor_expr unary_expr dot_expr object_expr new_initializer
%type <StmtValue> statement opt_else
%type <StmtListValue> opt_statement_list
%type <TypeValue> type basic_type
%type <DeclTreeValue> var_decl
%type <DeclListValue> opt_arg_decl_list arg_decl_list_tail
%type <ExprListValue> opt_call_arg_list call_arg_list_tail

%namespace MiniJava.Frontend
%using MiniJava.Frontend.Trees
%YYLTYPE Location
%partial

%union{
	public string StringValue;
	public long IntValue;
	public ExprTree ExprValue;
	public StmtTree StmtValue;
	public JavaType TypeValue;
	public LinkedList<ExprTree> ExprListValue;
	public LinkedList<VariableDeclTree> DeclListValue;
	public VariableDeclTree DeclTreeValue;
	public StmtListTree StmtListValue;
}

%%
program: opt_class_decls
	   ;

opt_class_decls: /* nothing */
			   | opt_class_decls class_decl
			   ;

class_decl: Class Identifier opt_extends { BeginClass(@1.Merge(@3), $2, $3); } 
		    '{' opt_class_entries '}' { EndClass(@$); }
		  ;
opt_extends: /* nothing */ { $$ = null; }
		   | Extends Identifier { $$ = $2; }
		   ;
opt_class_entries: /* nothing */
				 | opt_class_entries class_entry
				 ;
class_entry: var_decl ';' { AddField(@$, $1); } 
		   | method_decl
		   ;
method_decl: Public type Identifier '(' opt_arg_decl_list ')' '{' opt_statement_list '}'
				{ AddMethod(@1.Merge(@6), @$, $3, $2, $5, $8); }
		   | Public Static Void Main '(' ')'
		   '{' opt_statement_list '}' { AddMainMethod(@1.Merge(@6), @$, $8); }
		   ;
type: basic_type
	| basic_type DIM { $$ = new JavaArrayType($1); }
	;
basic_type: Int { $$ = JavaType.Int; }
		  | Boolean { $$ = JavaType.Bool; }
		  | Void { $$ = JavaType.Void; }
		  | Identifier { $$ = new JavaClassType($1); }
		  ;
opt_arg_decl_list: /* nothing */ { $$ = new LinkedList<VariableDeclTree>(); }
		| var_decl arg_decl_list_tail { $$ = $2; $$.AddFirst($1); }
		;
arg_decl_list_tail: /* nothing */ { $$ = new LinkedList<VariableDeclTree>(); }
			      | arg_decl_list_tail ',' var_decl { $$ = $1; $$.AddLast($3); }
			      ;
var_decl: type Identifier { $$ = new VariableDeclTree(@$, $1, $2); }
		;
opt_statement_list: /* nothing */ { $$ = new StmtListTree(@$); }
				  | opt_statement_list statement { $$ = $1; $$.Statements.Add($2); }
				  ;
statement: '{' opt_statement_list '}' { $$ = $2; }
		 | If '(' expr ')' statement opt_else { $$ = new IfStmtTree(@$, $3, $5, $6); }
		 | While '(' expr ')' statement { $$ = new WhileStmtTree(@$, $3, $5); }

		 | var_decl ';' { $$ = $1; }
		 | Assert expr ';' { $$ = new AssertStmtTree(@$, $2); }
		 | System '.' Out '.' PrintLn '(' opt_expr ')' ';' { $$ = new PrintStmtTree(@$, $7); }
		 | Return opt_expr ';' { $$ = new ReturnStmtTree(@$, $2); }
		 | expr ';' { $$ = new ExprStmtTree(@$, $1); }
		 | ';' { $$ = new StmtListTree(@$); }
		 | error ';' { $$ = new StmtListTree(@$); yyerrok(); }
		 ;
opt_else: /* nothing */ { $$ = null; }
		| Else statement { $$ = $2; }
		;
opt_expr : /* nothing */ { $$ = null; }
         | expr { $$ = $1; }
		 ;
expr: logical_or_expr
    | logical_or_expr '=' expr { $$ = new BinopTree(@$, Binop.Assign, $1, $3); }
	;
logical_or_expr: logical_and_expr
			| logical_or_expr OR logical_and_expr { $$ = new BinopTree(@$, Binop.Or, $1, $3); }
			;
logical_and_expr: cmp_expr
			| logical_and_expr AND cmp_expr { $$ = new BinopTree(@$, Binop.And, $1, $3); }
			;
cmp_expr: cmp_expr EQ rel_expr { $$ = new BinopTree(@$, Binop.EQ, $1, $3); }
		| cmp_expr NE rel_expr { $$ = new BinopTree(@$, Binop.NE, $1, $3); }
		| rel_expr
		;
rel_expr: term_expr
		| rel_expr '<' term_expr { $$ = new BinopTree(@$, Binop.LT, $1, $3); }
		| rel_expr '>' term_expr { $$ = new BinopTree(@$, Binop.GT, $1, $3); }
		| rel_expr LE term_expr { $$ = new BinopTree(@$, Binop.LE, $1, $3); }
		| rel_expr GE term_expr { $$ = new BinopTree(@$, Binop.GE, $1, $3); }
		;

term_expr: factor_expr
		 | term_expr '+' factor_expr { $$ = new BinopTree(@$, Binop.Plus, $1, $3); }
		 | term_expr '-' factor_expr { $$ = new BinopTree(@$, Binop.Minus, $1, $3); }
		 ;
factor_expr: unary_expr
		 | factor_expr '*' unary_expr { $$ = new BinopTree(@$, Binop.Times, $1, $3); }
		 | factor_expr '/' unary_expr { $$ = new BinopTree(@$, Binop.Over, $1, $3); }
		 | factor_expr '%' unary_expr { $$ = new BinopTree(@$, Binop.Mod, $1, $3); }
		 ;
unary_expr: dot_expr
		  | '-' unary_expr { $$ = UnopTree.Create(@$, Unop.Minus, $2); }
		  | '!' unary_expr { $$ = UnopTree.Create(@$, Unop.Not, $2); }
		  ;
dot_expr: dot_expr '.' Length { $$ = new ArrayLengthExprTree(@$, $1); }
		| dot_expr '.' Identifier { $$ = new FieldAccessExprTree(@$, $1, $3); }
		| dot_expr '.' Identifier '(' opt_call_arg_list ')' { $$ = new MethodCallExprTree(@$, $1, $3, $5); }
		| dot_expr '[' expr ']' { $$ = new ArrayAccessExprTree(@$, $1, $3); }
		| object_expr
		;
opt_call_arg_list: /* nothing */ { $$ = new LinkedList<ExprTree>(); }
                 | expr call_arg_list_tail { $$ = $2; $$.AddFirst($1); }
		         ;
call_arg_list_tail: /* nothing */ { $$ = new LinkedList<ExprTree>(); }
			      | call_arg_list_tail ',' expr { $$ = $1; $$.AddLast($3); }
			      ;
object_expr: This { $$ = new ThisLiteralTree(@$); }
		   | Null { $$ = new NullLiteralTree(@$); }
           | True { $$ = new LiteralValueTree(@$, true); }
           | False { $$ = new LiteralValueTree(@$, false); }
           | New basic_type new_initializer { $$ = new NewExprTree(@$, $2, $3); }
		   | IntLiteral { $$ = new LiteralValueTree(@$, $1); }
		   | Identifier { $$ = new VariableTree(@$, $1); }
		   | '(' expr ')' { $$ = $2; }
		   ;
new_initializer: '(' ')' { $$ = null; }
			   | '[' expr ']' { $$ = $2; }
               ;
%%