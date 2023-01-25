FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7199

ENV ASPNETCORE_URLS=http://+:7199

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DiagnoseMe.GateWay/DiagnoseMe.GateWay.csproj", "DiagnoseMe.GateWay/"]
RUN dotnet restore "DiagnoseMe.GateWay/DiagnoseMe.GateWay.csproj"
COPY . .
WORKDIR "/src/DiagnoseMe.GateWay"
RUN dotnet build "DiagnoseMe.GateWay.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "DiagnoseMe.GateWay.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiagnoseMe.GateWay.dll"]
