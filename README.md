# C# Excessives Library<br/>

**What is the C# Excessives library?**<br/>
The C# Excessives library is a utility library made to make your C# development life easier by giving you powerful tools that will make your code both shorter, and easier to read.<br/>

Instead of writing this:
```csharp
int[] values = { 1, 2, 3, 4, 5 };

foreach(int val in values) {
	Console.WriteLine(val.ToString());
}
```

Write this:<br/>
```csharp
int[] values = { 1, 2, 3, 4, 5 };
values.ForEach(n => n.WriteLine());
```

This library also adds many other useful things that assist in all projects:<br/>
 - Easy conversion from primitives to their binary/hex format and vice-versa.<br/>
 - A simple and fast event system that allows incredible de-coupling of separate systems that in reality should not depend on eachother.<br/>
 - A math extension library that supports:<br/>
    -- clamping<br/>
    -- n-rounding, n-ceiling, c-flooring<br/>
    -- linear interpolation, quadratic interpolation, sine interpolation<br/>
    -- reverse interpolation<br/>
    -- quick radian-degrees conversion<br/>
    -- an easy to use crypto rand<br/>
    -- Hex color selection library<br/>
 - Easy printing of enumerables(arrays/lists) and dictionaries<br/>
 - Forward and reverse enumerable looping extension methods<br/>
    -- `yourarray.ForEach(n => n.WriteLine());`<br/>
    -- `yourArray.For((n, index) => yourArray[index] = n + 1);<br/>`
    -- *And reverse versions too*<br/>
 - Intelligently construct sub arrays:<br/>
    -- `newArray = yourArray.SubArray(startIndex, length);`<br/>
 - Even with indices that wrap around the end of the array:<br/>
    -- `newArray = yourArray.SubArraySmart(startIndex, length, stepSize);`<br/>

**Usage**<br/>
To use just add this project to your own as a git submodule or just download the project in a zip file and unzip it into your project's source directory.<br/>
To learn more about how to use this library check out the wiki.