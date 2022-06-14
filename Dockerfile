FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

ENV ASPNETCORE_ENVIRONMENT=$(ENVIRONMENT)

WORKDIR /app

COPY . .

RUN dotnet restore src/IO.TedTalk.Api/IO.TedTalk.Api.csproj
RUN dotnet publish src/IO.TedTalk.Api/IO.TedTalk.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build-env /app/publish .

ENTRYPOINT ["dotnet", "IO.TedTalk.Api.dll"]