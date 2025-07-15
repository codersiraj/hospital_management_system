# Step 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .
WORKDIR /src/HospitalApplicationAPI
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Step 2: Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HospitalApplicationAPI.dll"]
