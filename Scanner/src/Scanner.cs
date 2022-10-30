namespace Scanner;

public class Scanner
{
    private IEnumerable<char> nonDerivableSymbols = new[] { '(', ')', '{', '}', ';' };
    private IEnumerable<char> operatorSymbols = new[] { '+', '-', '*', '/' };
    private IEnumerable<char> comparisonSymbols = new[] { '=', '<', '>', '!' };
    private IEnumerable<char> logicalSymbols = new[] { '&', '|' };

    private string buffer;
    private readonly IList<KeyValuePair<string, Token>> _tokens;
    
    private readonly FileReader _fileReader;

    public Scanner(FileReader fileReader)
    {
        _fileReader = fileReader;
        _tokens = new List<KeyValuePair<string, Token>>();
    }

    private void AddToken(string name, Token type)
    {
        var pair = new KeyValuePair<string, Token>(name, type);
        _tokens.Add(pair);
    } 
    
    public IList<KeyValuePair<string, Token>> Scan()
    {
        while (_fileReader.HasNext())
        {
            var current = _fileReader.Current;
            if (char.IsWhiteSpace(_fileReader.Current))
            {
                _fileReader.Next();
            }
            else if (char.IsLetter(_fileReader.Current))
            {
                HandleLetterStart();
            }
            else if (char.IsDigit(_fileReader.Current))
            {
                HandleDigitStart();
            }
            else if (operatorSymbols.Contains(current))
            {
                HandleOperatorStart(current);
            }
            else if (comparisonSymbols.Contains(current))
            {
                HandleComparisonStart(current);
            }
            else if (logicalSymbols.Contains(current))
            {
                HandleLogicalStart(current);
            }
            else if (nonDerivableSymbols.Contains(current))
            {
                HandleNonDerivableStart(current);
            }
            else 
            {
                _fileReader.Next();
            }
        } 

        AddToken(null, Token.EndOfFile);
        return _tokens;
    }

    private void HandleLetterStart()
    {
        buffer = _fileReader.Current.ToString();
        _fileReader.Next();
        while (_fileReader.HasNext())
        {
            if (char.IsLetterOrDigit(_fileReader.Current))
            {
                buffer += _fileReader.Current;
                _fileReader.Next();
            }
            else break;
        }

        var token = GetToken(buffer);
        AddToken(buffer, token);
    }

    private void HandleDigitStart()
    {
        buffer = _fileReader.Current.ToString();
        _fileReader.Next();

        while (_fileReader.HasNext())
        {
            if (char.IsDigit(_fileReader.Current))
            {
                buffer += _fileReader.Current;
                _fileReader.Next();
            }
            else break;
        }
        AddToken(buffer, Token.Number);
    }
    
    private void HandleOperatorStart(char symbol)
    {
        _fileReader.Next();
        var current = _fileReader.Current;

        if (current == symbol)
        {
            onNextSymbolIsRepeated(symbol);
        } else if (current == '=')
        {
            onNextSymbolIsAssignment(symbol);
        } 
        else
        {
            onNextSymbol(symbol);
        } 
    }

    private void HandleComparisonStart(char symbol)
    {
        _fileReader.Next();
    
        if (_fileReader.Current == '=')
        {
            onNextSymbolIsAssignment(symbol);
        }
        else
        {
            onNextSymbol(symbol);
        }
    }
    
    private void HandleLogicalStart(char symbol)
    {
        _fileReader.Next();
    
        if (_fileReader.Current == symbol)
        {
            onNextSymbolIsRepeated(symbol);
        }
        else
        {
            AddToken(null, Token.Invalid);
        }
    }
 
    private void HandleNonDerivableStart(char symbol)
    {
        onNextSymbol(symbol);
        _fileReader.Next();
    }
     
    private void onNextSymbolIsRepeated(char symbol)
    {
        var tokenSymbol = symbol.Concat(symbol);
        var token = GetToken(tokenSymbol);
        AddToken(tokenSymbol, token);
        _fileReader.Next();
    }

    private void onNextSymbolIsAssignment(char symbol)
    {
        var assignment = symbol.Concat('=');
        var token = GetToken(assignment);
        AddToken(assignment, token);
        _fileReader.Next();
    }

    private void onNextSymbol(char symbol)
    {
        var tokenSymbol = symbol.ToString();
        var token = GetToken(tokenSymbol);
        AddToken(tokenSymbol, token);
    }
 
    private Token GetToken(string name)
    {
        return name switch
        {
            "int" => Token.Integer,
            "for" => Token.ForLoop,
            "while" => Token.WhileLoop,
            "if" => Token.Conditional,
            "return" => Token.Return,
            ";" => Token.Semicolon,
            "+" => Token.Add,
            "++" => Token.Increment,
            "-" => Token.Subtract,
            "--" => Token.Decrement,
            "*" => Token.Multiply,
            "/" => Token.Divide,
            "(" => Token.LeftParenthesis,
            ")" => Token.RightParenthesis,
            "{" => Token.OpenBracket,
            "}" => Token.CloseBracket,
            "=" => Token.Assignment,
            "+=" => Token.AddAssignment,
            "-=" => Token.SubtractAssignment,
            "*=" => Token.MultiplyAssignment,
            "/=" => Token.DivideAssignment,
            "==" => Token.DoubleEqual,
            "!=" => Token.NotEqual,
            "<" => Token.LessThan,
            "<=" => Token.LessThanEqual,
            ">" => Token.GreaterThan,
            ">=" => Token.GreaterThanEqual,
            "&&" => Token.And,
            "||" => Token.Or,
            "!" => Token.Not,
            _ => Token.Identifier
        };
    }
}