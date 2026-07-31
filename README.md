![GitHub Actions: Build TinyWall Workflow](https://github.com/DaPa/TinyWall/actions/workflows/Build-TinyWall-NETFramework.yml/badge.svg)

<br />
<div align="center">
  <h3 align="center">TinyWall</h3>

  <p align="center">
    A free, lightweight and non-intrusive firewall
    <br />
    <a href="https://tinywall.pados.hu"><strong>Website »</strong></a> | <a href="https://tinywall.pados.hu/download.php"><strong>Download »</strong></a>
  </p>
</div>


# Note

Test build only!


## About


TinyWall is a free, lightweight, and non-intrusive, secure by default firewall for Windows. Built to just simply sit in your system tray, quietly blocking any application you did not explicitly allow network access. TinyWall installs no kernel drivers, so it cannot negatively influence system stability. It also repects your privacy and collects absolutely no data about the user or their computer.

This repository houses the source code of TinyWall as found at its [official website](https://tinywall.pados.hu).

## How to build

### Necessary tools

- Microsoft Visual Studio 2026 (or 2022)
- [Wix v3.14 Toolset](https://github.com/wixtoolset/wix3/releases/tag/wix3141rtm)
- [Visual Studio extension for Wix v3 Toolset](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2022Extension)

### To build the application

1. Open the solution file in Visual Studio and compile the `TinyWall` project. The other projects referenced inside the solution need not be built separately as they will be statically compiled into the application.
1. Done.

### To update/build build the database of known applications

1. Adjust the individual JSON files in the `TinyWall\Database` folder.
1. Start the application with the `/develtool` flag.
1. Use the `Database creator` tab to create one combined database file in JSON format. The output file will be called `profiles.json`.
1. To use the new database in debug builds, copy the output file to the `TinyWall\bin\Debug` folder.
1. Done.

### To build the installer

1. Copy the compiled application files and all dependencies into the `MsiSetup\Sources\ProgramFiles\TinyWall` folder.
1. Update the files as necessary inside the `MsiSetup\Sources\CommonAppData\TinyWall` folder. See instructions above about creating the database.
1. Open the solution file in Visual Studio and compile the `MsiSetup` project.
1. Done.

## Contributing

Feel free to open issues, feature- or pull-requests. I kindly ask for patience though, as TinyWall is in maintenance mode and my responses are often delayed. Nevertheless all issues and requests are looked at. 

New features are more likely to get implemented if you provide the necessary code changes yourself. The process for that is the same as for most other projects here on GitHub, in short:
1. Fork the Project
1. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
1. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
1. Push to the Branch (`git push origin feature/AmazingFeature`)
1. Open a Pull Request on GitHub

For complex features or large changes, please contact me first if your changes are still within the vision of TinyWall staying small, efficient and simple.

If your goal with forking is not code contribution but to build and distribute your own binaries, please choose a name dissimilar to "TinyWall" to avoid confusing users.

## License

| Contents in                     | Maintainer   | Origin                                                                                                                                | License                  |
|---------------------------------|--------------|---------------------------------------------------------------------------------------------------------------------------------------|--------------------------|
| Microsoft.Samples\TaskDialog\   | KevinGre     | [link](https://www.codeproject.com/Articles/17026/TaskDialog-for-WinForms)  ([archive.org](https://web.archive.org/web/20250211033156/https://www.codeproject.com/Articles/17026/TaskDialog-for-WinForms))                                                          | Public Domain            |
| Microsoft.Samples\Privilege.cs  | Mark Novak   | [link](https://learn.microsoft.com/en-us/archive/msdn-magazine/2005/march/using-net-making-privileges-reliable-secure-and-efficient)  | see Privilege.cs.LICENSE |
| DarkModeCS.cs                   | BlueMystic   | [link](https://github.com/BlueMystical/Dark-Mode-Forms)                                                                               | MIT                      |
| Everything else                 | Károly Pados | [this repo](https://github.com/pylorak/TinyWall)                                                                                      | GPLv3                    |

## Contact

Károly Pados - find e-mail at the bottom of the project website

Website: <https://tinywall.pados.hu>

GitHub: <https://github.com/pylorak/tinywall>
