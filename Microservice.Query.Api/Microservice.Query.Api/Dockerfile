#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservice.Query.Api/Microservice.Query.Api.csproj", "Microservice.Query.Api/"]
COPY ["Microservice.Query.Infra.IoC/Microservice.Query.Infra.IoC.csproj", "Microservice.Query.Infra.IoC/"]
COPY ["Microservice.Query.Infra.Data.Mongo/Microservice.Query.Infra.Data.Mongo.csproj", "Microservice.Query.Infra.Data.Mongo/"]
COPY ["Microservice.Query.Domain/Microservice.Query.Domain.csproj", "Microservice.Query.Domain/"]
COPY ["Microservice.Query.Infra.Data.SqlServer/Microservice.Query.Infra.Data.SqlServer.csproj", "Microservice.Query.Infra.Data.SqlServer/"]
RUN dotnet restore "Microservice.Query.Api/Microservice.Query.Api.csproj"
COPY . .
WORKDIR "/src/Microservice.Query.Api"
RUN dotnet build "Microservice.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Query.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Query.Api.dll"]