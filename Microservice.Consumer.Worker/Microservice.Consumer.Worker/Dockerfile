#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservice.Consumer.Worker/Microservice.Consumer.Worker.csproj", "Microservice.Consumer.Worker/"]
COPY ["Microservice.Consumer.Infra.IoC/Microservice.Consumer.Infra.IoC.csproj", "Microservice.Consumer.Infra.IoC/"]
COPY ["Microservice.Consumer.Infra.Data.SqlServer/Microservice.Consumer.Infra.Data.SqlServer.csproj", "Microservice.Consumer.Infra.Data.SqlServer/"]
COPY ["Microservice.Consumer.Domain/Microservice.Consumer.Domain.csproj", "Microservice.Consumer.Domain/"]
COPY ["Microservice.Consumer.Infra.MessagingBroker/Microservice.Consumer.Infra.MessagingBroker.csproj", "Microservice.Consumer.Infra.MessagingBroker/"]
RUN dotnet restore "Microservice.Consumer.Worker/Microservice.Consumer.Worker.csproj"
COPY . .
WORKDIR "/src/Microservice.Consumer.Worker"
RUN dotnet build "Microservice.Consumer.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Consumer.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Consumer.Worker.dll"]
