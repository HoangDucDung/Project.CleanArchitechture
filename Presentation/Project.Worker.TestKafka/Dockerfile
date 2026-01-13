# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/Project.Worker.TestKafka/Project.Worker.TestKafka.csproj", "Presentation/Project.Worker.TestKafka/"]
COPY ["Libs/Project.Host.Base/Project.Host.Base.csproj", "Libs/Project.Host.Base/"]
COPY ["Libs/Project.Libs/Project.Libs.csproj", "Libs/Project.Libs/"]
COPY ["Extensions/Project.Extensions/Project.Extensions.csproj", "Extensions/Project.Extensions/"]
COPY ["Presentation/Project.Worker.Base/Project.Worker.Base.csproj", "Presentation/Project.Worker.Base/"]
COPY ["Core/Project.Application.Contract/Project.Application.Contract.csproj", "Core/Project.Application.Contract/"]
COPY ["Core/Project.Domain.Share/Project.Domain.Share.csproj", "Core/Project.Domain.Share/"]
COPY ["Infrastructure/Project.Infastructure.Kafka/Project.Infastructure.Kafka.csproj", "Infrastructure/Project.Infastructure.Kafka/"]
RUN dotnet restore "./Presentation/Project.Worker.TestKafka/Project.Worker.TestKafka.csproj"
COPY . .
WORKDIR "/src/Presentation/Project.Worker.TestKafka"
RUN dotnet build "./Project.Worker.TestKafka.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Project.Worker.TestKafka.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:ErrorOnDuplicatePublishOutputFiles=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Configs /Configs
ENTRYPOINT ["dotnet", "Project.Worker.TestKafka.dll"]