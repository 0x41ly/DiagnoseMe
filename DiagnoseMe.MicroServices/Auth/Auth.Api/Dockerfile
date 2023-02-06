FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7095

ENV ASPNETCORE_URLS=http://+:7095
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ConnectionStrings__DefaultConnection="Server=authdb;Database=AuthDb;Uid=DiagnoseMe;Pwd=DiagnoseMe"

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Auth/Auth.Api/Auth.Api.csproj", "Auth/Auth.Api/"]
RUN dotnet restore "Auth/Auth.Api/Auth.Api.csproj"
COPY . .
WORKDIR "/src/Auth/Auth.Api"
RUN dotnet build "Auth.Api.csproj" -c Release -o /app/build

FROM build AS final
WORKDIR /src/Auth/Auth.Api
COPY --from=build /src/Auth/Auth.Api .
ENTRYPOINT ["dotnet", "run"]
