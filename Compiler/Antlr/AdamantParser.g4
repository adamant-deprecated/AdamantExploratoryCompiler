parser grammar AdamantParser;

options
{
	language=CSharp;
	tokenVocab=AdamantLexer;
}

compilationUnit
	: usingStatement*
	  // globalAttribute*
	  declaration*
	  EOF
	;

usingStatement
	: 'using' namespaceName ';'
	;

identifier
	: name=Identifier
	| name=EscapedIdentifier
	| name='conversion'
	;

namespaceName
	: identifiers+=identifier ('.' identifiers+=identifier)*
	;

declaration
	: 'namespace' namespaceName '{' usingStatement* declaration* '}'  #NamespaceDeclaration
	| attribute* modifier* 'class' name=identifier typeParameterList? baseTypes?
		typeParameterConstraintClause*
		'{' member* '}' #ClassDeclaration
	| attribute* modifier* kind=('var'|'let') name=identifier (':' ownershipType)? ('=' expression)? ';' #VariableDeclaration
	| attribute* modifier* name=identifier typeArguments? parameterList '->' returnType=ownershipType typeParameterConstraintClause* methodBody	 #FunctionDeclaration
	;

attribute
	: EscapedIdentifier ('(' ')')?
	;

baseTypes
	: (':' baseType=typeName? (':' interfaces+=typeName (',' interfaces+=typeName)*)?)
	;

modifier
	: Symbol='public'
	| Symbol='private'
	| Symbol='protected'
	| Symbol='package'
	| Symbol='safe'
	| Symbol='unsafe'
	| Symbol='abstract'
	| Symbol='partial'
	| Symbol='implicit'
	| Symbol='explicit'
	| Symbol='sealed'
	| Symbol='override'
	| Symbol='async'
	| Symbol='extern'
	;

typeParameterList
	: '<' typeParameter (',' typeParameter)* '>'
	;

typeParameter
	: name=identifier isList='...'? (':' baseType=typeName)?
	;

typeName
	: outerType=typeName '.' identifier typeArguments?
	| identifier typeArguments?
	;

typeArguments
	: '<' ownershipType (',' ownershipType)* '>'
	;

ownershipType // these are types with ownership modifiers
	: 'mut' plainType			#MutableType
	| 'own' 'mut'? plainType	#OwnedType
	| plainType					#ImmutableType
	;

plainType
	: typeName																#NamedType
	| 'string'																#StringType
	| ('byte'|IntType|UIntType|FloatType|FixedType|DecimalType|SizeType)	#PrimitiveNumericType
	| plainType '?'															#MaybeType
	| valueType=plainType '*'												#PointerType
	| elementType=plainType '[' constExpression (',' constExpression)* ']'	#ArrayType
	| elementType=plainType '[' dimensions+=','* ']'						#ArraySliceType
	| funcTypeParameterList '->' ownershipType								#FunctionType
	;

funcTypeParameterList
	: '(' funcTypeParameter (',' funcTypeParameter)* ')'
	| '(' ')'
	;

funcTypeParameter
	: parameterModifier* ownershipType
	;

constExpression
	: IntLiteral
	| StringLiteral
	| identifier
	;

typeParameterConstraintClause
	: 'where' typeParameter ':' typeParameterConstraint (',' typeParameterConstraint)*
	| 'where' typeParameter ('>='|'<='|'<'|'>') IntLiteral
	;

typeParameterConstraint
	: 'new' '(' ')'			#ConstructorConstraint
	| typeName				#TypeConstraint
	| typeParameter			#TypeListParameterConstraint // will only be hit for type lists (i.e. "foo...")
	;

member
	: attribute* modifier* 'new' name=identifier? parameterList ('->' returnType=ownershipType)? constructorInitializer? methodBody									#Constructor
	| attribute* modifier* 'delete' parameterList methodBody																										#Destructor
	| attribute* modifier* 'conversion' typeArguments? parameterList '->' ownershipType typeParameterConstraintClause* methodBody									#ConversionMethod
	| attribute* modifier* kind=('var'|'let') identifier (':' ownershipType)? ('=' expression)? ';'																	#Field
	| attribute* modifier* kind=('get'|'set') (name=identifier|('[' ']')) typeArguments? parameterList '->' ownershipType typeParameterConstraintClause* methodBody	#Property
	| attribute* modifier* name=identifier typeArguments? parameterList '->' returnType=ownershipType typeParameterConstraintClause* methodBody						#Method
	;

parameterList
	: '(' parameters+=parameter (',' parameters+=parameter)* ')'
	| '(' ')'
	;

parameter
	: modifiers+=parameterModifier* name=identifier? ':' type=ownershipType #namedParameter
	| modifiers+=parameterModifier* 'own'? 'mut'? 'self'					#selfParameter
	;

parameterModifier
	: 'params'
	;

constructorInitializer
	: ':' 'base' '(' argumentList ')'
	| ':' 'self' '(' argumentList ')'
	;

argumentList
	:  expressions+=expression (',' expressions+=expression)*
	|
	;

methodBody
	: '{' statement* '}'
	| ';'
	;

overloadableOperator
	: '*'
	| '&'
	| 'or'
	| 'and'
	| 'xor'
	| '?'
	| '??'
	| '.'
	| '[' ']'
	;

statement
	: localVariableDeclaration ';'							#VariableDeclarationStatement
	| 'unsafe' '{' statement* '}'							#UnsafeBlockStatement
	| '{' statement* '}'									#BlockStatement
	| ';'													#EmptyStatement
	| expression ';'										#ExpressionStatement
	| 'return' expression ';'								#ReturnStatement
	| 'throw' expression ';'								#ThrowStatement
	| 'if' '(' condition=expression ')' then=statement ('else' else=statement)?			#IfStatement
	| 'for' '(' localVariableDeclaration? ';' expression? ';' expression? ')' statement		#ForStatement
	| 'foreach' '(' localVariableDeclaration 'in' expression ')' statement					#ForeachStatement
	| 'delete' expression ';'								#DeleteStatement
	;

localVariableDeclaration
	: kind=('var'|'let') identifier (':' ownershipType)? ('=' expression)?
	;

expression
	: expression '.' identifier								#MemberExpression
	| expression '->' identifier							#PointerMemberExpression
	| expression '(' argumentList ')'						#CallExpression
	| expression '[' argumentList ']'						#ArrayAccessExpression
	| expression '?'										#NullCheckExpression
	| op=('+'|'-'|'not'|'&'|'*') expression					#UnaryExpression
	| expression op=('*'|'/') expression					#MultiplicativeExpression
	| expression op=('+'|'-') expression					#AdditiveExpression
	| expression (ops+='<' ops+='<' | ops+='>' ops+='>') expression #ShiftExpression
	| expression op=('<'|'<='|'>'|'>=') expression			#ComparativeExpression
	| lhs=expression op=('=='|'<>') rhs=expression			#EqualityExpression
	| expression 'and' expression							#AndExpression
	| expression 'xor' expression							#XorExpression
	| expression 'or' expression							#OrExpression
	| expression '??' expression							#CoalesceExpression
	| <assoc=right> condition=expression '?' then=expression ':' else=expression #IfExpression
	| <assoc=right> lvalue=expression op=('='|'*='|'/='|'+='|'-='|'and='|'xor='|'or=') rvalue=expression #AssignmentExpression
	| identifier											#VariableExpression
	// Since new Class.Constructor() is indistiguishable from new Namespace.Class() we can't parse for named constructor calls here
	| 'new' typeName '(' argumentList ')'					#NewExpression
	| 'new' baseTypes? '(' argumentList ')' '{' member* '}'	#NewObjectExpression
	| 'null'												#NullLiteralExpression
	| 'self'												#SelfExpression
	| BooleanLiteral										#BooleanLiteralExpression
	| IntLiteral											#IntLiteralExpression
	| 'uninitialized'										#UninitializedExpression
	| StringLiteral											#StringLiteralExpression
	;