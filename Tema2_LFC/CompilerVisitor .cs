using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Tema2_LFC
{
    public class CompilerVisitor : LanguageBaseVisitor<object>
    {
        public List<string> LexicalUnits { get; private set; } = new List<string>();
        public HashSet<string> GlobalVariablesDeclarations { get; private set; } = new HashSet<string>();
        public HashSet<string> GlobalVariables { get; private set; } = new HashSet<string>();
        public HashSet<string> LocalVariables { get; private set; } = new HashSet<string>();
        public List<string> Functions { get; private set; } = new List<string>();

        private readonly LanguageLexer _lexer;
        private bool _isInsideFunction = false;
        private List<string> _controlStructures = new List<string>();
        private Dictionary<string, string> _functionParametersTypes = new Dictionary<string, string>();
        private HashSet<string> _functionNames = new HashSet<string>();
        private HashSet<string> _crrParameterNames = new HashSet<string>();
        public CompilerVisitor(LanguageLexer lexer)
        {
            _lexer = lexer;
            ExtractLexicalUnits();
        }

        private void ExtractLexicalUnits()
        {
            _lexer.Reset();
            IToken token;
            while ((token = _lexer.NextToken()).Type != TokenConstants.EOF)
            {
                LexicalUnits.Add($"<{GetTokenName(token.Type)}, {token.Text}, linia {token.Line}>");
            }
        }

        public override object VisitDeclaration(LanguageParser.DeclarationContext context)
        {
            string type = context.type().GetText();
            string name = context.IDENTIFIER().GetText();

            if (context.expression() != null)
            {
                string value = context.expression().GetText();
                string declaration = $"{type} {name} = {value};";

                if (!value.Contains("(") && !IsTypeCompatible(type, value))
                {
                    Program.hasError = true;
                    Console.WriteLine($"Eroare semantica: Tipul valorii atribuite '{value}' nu corespunde cu tipul variabilei '{type}' pe linia {context.Start.Line}.");
                }
                if (_isInsideFunction)
                {
                    if (!LocalVariables.Add(name))
                    {
                        Program.hasError = true;
                        Console.WriteLine($"Eroare semantica: Variabila locala '{name}' a fost deja declarata.");
                    }
                    if (_crrParameterNames.Contains(context.IDENTIFIER().GetText()))
                    {
                        Program.hasError = true;
                        Console.WriteLine($"Eroare semantica: Variabila '{name}' nu poate avea acelasi nume ca un parametru.");
                    }
                }
                else
                {
                    string varDeclaration = $"{type} {name} = {value};";
                    GlobalVariablesDeclarations.Add(varDeclaration);
                    if (!GlobalVariables.Add(name))
                    {
                        Program.hasError = true;
                        Console.WriteLine($"Eroare semantica: Variabila globala '{name}' a fost deja declarata.");
                    }
                }
            }
            else
            {
                string declaration = $"{type} {name};";
                GlobalVariablesDeclarations.Add(declaration);
                if (_isInsideFunction)
                {
                    if (!LocalVariables.Add(name))
                    {
                        Program.hasError = true;
                        Console.WriteLine($"Eroare semantica: Variabila locala '{name}' a fost deja declarata.");
                    }
                    if (_crrParameterNames.Contains(context.IDENTIFIER().GetText()))
                    {
                        Program.hasError = true;
                        Console.WriteLine($"Eroare semantica: Variabila '{name}' nu poate avea acelasi nume ca un parametru.");
                    }
                }
                else
                {
                    if (!GlobalVariables.Add(name))
                    {
                        Program.hasError = true;
                        Console.WriteLine($"Eroare semantica: Variabila globala '{name}' a fost deja declarata.");
                    }
                }
            }
            return null;
        }

        public override object VisitFunction(LanguageParser.FunctionContext context)
        {
            _crrParameterNames.Clear();
            List<string> paramTypes = new List<string>();

            if (context.parameterList() != null)
            {
                foreach (var parameter in context.parameterList().parameter())
                {
                    string parameterName = parameter.IDENTIFIER().GetText();
                    string parameterType = parameter.type().GetText();
                    _crrParameterNames.Add(parameterName);
                    paramTypes.Add(parameterType);
                }
            }
           
            _isInsideFunction = true;

            string returnType = context.type().GetText();
            string functionName = context.IDENTIFIER().GetText();
            string parameters = context.parameterList()?.GetText() ?? "";
 
            bool isRecursive = IsRecursive(context);
            bool isMainFunction = functionName == "main";

            _controlStructures.Clear();
            LocalVariables.Clear();

            VisitBlock(context.block());

            string functionDetails = $"Functie: {returnType} {functionName}({parameters})\n" +
                                      $"Tip functie: {(isMainFunction ? "main" : isRecursive ? "recursiva" : "iterativa")}\n" +
                                      $"Variabile locale:\n  {string.Join("\n  ", LocalVariables)}\n" +
                                      $"Structuri de control:\n  {string.Join("\n  ", _controlStructures)}\n";

            if (_functionParametersTypes.ContainsKey(functionDetails) && _functionParametersTypes[functionDetails] == parameters)
            {
                Program.hasError = true;
                Console.WriteLine($"Eroare semantica: Functia '{functionName}({string.Join(", ", parameters)})' a fost deja declarata.");
            }
            else
            {
                Functions.Add(functionDetails);
                _functionNames.Add(functionName);
                _functionParametersTypes[functionDetails] = string.Join(" ", paramTypes);
            }

            _isInsideFunction = false;

            return null;
        }

        public override object VisitFunctionCall(LanguageParser.FunctionCallContext context)
        {
            string functionName = context.IDENTIFIER().GetText();
            List<string> arguments = new List<string>(); 
            if (context.argumentList() != null)
            {
                foreach (var argument in context.argumentList().expression())
                    arguments.Add(argument.GetText());
            }

            bool foundName = false;
            foreach (var key in _functionParametersTypes.Keys)
                if (key.Contains(functionName))
                {
                    foundName = true;
                    string functionParams = _functionParametersTypes[key];
                    List<string> functionParamTypes = functionParams.Split(' ').ToList();

                    if (functionParamTypes.Count() != arguments.Count())
                    {
                        Program.hasError = true;
                        Console.WriteLine($"Eroare semantica: Functia '{functionName}' a fost declarata cu alta lista de parametri.");
                        break;
                    }
                    for (int i = 0; i < arguments.Count(); i++)
                        if(!IsTypeCompatible(arguments[i], functionParamTypes[i]))
                        {
                            Program.hasError = true;
                            Console.WriteLine($"Eroare semantica: Functia '{functionName}' a fost declarata cu alta lista de parametri.");
                        }
                    break;
                }
            if(!foundName)
            {
                Program.hasError = true;
                Console.WriteLine($"Eroare semantica: Functia '{functionName}' nu a fost declarata.");
            }

            return null;
        }

        public override object VisitBlock(LanguageParser.BlockContext context)
        {
            foreach (var statement in context.statement())
            {
                if (statement.declaration() != null)
                {
                    VisitDeclaration(statement.declaration());
                }
                else if (statement.ifStatement() != null)
                {
                    _controlStructures.Add($"IF, linia {statement.ifStatement().Start.Line}");
                }
                else if (statement.forStatement() != null)
                {
                    _controlStructures.Add($"FOR, linia {statement.forStatement().Start.Line}");
                }
                else if (statement.whileStatement() != null)
                {
                    _controlStructures.Add($"WHILE, linia {statement.whileStatement().Start.Line}");
                }
            }

            return null;
        }
       
        private bool IsTypeCompatible(string type, string value)
        {
            switch (type)
            {
                case "int":
                    return int.TryParse(value, out _);
                case "float":
                    return float.TryParse(value, out _);
                case "double":
                    return double.TryParse(value, out _);
                case "string":
                    return value.StartsWith("\"") && value.EndsWith("\"");
                default:
                    return false;
            }
        }
        private static string GetTokenName(int tokenType)
        {
            switch (tokenType)
            {
                case LanguageLexer.IDENTIFIER: return "IDENTIFIER";
                case LanguageLexer.NUMBER: return "NUMBER";
                case LanguageLexer.STRING_LITERAL: return "STRING_LITERAL";
                case LanguageLexer.INT:
                case LanguageLexer.FLOAT:
                case LanguageLexer.DOUBLE:
                case LanguageLexer.STRING:
                case LanguageLexer.VOID:
                case LanguageLexer.IF:
                case LanguageLexer.ELSE:
                case LanguageLexer.FOR:
                case LanguageLexer.WHILE:
                case LanguageLexer.RETURN:
                    return "KEYWORD";
                case LanguageLexer.PLUS:
                case LanguageLexer.MINUS:
                case LanguageLexer.TIMES:
                case LanguageLexer.DIVIDE:
                case LanguageLexer.MOD:
                    return "ARITHMETIC_OPERATOR";
                case LanguageLexer.LT:
                case LanguageLexer.GT:
                case LanguageLexer.LE:
                case LanguageLexer.GE:
                case LanguageLexer.EQ:
                case LanguageLexer.NEQ:
                    return "RELATIONAL_OPERATOR";
                case LanguageLexer.AND: 
                case LanguageLexer.OR: 
                case LanguageLexer.NOT: 
                    return "LOGIC_OPERATOR";
                case LanguageLexer.ADD_ASSIGN:
                case LanguageLexer.SUB_ASSIGN:
                case LanguageLexer.MUL_ASSIGN:
                case LanguageLexer.DIV_ASSIGN:
                case LanguageLexer.MOD_ASSIGN:
                case LanguageLexer.ASSIGN:
                    return "ASSIGN_OPERATOR";
                case LanguageLexer.INCREMENT_OP: return "INCREMENT_OP";
                case LanguageLexer.DECREMENT_OP: return "DECREMENT_OP";
                case LanguageLexer.LPAREN:
                case LanguageLexer.RPAREN:
                case LanguageLexer.LBRACE:
                case LanguageLexer.RBRACE:
                case LanguageLexer.COMMA:
                case LanguageLexer.SEMICOLON:
                    return "DELIMITER";

                default: return "UNKNOWN";
            }
        }

        private static bool IsRecursive(LanguageParser.FunctionContext context)
        {
            string functionName = context.IDENTIFIER().GetText();
            string functionBody = context.block().GetText();

            return functionBody.Contains(functionName);
        }
    }
}
