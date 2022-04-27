### StringSwitch-Net

[![Dotnet](https://img.shields.io/badge/platform-.NET-blue)](https://www.nuget.org/packages/StringSwitch.Net/)
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/JKamsker/StringSwitch.Net/CI)](https://github.com/JKamsker/StringSwitch.Net/actions)
[![GitHub Repo stars](https://img.shields.io/github/stars/JKamsker/StringSwitch.Net)](https://github.com/JKamsker/StringSwitch.Net/stargazers)
[![Nuget version](https://img.shields.io/nuget/v/StringSwitch.Net)](https://www.nuget.org/packages/StringSwitch.Net/)
[![Nuget download](https://img.shields.io/nuget/dt/StringSwitch.Net)](https://www.nuget.org/packages/StringSwitch.Net/)
[![License](https://img.shields.io/github/license/JKamsker/StringSwitch.Net)](https://github.com/JKamsker/StringSwitch.Net/blob/main/LICENSE)



## Installation

The library is available as a nuget package. You can install it as any other nuget package from your IDE, try to search by `StringSwitch.Net`. You can find package details [on this webpage](https://www.nuget.org/packages/StringSwitch.Net/).

Package Manager
```xml

Install-Package StringSwitch.Net
```

.NET CLI
```xml
dotnet add package StringSwitch.Net
```

Package reference in .csproj file
```xml
<PackageReference Include="StringSwitch.Net" Version="1.0.7" />
```


## Samples
Executing code when some condition is met:
````csharp
StringSwitch.New.IgnoreCase
    .Case("value1", () => Console.WriteLine("Value 1 was received"))
    .Case("value2", () => Console.WriteLine("Value 2 was received"))
    .Case("value3", () => Console.WriteLine("Value 3 was received"))
    .Execute("Value1");
````

Return results:
````csharp
var result = StringSwitch.New.IgnoreCase
    .Case("value1", () => "Value 1 was received")
    .Case("value2", () => "Value 2 was received")
    .Case("value3", () => "Value 3 was received")
    .Execute("Value1");
    
Console.WriteLine(result);
````


You can also reuse the switch:
````csharp
var @switch = StringSwitch.New.IgnoreCase
    .Case("value1", () => Console.WriteLine("Value 1 was received"))
    .Case("value2", () => Console.WriteLine("Value 2 was received"))
    .Case("value3", () => Console.WriteLine("Value 3 was received"));

@switch.Execute("Value1");
````


That's it! Now you are ready to go!
