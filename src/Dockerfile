#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/azure-functions/dotnet:3.0 as base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Athena.csproj", ""]
RUN dotnet restore "./Athena.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Athena.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Athena.csproj" -c Release -o /app/publish

FROM base AS final
LABEL maintainer="AlexisMtr"
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true