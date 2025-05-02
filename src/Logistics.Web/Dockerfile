FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Logistics.Web/Logistics.Web.csproj", "src/Logistics.Web/"]
COPY ["src/Logistics.Application/Logistics.Application.csproj", "src/Logistics.Application/"]
COPY ["src/Logistics.Domain/Logistics.Domain.csproj", "src/Logistics.Domain/"]
COPY ["src/Logistics.Infrastructure/Logistics.Infrastructure.csproj", "src/Logistics.Infrastructure/"]
RUN dotnet restore "src/Logistics.Web/Logistics.Web.csproj"
COPY . .
WORKDIR "/src/src/Logistics.Web"
RUN dotnet build "Logistics.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Logistics.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logistics.Web.dll"]
