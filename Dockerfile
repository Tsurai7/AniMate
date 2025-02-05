FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

WORKDIR /app

COPY ["AniMate.Backend/Backend.API/Backend.API.csproj", "Backend.API/"]
COPY ["AniMate.Backend/Backend.Application/Backend.Application.csproj", "Backend.Application/"]
COPY ["AniMate.Backend/Backend.Domain/Backend.Domain.csproj", "Backend.Domain/"]
COPY ["AniMate.Backend/Backend.Infrastructure/Backend.Infrastructure.csproj", "Backend.Infrastructure/"]

RUN dotnet restore "Backend.API/Backend.API.csproj"
COPY AniMate.Backend/ .
RUN dotnet publish "Backend.API/Backend.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final

WORKDIR /app
COPY --from=build /app/publish .
COPY AniMate.Backend/Backend.API/.env .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Backend.API.dll"]