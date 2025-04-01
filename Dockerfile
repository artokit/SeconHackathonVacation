FROM mcr.microsoft.com/dotnet/sdk:8.0.407-bullseye-slim AS build
WORKDIR /src

COPY ["Api/Api.csproj", "Api/"]

RUN dotnet restore "Api/Api.csproj" --runtime linux-x64

COPY . .

WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.407-bullseye-slim
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]