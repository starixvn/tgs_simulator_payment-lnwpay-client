FROM hub.lumigame.com/base/dotnet/core/aspnet:8.0-alpine-24h1.1 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM hub.lumigame.com/base/dotnet/core/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Saba.Payment.lnwPay/Saba.Payment.lnwPay.csproj", "Saba.Payment.lnwPay/"]
COPY ["lnwPay.Core/lnwPay.Core.csproj", "lnwPay.Core/"]
COPY ["lnwPay.Client/lnwPay.Client.csproj", "lnwPay.Client/"]
RUN dotnet restore "./Saba.Payment.lnwPay/Saba.Payment.lnwPay.csproj"
COPY . .
WORKDIR "/src/Saba.Payment.lnwPay"
RUN dotnet build "./Saba.Payment.lnwPay.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Saba.Payment.lnwPay.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Saba.Payment.lnwPay.dll"]