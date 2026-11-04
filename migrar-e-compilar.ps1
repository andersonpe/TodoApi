# Uso (na raiz da solução, onde está o TodoApi.sln/.slnx):
#   .\migrar-e-compilar.ps1
#   .\migrar-e-compilar.ps1 -Nome AdicionaCampoX

param(
    [string]$Nome = "CriacaoInicial"
)

# Executa sempre a partir da pasta onde o script está
Set-Location $PSScriptRoot

Write-Host "==> Criando migration '$Nome'..." -ForegroundColor Cyan
dotnet ef migrations add $Nome `
    -p src/TodoApi.Infrastructure `
    -s src/TodoApi.Api `
    -o Migrations

if ($LASTEXITCODE -ne 0) {
    Write-Host "Falha ao criar a migration. Build cancelado." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "==> Compilando a solução..." -ForegroundColor Cyan
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "Falha no build." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "Migration criada e solução compilada com sucesso." -ForegroundColor Green