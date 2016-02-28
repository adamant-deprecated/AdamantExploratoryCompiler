cd Compiler\Antlr\
CALL ..\..\tools\antlr4.bat AdamantLexer.g4 -package Adamant.Exploratory.Compiler.Antlr
CALL ..\..\tools\antlr4.bat AdamantParser.g4 -visitor -package Adamant.Exploratory.Compiler.Antlr
CALL ..\..\tools\antlr4.bat PreprocessorLineLexer.g4 -package Adamant.Exploratory.Compiler.Antlr
CALL ..\..\tools\antlr4.bat PreprocessorLineParser.g4 -visitor -package Adamant.Exploratory.Compiler.Antlr
PAUSE