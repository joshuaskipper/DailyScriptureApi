# Use the official .NET image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["BibleNotificationApi/BibleNotificationApi.csproj", "BibleNotificationApi/"]
RUN dotnet restore "BibleNotificationApi/BibleNotificationApi.csproj"
COPY . .
WORKDIR "/src/BibleNotificationApi"
RUN dotnet build "BibleNotificationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BibleNotificationApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BibleNotificationApi.dll"]