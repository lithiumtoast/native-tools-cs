# Superseded by `C2CS.NativeTools` C# project: https://github.com/lithiumtoast/c2cs

# Native Tools C#

Shims and utilities for platform invoke (PInvoke) in .NET 5

## Developers: How to include into your C# project

Add the following to your .csproj:

```xml
<!-- NativeTools -->
<PropertyGroup>
  <NativeToolsSourcePath>PATH/TO/native-tools-cs/src</NativeToolsSourcePath>
</PropertyGroup>
<ItemGroup>
  <Compile Include="$(NativeToolsSourcePath)/**/*">
    <Link>native-tools-cs/%(Filename)%(Extension)</Link>
  </Compile>
</ItemGroup>
```
