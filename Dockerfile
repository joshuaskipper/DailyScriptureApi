# Use the official .NET image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["DailyScriptureApi/DailyScriptureApi.csproj", "DailyScriptureApi/"]
RUN dotnet restore "DailyScriptureApi/DailyScriptureApi.csproj"
COPY . .
WORKDIR "/src/DailyScriptureApi"
RUN dotnet build "DailyScriptureApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyScriptureApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DailyScriptureApi.dll"]
