# Pokedex

This is a web api for consuming https://pokeapi.co/.

# Instructions to run

Clone this project or download zip file.

Api has swagger so can test api from browser "localhost:<port>/swagger"

## Docker

- Install docker for windows, follow steps and recommended steps to install as appropriate and restart when prompted
- launch powershell and cd to project directory ~\Pokedex.Web\

## Visual Studio Directly

 - Install Visual Studio
 - Open Pokedex.Web\Pokedex.Web.Sln in visual studio
 - Right Click Pokedex.Web project and click set as start up.
 - Click run or press f5.
 - If missing libraries (install them) - Install .net core sdk 3.1 runtime https://dotnet.microsoft.com/en-us/download/dotnet/3.1
 - It should autolaunch browser and open swagger page for the apis


# Future improvements

### Caching

### Store results in nosql like mongodb so can use future data 

### Use translations for error messages

### additional layers and depth to api

### Detailed api documentation