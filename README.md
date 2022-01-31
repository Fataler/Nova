# Nova (English\Russian Version Fork)

![Nova banner](https://github.com/Lunatic-Works/Nova/wiki/img/nova_banner.png)

A programmer-friendly Visual Novel (VN)/Text Adventure Game (AVG) framework based on Unity

## Instructions for use

1. `git clone`, in order to save time, you can add `--depth 1`
2. Change the Unity version number in `ProjectSettings/ProjectVersion.txt` to your version number
3. Open `Assets/Scenes/Main.unity` in the Unity Editor, run the game, and read the sample work "Colorless"
4. Read the tutorial in the game and try to change the script. The script is `tut01.txt` and other files in the `Assets/Resources/Scenarios/` folder
5. If you want to modify the script of Colorless, you need to delete the English version of the script first, otherwise the Chinese and English scripts will not be compatible.
    * `Assets/Resources/LocalizedResources/English/Scenarios/` folder is the English version of the script, you can delete all
    * `Assets/Resources/LocalizedResourcePaths.txt` is used to record the files used in the English version, which can be deleted
6. For other information, please refer to [GitHub Wiki](https://github.com/Lunatic-Works/Nova/wiki/)

## common problem

**There are already many visual novel engines/frameworks online, how does Nova differ from them? **

    This article introduces the design ideas: https://zhuanlan.zhihu.com/p/272466277

**What version of Unity and what OS/platform are supported? **

    Support Unity 2019 and later, Windows/Linux/macOS/Android/iOS platforms. WebGL is not currently supported, but it seems that someone has successfully made our dependencies (tolua# and Json.NET) support WebGL, and we should be able to if we really want to.

**Can I add Puzzle/Battle games to Nova? Can I add Nova as a dialogue system to a puzzle/war game etc? **

    Most of the gameplay that Unity can do can be added to Nova, but it will be more difficult to add Nova to other games. The current positioning of Nova is "framework/template", not "plugin/extension package".

**Unity is dead, long live Godot! **

    Wait until Godot 4 comes out. .

## Release Notes

* v0.1: Fully compatible version with "Blue Box"
* v0.2: Rolling update version on `master` branch, important new features include new script parser, new save system, asynchronous `GameState`, these new features will not affect the workflow of game producers
* v0.3: The version that is being developed in sync with our new work (we will release a branch when we sort out the code), the expected new features include URP and Addressables, which have certain implications for the workflow of game producers Influence

## Links

* Our first visual novel work "Blue Box": [Steam](https://store.steampowered.com/app/1131740) [Zhihu](https://www.zhihu.com/question/409724349 ) [Bangumi](https://bgm.tv/subject/311066) [VNDB](https://vndb.org/v26506)
* Our Weibo: [@LunaticWorks](https://weibo.com/LunaticWorks)
* Our QQ group: 876212259, if there are more people discussing the program in the future, we may open another program group

Also take a look at other visual novel engines:

* [Ren'Py](https://github.com/renpy/renpy): If you have no programming foundation but want to learn, or have Python foundation, this engine is recommended
* [AVG.js](https://github.com/avgjs/avg-core): If you believe in web front-end, this engine is recommended
* [Librian](https://github.com/RimoChan/Librian): An engine that mixes Python backend and web frontend, the author is a Lolicon
* [AVGPlus](https://github.com/avg-plus/avg.renderer): Another engine that uses a web frontend, seems to be supported by Xihe Animation
* [Snowing](https://github.com/Strrationalism/Snowing): Hardcore engine written in C++

and a generic game engine:

* [PainterEngine](https://github.com/matrixcascade/PainterEngine): A cross-platform graphics application framework written in C language
* [EtherEngine](https://github.com/EtherProject/EtherEngine): Lua-based cross-platform game interface
* [Luna Engine](https://github.com/JX-Master/Luna-Engine-0.6): This engine is not made by us, but it looks interesting

as well as

* [UniGal](https://github.com/Uni-Gal/UniGal-Script): In order to solve the fragmentation problem of various visual novel engines, the general script format defined

Dependencies of this framework:

* [tolua#](https://github.com/topameng/tolua)
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)

Domestic mirror (updated by chance):

* https://gitee.com/woctordho/Nova