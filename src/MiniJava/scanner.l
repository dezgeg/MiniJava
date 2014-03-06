%{
	private int comments;
	private Location commentStartLocation;
	private CompileError prevUnknownCharErr;
	private CompileError prevNonprintableCharErr;

	private CompilerContext CompilerContext;
	public Scanner(CompilerContext ctx) : this()
	{
		CompilerContext = ctx;
	}
	public override void yyerror(string format, params object[] args)
	{
		CompilerContext.AddError(new CompileError(format, yylloc));
	}
	private Location currentLocation()
	{
		return new Location(tokLin,tokCol,tokELin,tokECol,tokPos,tokEPos);
	}
	private bool canMergeError(CompileError err)
	{
		if(err != null && err.Location.StartLine == tokLin)
		{
			err.Location = err.Location.Merge(currentLocation());
			return true;
		}
		return false;
	}
%}

SPACE [ \t\r\n\f\v]
INVALID ["#$':?@\\^`~]
NONPRINTABLE [\x00-\x08\x0e-\x1f\x7f-\xff]
CMT_SKIP  [^*/\x00-\x08\x0e-\x1f\x7f-\xff]
NL_SKIP   [^\n\x00-\x08\x0e-\x1f\x7f-\xff]

%x COMMENT
%x NL_COMMENT
%namespace MiniJava.Frontend
%using MiniJava;

%%

"<=" { return (int)Tokens.LE; }
"<" { return (int)'<'; }
">=" { return (int)Tokens.GE; }
">" { return (int)'>'; }
"==" { return (int)Tokens.EQ; }
"!=" { return (int)Tokens.NE; }

"!" { return (int)'!'; }
"||" { return (int)Tokens.OR; }
"&&" { return (int)Tokens.AND; }

"+" { return (int)'+'; }
"-" { return (int)'-'; }
"*" { return (int)'*'; }
"/" { return (int)'/'; }
"%" { return (int)'%'; }

"=" { return (int)'='; }
"," { return (int)','; }
";" { return (int)';'; }
"." { return (int)'.'; }

"(" { return (int)'('; }
")" { return (int)')'; }
"[" { return (int)'['; }
"]" { return (int)']'; }
"{" { return (int)'{'; }
"}" { return (int)'}'; }
"["{SPACE}*"]" { return (int)Tokens.DIM; }

assert { return (int)Tokens.Assert; }
boolean { return (int)Tokens.Boolean; }
class { return (int)Tokens.Class; }
extends { return (int)Tokens.Extends; }
else { return (int)Tokens.Else; }
false { return (int)Tokens.False; }
if { return (int)Tokens.If; }
int { return (int)Tokens.Int; }
length { return (int)Tokens.Length; }
main { return (int)Tokens.Main; }
new { return (int)Tokens.New; }
null { return (int)Tokens.Null; }
out { return (int)Tokens.Out; }
println { return (int)Tokens.PrintLn; }
public { return (int)Tokens.Public; }
return { return (int)Tokens.Return; }
static { return (int)Tokens.Static; }
System { return (int)Tokens.System; }
this { return (int)Tokens.This; }
true { return (int)Tokens.True; }
void { return (int)Tokens.Void; }
while { return (int)Tokens.While; }

[0-9]+ {
    // this gets awfully tricky if we want to be able to type Int32.MinValue in our MiniJava code
	// we define YYSTYPE#IntValue as long, and deal with range checks in semantic checker
	// also, we have to kludge the UnopTree to turn Unop<Minus>(Lit<Int>:42) to Lit<Int>(-42)
	try {
		yylval.IntValue = Convert.ToInt64(yytext);
	} catch (OverflowException)
	{
		yylval.IntValue = Int64.MaxValue;
	}
	return (int)Tokens.IntLiteral;
}
[a-zA-Z_][a-zA-Z_0-9]*	{ yylval.StringValue = yytext; return (int)Tokens.Identifier; }

{SPACE}+				{ ; }
{SPACE}*"//" { BEGIN(NL_COMMENT); }

<NL_COMMENT> {
	\n { BEGIN(INITIAL); }
	{NL_SKIP}+   { ; }
}

"/*" { 
	comments = 1;
	commentStartLocation = currentLocation();
	BEGIN(COMMENT);
}
{INVALID}+ {
	if(!canMergeError(prevUnknownCharErr))
	{
		prevUnknownCharErr = new CompileError("Unknown characters in source file", currentLocation());
		CompilerContext.AddError(prevUnknownCharErr);
	}
}

<COMMENT> {
	("*"|{CMT_SKIP})+	{ ; }
	("/"|{CMT_SKIP})+	{ ; }
	("*"|{CMT_SKIP})*"*/" { comments--; if(comments == 0) BEGIN(INITIAL); }
	("/"|{CMT_SKIP})*"/*"{CMT_SKIP}* { comments++; }
	<<EOF>> { CompilerContext.AddError(new CompileError("Unterminated comment",
	             currentLocation(),
				 "Comment started",
				 commentStartLocation));
	}
}
<*> {
	{NONPRINTABLE}+ {
		if(!canMergeError(prevNonprintableCharErr))
		{
			prevNonprintableCharErr = new CompileError("Nonprintable or non-ASCII characters in source file", currentLocation());
			CompilerContext.AddError(prevNonprintableCharErr);
		}
	}
}
%{
	this.yylloc = currentLocation();
%}