# Step 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Step 2: Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copy published files
COPY --from=build /app/publish .

# Set environment variable for Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "HospitalApplicationAPI.dll"]