# TypeScript Analyzer

**TypeScript Analyzer** is a C# tool that extracts and analyzes structural elements from TypeScript source code — including functions, class methods, arrow functions, and more. It is designed for static code analysis, documentation generation, or code transformation tools.


## 🔍 Features

- ✅ Extracts global and class-based functions
- ✅ Supports `async`, generator (`*`), and arrow functions
- ✅ Captures full method bodies and parameter lists
- ✅ Groups methods by class name
- ✅ Written in C# – fast and easy to integrate
- 🧠 Future-proof for support of:
  - Interfaces, enums, types
  - Decorators and comments
  - Import/export declarations


## 📦 Installation

Clone the repository and build the project:

```bash
git clone https://github.com/your-username/typescript-analyzer.git
cd typescript-analyzer
dotnet build
````


## 🚀 Usage

Place a `.ts` file in the project folder (or specify a path), then run:

```bash
dotnet run --project TypeScriptAnalyzer.csproj
```

Edit `Program.cs` to point to your TypeScript file:

```csharp
string filePath = "sample.ts";
```


## 📄 Sample Output

```
=== Global Functions ===
Name: outside
Params: (msg: string)
ReturnType: void
...

=== Class Methods === Class: Calculator ---
Name: add
Params: (a: number, b: number)
ReturnType: number
...
```


## 📁 Project Structure

```
typescript-analyzer/
├── sample.ts               # Sample TypeScript input file
├── Program.cs              # Main analyzer logic
├── TsFunction.cs           # Function data model
├── README.md               # You're here!
└── ...
```


## 🛠 Roadmap

* [ ] Extract interfaces, enums, types
* [ ] Parse decorators and comments
* [ ] Export results to JSON or markdown
* [ ] Add CLI argument support
* [ ] Build a web-based UI


## 🤝 Contributing

Pull requests, issues, and suggestions are welcome!

To contribute:

1. Fork this repo
2. Create a new branch
3. Make your changes
4. Submit a pull request


## 📄 License

MIT License. See [LICENSE](LICENSE) for more information.


## 👨‍💻 Author

Built by [Andikat Jacob Dennis](https://github.com/andikatjacobdennis).


## 🌟 Star this repo

If you find this project useful, please give it a ⭐️ on GitHub! It helps others find it too.
- GitHub Actions setup for testing or CI

Ready to paste into your repo!
```
