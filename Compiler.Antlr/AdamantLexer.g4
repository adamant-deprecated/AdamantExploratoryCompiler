lexer grammar AdamantLexer;

import AdamantCommon; // defines Whitespace, Newline and InputChar

channels { DocComments }

options
{
	language=CSharp;
}

//*************
// Comments
//*************
SingleLineDocComment
	: '///' InputChar* -> channel(DocComments)
	;

SingleLineComment
	: '//' InputChar* -> skip
	;

BlockComment
	: '/*' .*? '*/' -> skip
	;

//*************
// Preprocessor
//*************
PreprocessorLine
	: Whitespace? '#' InputChar* { Preprocess(); } -> skip
	;

// Here we use a mode to handle preprocessor sections that are skipped, this mode will be entered by the preprocessor code
mode PREPROCESSOR_SKIP;

PreprocessorLineInSkipped
	// the type here prevents it from creating another token type
	: Whitespace? '#' InputChar* { Preprocess(); } -> type(PreprocessorLine), skip
	;

PreprocessorSkippedSection
	// anything except newline or #
	// newline is excluded because otherwise a multi-line match could swallow the leading whitespace we need to check
	// that preprocessor directives are the first thing on the line
	: ~[\u000D\u000A\u0085\u2028\u2029#]+ -> skip
	;

PreprocessorSkippedNewline
	// the type here prevents it from creating another token type
	: Newline -> type(Newline), skip
	;

// And switch back to default mode so rest of file is correct
mode DEFAULT_MODE;

//*************
// Keywords
//*************
Using : 'using';
Namespace : 'namespace';
Class : 'class';
Enum : 'enum';
New : 'new';
Delete : 'delete';
Self : 'self';
Uninitialized : 'uninitialized';
Where : 'where';
Base : 'base';
Operator : 'operator';

// Properties
Var : 'var';
Let : 'let';
Get : 'get';
Set : 'set';

// Modifiers
Sealed : 'sealed';
Override : 'override';
Partial : 'partial';
Abstract : 'abstract';
Params : 'params';
Extern : 'extern';

// Control Flow
Do : 'do';
While : 'while';
If : 'if';
Else : 'else';
For : 'for';
In : 'in';
Foreach : 'foreach';
Yield : 'yeild';
Switch : 'switch';
Break : 'break';
Continue : 'continue';
Return : 'return';

// Exceptions
Try : 'try';
TryPanic : 'try!';
TryResult : 'try?';
Catch : 'catch';
Finally : 'finally';
Throw : 'throw';

// Conversion
Implicit : 'implicit';
Explicit : 'explicit';
Conversion : 'conversion';
As : 'as';
AsPanic : 'as!';
AsResult : 'as?';

// Access modifiers
Public : 'public';
Private : 'private';
Protected : 'protected';
Package : 'package';

// Safety
Safe : 'safe';
Unsafe : 'unsafe';

// Lifetime
Own : 'own';
Mutable : 'mut';
Immutable : 'immut';

// Async
Async: 'async';
Await: 'await';

// Contracts
Requires: 'requires';
Ensures: 'ensures';

//*************
// Predefined Types
//*************
Void : 'void';
String : 'string';
ByteType : 'byte';
IntType : 'int' IntLiteral?;
UIntType : 'uint' IntLiteral?;
FloatType : 'float' IntLiteral?;
FixedType : 'fixed' IntLiteral '.' IntLiteral;
DecimalType : 'decimal' IntLiteral?;
SizeType : 'size';
OffsetType : 'offset';
UnsafeArrayType : 'UnsafeArray';

//*************
// Literals
//*************
BooleanLiteral
	: 'true'
	| 'false'
	;

IntLiteral
	: [1-9] ('_'|DecimalDigit)*
	| '0'
	;

NullLiteral : 'null';

StringLiteral
	: '"' (InputChar|'\"'|'\\')*? '"'
	;

CharLiteral
	: '\'' (InputChar|'\\\'')*? '\''
	;

//*************
// Operators
//*************
Semicolon : ';';
Colon : ':';
Dot : '.';
DotDot : '..';
ColonColon: '::';
Tilde : '~';
Comma : ',';
Lambda : '->';
LeftBrace : '{';
RightBrace : '}';
LeftAngle : '<';
RightAngle : '>';
LeftBracket : '[';
RightBracket : ']';
LeftParen : '(';
RightParen : ')';
Asterisk : '*';
AtSign : '@';
AddressOf : '&';
Coalesce : '??';
IsNull : '?';
Equal : '==';
NotEqual : '<>';
LessThanOrEqual : '<=';
GreaterThanOrEqual : '>=';
TypeList : '...';
Plus : '+';
Minus : '-';
Divide : '/';
Pipe : '|';
And : 'and';
Xor : 'xor';
Or : 'or';
Not : 'not';
BitAnd : 'bit_and';
BitOr : 'bit_or';
BitXor : 'bit_xor';
BitNot : 'bit_not';
BitShiftLeft : 'bit_shift_left';
BitShiftRight : 'bit_shift_right';

Assign : '=';
AddAssign : '+=';
SubtractAssign : '-=';
MultiplyAssign : '*=';
DivideAssign : '/=';
AndAssign : 'and=';
XorAssign : 'xor=';
OrAssign : 'or=';

PlaceHolder : '_';

// must be defined after all keywords so it will not match a keyword
Identifier
	: IdentifierStartChar IdentifierPartChar*
	;

EscapedIdentifier
	: '`' IdentifierStartChar IdentifierPartChar*
	;

//*************
// Error Rules
//*************
BadNotEqual : '!=' { NotifyErrorListeners("Invalid operator, use '<>' for not equal instead of '!='"); } -> type(NotEqual);
Unknown : .; // An error catch rule for everything else