# Pokedex

This is a web api for consuming https://pokeapi.co/.

# Instructions to run

Clone this project or download zip file.

Api has swagger so can test api from browser and view api documentation "localhost:<port>/swagger"

## Docker

- Install docker for windows, follow steps and recommended steps to install as appropriate and restart when prompted
- launch powershell and cd to project directory ~\Pokedex.Web\ (Dockerfile should be in this directory)
- run this command: 
	> docker build -t pokedexapi-image -f Dockerfile .
- This should create the image for you - confirm the image is created by running the following:
	> docker images
- Now run the following the run the docker container with that image
	> docker run -d -p 8080:80 --name myPokedexapi pokedexapi-image
- Now Api should be accessible on http://localhost:8080 and swagger at http://localhost:8080/swagger/

## Visual Studio Directly

 - Install Visual Studio
 - Open Pokedex.Web\Pokedex.Web.Sln in visual studio
 - Right Click Pokedex.Web project and click set as start up.
 - Click run or press f5.
 - If missing libraries (install them) - Install .net core sdk 3.1 runtime https://dotnet.microsoft.com/en-us/download/dotnet/3.1
 - It should autolaunch browser and open swagger page for the testing the apis.


# Future improvements

### Caching

### Store results in nosql like mongodb so can use future data 

### Use translations for error messages

### additional layers and depth to api

### Detailed api documentation