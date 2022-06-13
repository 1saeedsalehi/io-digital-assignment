# IO digital assignment



# Project structure

I've used a clean architecture 
here are my layers:

Project Name  | Layer  | Responsibilites
------------- |------------- | -------------
IO.TedTalk.Api  | Presentation layer | Validate inputs , wrap output results and exceptions , create output
IO.TedTalk.Core  | Domain | extensions , customized exception types , domain models definition , application wide constants
IO.TedTalk.Data | Data layer | communicate with data layer such as SQLite db and csv data file and provides an abstraction with reposiotry pattern to access data
IO.TedTalk.Services | Service Layer | caching , map data between presentation layer and data layer , implement our logics


# Notes

things that I've used in my assignment task :

- .Net core 6 uing C#
- Ef Core 6 using SQLite db
- Api Versioning
- Api documentation with swagger 
- Docker
- Some Unit Tests
- Caching with memory cache
- AutoMapper



# How to run

there are two options for run this project


### Option 1 - using visual studio

you can easily open solution file in `src/IO.TedTalk.sln `
and then run `IO.TedTalk.Api` project

you can find api documentation here [https://localhost:7212/swagger/](https://localhost:7212/swagger/)

![Screenshot](/docs/screenshot.png)

### Option 2 - using Docker

make sure you have installed docker 
then navigate to project root folder
use this command to build an image from this project

`docker build . -t io_assignment`

then make a container with image you have build in previous step:

`docker run io_assignment -p 7012:80 --name io_assignment`




