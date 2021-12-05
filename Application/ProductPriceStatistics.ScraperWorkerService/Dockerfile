#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Application/ProductPriceStatistics.ScraperWorkerService/ProductPriceStatistics.ScraperWorkerService.csproj", "Application/ProductPriceStatistics.ScraperWorkerService/"]
COPY ["Core/ProductPriceStatistics.Core/ProductPriceStatistics.Core.csproj", "Core/ProductPriceStatistics.Core/"]
COPY ["Infrastructure/ProductPriceStatistics.Infrastructure.EFCoreRepository/ProductPriceStatistics.Infrastructure.EFCoreRepository.csproj", "Infrastructure/ProductPriceStatistics.Infrastructure.EFCoreRepository/"]
COPY ["Infrastructure/ProductPriceStatistics.Infrastructure.RabbitMQService/ProductPriceStatistics.Infrastructure.RabbitMQService.csproj", "Infrastructure/ProductPriceStatistics.Infrastructure.RabbitMQService/"]
COPY ["Core/ProductPriceStatistics.ScraperService/ProductPriceStatistics.ScraperService.csproj", "Core/ProductPriceStatistics.ScraperService/"]
RUN dotnet restore "Application/ProductPriceStatistics.ScraperWorkerService/ProductPriceStatistics.ScraperWorkerService.csproj"
COPY . .
WORKDIR "/src/Application/ProductPriceStatistics.ScraperWorkerService"
RUN dotnet build "ProductPriceStatistics.ScraperWorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductPriceStatistics.ScraperWorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductPriceStatistics.ScraperWorkerService.dll"]