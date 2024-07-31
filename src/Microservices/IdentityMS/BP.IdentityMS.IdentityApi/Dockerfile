FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Microservices/IdentityMS/BP.IdentityMS.IdentityApi/BP.IdentityMS.IdentityApi.csproj", "src/Microservices/IdentityMS/BP.IdentityMS.IdentityApi/"]
COPY ["src/Common/BP.Api.Common/BP.Api.Common.csproj", "src/Common/BP.Api.Common/"]
COPY ["src/Common/BP.Business.Common/BP.Business.Common.csproj", "src/Common/BP.Business.Common/"]
COPY ["src/Microservices/IdentityMS/BP.IdentityMS.Business/BP.IdentityMS.Business.csproj", "src/Microservices/IdentityMS/BP.IdentityMS.Business/"]
COPY ["src/Infrastructure/BP.AuthProvider/BP.AuthProvider.csproj", "src/Infrastructure/BP.AuthProvider/"]
COPY ["src/Utils/BP.Utils/BP.Utils.csproj", "src/Utils/BP.Utils/"]
COPY ["src/Microservices/IdentityMS/BP.IdentityMS.Data/BP.IdentityMS.Data.csproj", "src/Microservices/IdentityMS/BP.IdentityMS.Data/"]
RUN dotnet restore "./src/Microservices/IdentityMS/BP.IdentityMS.IdentityApi/./BP.IdentityMS.IdentityApi.csproj"
COPY . .
WORKDIR "/src/src/Microservices/IdentityMS/BP.IdentityMS.IdentityApi"
RUN dotnet build "./BP.IdentityMS.IdentityApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BP.IdentityMS.IdentityApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BP.IdentityMS.IdentityApi.dll"]