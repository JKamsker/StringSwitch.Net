### StringSwitch-Net

Samples:
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
var @switch = StringSwitch.New.IgnoreCase
    .Case("value1", () => "Value 1 was received")
    .Case("value2", () => "Value 2 was received")
    .Case("value3", () => "Value 3 was received");

var result = @switch.Execute("Value1");
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
