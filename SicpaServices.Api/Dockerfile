#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SicpaServices.Api/SicpaServices.Api.csproj", "SicpaServices.Api/"]
COPY ["Sicpa.Api.Application/Sicpa.Api.Application.csproj", "Sicpa.Api.Application/"]
COPY ["Sicpa.Api.Domain/Sicpa.Api.Domain.csproj", "Sicpa.Api.Domain/"]
RUN dotnet restore "SicpaServices.Api/SicpaServices.Api.csproj"
COPY . .
WORKDIR "/src/SicpaServices.Api"
RUN dotnet build "SicpaServices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SicpaServices.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SicpaServices.Api.dll"]