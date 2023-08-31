#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microsservices/Catalog/2 - Infrastructure/In/ECommerce.Catalog.InfrastructureAdapter.In.WebApi/ECommerce.Catalog.InfrastructureAdapter.In.WebApi.csproj", "Microsservices/Catalog/2 - Infrastructure/In/ECommerce.Catalog.InfrastructureAdapter.In.WebApi/"]
RUN dotnet restore "Microsservices/Catalog/2 - Infrastructure/In/ECommerce.Catalog.InfrastructureAdapter.In.WebApi/ECommerce.Catalog.InfrastructureAdapter.In.WebApi.csproj"
COPY . .
WORKDIR "/src/Microsservices/Catalog/2 - Infrastructure/In/ECommerce.Catalog.InfrastructureAdapter.In.WebApi"
RUN dotnet build "ECommerce.Catalog.InfrastructureAdapter.In.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Catalog.InfrastructureAdapter.In.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Catalog.InfrastructureAdapter.In.WebApi.dll"]