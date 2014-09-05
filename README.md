BloxAR
======
_BloxAR_ is a highly interactive social augmented reality game aiming to bring people together through their smartphones. This repository contains the source code required to build _BloxAR_.

Structure
---------
This repository contains the three components of the _BloxAR_ smartphone game: the server, the client and common code.

The server and the client each have their own Unity3d project. Since Unity3d does not allow project assets such as scripts and prefabs to be placed outside the project folder, the Common folder in the root is duplicated to both Client/Assets and Server/Assets, resulting in large amounts of code duplication in the Common folders (100%, to be precise).

Scripts used are contained in the Scripts folders inside Client, Server and Common. Prefabs and other resources are located in the Resources folder and tests in the Editor/Tests folders.

The way Unity3d is set up allows little freedom in the way to structure code. Every script attached to a `GameObject` must extend from the `MonoBehaviour` class and cannot be created using the `new` keyword. To make testing somewhat possible, the Common/Scripts/UnityBridge folder contains interfaces and wrappers for many of the Unity3d components used in the code. Furthermore, many scripts have a matching *Loader* class which extends from `MonoBehaviour` and provides a layer between Unity3d and BuildingBlocks.

The way Remote Procedure Calls (and networking in general) work in Unity3d forces us to pass two additional arguments to every RPC call (namely the function name and the receiver(s)). Furthermore, as much information as logical is passed into a single RPC call with regard to efficiency. This results in some calls with very many arguments.

Third party libraries and tools
-------------------------------
AR SDK: http://developer.vuforia.com/

Unity3d: http://www.unity3d.com

SmoothCamera.cs and Math3D (pre-pruning): https://gist.github.com/jankolkmeier/8543156

Icons made by [Freepik](http://www.freepik.com).
