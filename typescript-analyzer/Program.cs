using System.Text.RegularExpressions;

public class TsFunction
{
    public string Name { get; set; }
    public string Parameters { get; set; }
    public string FullCode { get; set; }
    public string ReturnType { get; set; }
    public string ClassName { get; set; } // null for global
    public override string ToString() =>
        $"Name: {Name}\nParams: {Parameters}\nReturnType: {ReturnType}\nCode:\n{FullCode}\n--------------------------";
}

public class TsDeclaration
{
    public string Kind { get; set; } // "interface", "enum", "type"
    public string Name { get; set; }
    public string FullCode { get; set; }
    public override string ToString() =>
        $"Kind: {Kind}\nName: {Name}\nCode:\n{FullCode}\n--------------------------";
}

class Program
{
    static void Main()
    {
        string filePath = "sample.ts";
        string code = File.ReadAllText(filePath);

        var allFunctions = ExtractFunctions(code);
        var declarations = ExtractDeclarations(code);

        Console.WriteLine("=== Global Functions and Arrows ===");
        foreach (var f in allFunctions.Where(f => string.IsNullOrEmpty(f.ClassName)))
            Console.WriteLine(f);

        Console.WriteLine("=== Class Methods ===");
        foreach (var grp in allFunctions.Where(f => !string.IsNullOrEmpty(f.ClassName)).GroupBy(f => f.ClassName))
        {
            Console.WriteLine($"\n--- Class: {grp.Key} ---");
            foreach (var f in grp)
                Console.WriteLine(f);
        }

        Console.WriteLine("=== Interfaces / Enums / Types ===");
        foreach (var d in declarations)
            Console.WriteLine(d);
    }

    static List<TsFunction> ExtractFunctions(string code)
    {
        var functions = new List<TsFunction>();

        // Match classes to extract their methods
        var classRegex = new Regex(@"class\s+(?<class>[A-Za-z0-9_]+)\s*{", RegexOptions.Compiled);
        foreach (Match match in classRegex.Matches(code))
        {
            string className = match.Groups["class"].Value;
            int bodyStart = code.IndexOf('{', match.Index);
            int bodyEnd = FindMatchingBrace(code, bodyStart);
            string body = code.Substring(bodyStart + 1, bodyEnd - bodyStart - 1);

            foreach (var func in ExtractFunctionBodies(body))
            {
                func.ClassName = className;
                functions.Add(func);
            }
        }

        // Global functions
        foreach (var func in ExtractFunctionBodies(code))
        {
            if (string.IsNullOrEmpty(func.ClassName))
                functions.Add(func);
        }

        return functions;
    }

    static List<TsFunction> ExtractFunctionBodies(string code)
    {
        var functions = new List<TsFunction>();

        // Named function
        var functionRegex = new Regex(@"(?<async>async\s+)?function\s*(\*)?\s*(?<name>[A-Za-z0-9_]+)?\s*\((?<params>[^\)]*)\)\s*(:\s*(?<returnType>[^{\n]+))?\s*{", RegexOptions.Compiled);
        foreach (Match match in functionRegex.Matches(code))
        {
            int start = match.Index;
            int open = code.IndexOf('{', start);
            int end = FindMatchingBrace(code, open);
            string full = code.Substring(start, end - start + 1);

            functions.Add(new TsFunction
            {
                Name = match.Groups["name"].Value ?? "<anonymous>",
                Parameters = match.Groups["params"].Value,
                ReturnType = match.Groups["returnType"].Value,
                FullCode = full
            });
        }

        // Arrow functions (with or without braces)
        var arrowFuncRegex = new Regex(@"(?<name>[A-Za-z0-9_]+)?\s*[:=]\s*(?<params>\([^\)]*\)|[A-Za-z0-9_]+)\s*=>\s*(?<body>{[^}]*}|[^\n;]+)", RegexOptions.Compiled);
        foreach (Match match in arrowFuncRegex.Matches(code))
        {
            string name = match.Groups["name"].Success ? match.Groups["name"].Value : "<anonymous>";
            string parameters = match.Groups["params"].Value;
            string body = match.Groups["body"].Value;
            string fullCode = match.Value;

            functions.Add(new TsFunction
            {
                Name = name,
                Parameters = parameters,
                ReturnType = "",
                FullCode = fullCode
            });
        }

        return functions;
    }

    static List<TsDeclaration> ExtractDeclarations(string code)
    {
        var declarations = new List<TsDeclaration>();

        // Interface
        var interfaceRegex = new Regex(@"interface\s+(?<name>[A-Za-z0-9_]+)\s*(?:extends\s+[^{]+)?\s*{", RegexOptions.Compiled);
        foreach (Match match in interfaceRegex.Matches(code))
        {
            int bodyStart = code.IndexOf('{', match.Index);
            int bodyEnd = FindMatchingBrace(code, bodyStart);
            string full = code.Substring(match.Index, bodyEnd - match.Index + 1);
            declarations.Add(new TsDeclaration
            {
                Kind = "interface",
                Name = match.Groups["name"].Value,
                FullCode = full
            });
        }

        // Enum
        var enumRegex = new Regex(@"enum\s+(?<name>[A-Za-z0-9_]+)\s*{", RegexOptions.Compiled);
        foreach (Match match in enumRegex.Matches(code))
        {
            int bodyStart = code.IndexOf('{', match.Index);
            int bodyEnd = FindMatchingBrace(code, bodyStart);
            string full = code.Substring(match.Index, bodyEnd - match.Index + 1);
            declarations.Add(new TsDeclaration
            {
                Kind = "enum",
                Name = match.Groups["name"].Value,
                FullCode = full
            });
        }

        // Type alias
        var typeRegex = new Regex(@"type\s+(?<name>[A-Za-z0-9_]+)\s*=\s*([^;]+);", RegexOptions.Compiled);
        foreach (Match match in typeRegex.Matches(code))
        {
            declarations.Add(new TsDeclaration
            {
                Kind = "type",
                Name = match.Groups["name"].Value,
                FullCode = match.Value
            });
        }

        return declarations;
    }

    static int FindMatchingBrace(string code, int start)
    {
        int count = 0;
        for (int i = start; i < code.Length; i++)
        {
            if (code[i] == '{') count++;
            if (code[i] == '}') count--;
            if (count == 0) return i;
        }
        return -1;
    }
}
