# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar o arquivo de projeto e restaurar dependências
COPY ["Flight-Alert-API.csproj", "./"]
RUN dotnet restore "Flight-Alert-API.csproj"

# Copiar todo o código e compilar
COPY . .
RUN dotnet build "Flight-Alert-API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "Flight-Alert-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copiar os arquivos publicados
COPY --from=publish /app/publish .

# Criar diretório para logs
RUN mkdir -p /app/Logs

ENTRYPOINT ["dotnet", "Flight-Alert-API.dll"]
