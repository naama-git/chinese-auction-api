# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ChineseAuctionAPI.csproj", "./"]
RUN dotnet restore "./ChineseAuctionAPI.csproj"

COPY . .
RUN dotnet build "ChineseAuctionAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChineseAuctionAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ChineseAuctionAPI.dll"]