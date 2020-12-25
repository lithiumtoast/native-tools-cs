# Native Tools C#

Shims and utilities for platform invoke (PInvoke) in .NET 5

## Shims

- [BlittableBoolean](./src/BlittableBoolean.cs): The [.NET `bool` is not blittable](https://docs.microsoft.com/en-us/dotnet/standard/native-interop/best-practices#blittable-types). This little wrapper for a `byte` is useful when you have structs which contain C standard booleans (`_Bool`) but also need to be blittable themselves.

## Utilities

- [UnmanagedStrings](./src/UnmanagedStrings.cs): All .NET `string` types are objects which get heap allocated/tracked by the garbage collector (GC). This makes it annoying for using C APIs because C strings are just pointers. This utility helps transform and cache .NET strings to C strings and vice versa. The cache should be cleared when appropriate to reclaim memory.

- [RuntimePlatform](./src/RuntimePlatform.cs): The .NET base library doesn't have an easy and simple predefined constants for determining what the current platform is. This simple enum data type is meant to solve that. Usable from the `Runtime` static class.
