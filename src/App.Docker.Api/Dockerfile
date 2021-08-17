#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./App.Docker.Api/App.Docker.Api.csproj", "App.Docker.Api/"]
COPY ["./App.Docker.Infra.CrossCutting.Identity/App.Docker.Infra.CrossCutting.Identity.csproj", "App.Docker.Infra.CrossCutting.Identity/"]
COPY ["./App.Docker.Infra.CrossCutting.Ioc/App.Docker.Infra.CrossCutting.Ioc.csproj", "App.Docker.Infra.CrossCutting.Ioc/"]
COPY ["./App.Docker.Infra.Data/App.Docker.Infra.Data.csproj", "App.Docker.Infra.Data/"]
COPY ["./App.Docker.Domain/App.Docker.Domain.csproj", "App.Docker.Domain/"]

RUN dotnet restore "App.Docker.Api/App.Docker.Api.csproj"
COPY . .
WORKDIR "App.Docker.Api"
RUN dotnet build "App.Docker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Docker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Docker.Api.dll"]