# EntityMigrationFramework

[![Apache 2.0 License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Project Status: WIP](https://img.shields.io/badge/Status-Early_Alpha-red)](https://github.com/MohammadAyaad/EntityMigrationFramework)
[![.NET Version](https://img.shields.io/badge/.NET-9.0-purple.svg)](https://dotnet.microsoft.com)

**A type-safe data migration framework for .NET applications**  
*Early-stage solution for model versioning and evolution with compile-time validated transformations*

---

## 🚧 Project Status: Early Alpha 🚧

This project is currently in **active early development** and should be considered experimental. Key notes:

🔥 **Not production-ready**  
🔨 **API surface will change**  
📖 **Documentation is incomplete**  
⚡ **Performance not optimized**

**Proceed with caution** - ideal for experimentation and feedback, not production systems.

---

## 📚 Documentation

Explore our growing documentation at:  
[https://mohammadayaad.github.io/EntityMigrationFramework](https://mohammadayaad.github.io/EntityMigrationFramework)

Current documentation covers:
- Basic concepts
- Core API reference
- Example application walkthrough

---

## ✨ Features

\```csharp
// Type-safe migration implementation
public class UserMigration : IMigration<UserV1, UserV2> 
{
    public UserV2 Migrate(UserV1 src) => new() 
    {
        // Compile-time validated transformation
        FullName = $"{src.FirstName} {src.LastName}"
    };
}
\```

- Automatic migration path resolution
- Strong type validation
- Customizable registry system
- Version chain validation

---

## 🚀 Getting Started

1. Add the core package:
\```bash
dotnet add package EntityMigrationFramework
\```

2. Create your first migration:
\```csharp
public class SampleMigration : IMigration<OldModel, NewModel> 
{
    public NewModel Migrate(OldModel src) => new() 
    {
        // Transformation logic
    };
}
\```

3. Configure the manager:
\```csharp
var manager = new MigrationBuilder<BaseModel>()
    .AddMigration<SampleMigration, OldModel, NewModel>()
    .Prepare()
    .UseDefaultRegistry()
    .Build();
\```

---

## 🔍 Example Application

Explore the reference implementation in:  
`/src/EntityMigrationExampleApp`

Demonstrates:
- Multi-step model evolution
- Direct and indirect migrations
- JSON serialization integration

---

## 🛠️ Contributing

**This project urgently needs:**
- API surface refinement
- Documentation improvements
- Test coverage
- Performance optimizations

While formal guidelines are being developed, please:
1. Open an issue to discuss changes
2. Keep PRs focused and well-documented
3. Follow existing code patterns

---

## 📜 License

Licensed under **Apache 2.0** - full text in [LICENSE](LICENSE):

\```text
Copyright 2024 Mohammad Ayaad

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
\```

---

## 👨💻 Author

**Mohammad Ayaad**  
[GitHub Profile](https://github.com/MohammadAyaad)  
[Project Discussions](https://github.com/MohammadAyaad/EntityMigrationFramework/discussions)

*"Constructive feedback and brave testers wanted!"*