# English Dictionary CLI

## TL;DR {#tl-dr}
This CLI tool give you the english definition of a word base, also use a [Trie](https://en.wikipedia.org/wiki/Trie#:~:text=In%20computer%20science%2C%20a%20trie,key%2C%20but%20by%20individual%20characters.) to give you predictions as you type your word.

## Table of Contents
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#tl-dr">TL;DR</a>
    </li>
    <li>
      <a href="#project-structure">Project Structure</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

## CLI App {#cli-app}

Normal Funtionality:

![norlmal](media/normal.gif)

Pipe Functionality:

![pipe](media/pipe.gif)


## Project Structure {#project-structure}

This project return the definition of a word in english and use a Trie to give you predictions as you type your word.

The project is divided in two parts:
- cli: The CLI tool
- cli.test: The test project

The CLI tool is divide in two files `Program.cs` and `Trie.cs`. The `Program.cs` is the main file that run the CLI tool and the `Trie.cs` is the data structure that hold the dictionary.

The projecr is structure as follow:

```bash
.
├── CliApp.sln
├── LICENSE
├── README.md
├── data
│   └── dictionary_compact.json -- Dictionary
├── cli
│   ├── CliApp.csproj
│   ├── Program.cs -- Program
│   └── Trie.cs -- Data Structure
└── cli.test
    ├── GlobalUsings.cs
    ├── UnitTest1.cs
    └── cli.test.csproj -- Test Project
```
## Prerequisites {#prerequisites}

- .NET 8.0


## Settings {#settings}

```shell
dotnet run --project cli
```

## Testing {#testing}

```shell
dotnet test
```

## Acknowledgements {#acknowledgements}

 
- [Trie wiki](https://en.wikipedia.org/wiki/Trie#:~:text=In%20computer%20science%2C%20a%20trie,key%2C%20but%20by%20individual%20characters.)
- [Dictionary](https://github.com/matthewreagan/WebstersEnglishDictionary?tab=readme-ov-file)


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/othneildrew/Best-README-Template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/othneildrew/Best-README-Template/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/othneildrew/Best-README-Template/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/othneildrew/Best-README-Template/issues
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=for-the-badge
[license-url]: https://github.com/othneildrew/Best-README-Template/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/othneildrew
[product-screenshot]: images/screenshot.png
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 



# Acknowledgments
- https://github.com/matthewreagan/WebstersEnglishDictionary?tab=readme-ov-file


