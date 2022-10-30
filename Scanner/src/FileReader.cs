public class FileReader
{
    private readonly string _rawContent;
    private readonly int _size;
    private int _index;
    
    public FileReader(string filename)
    {
        _rawContent = File.ReadAllText(filename);
        _size = _rawContent.Length;
        _index = 0;
    }

    public char Current => _rawContent[_index];

    public void Next()
    {
        _index++;
    }
    
    public bool HasNext()
    {
        return _index < _size;
    }
} 