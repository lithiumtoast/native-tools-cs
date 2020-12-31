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
  <Compile Include="$(NativeToolsSourcePath)/*.cs">
    <Link>native-tools-cs/*.cs</Link>
  </Compile>
</ItemGroup>
```