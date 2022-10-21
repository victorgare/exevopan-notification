#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ExevopanNotification.Api/ExevopanNotification.Api.csproj", "ExevopanNotification.Api/"]
RUN dotnet restore "src/ExevopanNotification.Api/ExevopanNotification.Api.csproj"
COPY . .
WORKDIR "/src/ExevopanNotification.Api"
RUN dotnet build "ExevopanNotification.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/ExevopanNotification.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExevopanNotification.Api.dll"]