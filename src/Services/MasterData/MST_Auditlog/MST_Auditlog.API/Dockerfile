#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src 
COPY ["src/Services/MasterData/MST_Auditlog/MST_Auditlog.API/MST_Auditlog.API.csproj", "src/Services/MasterData/MST_Auditlog/MST_Auditlog.API/"]
COPY ["src/Services/Base.DTOs/Base.DTOs.csproj", "src/Services/Base.DTOs/"]
COPY ["lib/External.WebHttpClient/External.WebHttpClient.csproj", "lib/External.WebHttpClient/"]
COPY ["lib/Common/Common.csproj", "lib/Common/"]
COPY ["lib/Auth/Auth.csproj", "lib/Auth/"]
COPY ["lib/CustomAutoFixture/CustomAutoFixture.csproj", "lib/CustomAutoFixture/"]
COPY ["lib/DateTimeExtensions/DateTimeExtensions.csproj", "lib/DateTimeExtensions/"]
COPY ["lib/ErrorHandling/ErrorHandling.csproj", "lib/ErrorHandling/"]
COPY ["lib/ExcelExtension/ExcelExtension.csproj", "lib/ExcelExtension/"]
COPY ["lib/External.Kafka/External.Kafka.csproj", "lib/External.Kafka/"]
COPY ["lib/External.WebHttpClient/External.WebHttpClient.csproj", "lib/External.WebHttpClient/"]
COPY ["lib/FileStorage/FileStorage.csproj", "lib/FileStorage/"]
COPY ["lib/PagingExtensions/PagingExtensions.csproj", "lib/PagingExtensions/"]
COPY ["lib/RequestLogging/RequestLogging.csproj", "lib/RequestLogging/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
COPY ["src/Database/Database.AuditLogs/Database.AuditLogs.csproj", "src/Database/Database.AuditLogs/"]
WORKDIR /src/src/Services/MasterData/MST_Auditlog
RUN dotnet restore "MST_Auditlog.API/MST_Auditlog.API.csproj"

WORKDIR /src
COPY . .
WORKDIR "/src/src/Services/MasterData/MST_Auditlog/MST_Auditlog.API"
RUN dotnet build "MST_Auditlog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MST_Auditlog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MST_Auditlog.API.dll"]

ENV TZ=Asia/Bangkok
#RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone