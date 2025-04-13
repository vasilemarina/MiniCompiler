grammar Language;

// Root rule
program: (declaration | function | functionCall)* EOF;

declaration: type IDENTIFIER ('=' expression)? ';';

function: type IDENTIFIER '(' parameterList? ')' block;

functionCall: IDENTIFIER '(' argumentList? ')' ';'?;

block: '{' statement* '}';

statement:
	declaration
	| ifStatement
	| forStatement
	| whileStatement
	| returnStatement
	| block
	| expression ';';

ifStatement: 'if' '(' expression ')' block ('else' block)?;

forStatement:
	'for' '(' (declaration | expression ';')? expression? ';' expression? ')' block;

whileStatement: 'while' '(' expression ')' block;

returnStatement: 'return' expression? ';';

expression:
	IDENTIFIER
	| NUMBER
	| STRING_LITERAL
	| functionCall
	| expression ('+' | '-' | '*' | '/' | '%') expression
	| expression (
		'<'
		| '<='
		| '>'
		| '>='
		| '=='
		| '!='
		| '&&'
		| '||'
	) expression
	| '(' expression ')'
	| expression ('=' | '+=' | '-=' | '*=' | '/=' | '%=') expression
	| expression ('++' | '--')
	| ('++' | '--') expression;

type: 'int' | 'float' | 'double' | 'string' | 'void';

parameterList: parameter (',' parameter)*;

parameter: type IDENTIFIER;

argumentList: expression (',' expression)*;

COMMENT: '//' ~[\r\n]* -> skip;

MULTILINE_COMMENT: '/*' .*? '*/' -> skip;

INT: 'int';
FLOAT: 'float';
DOUBLE: 'double';
STRING: 'string';
VOID: 'void';
IF: 'if';
ELSE: 'else';
FOR: 'for';
WHILE: 'while';
RETURN: 'return';

PLUS: '+';
MINUS: '-';
TIMES: '*';
DIVIDE: '/';
MOD: '%';

LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '==';
NEQ: '!=';

AND: '&&';
OR: '||';
NOT: '!';

ADD_ASSIGN: '+=';
SUB_ASSIGN: '-=';
MUL_ASSIGN: '*=';
DIV_ASSIGN: '/=';
MOD_ASSIGN: '%=';
ASSIGN: '=';

IDENTIFIER: [a-zA-Z_][a-zA-Z_0-9]*;

NUMBER: [0-9]+ ('.' [0-9]+)?;

STRING_LITERAL: '"' .*? '"';

INCREMENT_OP: '++';
DECREMENT_OP: '--';

// Delimitatori
SEMICOLON: ';';
COMMA: ',';
LPAREN: '(';
RPAREN: ')';
LBRACE: '{';
RBRACE: '}';

WS: [ \t\r\n]+ -> skip;