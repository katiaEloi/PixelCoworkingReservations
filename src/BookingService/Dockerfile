# BookingService Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8082
ENV ASPNETCORE_URLS=http://+:8082
ENTRYPOINT [ "dotnet", "BookingService.dll" ]
