FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef
RUN dotnet restore
RUN dotnet ef database update
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "amaranth.dll"]