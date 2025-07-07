# 1. SDK katmanı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .                              # Her şeyi kopyala
RUN dotnet restore                    # Restore yap
RUN dotnet publish -c Release -o /app # Yayınla

# 2. Runtime katmanı
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MockApi.dll"]  # .dll adını .csproj'e göre AYARLA!
