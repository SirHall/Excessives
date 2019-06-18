# C# Excessives Library

**What is the C# Excessives library?**
The C# Excessives library is a utility library made to make your C# development life easier by giving you powerful tools that will make your code both shorter, and easier to read.

Instead of writing this:
```csharp
int[] values = { 1, 2, 3, 4, 5 };

foreach(int val in values) {
	Console.WriteLine(val.ToString());
}
```

Write this:
```csharp
int[] values = { 1, 2, 3, 4, 5 };
values.ForEach(n => n.WriteLine());
```

This library also adds many other useful things that assist in all projects: 
 - Easy conversion from primitives to their binary/hex format and vice-versa.
 - A simple and fast event system that allows incredible de-coupling of separate systems that in reality should not depend on eachother.
 - A math extension library that supports:
    -- clamping 
    -- n-rounding, n-ceiling, c-flooring
    -- linear interpolation, quadratic interpolation, sine interpolation
    -- reverse interpolation
    -- quick radian-degrees conversion
    -- an easy to use crypto rand
    -- Hex color selection library
 - Easy printing of enumerables(arrays/lists) and dictionaries
 - Forward and reverse enumerable looping extension methods
    -- `yourarray.ForEach(n => n.WriteLine());`
    -- `yourArray.For((n, index) => yourArray[index] = n + 1);`
    -- *And reverse versions too*
 - Intelligently construct sub arrays:
    -- `newArray = yourArray.SubArray(startIndex, length);`
 - Even with indices that wrap around the end of the array:
    -- `newArray = yourArray.SubArraySmart(startIndex, length, stepSize);`

**Usage**
To use just add this project to your own as a git submodule or just download the project in a zip file and unzip it into your project's source directory.
To learn more about how to use this library check out the wiki.