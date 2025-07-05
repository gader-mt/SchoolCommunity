# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy everything
COPY . ./

# Run publish with correct path to the csproj
RUN dotnet publish "SchoolCommunity/SchoolCommunity.csproj" -c Release -o /app/publish

# Use runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .

# Configure URL and expose port
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

# Start the app
ENTRYPOINT ["dotnet", "SchoolCommunity.dll"]
