# Use a base image with the .NET SDK installed
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project files into the container
COPY . .

# Run dotnet restore to restore dependencies


# Run dotnet publish to build the project
RUN dotnet publish "./EduTrack.csproj" -c release -o /app --no-restore

# Define the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EduTrack.dll"]
