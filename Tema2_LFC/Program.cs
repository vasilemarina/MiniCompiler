using System;
using System.IO;
using System.Text.RegularExpressions;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;

namespace Tema2_LFC
{
    internal class Program
    {
        public static bool hasError = false;

        static void Main(string[] args)
        {
            string inputFilePath = "../../input.txt";
            string tokensFilePath = "tokens.txt";
            string globalVariablesFilePath = "global_variables.txt";
            string functionsFilePath = "functions.txt";

            try
            {
                if (!File.Exists(inputFilePath))
                {
                    throw new FileNotFoundException($"Fisierul de intrare nu a fost gasit: {Path.GetFullPath(inputFilePath)}");
                }

                string sourceCode = File.ReadAllText(inputFilePath);
                sourceCode = RemoveSpacesAndComments(sourceCode);

                Console.WriteLine("Codul sursa:");
                Console.WriteLine(sourceCode);
                Console.WriteLine();
                
                AntlrInputStream inputStream = new AntlrInputStream(sourceCode);
                LanguageLexer lexer = new LanguageLexer(inputStream);
                CommonTokenStream tokenStream = new CommonTokenStream(lexer);
                lexer.RemoveErrorListeners();
                lexer.AddErrorListener(new LexerErrorListener());

                LanguageParser parser = new LanguageParser(tokenStream);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ParserErrorListener());
                IParseTree tree = parser.program();

                CompilerVisitor visitor = new CompilerVisitor(lexer);
                visitor.Visit(tree);

                File.WriteAllLines(tokensFilePath, visitor.LexicalUnits);
                File.WriteAllLines(globalVariablesFilePath, visitor.GlobalVariablesDeclarations);
                File.WriteAllLines(functionsFilePath, visitor.Functions);

                if (!hasError)
                    Console.WriteLine("\nCompilarea s-a realizat cu succes!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A aparut o eroare: {ex.Message}");
            }

            Console.WriteLine("Apasati Enter pentru a inchide...");
            Console.ReadLine();
        }
        private static string RemoveSpacesAndComments(string code)
        {
            string noBlockCommentsCode = Regex.Replace(code, @"/\*.*?\*/", "", RegexOptions.Singleline);
            string noLineCommentsCode = Regex.Replace(noBlockCommentsCode, @"//.*", "");
            string noWhiteSpacesCode = Regex.Replace(noLineCommentsCode, @"^\s*$\n|\r", "", RegexOptions.Multiline);

            return noWhiteSpacesCode.Trim();
        }
    }
}
