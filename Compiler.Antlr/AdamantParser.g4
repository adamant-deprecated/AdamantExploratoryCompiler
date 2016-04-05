parser grammar AdamantParser;

options
{
	language=CSharp;
	tokenVocab=AdamantLexer;
}

compilationUnit
	: usingDirective*
	  // globalAttribute*
	  declaration*
	  EOF
	;

usingDirective
	: 'using' namespaceName ';' // TODO using names are allowed to be more complicated than this
	;

identifier
	: token=Identifier
	| token=EscapedIdentifier
	;

namespaceName
	: identifiers+=identifier ('.' identifiers+=identifier)*
	;

declaration
	: 'namespace' namespaceName '{' usingDirective* declaration* '}'  #NamespaceDeclaration
	| attribute* modifier* 'class' identifier typeParameters? baseTypes?
		typeParameterConstraintClause*
		'{' member* '}' #ClassDeclaration
	| attribute* modifier* kind=('var'|'let') identifier (':' referenceType)? ('=' expression)? ';' #VariableDeclaration
	| attribute* modifier* identifier typeArguments? parameterList '->' returnType=referenceType typeParameterConstraintClause* methodBody	 #FunctionDeclaration
	;

attribute
	: EscapedIdentifier ('(' ')')? // TODO needs fixed now that escaped identifiers use ` but attributes should still be @
	;

baseTypes
	: (':' baseType=name? (':' interfaces+=name (',' interfaces+=name)*)?)
	;

modifier
	: token='public'
	| token='private'
	| token='protected'
	| token='package'
	| token='safe'
	| token='unsafe'
	| token='abstract'
	| token='partial'
	| token='implicit'
	| token='explicit'
	| token='sealed'
	| token='override'
	| token='async'
	| token='extern'
	;

typeParameters
	: '<' typeParameter (',' typeParameter)* '>'
	;

typeParameter
	: identifier isList='...'? (':' baseType=valueType)?
	;

typeArguments
	: '<' referenceType (',' referenceType)* '>'
	;

identifierOrPredefinedType
	: identifier
	| token='string'
	| token='byte'
	| token=IntType
	| token=UIntType
	| token=FloatType
	| token=FixedType
	| token=DecimalType
	| token=SizeType
	| token=OffsetType
	| token=UnsafeArrayType
	;

simpleName
	: identifierOrPredefinedType				#IdentifierName
	| identifierOrPredefinedType typeArguments	#GenericName
	;

name
	: simpleName								#SimpleNameName
	| leftName=name '.' rightName=simpleName	#QualifiedName
	;

valueType
	: name																		#NamedType
	| valueType '?'																#MaybeType
	| valueType '*'																#PointerType
	| ('[' types+=valueType (',' types+=valueType)* ']' | '[' ']')				#TupleType
	| funcTypeParameterList '->' referenceType									#FunctionType
	;

referenceType // these are types with lifetimes
	: valueType				#ImmutableReferenceType
	| 'mut' valueType		#MutableReferenceType
	| 'own' valueType		#OwnedImmutableReferenceType
	| 'own' 'mut' valueType	#OwnedMutableReferenceType
	;

funcTypeParameterList
	: '(' funcTypeParameter (',' funcTypeParameter)* ')'
	| '(' ')'
	;

funcTypeParameter
	: parameterModifier* referenceType
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
	| valueType				#TypeConstraint
	| typeParameter			#TypeListParameterConstraint // will only be hit for type lists (i.e. "foo...")
	;

member
	: attribute* modifier* 'new' identifier? parameterList ('->' returnType=referenceType)? constructorInitializer? methodBody						#Constructor
	| attribute* modifier* 'delete' parameterList methodBody																						#Destructor
	| attribute* modifier* 'conversion' typeArguments? parameterList '->' referenceType typeParameterConstraintClause* methodBody					#ConversionMethod
	| attribute* modifier* kind=('var'|'let') identifier (':' referenceType)? ('=' expression)? ';'													#Field
	| attribute* modifier* kind=('get'|'set') identifier typeArguments? parameterList '->' referenceType typeParameterConstraintClause* methodBody	#Accessor
	| attribute* modifier* kind=('get'|'set') '[' ']' typeArguments? parameterList '->' referenceType typeParameterConstraintClause* methodBody		#Indexer
	| attribute* modifier* identifier typeArguments? parameterList '->' returnType=referenceType typeParameterConstraintClause* methodBody			#Method
	;

parameterList
	: '(' parameters+=parameter (',' parameters+=parameter)* ')'
	| '(' ')'
	;

parameter
	: modifiers+=parameterModifier* identifier? ':' referenceType	#namedParameter
	| modifiers+=parameterModifier* 'own'? 'mut'? token='self'		#selfParameter
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
	: kind=('var'|'let') identifier (':' referenceType)? ('=' expression)?
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
	| expression op=('<'|'<='|'>'|'>=') expression			#ComparativeExpression
	| lhs=expression op=('=='|'<>') rhs=expression			#EqualityExpression
	| expression 'and' expression							#AndExpression
	| expression 'xor' expression							#XorExpression
	| expression 'or' expression							#OrExpression
	| expression '??' expression							#CoalesceExpression
	| <assoc=right> condition=expression '?' then=expression ':' else=expression #IfExpression
	| <assoc=right> lvalue=expression op=('='|'*='|'/='|'+='|'-='|'and='|'xor='|'or=') rvalue=expression #AssignmentExpression
	| identifier											#NameExpression
	// Since new Class.Constructor() is indistiguishable from new Namespace.Class() we can't parse for named constructor calls here
	| 'new' name '(' argumentList ')'						#NewExpression
	| 'new' baseTypes? '(' argumentList ')' '{' member* '}'	#NewObjectExpression
	| 'null'												#NullLiteralExpression
	| 'self'												#SelfExpression
	| BooleanLiteral										#BooleanLiteralExpression
	| IntLiteral											#IntLiteralExpression
	| 'uninitialized'										#UninitializedExpression
	| StringLiteral											#StringLiteralExpression
	;