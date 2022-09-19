#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservice.Producer.Api/Microservice.Producer.Api.csproj", "Microservice.Producer.Api/"]
COPY ["Microservice.Producer.Infra.Helpers/Microservice.Producer.Infra.Helpers.csproj", "Microservice.Producer.Infra.Helpers/"]
COPY ["Microservice.Producer.Infra.IoC/Microservice.Producer.Infra.IoC.csproj", "Microservice.Producer.Infra.IoC/"]
COPY ["Microservice.Producer.Domain/Microservice.Producer.Domain.csproj", "Microservice.Producer.Domain/"]
COPY ["Microservice.Producer.Infra.MessagingBroker/Microservice.Producer.Infra.MessagingBroker.csproj", "Microservice.Producer.Infra.MessagingBroker/"]
RUN dotnet restore "Microservice.Producer.Api/Microservice.Producer.Api.csproj"
COPY . .
WORKDIR "/src/Microservice.Producer.Api"
RUN dotnet build "Microservice.Producer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Producer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Producer.Api.dll"]
