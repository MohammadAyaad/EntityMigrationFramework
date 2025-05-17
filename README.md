# EntityMigrationFramework   
[![NuGet](https://img.shields.io/nuget/v/EntityMigrationFramework.svg?style=flat-square)](https://www.nuget.org/packages/EntityMigrationFramework/)  

A type-safe migration engine for .NET that automates object version transformations through graph-based pathfinding.

## Installation
```bash
Install-Package EntityMigrationFramework
```

## Quick Start
`````csharp
// 1. Define base type
public abstract class DataModel {
    public Guid Id { get; set; }
}

// 2. Create versioned models
public class ProductV1 : DataModel {
    public string SKU { get; set; }
}

public class ProductV2 : DataModel {
    public string ProductCode { get; set; }
    public DateTime Created { get; set; }
}

// 3. Implement migration
public class V1ToV2Migration : IMigration<ProductV1, ProductV2> {
    public ProductV2 Migrate(ProductV1 src) => new() {
        Id = src.Id,
        ProductCode = src.SKU,
        Created = DateTime.UtcNow
    };
}

// 4. Configure and use
var manager = new MigrationBuilder<DataModel>()
    .AddMigration<V1ToV2Migration, ProductV1, ProductV2>()
    .Prepare()
    .UseDefaultGraphRegistry()
    .Build();

ProductV2 migrated = manager.Migrate<ProductV1, ProductV2>(new ProductV1 { SKU = "ABC123" });
`````

## How It Works
`````mermaid
graph LR
    A[ProductV1] -->|V1→V2| B[ProductV2]
    B -->|V2→V3| C[ProductV3]
    A -->|Direct| D[ProductV4]
    C -->|V3→V4| D
`````

## Key Features
- **Automatic Pathfinding** - BFS-based shortest path detection
- **Type Safety** - Compile-time validation of migrations
- **Performance** - Compiled expression trees for fast execution

## Advanced Usage
`````csharp
// Custom weighted registry
public class WeightedRegistry<TBase> : IMigrationRegistry<TBase> {
    public List<Func<object, object>> GetMigrationPath<TFrom, TTo>() {
        // Implement custom pathfinding with weights
    }
}

// Configure with dependency injection
services.AddSingleton<IMigrationRegistry<DataModel>>(provider => 
    new WeightedRegistry<DataModel>(provider.GetServices<IMigration>()));
`````