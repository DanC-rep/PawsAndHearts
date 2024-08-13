FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PawsAndHearts.API/PawsAndHearts.API.csproj", "src/PawsAndHearts.API/"]
COPY ["src/PawsAndHearts.Application/PawsAndHearts.Application.csproj", "src/PawsAndHearts.Application/"]
COPY ["src/PawsAndHearts.Domain/PawsAndHearts.Domain.csproj", "src/PawsAndHearts.Domain/"]
COPY ["src/PawsAndHearts.Infrastructure/PawsAndHearts.Infrastructure.csproj", "src/PawsAndHearts.Infrastructure/"]
RUN dotnet restore "src/PawsAndHearts.API/PawsAndHearts.API.csproj"
COPY . .
WORKDIR "/src/src/PawsAndHearts.API"
RUN dotnet build "PawsAndHearts.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PawsAndHearts.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PawsAndHearts.API.dll"]
