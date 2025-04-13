using Antlr4.Runtime;
using System.IO;

namespace Tema2_LFC
{
    public class LexerErrorListener : IAntlrErrorListener<int>
        {
            public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line,
                                    int charPositionInLine, string msg, RecognitionException e)
            {
                output.WriteLine($"Eroare lexicala la linia {line}, coloana {charPositionInLine}: {msg}");
                Program.hasError = true;
            }
        }
}
