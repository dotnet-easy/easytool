<h1 align="center"> EasyTool </h1>

<div align="center">

An open source C# tool to make .NET easy. 


[![pull_request](https://github.com/786744873/easytool/actions/workflows/pull_request.yml/badge.svg)](https://github.com/786744873/easytool/actions/workflows/pull_request.yml)
[![](https://img.shields.io/nuget/v/EasyTool.Core.svg)](https://www.nuget.org/packages/EasyTool.Core)
<p>
    <span>English</span> |  <a href="README.md">中文</a>
</p>
</div>

## 📚 Introduce

EasyTool is a .NET tool to make .Net easy. It provides a large number of help classes to help developers complete various development tasks. It covers a series of operations such as string, number, collection, encoding, date, file, IO, encryption, database, JSON, HTTP client, etc. 
> [More information](https://easy-dotnet.com/pages/easytool/)
> 
## 🚀 Get started
### install
Install EasyTool.Core from the package manager console:
~~~
PM> Install-Package EasyTool.Core
~~~
Or from the .NET CLI as:
~~~
dotnet add package EasyTool.Core
~~~

### use
Copy file or directory
~~~csharp
FileUtil.Copy(sourceDir, destinationDir, isOverwrite)
~~~
Clone an object
~~~csharp
var a = CloneUtil.Clone<Person>(person);
~~~


## 🛠️ Catalog
Easytool provides some of the most commonly used experiences and methods in the development process

| Catalog                                           |     Introduce                                                                        |
| --------------------------------------------------|---------------------------------------------------------------------------------- |
| [clone](EasyTool.Core/CloneCategory/)             |     clone an object                                             |
| [code](EasyTool.Core/CodeCategory/)               |     base32, base62, etc                                             |
| [collection](EasyTool.Core/CollectionsCategory/)  |                dictionary,List,LinkList, etc                                                      |
| [converter](EasyTool.Core/ConvertCategory/)       |             convert data type                                     |
| [datetime](EasyTool.Core/DateTimeCategory/)       |     timerutil,timestamp,etc                                       |

## .NET Runtime Reference

// TODO

## Exchange community

**微信：ygdxg8657 （备注进群） QQ群：543829648  903210423（已满）** 

![easy-tool](https://raw.githubusercontent.com/786744873/easy-dotnet/main/files/img/easytool.png)
