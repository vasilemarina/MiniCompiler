using Antlr4.Runtime;
using System.IO;


namespace Tema2_LFC
{
    public class ParserErrorListener : IAntlrErrorListener<IToken>
    {
        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line,
                                int charPositionInLine, string msg, RecognitionException e)
        {
            output.WriteLine($"Eroare sintactica la linia {line}, coloana {charPositionInLine}: {msg}");
            Program.hasError = true;
        }
    }
}
