namespace Scanner;

public static class CharExtensions
{
    public static string Concat(this char original, char toCombine)
    {
        return original.ToString() + toCombine.ToString();
    }
}
 