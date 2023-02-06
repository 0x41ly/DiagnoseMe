FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7131

ENV ASPNETCORE_URLS=http://+:7131
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ConnectionStrings__DefaultConnection="Server=medicalblogdb;Database=MedicalBlogDb;Uid=DiagnoseMe;Pwd=DiagnoseMe"

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MedicalBlog/MedicalBlog.Api/MedicalBlog.Api.csproj", "MedicalBlog/MedicalBlog.Api/"]
RUN dotnet restore "MedicalBlog/MedicalBlog.Api/MedicalBlog.Api.csproj"
COPY . .
WORKDIR "/src/MedicalBlog/MedicalBlog.Api"
RUN dotnet build "MedicalBlog.Api.csproj" -c Release -o /app/build

FROM build AS final
WORKDIR /src/MedicalBlog/MedicalBlog.Api
COPY --from=build /src/MedicalBlog/MedicalBlog.Api .
ENTRYPOINT ["dotnet", "run"]
