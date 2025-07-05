# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy everything and publish
COPY . ./
RUN dotnet publish "SchoolCommunity.csproj" -c Release -o /app/publish

# Use runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .

# Set URL and expose port for Render
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

# Run the app
ENTRYPOINT ["dotnet", "SchoolCommunity.dll"]
