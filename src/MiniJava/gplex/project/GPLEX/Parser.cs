// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2008
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.3.5.190
// Machine:  RADNOR2
// DateTime: 19/03/2009 10:09:12 AM
// UserName: John Gough
// Input file <gplex.y>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using System.Collections;

namespace QUT.Gplex.Parser
{
internal enum Tokens {
    error=1,EOF=2,csKeyword=3,csIdent=4,csNumber=5,csLitstr=6,
    csLitchr=7,csOp=8,csBar=9,csDot=10,semi=11,csStar=12,
    csLT=13,csGT=14,comma=15,csSlash=16,csLBrac=17,csRBrac=18,
    csLPar=19,csRPar=20,csLBrace=21,csRBrace=22,verbatim=23,pattern=24,
    name=25,lCond=26,rCond=27,lxLBrace=28,lxRBrace=29,lxBar=30,
    defCommentS=31,defCommentE=32,csCommentS=33,csCommentE=34,usingTag=35,namespaceTag=36,
    optionTag=37,charSetPredTag=38,inclTag=39,exclTag=40,lPcBrace=41,rPcBrace=42,
    visibilityTag=43,PCPC=44,userCharPredTag=45,scanbaseTag=46,tokentypeTag=47,scannertypeTag=48,
    lxIndent=49,lxEndIndent=50,maxParseToken=51,EOL=52,csCommentL=53,errTok=54,
    repErr=55};

// Abstract base class for GPLEX scanners
internal abstract class ScanBase : AbstractScanner<int,LexSpan> {
  private LexSpan __yylloc = new LexSpan();
  public override LexSpan yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

internal partial class Parser: ShiftReduceParser<int, LexSpan>
{
#pragma warning disable 649
    private Dictionary<int, string> aliasses;
#pragma warning restore 649

  protected override void Initialize()
  {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);

    this.InitStateTable(160);
    AddState(0,new State(new int[]{25,116,40,119,39,121,35,123,36,127,43,129,37,131,41,97,31,139,32,140,49,105,38,142,46,144,47,146,48,148,45,150,44,157,1,158},new int[]{-1,1,-3,3,-7,113,-9,156,-12,133,-13,134,-17,135,-14,141}));
    AddState(1,new State(new int[]{2,2}));
    AddState(2,new State(-1));
    AddState(3,new State(new int[]{26,80,24,92,1,94,41,97,49,105,33,19,34,111,44,-53,2,-53},new int[]{-4,4,-5,6,-21,59,-22,112,-23,61,-25,62,-26,63,-24,95,-12,96,-14,104,-19,110,-20,13}));
    AddState(4,new State(new int[]{44,5,2,-2}));
    AddState(5,new State(-7));
    AddState(6,new State(new int[]{33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49,1,58,2,-9},new int[]{-6,7,-8,8,-16,9,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(7,new State(-3));
    AddState(8,new State(-8));
    AddState(9,new State(new int[]{33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49,2,-72,26,-72,24,-72,1,-72,41,-72,49,-72,44,-72,29,-72,50,-72},new int[]{-29,10,-30,11,-19,12,-20,13,-11,20}));
    AddState(10,new State(-74));
    AddState(11,new State(-75));
    AddState(12,new State(-89));
    AddState(13,new State(new int[]{33,15,31,16,32,17,34,18},new int[]{-18,14}));
    AddState(14,new State(-48));
    AddState(15,new State(-49));
    AddState(16,new State(-50));
    AddState(17,new State(-43));
    AddState(18,new State(-44));
    AddState(19,new State(-51));
    AddState(20,new State(new int[]{10,21,33,-90,4,-90,3,-90,34,-90,53,-90,5,-90,6,-90,7,-90,8,-90,12,-90,13,-90,14,-90,11,-90,15,-90,16,-90,9,-90,21,-90,19,-90,17,-90,2,-90,26,-90,24,-90,1,-90,41,-90,49,-90,44,-90,29,-90,50,-90,22,-90,20,-90,18,-90,42,-90}));
    AddState(21,new State(new int[]{4,22}));
    AddState(22,new State(-88));
    AddState(23,new State(-86));
    AddState(24,new State(new int[]{10,25,33,-93,4,-93,3,-93,34,-93,53,-93,5,-93,6,-93,7,-93,8,-93,12,-93,13,-93,14,-93,11,-93,15,-93,16,-93,9,-93,21,-93,19,-93,17,-93,2,-93,26,-93,24,-93,1,-93,41,-93,49,-93,44,-93,29,-93,50,-93,22,-93,20,-93,18,-93,42,-93}));
    AddState(25,new State(new int[]{4,26}));
    AddState(26,new State(-87));
    AddState(27,new State(-91));
    AddState(28,new State(-92));
    AddState(29,new State(-94));
    AddState(30,new State(-95));
    AddState(31,new State(-96));
    AddState(32,new State(-97));
    AddState(33,new State(-98));
    AddState(34,new State(-99));
    AddState(35,new State(-100));
    AddState(36,new State(-101));
    AddState(37,new State(-102));
    AddState(38,new State(-103));
    AddState(39,new State(-104));
    AddState(40,new State(-105));
    AddState(41,new State(new int[]{22,42,1,57,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-16,43,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(42,new State(-77));
    AddState(43,new State(new int[]{22,44,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-29,10,-30,11,-19,12,-20,13,-11,20}));
    AddState(44,new State(-78));
    AddState(45,new State(new int[]{20,46,1,56,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-16,47,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(46,new State(-79));
    AddState(47,new State(new int[]{20,48,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-29,10,-30,11,-19,12,-20,13,-11,20}));
    AddState(48,new State(-80));
    AddState(49,new State(new int[]{18,50,1,53,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-16,51,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(50,new State(-81));
    AddState(51,new State(new int[]{18,52,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-29,10,-30,11,-19,12,-20,13,-11,20}));
    AddState(52,new State(-82));
    AddState(53,new State(-84));
    AddState(54,new State(-73));
    AddState(55,new State(-76));
    AddState(56,new State(-83));
    AddState(57,new State(-85));
    AddState(58,new State(-10));
    AddState(59,new State(new int[]{26,80,24,92,1,94,41,97,49,105,33,19,34,111,44,-52,2,-52},new int[]{-22,60,-23,61,-25,62,-26,63,-24,95,-12,96,-14,104,-19,110,-20,13}));
    AddState(60,new State(-54));
    AddState(61,new State(-56));
    AddState(62,new State(-62));
    AddState(63,new State(new int[]{24,64,28,75}));
    AddState(64,new State(new int[]{28,66,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49,30,73,1,74},new int[]{-28,65,-8,72,-16,9,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(65,new State(-67));
    AddState(66,new State(new int[]{29,69,1,70,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-8,67,-16,9,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(67,new State(new int[]{29,68}));
    AddState(68,new State(-106));
    AddState(69,new State(-109));
    AddState(70,new State(new int[]{29,71}));
    AddState(71,new State(-110));
    AddState(72,new State(-107));
    AddState(73,new State(-108));
    AddState(74,new State(-111));
    AddState(75,new State(-64,new int[]{-27,76}));
    AddState(76,new State(new int[]{29,77,26,80,24,92,1,94},new int[]{-25,78,-24,79,-26,63}));
    AddState(77,new State(-63));
    AddState(78,new State(-65));
    AddState(79,new State(-66));
    AddState(80,new State(new int[]{12,83,25,89,5,90,1,91},new int[]{-10,81,-15,85}));
    AddState(81,new State(new int[]{27,82}));
    AddState(82,new State(-70));
    AddState(83,new State(new int[]{27,84}));
    AddState(84,new State(-71));
    AddState(85,new State(new int[]{15,86,27,-31,44,-31,25,-31,40,-31,39,-31,35,-31,36,-31,43,-31,37,-31,41,-31,31,-31,32,-31,49,-31,38,-31,46,-31,47,-31,48,-31,45,-31}));
    AddState(86,new State(new int[]{25,87,1,88}));
    AddState(87,new State(-33));
    AddState(88,new State(-36));
    AddState(89,new State(-34));
    AddState(90,new State(-35));
    AddState(91,new State(-32));
    AddState(92,new State(new int[]{28,66,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49,30,73,1,74},new int[]{-28,93,-8,72,-16,9,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(93,new State(-68));
    AddState(94,new State(-69));
    AddState(95,new State(-57));
    AddState(96,new State(-58));
    AddState(97,new State(new int[]{42,98,1,101,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-16,99,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(98,new State(-37));
    AddState(99,new State(new int[]{42,100,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-29,10,-30,11,-19,12,-20,13,-11,20}));
    AddState(100,new State(-38));
    AddState(101,new State(new int[]{42,102,44,103}));
    AddState(102,new State(-39));
    AddState(103,new State(-40));
    AddState(104,new State(-59));
    AddState(105,new State(new int[]{1,108,33,19,4,23,3,24,34,27,53,28,5,29,6,30,7,31,8,32,10,33,12,34,13,35,14,36,11,37,15,38,16,39,9,40,21,41,19,45,17,49},new int[]{-8,106,-16,9,-29,54,-19,12,-20,13,-11,20,-30,55}));
    AddState(106,new State(new int[]{50,107}));
    AddState(107,new State(-29));
    AddState(108,new State(new int[]{50,109}));
    AddState(109,new State(-30));
    AddState(110,new State(-60));
    AddState(111,new State(-61));
    AddState(112,new State(-55));
    AddState(113,new State(new int[]{44,114,25,116,40,119,39,121,35,123,36,127,43,129,37,131,41,97,31,139,32,140,49,105,38,142,46,144,47,146,48,148,45,150},new int[]{-9,115,-12,133,-13,134,-17,135,-14,141}));
    AddState(114,new State(-4));
    AddState(115,new State(-11));
    AddState(116,new State(new int[]{23,117,24,118}));
    AddState(117,new State(-13));
    AddState(118,new State(-14));
    AddState(119,new State(new int[]{25,89,5,90,1,91},new int[]{-10,120,-15,85}));
    AddState(120,new State(-15));
    AddState(121,new State(new int[]{25,89,5,90,1,91},new int[]{-10,122,-15,85}));
    AddState(122,new State(-16));
    AddState(123,new State(new int[]{4,23,3,126},new int[]{-11,124}));
    AddState(124,new State(new int[]{11,125,10,21}));
    AddState(125,new State(-17));
    AddState(126,new State(new int[]{10,25}));
    AddState(127,new State(new int[]{4,23,3,126},new int[]{-11,128}));
    AddState(128,new State(new int[]{10,21,44,-18,25,-18,40,-18,39,-18,35,-18,36,-18,43,-18,37,-18,41,-18,31,-18,32,-18,49,-18,38,-18,46,-18,47,-18,48,-18,45,-18}));
    AddState(129,new State(new int[]{3,130}));
    AddState(130,new State(-19));
    AddState(131,new State(new int[]{23,132}));
    AddState(132,new State(-20));
    AddState(133,new State(-21));
    AddState(134,new State(-22));
    AddState(135,new State(new int[]{31,137,33,138,32,17,34,18},new int[]{-18,136}));
    AddState(136,new State(-41));
    AddState(137,new State(-45));
    AddState(138,new State(-46));
    AddState(139,new State(-47));
    AddState(140,new State(-42));
    AddState(141,new State(-23));
    AddState(142,new State(new int[]{25,89,5,90,1,91},new int[]{-10,143,-15,85}));
    AddState(143,new State(-24));
    AddState(144,new State(new int[]{4,145}));
    AddState(145,new State(-25));
    AddState(146,new State(new int[]{4,147}));
    AddState(147,new State(-26));
    AddState(148,new State(new int[]{4,149}));
    AddState(149,new State(-27));
    AddState(150,new State(new int[]{4,151}));
    AddState(151,new State(new int[]{17,152}));
    AddState(152,new State(new int[]{4,23,3,126},new int[]{-11,153}));
    AddState(153,new State(new int[]{18,154,10,21}));
    AddState(154,new State(new int[]{4,23,3,126},new int[]{-11,155}));
    AddState(155,new State(new int[]{10,21,44,-28,25,-28,40,-28,39,-28,35,-28,36,-28,43,-28,37,-28,41,-28,31,-28,32,-28,49,-28,38,-28,46,-28,47,-28,48,-28,45,-28}));
    AddState(156,new State(-12));
    AddState(157,new State(-5));
    AddState(158,new State(new int[]{44,159}));
    AddState(159,new State(-6));

    Rule[] rules=new Rule[112];
    rules[1]=new Rule(-2, new int[]{-1,2});
    rules[2]=new Rule(-1, new int[]{-3,-4});
    rules[3]=new Rule(-1, new int[]{-3,-5,-6});
    rules[4]=new Rule(-3, new int[]{-7,44});
    rules[5]=new Rule(-3, new int[]{44});
    rules[6]=new Rule(-3, new int[]{1,44});
    rules[7]=new Rule(-5, new int[]{-4,44});
    rules[8]=new Rule(-6, new int[]{-8});
    rules[9]=new Rule(-6, new int[]{});
    rules[10]=new Rule(-6, new int[]{1});
    rules[11]=new Rule(-7, new int[]{-7,-9});
    rules[12]=new Rule(-7, new int[]{-9});
    rules[13]=new Rule(-9, new int[]{25,23});
    rules[14]=new Rule(-9, new int[]{25,24});
    rules[15]=new Rule(-9, new int[]{40,-10});
    rules[16]=new Rule(-9, new int[]{39,-10});
    rules[17]=new Rule(-9, new int[]{35,-11,11});
    rules[18]=new Rule(-9, new int[]{36,-11});
    rules[19]=new Rule(-9, new int[]{43,3});
    rules[20]=new Rule(-9, new int[]{37,23});
    rules[21]=new Rule(-9, new int[]{-12});
    rules[22]=new Rule(-9, new int[]{-13});
    rules[23]=new Rule(-9, new int[]{-14});
    rules[24]=new Rule(-9, new int[]{38,-10});
    rules[25]=new Rule(-9, new int[]{46,4});
    rules[26]=new Rule(-9, new int[]{47,4});
    rules[27]=new Rule(-9, new int[]{48,4});
    rules[28]=new Rule(-9, new int[]{45,4,17,-11,18,-11});
    rules[29]=new Rule(-14, new int[]{49,-8,50});
    rules[30]=new Rule(-14, new int[]{49,1,50});
    rules[31]=new Rule(-10, new int[]{-15});
    rules[32]=new Rule(-10, new int[]{1});
    rules[33]=new Rule(-15, new int[]{-15,15,25});
    rules[34]=new Rule(-15, new int[]{25});
    rules[35]=new Rule(-15, new int[]{5});
    rules[36]=new Rule(-15, new int[]{-15,15,1});
    rules[37]=new Rule(-12, new int[]{41,42});
    rules[38]=new Rule(-12, new int[]{41,-16,42});
    rules[39]=new Rule(-12, new int[]{41,1,42});
    rules[40]=new Rule(-12, new int[]{41,1,44});
    rules[41]=new Rule(-13, new int[]{-17,-18});
    rules[42]=new Rule(-13, new int[]{32});
    rules[43]=new Rule(-18, new int[]{32});
    rules[44]=new Rule(-18, new int[]{34});
    rules[45]=new Rule(-17, new int[]{-17,31});
    rules[46]=new Rule(-17, new int[]{-17,33});
    rules[47]=new Rule(-17, new int[]{31});
    rules[48]=new Rule(-19, new int[]{-20,-18});
    rules[49]=new Rule(-20, new int[]{-20,33});
    rules[50]=new Rule(-20, new int[]{-20,31});
    rules[51]=new Rule(-20, new int[]{33});
    rules[52]=new Rule(-4, new int[]{-21});
    rules[53]=new Rule(-4, new int[]{});
    rules[54]=new Rule(-21, new int[]{-21,-22});
    rules[55]=new Rule(-21, new int[]{-22});
    rules[56]=new Rule(-22, new int[]{-23});
    rules[57]=new Rule(-22, new int[]{-24});
    rules[58]=new Rule(-22, new int[]{-12});
    rules[59]=new Rule(-22, new int[]{-14});
    rules[60]=new Rule(-22, new int[]{-19});
    rules[61]=new Rule(-22, new int[]{34});
    rules[62]=new Rule(-23, new int[]{-25});
    rules[63]=new Rule(-24, new int[]{-26,28,-27,29});
    rules[64]=new Rule(-27, new int[]{});
    rules[65]=new Rule(-27, new int[]{-27,-25});
    rules[66]=new Rule(-27, new int[]{-27,-24});
    rules[67]=new Rule(-25, new int[]{-26,24,-28});
    rules[68]=new Rule(-25, new int[]{24,-28});
    rules[69]=new Rule(-25, new int[]{1});
    rules[70]=new Rule(-26, new int[]{26,-10,27});
    rules[71]=new Rule(-26, new int[]{26,12,27});
    rules[72]=new Rule(-8, new int[]{-16});
    rules[73]=new Rule(-16, new int[]{-29});
    rules[74]=new Rule(-16, new int[]{-16,-29});
    rules[75]=new Rule(-16, new int[]{-16,-30});
    rules[76]=new Rule(-16, new int[]{-30});
    rules[77]=new Rule(-30, new int[]{21,22});
    rules[78]=new Rule(-30, new int[]{21,-16,22});
    rules[79]=new Rule(-30, new int[]{19,20});
    rules[80]=new Rule(-30, new int[]{19,-16,20});
    rules[81]=new Rule(-30, new int[]{17,18});
    rules[82]=new Rule(-30, new int[]{17,-16,18});
    rules[83]=new Rule(-30, new int[]{19,1});
    rules[84]=new Rule(-30, new int[]{17,1});
    rules[85]=new Rule(-30, new int[]{21,1});
    rules[86]=new Rule(-11, new int[]{4});
    rules[87]=new Rule(-11, new int[]{3,10,4});
    rules[88]=new Rule(-11, new int[]{-11,10,4});
    rules[89]=new Rule(-29, new int[]{-19});
    rules[90]=new Rule(-29, new int[]{-11});
    rules[91]=new Rule(-29, new int[]{34});
    rules[92]=new Rule(-29, new int[]{53});
    rules[93]=new Rule(-29, new int[]{3});
    rules[94]=new Rule(-29, new int[]{5});
    rules[95]=new Rule(-29, new int[]{6});
    rules[96]=new Rule(-29, new int[]{7});
    rules[97]=new Rule(-29, new int[]{8});
    rules[98]=new Rule(-29, new int[]{10});
    rules[99]=new Rule(-29, new int[]{12});
    rules[100]=new Rule(-29, new int[]{13});
    rules[101]=new Rule(-29, new int[]{14});
    rules[102]=new Rule(-29, new int[]{11});
    rules[103]=new Rule(-29, new int[]{15});
    rules[104]=new Rule(-29, new int[]{16});
    rules[105]=new Rule(-29, new int[]{9});
    rules[106]=new Rule(-28, new int[]{28,-8,29});
    rules[107]=new Rule(-28, new int[]{-8});
    rules[108]=new Rule(-28, new int[]{30});
    rules[109]=new Rule(-28, new int[]{28,29});
    rules[110]=new Rule(-28, new int[]{28,1,29});
    rules[111]=new Rule(-28, new int[]{1});
    this.InitRules(rules);

    this.InitNonTerminals(new string[] {"", "Program", "$accept", "DefinitionSection", 
      "Rules", "RulesSection", "UserCodeSection", "DefinitionSeq", "CSharp", 
      "Definition", "NameList", "DottedName", "PcBraceSection", "DefComment", 
      "IndentedCode", "NameSeq", "CSharpN", "DefComStart", "CommentEnd", "BlockComment", 
      "CsComStart", "RuleList", "Rule", "Production", "ProductionGroup", "ARule", 
      "StartCondition", "PatActionList", "Action", "NonPairedToken", "WFCSharpN", 
      });

    aliasses = new Dictionary<int, string>();
    aliasses.Add(9, "\"|\"");
    aliasses.Add(10, "\".\"");
    aliasses.Add(11, "\";\"");
    aliasses.Add(12, "\"*\"");
    aliasses.Add(13, "\"<\"");
    aliasses.Add(14, "\">\"");
    aliasses.Add(15, "\",\"");
    aliasses.Add(16, "\"/\"");
    aliasses.Add(17, "\"[\"");
    aliasses.Add(18, "\"]\"");
    aliasses.Add(19, "\"(\"");
    aliasses.Add(20, "\")\"");
    aliasses.Add(21, "\"{\"");
    aliasses.Add(22, "\"}\"");
    aliasses.Add(26, "\"<\"");
    aliasses.Add(27, "\">\"");
    aliasses.Add(28, "\"{\"");
    aliasses.Add(29, "\"}\"");
    aliasses.Add(30, "\"|\"");
    aliasses.Add(31, "\"/*\"");
    aliasses.Add(32, "\"/*...*/\"");
    aliasses.Add(33, "\"/*\"");
    aliasses.Add(34, "\"/*...*/\"");
    aliasses.Add(35, "\"%using\"");
    aliasses.Add(36, "\"%namespace\"");
    aliasses.Add(37, "\"%option\"");
    aliasses.Add(38, "\"%charClassPredicate\"");
    aliasses.Add(39, "\"%s\"");
    aliasses.Add(40, "\"%x\"");
    aliasses.Add(41, "\"%{\"");
    aliasses.Add(42, "\"%}\"");
    aliasses.Add(43, "\"%visibility\"");
    aliasses.Add(44, "\"%%\"");
    aliasses.Add(45, "\"userCharPredicate\"");
    aliasses.Add(46, "\"%scanbasetype\"");
    aliasses.Add(47, "\"%tokentype\"");
    aliasses.Add(48, "\"scannertype\"");
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
      case 4: // DefinitionSection -> DefinitionSeq, "%%"
{
                       isBar = false; 
                       typedeclOK = false;
                       if (aast.nameString == null) 
                           handler.ListError(LocationStack[LocationStack.Depth-1], 73); 
                     }
        break;
      case 5: // DefinitionSection -> "%%"
{ isBar = false; }
        break;
      case 6: // DefinitionSection -> error, "%%"
{ handler.ListError(LocationStack[LocationStack.Depth-2], 62, "%%"); }
        break;
      case 7: // RulesSection -> Rules, "%%"
{ typedeclOK = true; }
        break;
      case 8: // UserCodeSection -> CSharp
{ aast.UserCode = LocationStack[LocationStack.Depth-1]; }
        break;
      case 9: // UserCodeSection -> /* empty */
{  /* empty */  }
        break;
      case 10: // UserCodeSection -> error
{ handler.ListError(LocationStack[LocationStack.Depth-1], 62, "EOF"); }
        break;
      case 13: // Definition -> name, verbatim
{ AddLexCategory(LocationStack[LocationStack.Depth-2], LocationStack[LocationStack.Depth-1]); }
        break;
      case 14: // Definition -> name, pattern
{ AddLexCategory(LocationStack[LocationStack.Depth-2], LocationStack[LocationStack.Depth-1]); }
        break;
      case 15: // Definition -> "%x", NameList
{ AddNames(true); }
        break;
      case 16: // Definition -> "%s", NameList
{ AddNames(false); }
        break;
      case 17: // Definition -> "%using", DottedName, ";"
{ aast.usingStrs.Add(LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1])); }
        break;
      case 18: // Definition -> "%namespace", DottedName
{ aast.nameString = LocationStack[LocationStack.Depth-1]; }
        break;
      case 19: // Definition -> "%visibility", csKeyword
{ aast.AddVisibility(LocationStack[LocationStack.Depth-1]); }
        break;
      case 20: // Definition -> "%option", verbatim
{ ParseOption(LocationStack[LocationStack.Depth-1]); }
        break;
      case 21: // Definition -> PcBraceSection
{ aast.AddCodeSpan(Dest,LocationStack[LocationStack.Depth-1]); }
        break;
      case 22: // Definition -> DefComment
{ aast.AddCodeSpan(Dest,LocationStack[LocationStack.Depth-1]); }
        break;
      case 23: // Definition -> IndentedCode
{ aast.AddCodeSpan(Dest,LocationStack[LocationStack.Depth-1]); }
        break;
      case 24: // Definition -> "%charClassPredicate", NameList
{ AddCharSetPredicates(); }
        break;
      case 25: // Definition -> "%scanbasetype", csIdent
{ aast.SetScanBaseName(LocationStack[LocationStack.Depth-1].ToString()); }
        break;
      case 26: // Definition -> "%tokentype", csIdent
{ aast.SetTokenTypeName(LocationStack[LocationStack.Depth-1].ToString()); }
        break;
      case 27: // Definition -> "scannertype", csIdent
{ aast.SetScannerTypeName(LocationStack[LocationStack.Depth-1].ToString()); }
        break;
      case 28: // Definition -> "userCharPredicate", csIdent, "[", DottedName, "]", DottedName
{
                                 aast.AddUserPredicate(LocationStack[LocationStack.Depth-5].ToString(), LocationStack[LocationStack.Depth-3], LocationStack[LocationStack.Depth-1]);
                               }
        break;
      case 29: // IndentedCode -> lxIndent, CSharp, lxEndIndent
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-2]; }
        break;
      case 30: // IndentedCode -> lxIndent, error, lxEndIndent
{ handler.ListError(LocationStack[LocationStack.Depth-2], 64); }
        break;
      case 32: // NameList -> error
{ handler.ListError(LocationStack[LocationStack.Depth-1], 67); }
        break;
      case 33: // NameSeq -> NameSeq, ",", name
{ AddName(LocationStack[LocationStack.Depth-1]); }
        break;
      case 34: // NameSeq -> name
{ AddName(LocationStack[LocationStack.Depth-1]); }
        break;
      case 35: // NameSeq -> csNumber
{ AddName(LocationStack[LocationStack.Depth-1]); }
        break;
      case 36: // NameSeq -> NameSeq, ",", error
{ handler.ListError(LocationStack[LocationStack.Depth-2], 67); }
        break;
      case 37: // PcBraceSection -> "%{", "%}"
{ CurrentLocationSpan = BlankSpan; /* skip blank lines */ }
        break;
      case 38: // PcBraceSection -> "%{", CSharpN, "%}"
{ 
                                 CurrentLocationSpan = LocationStack[LocationStack.Depth-2]; 
                               }
        break;
      case 39: // PcBraceSection -> "%{", error, "%}"
{ handler.ListError(LocationStack[LocationStack.Depth-2], 62, "%}"); }
        break;
      case 40: // PcBraceSection -> "%{", error, "%%"
{ handler.ListError(LocationStack[LocationStack.Depth-2], 62, "%%"); }
        break;
      case 52: // Rules -> RuleList
{ 
                                rb.FinalizeCode(aast); 
                                aast.FixupBarActions(); 
                              }
        break;
      case 57: // Rule -> ProductionGroup
{ scope.ClearScope(); /* for error recovery */ }
        break;
      case 58: // Rule -> PcBraceSection
{ rb.AddSpan(LocationStack[LocationStack.Depth-1]); }
        break;
      case 59: // Rule -> IndentedCode
{ rb.AddSpan(LocationStack[LocationStack.Depth-1]); }
        break;
      case 60: // Rule -> BlockComment
{ /* ignore */ }
        break;
      case 61: // Rule -> "/*...*/"
{ /* ignore */ }
        break;
      case 62: // Production -> ARule
{
			                    int thisLine = LocationStack[LocationStack.Depth-1].startLine;
			                    rb.LLine = thisLine;
			                    if (rb.FLine == 0) rb.FLine = thisLine;
		                      }
        break;
      case 63: // ProductionGroup -> StartCondition, "{", PatActionList, "}"
{
                                scope.ExitScope();
                              }
        break;
      case 64: // PatActionList -> /* empty */
{ 
                                int thisLine = CurrentLocationSpan.startLine;
			                    rb.LLine = thisLine;
			                    if (rb.FLine == 0) 
			                        rb.FLine = thisLine;
			                  }
        break;
      case 67: // ARule -> StartCondition, pattern, Action
{
			                    RuleDesc rule = new RuleDesc(LocationStack[LocationStack.Depth-2], LocationStack[LocationStack.Depth-1], scope.Current, isBar);
			                    aast.ruleList.Add(rule);
			                    rule.ParseRE(aast);
			                    isBar = false; // Reset the flag ...
			                    scope.ExitScope();
		                      }
        break;
      case 68: // ARule -> pattern, Action
{
			                    RuleDesc rule = new RuleDesc(LocationStack[LocationStack.Depth-2], LocationStack[LocationStack.Depth-1], scope.Current, isBar); 
			                    aast.ruleList.Add(rule);
			                    rule.ParseRE(aast); 
			                    isBar = false; // Reset the flag ...
		                      }
        break;
      case 69: // ARule -> error
{ handler.ListError(LocationStack[LocationStack.Depth-1], 68); scope.ClearScope(); }
        break;
      case 70: // StartCondition -> "<", NameList, ">"
{ 
                                List<StartState> list =  new List<StartState>();
                                AddNameListToStateList(list);
                                scope.EnterScope(list); 
                              }
        break;
      case 71: // StartCondition -> "<", "*", ">"
{
                                List<StartState> list =  new List<StartState>(); 
                                list.Add(StartState.allState);
                                scope.EnterScope(list); 
                              }
        break;
      case 83: // WFCSharpN -> "(", error
{ handler.ListError(LocationStack[LocationStack.Depth-1], 61, "')'"); }
        break;
      case 84: // WFCSharpN -> "[", error
{ handler.ListError(LocationStack[LocationStack.Depth-1], 61, "']'"); }
        break;
      case 85: // WFCSharpN -> "{", error
{ handler.ListError(LocationStack[LocationStack.Depth-1], 61, "'}'"); }
        break;
      case 86: // DottedName -> csIdent
{ /* skip1 */ }
        break;
      case 87: // DottedName -> csKeyword, ".", csIdent
{ /* skip2 */ }
        break;
      case 88: // DottedName -> DottedName, ".", csIdent
{ /* skip3 */ }
        break;
      case 93: // NonPairedToken -> csKeyword
{ 
                                 string text = aast.scanner.yytext;
                                 if (text.Equals("using")) {
                                     handler.ListError(LocationStack[LocationStack.Depth-1], 56);
                                 } else if (text.Equals("namespace")) {
                                     handler.ListError(LocationStack[LocationStack.Depth-1], 57);
                                 } else {
                                     if ((text.Equals("class") || text.Equals("struct") ||
                                          text.Equals("enum")) && !typedeclOK) handler.ListError(LocationStack[LocationStack.Depth-1],58);
                                 }
                               }
        break;
      case 106: // Action -> "{", CSharp, "}"
{ CurrentLocationSpan = LocationStack[LocationStack.Depth-2]; }
        break;
      case 108: // Action -> "|"
{ isBar = true; }
        break;
      case 110: // Action -> "{", error, "}"
{ handler.ListError(CurrentLocationSpan, 65); }
        break;
      case 111: // Action -> error
{ handler.ListError(LocationStack[LocationStack.Depth-1], 63); }
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
