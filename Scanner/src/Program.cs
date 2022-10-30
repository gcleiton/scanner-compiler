class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please enter a file path argument.");
            Console.WriteLine("The program must be written in C language.");
            return 0;
        }

        var filename = args[0];
        if (File.Exists(filename))
        {
            var fileReader = new FileReader(filename);
            var scanner = new Scanner.Scanner(fileReader);
            var tokens = scanner.Scan();
            
            Console.WriteLine("Obtained tokens:");
            foreach (var token in tokens)
            {
                Console.WriteLine($"{token.Value}: {token.Key}");
            }
            
            return 1;
        }
        
        Console.WriteLine("File not found! Please enter again.");
        return 0;
    }
}
