# docker commands
# do it from root directory
# docker build -t orderservice -f OrderService/Dockerfile .
# docker run -d -p 5001:5001 orderservice
# docker run -d -p 5001:5001 -e CONNECTION_STRING="Secret" orderservice
# docker run -d -p 5001:5001 --env-file OrderService/.env orderservice

# docker stop productservice orderservice
# docker rm productservice orderservice

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 6001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrderService/OrderService.csproj", "OrderService/"]
RUN dotnet restore "OrderService/OrderService.csproj"
COPY . .
WORKDIR "/src/OrderService"
RUN dotnet build "OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY OrderService/.env .env 
ENTRYPOINT ["dotnet", "OrderService.dll"]
