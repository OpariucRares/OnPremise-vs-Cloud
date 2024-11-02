# Use the official .NET SDK image to build the application
# do it from root directory
# docker build -t productservice -f ProductService/Dockerfile .
# docker run -d -p 5002:5002 productservice
# docker stop productservice orderservice
# docker rm productservice orderservice

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002
EXPOSE 6002

# Use the .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProductService/ProductService.csproj", "ProductService/"]
RUN dotnet restore "ProductService/ProductService.csproj"
COPY . .
WORKDIR "/src/ProductService"
RUN dotnet build "ProductService.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ProductService.csproj" -c Release -o /app/publish

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ProductService/.env .env 
ENTRYPOINT ["dotnet", "ProductService.dll"]
