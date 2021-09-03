#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ProductPriceStatistics.WebAPI/ProductPriceStatistics.WebAPI.csproj", "ProductPriceStatistics.WebAPI/"]
RUN dotnet restore "ProductPriceStatistics.WebAPI/ProductPriceStatistics.WebAPI.csproj"
COPY . .
WORKDIR "/src/ProductPriceStatistics.WebAPI"
RUN dotnet build "ProductPriceStatistics.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductPriceStatistics.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductPriceStatistics.WebAPI.dll"]