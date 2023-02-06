FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7234

ENV ASPNETCORE_URLS=http://+:7234
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ConnectionStrings__DefaultConnection="Server=blooddonationdb;Database=BloodDonationDb;Uid=DiagnoseMe;Pwd=DiagnoseMe"

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BloodDonation/BloodDonation.Api/BloodDonation.Api.csproj", "BloodDonation/BloodDonation.Api/"]
RUN dotnet restore "BloodDonation/BloodDonation.Api/BloodDonation.Api.csproj"
COPY . .
WORKDIR "/src/BloodDonation/BloodDonation.Api"
RUN dotnet build "BloodDonation.Api.csproj" -c Release -o /app/build

FROM build AS final
WORKDIR /src/BloodDonation/BloodDonation.Api
COPY --from=publish /src/BloodDonation/BloodDonation.Api .
ENTRYPOINT ["dotnet", "run"]
