FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebAPI/StockBack.API.csproj", "WebAPI/"]
COPY ["StockBack.Application/StockBack.Application.csproj", "StockBack.Application/"]
COPY ["StockBack.Domain/StockBack.Domain.csproj", "StockBack.Domain/"]
COPY ["StockBack.Infrastructure/StockBack.Infrastructure.csproj", "StockBack.Infrastructure/"]
RUN dotnet restore "./WebAPI/StockBack.API.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "./StockBack.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StockBack.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockBack.API.dll"]