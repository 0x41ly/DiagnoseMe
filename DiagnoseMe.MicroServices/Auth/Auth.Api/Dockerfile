FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7095

ENV ASPNETCORE_URLS=http://+:7095
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ConnectionStrings__DefaultConnection="Server=authdb;Database=AuthDb;;Uid=DiagnoseMe;Pwd=DiagnoseMe"

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DiagnoseMe.MicroServices/Auth/Auth.Api/Auth.Api.csproj", "DiagnoseMe.MicroServices/Auth/Auth.Api/"]
RUN dotnet restore "DiagnoseMe.MicroServices/Auth/Auth.Api/Auth.Api.csproj"
COPY . .
WORKDIR "/src/DiagnoseMe.MicroServices/Auth/Auth.Api"
RUN dotnet build "Auth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Api.dll"]
