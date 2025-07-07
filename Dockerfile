# 1. SDK ile build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./MockApi/ ./MockApi/  # sadece gerekli dosyaları al
WORKDIR /app/MockApi        # proje dizinine gir
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# 2. Runtime için image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "MockApi.dll"]  # isme dikkat
