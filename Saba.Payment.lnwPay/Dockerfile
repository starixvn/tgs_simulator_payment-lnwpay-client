FROM hub.lumigame.com/base/dotnet/core/aspnet:8.0-alpine-24h1.1 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM hub.lumigame.com/base/dotnet/core/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Saba.Payment.LnwPay/Saba.Payment.LnwPay.csproj", "Saba.Payment.LnwPay/"]
COPY ["LnwPay.Core/LnwPay.Core.csproj", "LnwPay.Core/"]
COPY ["LnwPay.Client/LnwPay.Client.csproj", "LnwPay.Client/"]
RUN dotnet restore "./Saba.Payment.LnwPay/Saba.Payment.LnwPay.csproj"
COPY . .
WORKDIR "/src/Saba.Payment.LnwPay"
RUN dotnet build "./Saba.Payment.LnwPay.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Saba.Payment.LnwPay.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Saba.Payment.LnwPay.dll"]