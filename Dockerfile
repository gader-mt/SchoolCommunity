# Use .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . ./

# Publish the app (correct path to .csproj)
RUN dotnet publish "SchoolCommunity/SchoolCommunity.csproj" -c Release -o /app/publish

# Use .NET 8 runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Set environment variable and expose port
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

# Start the app
ENTRYPOINT ["dotnet", "SchoolCommunity.dll"]
