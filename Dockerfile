# ---------- Build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia só os .csproj primeiro para aproveitar o cache do restore
COPY src/TodoApi.Domain/*.csproj         src/TodoApi.Domain/
COPY src/TodoApi.Application/*.csproj    src/TodoApi.Application/
COPY src/TodoApi.Infrastructure/*.csproj src/TodoApi.Infrastructure/
COPY src/TodoApi.Api/*.csproj            src/TodoApi.Api/
RUN dotnet restore src/TodoApi.Api/TodoApi.Api.csproj

# Copia o código e publica
COPY src/ src/
RUN dotnet publish src/TodoApi.Api/TodoApi.Api.csproj \
    -c Release -o /app/publish --no-restore

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__Default="Data Source=/app/data/todo.db"

# Pasta do SQLite com permissão para o usuário não-root da imagem
RUN mkdir -p /app/data && chown -R $APP_UID /app/data

COPY --from=build /app/publish .

USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoApi.Api.dll"]