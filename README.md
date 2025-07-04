# TypeScript Analyzer

**TypeScript Analyzer** is a C# tool that extracts and analyzes structural elements from TypeScript source code — including functions, class methods, arrow functions, interfaces, enums, and more. It’s built for static code analysis, documentation generation, or code transformation tooling.

## Features

- Extracts global and class-based functions
- Supports `async`, generator (`*`), and arrow functions
- Captures full method bodies and parameter lists
- Groups methods by class name
- Written in C# – fast and cross-platform

### Planned Enhancements

- [x] Extract interfaces, enums, type aliases
- [ ] Parse decorators and comments
- [ ] Detect import/export declarations
- [ ] Export results to JSON/Markdown
- [ ] Add CLI argument/file support
- [ ] Build a web-based UI viewer

## 🛠 Installation

```bash
git clone https://github.com/andikatjacobdennis/typescript-analyzer.git
cd typescript-analyzer
dotnet build
```

## Usage

1. Place your `.ts` file (e.g., `sample.ts`) inside the project directory.
2. Update the file path inside `Program.cs`:

```csharp
string filePath = "sample.ts";
```

3. Run the analyzer:

```bash
dotnet run --project typescript-analyzer.csproj
```

## Sample Output

```
=== Global Functions ===
Name: sayHello
Params: (name: string)
ReturnType: void
Code:
function sayHello(name: string): void {
    console.log("Hello, " + name);
}-----------------------

=== Class Methods === Class: Calculator ---
Name: add
Params: (a: number, b: number)
ReturnType: number
Code:
add(a: number, b: number): number {
    return a + b;
}-----------------------
```

## Project Structure

```
typescript-analyzer/
├── .gitignore
├── LICENSE
├── README.md
└── typescript-analyzer/
    ├── Program.cs                # Main analyzer logic
    ├── sample.ts                 # Example TypeScript input file
    └── typescript-analyzer.csproj
```

## Contributing

Pull requests, issues, and suggestions are welcome!

## License

This project is licensed under the [MIT License](LICENSE).

## Author

Built by [Andikat Jacob Dennis](https://github.com/andikatjacobdennis)

## Show your support

If you find this project useful, please star the repo — it helps others discover it too!
