# ProductPriceStatistics

Проект по парсингу сайтов с целью сбора и хранения статистики по ценам на различные товары.

## Структура проекта

- ProductPriceStatistics.ScraperWorkerService - консольное приложение для парсинга сайтов и сбора статистики в базу данных.
- ProductPriceStatistics.WebApi - приложение ASP.NET Core(WebAPI) для предоставления публичного интерфейса доступа к статистике.
- ProductPriceStatistics.Core - библиотека с основными классами для решения.
- ProductPriceStatistics.ScraperService - библиотека с классами для парсинга сайтов.
- ProductPriceStatistics.Infrastructure.EFCoreRepository - проект реализующий интерфейсы репозиториев. В качестве реализации используется ORM Entity Framework Core.

## Установка

1. Скачать проект HtmlParser
```
git clone https://github.com/kirvas-d/HtmlParser.git
```
2. Опубликовать пакет nuget
```
cd HtmlParser
dotnet pack
```
3. Скачать проект
```
git clone https://github.com/kirvas-d/ProductPriceStatistics.git
```
4. Добавить папку с HtmlParser.x.x.x.nupkg в источники nuget
```
cd ProductPriceStatistics
dotnet nuget add source $folderToHtmlParser.nupkg
```
5. Собрать проект
```
dotnet build
```