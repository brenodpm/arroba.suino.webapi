build:
	dotnet build
clean:
	dotnet clean
restore:
	dotnet restore
watch:
	dotnet watch --project 1\ -\ Application/arroba.suino.webapi.Application.csproj
start:
	dotnet run --project 1\ -\ Application/arroba.suino.webapi.Application.csproj