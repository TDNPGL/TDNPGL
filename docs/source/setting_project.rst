Setting up project
==================================
.. toctree::
   :maxdepth: 2
   :caption: Contents:
.. _setting_project:

You can create project using two ways

First way
---------
* Create a .NET Core project, I recommend you call it [Game Name] .Assets.
* Create a Xamarin Forms, WinForms or GTKSharp application, I recommend you call it [Game Name]. [Platform name].
* After that you need to add references to TDNPGL.Core to all these projects and TDNPGL.Views. [Platform name] to the client project of the game.

.. tip:: I recommend using the second method because the first is deprecated
Second way
----------
* Download TDNPGL CLI from github repository
* Install .NET Cli
* Type ``tdnpgl -create [Game Name]`` in directory for game sources