// Generated from d:/Facultate_An2_Sem1/Tema2_LFC/Tema2_LFC/Tema2_LFC/Language.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link LanguageParser}.
 */
public interface LanguageListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link LanguageParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(LanguageParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(LanguageParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#declaration}.
	 * @param ctx the parse tree
	 */
	void enterDeclaration(LanguageParser.DeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#declaration}.
	 * @param ctx the parse tree
	 */
	void exitDeclaration(LanguageParser.DeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#function}.
	 * @param ctx the parse tree
	 */
	void enterFunction(LanguageParser.FunctionContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#function}.
	 * @param ctx the parse tree
	 */
	void exitFunction(LanguageParser.FunctionContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCall(LanguageParser.FunctionCallContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCall(LanguageParser.FunctionCallContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#block}.
	 * @param ctx the parse tree
	 */
	void enterBlock(LanguageParser.BlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#block}.
	 * @param ctx the parse tree
	 */
	void exitBlock(LanguageParser.BlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterStatement(LanguageParser.StatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitStatement(LanguageParser.StatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void enterIfStatement(LanguageParser.IfStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void exitIfStatement(LanguageParser.IfStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void enterForStatement(LanguageParser.ForStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void exitForStatement(LanguageParser.ForStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void enterWhileStatement(LanguageParser.WhileStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void exitWhileStatement(LanguageParser.WhileStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#returnStatement}.
	 * @param ctx the parse tree
	 */
	void enterReturnStatement(LanguageParser.ReturnStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#returnStatement}.
	 * @param ctx the parse tree
	 */
	void exitReturnStatement(LanguageParser.ReturnStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterExpression(LanguageParser.ExpressionContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitExpression(LanguageParser.ExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#type}.
	 * @param ctx the parse tree
	 */
	void enterType(LanguageParser.TypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#type}.
	 * @param ctx the parse tree
	 */
	void exitType(LanguageParser.TypeContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#parameterList}.
	 * @param ctx the parse tree
	 */
	void enterParameterList(LanguageParser.ParameterListContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#parameterList}.
	 * @param ctx the parse tree
	 */
	void exitParameterList(LanguageParser.ParameterListContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#parameter}.
	 * @param ctx the parse tree
	 */
	void enterParameter(LanguageParser.ParameterContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#parameter}.
	 * @param ctx the parse tree
	 */
	void exitParameter(LanguageParser.ParameterContext ctx);
	/**
	 * Enter a parse tree produced by {@link LanguageParser#argumentList}.
	 * @param ctx the parse tree
	 */
	void enterArgumentList(LanguageParser.ArgumentListContext ctx);
	/**
	 * Exit a parse tree produced by {@link LanguageParser#argumentList}.
	 * @param ctx the parse tree
	 */
	void exitArgumentList(LanguageParser.ArgumentListContext ctx);
}