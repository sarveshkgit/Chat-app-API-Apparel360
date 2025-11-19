# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
# COPY *.sln ./
COPY . ./
# COPY YourProjectName/*.csproj ./YourProjectName/
RUN dotnet restore

# Copy the rest of the code
# COPY YourProjectName/. ./YourProjectName/
# WORKDIR /app/YourProjectName

# Build and publish the app
RUN dotnet publish -c Release -o out

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose port (match with your launchSettings.json or Program.cs if hardcoded)
EXPOSE 8085
# EXPOSE 443

ENTRYPOINT ["dotnet", "Apperel360.API.dll"]
