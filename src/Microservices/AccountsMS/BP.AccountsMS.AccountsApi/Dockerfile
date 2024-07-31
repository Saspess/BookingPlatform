FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Microservices/AccountsMS/BP.AccountsMS.AccountsApi/BP.AccountsMS.AccountsApi.csproj", "src/Microservices/AccountsMS/BP.AccountsMS.AccountsApi/"]
COPY ["src/Common/BP.Api.Common/BP.Api.Common.csproj", "src/Common/BP.Api.Common/"]
COPY ["src/Common/BP.Business.Common/BP.Business.Common.csproj", "src/Common/BP.Business.Common/"]
COPY ["src/Microservices/AccountsMS/BP.AccountsMS.Business/BP.AccountsMS.Business.csproj", "src/Microservices/AccountsMS/BP.AccountsMS.Business/"]
COPY ["src/Infrastructure/BP.AuthProvider/BP.AuthProvider.csproj", "src/Infrastructure/BP.AuthProvider/"]
COPY ["src/Infrastructure/BP.EmailSender/BP.EmailSender.csproj", "src/Infrastructure/BP.EmailSender/"]
COPY ["src/Utils/BP.Utils/BP.Utils.csproj", "src/Utils/BP.Utils/"]
COPY ["src/Microservices/AccountsMS/BP.AccountsMS.Data/BP.AccountsMS.Data.csproj", "src/Microservices/AccountsMS/BP.AccountsMS.Data/"]
RUN dotnet restore "./src/Microservices/AccountsMS/BP.AccountsMS.AccountsApi/./BP.AccountsMS.AccountsApi.csproj"
COPY . .
WORKDIR "/src/src/Microservices/AccountsMS/BP.AccountsMS.AccountsApi"
RUN dotnet build "./BP.AccountsMS.AccountsApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BP.AccountsMS.AccountsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BP.AccountsMS.AccountsApi.dll"]