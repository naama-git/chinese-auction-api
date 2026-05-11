FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /src

COPY . .

RUN dir

RUN dotnet restore "ChineseAuctionAPI.csproj"

RUN dotnet publish "ChineseAuctionAPI.csproj" -c Release -o /app

ENTRYPOINT ["dotnet", "/app/ChineseAuctionAPI.dll"]