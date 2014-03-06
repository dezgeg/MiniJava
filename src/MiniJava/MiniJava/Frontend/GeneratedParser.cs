// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2008
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.3.5.190
// Machine:  WIN-6BC1J0A4YGN
// DateTime: 22.5.2012 18:52:06
// UserName: Dezgeg
// Input file <parser.y>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using MiniJava.Frontend.Trees;

namespace MiniJava.Frontend
{
public enum Tokens {error=126,
    EOF=127,Assert=128,Boolean=129,Class=130,Else=131,Extends=132,
    False=133,If=134,Int=135,Length=136,Main=137,New=138,
    Null=139,Out=140,PrintLn=141,Public=142,Return=143,Static=144,
    System=145,This=146,True=147,Void=148,While=149,IntLiteral=150,
    Identifier=151,EQ=152,NE=153,GE=154,LE=155,OR=156,
    AND=157,DIM=158};

public partial struct ValueType
{
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
// Abstract base class for GPLEX scanners
public abstract class ScanBase : AbstractScanner<ValueType,Location> {
  private Location __yylloc = new Location();
  public override Location yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

public partial class Parser: ShiftReduceParser<ValueType, Location>
{
	public CompilerContext CompilerContext { get; protected set; }
	protected ClassTree CurrentClass { get; set; }
	public IDictionary<String, ClassTree> Classes
	{
		get { return CompilerContext.Classes; }
	}
#pragma warning disable 649
    private Dictionary<int, string> aliasses;
#pragma warning restore 649

  protected override void Initialize()
  {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);

    this.InitStateTable(159);
    AddState(0,new State(-3,new int[]{-24,1,-26,3}));
    AddState(1,new State(new int[]{127,2}));
    AddState(2,new State(-1));
    AddState(3,new State(new int[]{130,5,127,-2},new int[]{-27,4}));
    AddState(4,new State(-4));
    AddState(5,new State(new int[]{151,6}));
    AddState(6,new State(new int[]{132,157,123,-7},new int[]{-1,7}));
    AddState(7,new State(-5,new int[]{-28,8}));
    AddState(8,new State(new int[]{123,9}));
    AddState(9,new State(-9,new int[]{-29,10}));
    AddState(10,new State(new int[]{125,11,135,19,129,20,148,21,151,22,142,24},new int[]{-30,12,-19,13,-17,15,-18,17,-31,23}));
    AddState(11,new State(-6));
    AddState(12,new State(-10));
    AddState(13,new State(new int[]{59,14}));
    AddState(14,new State(-11));
    AddState(15,new State(new int[]{151,16}));
    AddState(16,new State(-25));
    AddState(17,new State(new int[]{158,18,151,-15}));
    AddState(18,new State(-16));
    AddState(19,new State(-17));
    AddState(20,new State(-18));
    AddState(21,new State(-19));
    AddState(22,new State(-20));
    AddState(23,new State(-12));
    AddState(24,new State(new int[]{144,149,135,19,129,20,148,21,151,22},new int[]{-17,25,-18,17}));
    AddState(25,new State(new int[]{151,26}));
    AddState(26,new State(new int[]{40,27}));
    AddState(27,new State(new int[]{135,19,129,20,148,21,151,22,41,-21},new int[]{-20,28,-19,145,-17,15,-18,17}));
    AddState(28,new State(new int[]{41,29}));
    AddState(29,new State(new int[]{123,30}));
    AddState(30,new State(-26,new int[]{-16,31}));
    AddState(31,new State(new int[]{125,32,123,34,134,37,149,45,135,19,129,20,148,21,151,52,128,53,145,127,143,137,146,91,139,92,147,93,133,94,138,95,150,109,40,111,45,116,33,118,59,142,126,143},new int[]{-14,33,-19,50,-17,15,-18,17,-2,140,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(32,new State(-13));
    AddState(33,new State(-27));
    AddState(34,new State(-26,new int[]{-16,35}));
    AddState(35,new State(new int[]{125,36,123,34,134,37,149,45,135,19,129,20,148,21,151,52,128,53,145,127,143,137,146,91,139,92,147,93,133,94,138,95,150,109,40,111,45,116,33,118,59,142,126,143},new int[]{-14,33,-19,50,-17,15,-18,17,-2,140,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(36,new State(-28));
    AddState(37,new State(new int[]{40,38}));
    AddState(38,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,39,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(39,new State(new int[]{41,40}));
    AddState(40,new State(new int[]{123,34,134,37,149,45,135,19,129,20,148,21,151,52,128,53,145,127,143,137,146,91,139,92,147,93,133,94,138,95,150,109,40,111,45,116,33,118,59,142,126,143},new int[]{-14,41,-19,50,-17,15,-18,17,-2,140,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(41,new State(new int[]{131,43,125,-38,123,-38,134,-38,149,-38,135,-38,129,-38,148,-38,151,-38,128,-38,145,-38,143,-38,146,-38,139,-38,147,-38,133,-38,138,-38,150,-38,40,-38,45,-38,33,-38,59,-38,126,-38},new int[]{-15,42}));
    AddState(42,new State(-29));
    AddState(43,new State(new int[]{123,34,134,37,149,45,135,19,129,20,148,21,151,52,128,53,145,127,143,137,146,91,139,92,147,93,133,94,138,95,150,109,40,111,45,116,33,118,59,142,126,143},new int[]{-14,44,-19,50,-17,15,-18,17,-2,140,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(44,new State(-39));
    AddState(45,new State(new int[]{40,46}));
    AddState(46,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,47,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(47,new State(new int[]{41,48}));
    AddState(48,new State(new int[]{123,34,134,37,149,45,135,19,129,20,148,21,151,52,128,53,145,127,143,137,146,91,139,92,147,93,133,94,138,95,150,109,40,111,45,116,33,118,59,142,126,143},new int[]{-14,49,-19,50,-17,15,-18,17,-2,140,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(49,new State(-30));
    AddState(50,new State(new int[]{59,51}));
    AddState(51,new State(-31));
    AddState(52,new State(new int[]{158,-20,151,-20,46,-81,91,-81,42,-81,47,-81,37,-81,43,-81,45,-81,60,-81,62,-81,155,-81,154,-81,152,-81,153,-81,157,-81,156,-81,61,-81,59,-81}));
    AddState(53,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,54,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(54,new State(new int[]{59,55}));
    AddState(55,new State(-32));
    AddState(56,new State(new int[]{156,57,61,125,59,-42,41,-42,44,-42,93,-42}));
    AddState(57,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-5,58,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(58,new State(new int[]{157,59,156,-45,61,-45,59,-45,41,-45,44,-45,93,-45}));
    AddState(59,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-6,60,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(60,new State(new int[]{152,61,153,82,157,-47,156,-47,61,-47,59,-47,41,-47,44,-47,93,-47}));
    AddState(61,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-7,62,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(62,new State(new int[]{60,63,62,84,155,104,154,120,152,-48,153,-48,157,-48,156,-48,61,-48,59,-48,41,-48,44,-48,93,-48}));
    AddState(63,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-8,64,-9,106,-10,115,-11,69,-12,90}));
    AddState(64,new State(new int[]{43,65,45,86,60,-52,62,-52,155,-52,154,-52,152,-52,153,-52,157,-52,156,-52,61,-52,59,-52,41,-52,44,-52,93,-52}));
    AddState(65,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-9,66,-10,115,-11,69,-12,90}));
    AddState(66,new State(new int[]{42,67,47,88,37,107,43,-57,45,-57,60,-57,62,-57,155,-57,154,-57,152,-57,153,-57,157,-57,156,-57,61,-57,59,-57,41,-57,44,-57,93,-57}));
    AddState(67,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-10,68,-11,69,-12,90}));
    AddState(68,new State(-60));
    AddState(69,new State(new int[]{46,70,91,122,42,-63,47,-63,37,-63,43,-63,45,-63,60,-63,62,-63,155,-63,154,-63,152,-63,153,-63,157,-63,156,-63,61,-63,59,-63,41,-63,44,-63,93,-63}));
    AddState(70,new State(new int[]{136,71,151,72}));
    AddState(71,new State(-66));
    AddState(72,new State(new int[]{40,73,46,-67,91,-67,42,-67,47,-67,37,-67,43,-67,45,-67,60,-67,62,-67,155,-67,154,-67,152,-67,153,-67,157,-67,156,-67,61,-67,59,-67,41,-67,44,-67,93,-67}));
    AddState(73,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118,41,-71},new int[]{-22,74,-2,76,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(74,new State(new int[]{41,75}));
    AddState(75,new State(-68));
    AddState(76,new State(-73,new int[]{-23,77}));
    AddState(77,new State(new int[]{44,78,41,-72}));
    AddState(78,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,79,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(79,new State(-74));
    AddState(80,new State(new int[]{157,59,156,-44,61,-44,59,-44,41,-44,44,-44,93,-44}));
    AddState(81,new State(new int[]{152,61,153,82,157,-46,156,-46,61,-46,59,-46,41,-46,44,-46,93,-46}));
    AddState(82,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-7,83,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(83,new State(new int[]{60,63,62,84,155,104,154,120,152,-49,153,-49,157,-49,156,-49,61,-49,59,-49,41,-49,44,-49,93,-49}));
    AddState(84,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-8,85,-9,106,-10,115,-11,69,-12,90}));
    AddState(85,new State(new int[]{43,65,45,86,60,-53,62,-53,155,-53,154,-53,152,-53,153,-53,157,-53,156,-53,61,-53,59,-53,41,-53,44,-53,93,-53}));
    AddState(86,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-9,87,-10,115,-11,69,-12,90}));
    AddState(87,new State(new int[]{42,67,47,88,37,107,43,-58,45,-58,60,-58,62,-58,155,-58,154,-58,152,-58,153,-58,157,-58,156,-58,61,-58,59,-58,41,-58,44,-58,93,-58}));
    AddState(88,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-10,89,-11,69,-12,90}));
    AddState(89,new State(-61));
    AddState(90,new State(-70));
    AddState(91,new State(-75));
    AddState(92,new State(-76));
    AddState(93,new State(-77));
    AddState(94,new State(-78));
    AddState(95,new State(new int[]{135,19,129,20,148,21,151,22},new int[]{-18,96}));
    AddState(96,new State(new int[]{40,98,91,100},new int[]{-13,97}));
    AddState(97,new State(-79));
    AddState(98,new State(new int[]{41,99}));
    AddState(99,new State(-83));
    AddState(100,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,101,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(101,new State(new int[]{93,102}));
    AddState(102,new State(-84));
    AddState(103,new State(new int[]{60,63,62,84,155,104,154,120,152,-50,153,-50,157,-50,156,-50,61,-50,59,-50,41,-50,44,-50,93,-50}));
    AddState(104,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-8,105,-9,106,-10,115,-11,69,-12,90}));
    AddState(105,new State(new int[]{43,65,45,86,60,-54,62,-54,155,-54,154,-54,152,-54,153,-54,157,-54,156,-54,61,-54,59,-54,41,-54,44,-54,93,-54}));
    AddState(106,new State(new int[]{42,67,47,88,37,107,43,-56,45,-56,60,-56,62,-56,155,-56,154,-56,152,-56,153,-56,157,-56,156,-56,61,-56,59,-56,41,-56,44,-56,93,-56}));
    AddState(107,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-10,108,-11,69,-12,90}));
    AddState(108,new State(-62));
    AddState(109,new State(-80));
    AddState(110,new State(-81));
    AddState(111,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,112,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(112,new State(new int[]{41,113}));
    AddState(113,new State(-82));
    AddState(114,new State(new int[]{43,65,45,86,60,-51,62,-51,155,-51,154,-51,152,-51,153,-51,157,-51,156,-51,61,-51,59,-51,41,-51,44,-51,93,-51}));
    AddState(115,new State(-59));
    AddState(116,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-10,117,-11,69,-12,90}));
    AddState(117,new State(-64));
    AddState(118,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-10,119,-11,69,-12,90}));
    AddState(119,new State(-65));
    AddState(120,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-8,121,-9,106,-10,115,-11,69,-12,90}));
    AddState(121,new State(new int[]{43,65,45,86,60,-55,62,-55,155,-55,154,-55,152,-55,153,-55,157,-55,156,-55,61,-55,59,-55,41,-55,44,-55,93,-55}));
    AddState(122,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,123,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(123,new State(new int[]{93,124}));
    AddState(124,new State(-69));
    AddState(125,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118},new int[]{-2,126,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(126,new State(-43));
    AddState(127,new State(new int[]{46,128}));
    AddState(128,new State(new int[]{140,129}));
    AddState(129,new State(new int[]{46,130}));
    AddState(130,new State(new int[]{141,131}));
    AddState(131,new State(new int[]{40,132}));
    AddState(132,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118,41,-40},new int[]{-3,133,-2,136,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(133,new State(new int[]{41,134}));
    AddState(134,new State(new int[]{59,135}));
    AddState(135,new State(-33));
    AddState(136,new State(-41));
    AddState(137,new State(new int[]{146,91,139,92,147,93,133,94,138,95,150,109,151,110,40,111,45,116,33,118,59,-40},new int[]{-3,138,-2,136,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(138,new State(new int[]{59,139}));
    AddState(139,new State(-34));
    AddState(140,new State(new int[]{59,141}));
    AddState(141,new State(-35));
    AddState(142,new State(-36));
    AddState(143,new State(new int[]{59,144}));
    AddState(144,new State(-37));
    AddState(145,new State(-23,new int[]{-21,146}));
    AddState(146,new State(new int[]{44,147,41,-22}));
    AddState(147,new State(new int[]{135,19,129,20,148,21,151,22},new int[]{-19,148,-17,15,-18,17}));
    AddState(148,new State(-24));
    AddState(149,new State(new int[]{148,150}));
    AddState(150,new State(new int[]{137,151}));
    AddState(151,new State(new int[]{40,152}));
    AddState(152,new State(new int[]{41,153}));
    AddState(153,new State(new int[]{123,154}));
    AddState(154,new State(-26,new int[]{-16,155}));
    AddState(155,new State(new int[]{125,156,123,34,134,37,149,45,135,19,129,20,148,21,151,52,128,53,145,127,143,137,146,91,139,92,147,93,133,94,138,95,150,109,40,111,45,116,33,118,59,142,126,143},new int[]{-14,33,-19,50,-17,15,-18,17,-2,140,-4,56,-5,80,-6,81,-7,103,-8,114,-9,106,-10,115,-11,69,-12,90}));
    AddState(156,new State(-14));
    AddState(157,new State(new int[]{151,158}));
    AddState(158,new State(-8));

    Rule[] rules=new Rule[85];
    rules[1]=new Rule(-25, new int[]{-24,127});
    rules[2]=new Rule(-24, new int[]{-26});
    rules[3]=new Rule(-26, new int[]{});
    rules[4]=new Rule(-26, new int[]{-26,-27});
    rules[5]=new Rule(-28, new int[]{});
    rules[6]=new Rule(-27, new int[]{130,151,-1,-28,123,-29,125});
    rules[7]=new Rule(-1, new int[]{});
    rules[8]=new Rule(-1, new int[]{132,151});
    rules[9]=new Rule(-29, new int[]{});
    rules[10]=new Rule(-29, new int[]{-29,-30});
    rules[11]=new Rule(-30, new int[]{-19,59});
    rules[12]=new Rule(-30, new int[]{-31});
    rules[13]=new Rule(-31, new int[]{142,-17,151,40,-20,41,123,-16,125});
    rules[14]=new Rule(-31, new int[]{142,144,148,137,40,41,123,-16,125});
    rules[15]=new Rule(-17, new int[]{-18});
    rules[16]=new Rule(-17, new int[]{-18,158});
    rules[17]=new Rule(-18, new int[]{135});
    rules[18]=new Rule(-18, new int[]{129});
    rules[19]=new Rule(-18, new int[]{148});
    rules[20]=new Rule(-18, new int[]{151});
    rules[21]=new Rule(-20, new int[]{});
    rules[22]=new Rule(-20, new int[]{-19,-21});
    rules[23]=new Rule(-21, new int[]{});
    rules[24]=new Rule(-21, new int[]{-21,44,-19});
    rules[25]=new Rule(-19, new int[]{-17,151});
    rules[26]=new Rule(-16, new int[]{});
    rules[27]=new Rule(-16, new int[]{-16,-14});
    rules[28]=new Rule(-14, new int[]{123,-16,125});
    rules[29]=new Rule(-14, new int[]{134,40,-2,41,-14,-15});
    rules[30]=new Rule(-14, new int[]{149,40,-2,41,-14});
    rules[31]=new Rule(-14, new int[]{-19,59});
    rules[32]=new Rule(-14, new int[]{128,-2,59});
    rules[33]=new Rule(-14, new int[]{145,46,140,46,141,40,-3,41,59});
    rules[34]=new Rule(-14, new int[]{143,-3,59});
    rules[35]=new Rule(-14, new int[]{-2,59});
    rules[36]=new Rule(-14, new int[]{59});
    rules[37]=new Rule(-14, new int[]{126,59});
    rules[38]=new Rule(-15, new int[]{});
    rules[39]=new Rule(-15, new int[]{131,-14});
    rules[40]=new Rule(-3, new int[]{});
    rules[41]=new Rule(-3, new int[]{-2});
    rules[42]=new Rule(-2, new int[]{-4});
    rules[43]=new Rule(-2, new int[]{-4,61,-2});
    rules[44]=new Rule(-4, new int[]{-5});
    rules[45]=new Rule(-4, new int[]{-4,156,-5});
    rules[46]=new Rule(-5, new int[]{-6});
    rules[47]=new Rule(-5, new int[]{-5,157,-6});
    rules[48]=new Rule(-6, new int[]{-6,152,-7});
    rules[49]=new Rule(-6, new int[]{-6,153,-7});
    rules[50]=new Rule(-6, new int[]{-7});
    rules[51]=new Rule(-7, new int[]{-8});
    rules[52]=new Rule(-7, new int[]{-7,60,-8});
    rules[53]=new Rule(-7, new int[]{-7,62,-8});
    rules[54]=new Rule(-7, new int[]{-7,155,-8});
    rules[55]=new Rule(-7, new int[]{-7,154,-8});
    rules[56]=new Rule(-8, new int[]{-9});
    rules[57]=new Rule(-8, new int[]{-8,43,-9});
    rules[58]=new Rule(-8, new int[]{-8,45,-9});
    rules[59]=new Rule(-9, new int[]{-10});
    rules[60]=new Rule(-9, new int[]{-9,42,-10});
    rules[61]=new Rule(-9, new int[]{-9,47,-10});
    rules[62]=new Rule(-9, new int[]{-9,37,-10});
    rules[63]=new Rule(-10, new int[]{-11});
    rules[64]=new Rule(-10, new int[]{45,-10});
    rules[65]=new Rule(-10, new int[]{33,-10});
    rules[66]=new Rule(-11, new int[]{-11,46,136});
    rules[67]=new Rule(-11, new int[]{-11,46,151});
    rules[68]=new Rule(-11, new int[]{-11,46,151,40,-22,41});
    rules[69]=new Rule(-11, new int[]{-11,91,-2,93});
    rules[70]=new Rule(-11, new int[]{-12});
    rules[71]=new Rule(-22, new int[]{});
    rules[72]=new Rule(-22, new int[]{-2,-23});
    rules[73]=new Rule(-23, new int[]{});
    rules[74]=new Rule(-23, new int[]{-23,44,-2});
    rules[75]=new Rule(-12, new int[]{146});
    rules[76]=new Rule(-12, new int[]{139});
    rules[77]=new Rule(-12, new int[]{147});
    rules[78]=new Rule(-12, new int[]{133});
    rules[79]=new Rule(-12, new int[]{138,-18,-13});
    rules[80]=new Rule(-12, new int[]{150});
    rules[81]=new Rule(-12, new int[]{151});
    rules[82]=new Rule(-12, new int[]{40,-2,41});
    rules[83]=new Rule(-13, new int[]{40,41});
    rules[84]=new Rule(-13, new int[]{91,-2,93});
    this.InitRules(rules);

    this.InitNonTerminals(new string[] {"", "opt_extends", "expr", "opt_expr", 
      "logical_or_expr", "logical_and_expr", "cmp_expr", "rel_expr", "term_expr", 
      "factor_expr", "unary_expr", "dot_expr", "object_expr", "new_initializer", 
      "statement", "opt_else", "opt_statement_list", "type", "basic_type", "var_decl", 
      "opt_arg_decl_list", "arg_decl_list_tail", "opt_call_arg_list", "call_arg_list_tail", 
      "program", "$accept", "opt_class_decls", "class_decl", "Anon@1", "opt_class_entries", 
      "class_entry", "method_decl", });
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
      case 5: // Anon@1 -> /* empty */
{ BeginClass(LocationStack[LocationStack.Depth-3].Merge(LocationStack[LocationStack.Depth-1]), ValueStack[ValueStack.Depth-2].StringValue, ValueStack[ValueStack.Depth-1].StringValue); }
        break;
      case 6: // class_decl -> Class, Identifier, opt_extends, Anon@1, '{', opt_class_entries, 
              //               '}'
{ EndClass(CurrentLocationSpan); }
        break;
      case 7: // opt_extends -> /* empty */
{ CurrentSemanticValue.StringValue = null; }
        break;
      case 8: // opt_extends -> Extends, Identifier
{ CurrentSemanticValue.StringValue = ValueStack[ValueStack.Depth-1].StringValue; }
        break;
      case 11: // class_entry -> var_decl, ';'
{ AddField(CurrentLocationSpan, ValueStack[ValueStack.Depth-2].DeclTreeValue); }
        break;
      case 13: // method_decl -> Public, type, Identifier, '(', opt_arg_decl_list, ')', '{', 
               //                opt_statement_list, '}'
{ AddMethod(LocationStack[LocationStack.Depth-9].Merge(LocationStack[LocationStack.Depth-4]), CurrentLocationSpan, ValueStack[ValueStack.Depth-7].StringValue, ValueStack[ValueStack.Depth-8].TypeValue, ValueStack[ValueStack.Depth-5].DeclListValue, ValueStack[ValueStack.Depth-2].StmtListValue); }
        break;
      case 14: // method_decl -> Public, Static, Void, Main, '(', ')', '{', opt_statement_list, 
               //                '}'
{ AddMainMethod(LocationStack[LocationStack.Depth-9].Merge(LocationStack[LocationStack.Depth-4]), CurrentLocationSpan, ValueStack[ValueStack.Depth-2].StmtListValue); }
        break;
      case 16: // type -> basic_type, DIM
{ CurrentSemanticValue.TypeValue = new JavaArrayType(ValueStack[ValueStack.Depth-2].TypeValue); }
        break;
      case 17: // basic_type -> Int
{ CurrentSemanticValue.TypeValue = JavaType.Int; }
        break;
      case 18: // basic_type -> Boolean
{ CurrentSemanticValue.TypeValue = JavaType.Bool; }
        break;
      case 19: // basic_type -> Void
{ CurrentSemanticValue.TypeValue = JavaType.Void; }
        break;
      case 20: // basic_type -> Identifier
{ CurrentSemanticValue.TypeValue = new JavaClassType(ValueStack[ValueStack.Depth-1].StringValue); }
        break;
      case 21: // opt_arg_decl_list -> /* empty */
{ CurrentSemanticValue.DeclListValue = new LinkedList<VariableDeclTree>(); }
        break;
      case 22: // opt_arg_decl_list -> var_decl, arg_decl_list_tail
{ CurrentSemanticValue.DeclListValue = ValueStack[ValueStack.Depth-1].DeclListValue; CurrentSemanticValue.DeclListValue.AddFirst(ValueStack[ValueStack.Depth-2].DeclTreeValue); }
        break;
      case 23: // arg_decl_list_tail -> /* empty */
{ CurrentSemanticValue.DeclListValue = new LinkedList<VariableDeclTree>(); }
        break;
      case 24: // arg_decl_list_tail -> arg_decl_list_tail, ',', var_decl
{ CurrentSemanticValue.DeclListValue = ValueStack[ValueStack.Depth-3].DeclListValue; CurrentSemanticValue.DeclListValue.AddLast(ValueStack[ValueStack.Depth-1].DeclTreeValue); }
        break;
      case 25: // var_decl -> type, Identifier
{ CurrentSemanticValue.DeclTreeValue = new VariableDeclTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-2].TypeValue, ValueStack[ValueStack.Depth-1].StringValue); }
        break;
      case 26: // opt_statement_list -> /* empty */
{ CurrentSemanticValue.StmtListValue = new StmtListTree(CurrentLocationSpan); }
        break;
      case 27: // opt_statement_list -> opt_statement_list, statement
{ CurrentSemanticValue.StmtListValue = ValueStack[ValueStack.Depth-2].StmtListValue; CurrentSemanticValue.StmtListValue.Statements.Add(ValueStack[ValueStack.Depth-1].StmtValue); }
        break;
      case 28: // statement -> '{', opt_statement_list, '}'
{ CurrentSemanticValue.StmtValue = ValueStack[ValueStack.Depth-2].StmtListValue; }
        break;
      case 29: // statement -> If, '(', expr, ')', statement, opt_else
{ CurrentSemanticValue.StmtValue = new IfStmtTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-4].ExprValue, ValueStack[ValueStack.Depth-2].StmtValue, ValueStack[ValueStack.Depth-1].StmtValue); }
        break;
      case 30: // statement -> While, '(', expr, ')', statement
{ CurrentSemanticValue.StmtValue = new WhileStmtTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].StmtValue); }
        break;
      case 31: // statement -> var_decl, ';'
{ CurrentSemanticValue.StmtValue = ValueStack[ValueStack.Depth-2].DeclTreeValue; }
        break;
      case 32: // statement -> Assert, expr, ';'
{ CurrentSemanticValue.StmtValue = new AssertStmtTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-2].ExprValue); }
        break;
      case 33: // statement -> System, '.', Out, '.', PrintLn, '(', opt_expr, ')', ';'
{ CurrentSemanticValue.StmtValue = new PrintStmtTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-3].ExprValue); }
        break;
      case 34: // statement -> Return, opt_expr, ';'
{ CurrentSemanticValue.StmtValue = new ReturnStmtTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-2].ExprValue); }
        break;
      case 35: // statement -> expr, ';'
{ CurrentSemanticValue.StmtValue = new ExprStmtTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-2].ExprValue); }
        break;
      case 36: // statement -> ';'
{ CurrentSemanticValue.StmtValue = new StmtListTree(CurrentLocationSpan); }
        break;
      case 37: // statement -> error, ';'
{ CurrentSemanticValue.StmtValue = new StmtListTree(CurrentLocationSpan); yyerrok(); }
        break;
      case 38: // opt_else -> /* empty */
{ CurrentSemanticValue.StmtValue = null; }
        break;
      case 39: // opt_else -> Else, statement
{ CurrentSemanticValue.StmtValue = ValueStack[ValueStack.Depth-1].StmtValue; }
        break;
      case 40: // opt_expr -> /* empty */
{ CurrentSemanticValue.ExprValue = null; }
        break;
      case 41: // opt_expr -> expr
{ CurrentSemanticValue.ExprValue = ValueStack[ValueStack.Depth-1].ExprValue; }
        break;
      case 43: // expr -> logical_or_expr, '=', expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.Assign, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 45: // logical_or_expr -> logical_or_expr, OR, logical_and_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.Or, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 47: // logical_and_expr -> logical_and_expr, AND, cmp_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.And, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 48: // cmp_expr -> cmp_expr, EQ, rel_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.EQ, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 49: // cmp_expr -> cmp_expr, NE, rel_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.NE, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 52: // rel_expr -> rel_expr, '<', term_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.LT, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 53: // rel_expr -> rel_expr, '>', term_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.GT, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 54: // rel_expr -> rel_expr, LE, term_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.LE, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 55: // rel_expr -> rel_expr, GE, term_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.GE, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 57: // term_expr -> term_expr, '+', factor_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.Plus, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 58: // term_expr -> term_expr, '-', factor_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.Minus, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 60: // factor_expr -> factor_expr, '*', unary_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.Times, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 61: // factor_expr -> factor_expr, '/', unary_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.Over, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 62: // factor_expr -> factor_expr, '%', unary_expr
{ CurrentSemanticValue.ExprValue = new BinopTree(CurrentLocationSpan, Binop.Mod, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 64: // unary_expr -> '-', unary_expr
{ CurrentSemanticValue.ExprValue = UnopTree.Create(CurrentLocationSpan, Unop.Minus, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 65: // unary_expr -> '!', unary_expr
{ CurrentSemanticValue.ExprValue = UnopTree.Create(CurrentLocationSpan, Unop.Not, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 66: // dot_expr -> dot_expr, '.', Length
{ CurrentSemanticValue.ExprValue = new ArrayLengthExprTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-3].ExprValue); }
        break;
      case 67: // dot_expr -> dot_expr, '.', Identifier
{ CurrentSemanticValue.ExprValue = new FieldAccessExprTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-3].ExprValue, ValueStack[ValueStack.Depth-1].StringValue); }
        break;
      case 68: // dot_expr -> dot_expr, '.', Identifier, '(', opt_call_arg_list, ')'
{ CurrentSemanticValue.ExprValue = new MethodCallExprTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-6].ExprValue, ValueStack[ValueStack.Depth-4].StringValue, ValueStack[ValueStack.Depth-2].ExprListValue); }
        break;
      case 69: // dot_expr -> dot_expr, '[', expr, ']'
{ CurrentSemanticValue.ExprValue = new ArrayAccessExprTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-4].ExprValue, ValueStack[ValueStack.Depth-2].ExprValue); }
        break;
      case 71: // opt_call_arg_list -> /* empty */
{ CurrentSemanticValue.ExprListValue = new LinkedList<ExprTree>(); }
        break;
      case 72: // opt_call_arg_list -> expr, call_arg_list_tail
{ CurrentSemanticValue.ExprListValue = ValueStack[ValueStack.Depth-1].ExprListValue; CurrentSemanticValue.ExprListValue.AddFirst(ValueStack[ValueStack.Depth-2].ExprValue); }
        break;
      case 73: // call_arg_list_tail -> /* empty */
{ CurrentSemanticValue.ExprListValue = new LinkedList<ExprTree>(); }
        break;
      case 74: // call_arg_list_tail -> call_arg_list_tail, ',', expr
{ CurrentSemanticValue.ExprListValue = ValueStack[ValueStack.Depth-3].ExprListValue; CurrentSemanticValue.ExprListValue.AddLast(ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 75: // object_expr -> This
{ CurrentSemanticValue.ExprValue = new ThisLiteralTree(CurrentLocationSpan); }
        break;
      case 76: // object_expr -> Null
{ CurrentSemanticValue.ExprValue = new NullLiteralTree(CurrentLocationSpan); }
        break;
      case 77: // object_expr -> True
{ CurrentSemanticValue.ExprValue = new LiteralValueTree(CurrentLocationSpan, true); }
        break;
      case 78: // object_expr -> False
{ CurrentSemanticValue.ExprValue = new LiteralValueTree(CurrentLocationSpan, false); }
        break;
      case 79: // object_expr -> New, basic_type, new_initializer
{ CurrentSemanticValue.ExprValue = new NewExprTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-2].TypeValue, ValueStack[ValueStack.Depth-1].ExprValue); }
        break;
      case 80: // object_expr -> IntLiteral
{ CurrentSemanticValue.ExprValue = new LiteralValueTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-1].IntValue); }
        break;
      case 81: // object_expr -> Identifier
{ CurrentSemanticValue.ExprValue = new VariableTree(CurrentLocationSpan, ValueStack[ValueStack.Depth-1].StringValue); }
        break;
      case 82: // object_expr -> '(', expr, ')'
{ CurrentSemanticValue.ExprValue = ValueStack[ValueStack.Depth-2].ExprValue; }
        break;
      case 83: // new_initializer -> '(', ')'
{ CurrentSemanticValue.ExprValue = null; }
        break;
      case 84: // new_initializer -> '[', expr, ']'
{ CurrentSemanticValue.ExprValue = ValueStack[ValueStack.Depth-2].ExprValue; }
        break;
    }
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

}
}