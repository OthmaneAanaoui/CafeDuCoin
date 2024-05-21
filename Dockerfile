# Stage 1: Build the frontend
FROM node:16 AS frontend-build
WORKDIR /frontend

# Copy the frontend project files
COPY ./frontend ./

# Install dependencies and build the frontend
RUN npm install
RUN npm run build

# Stage 2: Build the backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything and restore as distinct layers
COPY . ./
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Stage 3: Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copy the backend output
COPY --from=build-env /App/out .

# Copy the built frontend into the wwwroot of the backend
COPY --from=frontend-build /frontend/build ./wwwroot

ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
