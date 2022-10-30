namespace Scanner;

public enum Token
{
    Invalid,
    EndOfFile,
    Identifier,
    Integer,
    Number,
    Add,
    Subtract,
    Multiply,
    Divide,
    Increment,
    Decrement,
    Assignment,
    AddAssignment,
    SubtractAssignment,
    MultiplyAssignment,
    DivideAssignment,
    NotEqual,
    LessThan,
    LessThanEqual,
    GreaterThan,
    GreaterThanEqual,
    DoubleEqual,
    And,
    Or,
    Not,
    LeftParenthesis,
    RightParenthesis,
    OpenBracket,
    CloseBracket,
    Semicolon,
    WhileLoop,
    ForLoop,
    Conditional,
    Return
}