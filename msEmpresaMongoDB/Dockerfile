# ------------------------------
# Base runtime image
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# ------------------------------
# Build & publish stage
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["msEmpresaMongoDB.csproj", "./"]
RUN dotnet restore "./msEmpresaMongoDB.csproj"

# Copy all files
COPY . .

# Publish directly (no dotnet build)
RUN dotnet publish "./msEmpresaMongoDB.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ------------------------------
# Final image
# ------------------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "msEmpresaMongoDB.dll"]
