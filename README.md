<h1 align="center"> EasyTool </h1>

<div align="center">

一个开源的 .NET 工具库, 使得开发变得更加有效率


[![pull_request](https://github.com/dotnet-easy/easytool/actions/workflows/pull_request.yml/badge.svg)](https://github.com/dotnet-easy/easytool/actions/workflows/pull_request.yml)
[![](https://img.shields.io/nuget/v/EasyTool.Core.svg)](https://www.nuget.org/packages/EasyTool.Core)
<p>
    <span>中文</span> |  <a href="README.EN-US.md">English</a>
</p>
</div>

## 📚 简介

Easytool 是一个功能丰富且易用的 .NET 工具库，旨在帮助开发者快速、便捷地完成各类开发任务。 这些封装的工具涵盖了字符串、数字、集合、编码、日期、文件、IO、加密、JSON、HTTP客户端等一系列操作， 可以满足各种不同的开发需求。
> [More information](https://easy-dotnet.com/pages/easytool/)
> 
## 🚀 快速开始
### 安装 
~~~
PM> Install-Package EasyTool.Core
~~~
或者 .NET CLI 👇
~~~
dotnet add package EasyTool.Core
~~~

### 使用
复制文件或者目录
~~~csharp
FileUtil.Copy(sourceDir, destinationDir, isOverwrite)
~~~
克隆对象
~~~csharp
var a = CloneUtil.Clone<Person>(person);
~~~


## 🛠️ 目录
Easytool 封装了开发过程中一些常用的方法

| Catalog                                           |     Introduce                                                                        |
| --------------------------------------------------|---------------------------------------------------------------------------------- |
| [clone](EasyTool.Core/CloneCategory/)             |     使用 CloneUtil.Clone 方法实现 .NET 对象的深度复制                                       |
| [code](EasyTool.Core/CodeCategory/)               |     提供基于 base32, base62 等编解码工具                                             |

## 代码共享


## 社区交流

**微信：ygdxg8657 （备注进群） QQ群：543829648  903210423（已满）** 

![easy-tool](https://raw.githubusercontent.com/dotnet-easy/easy-dotnet/main/files/img/easytool.png)
