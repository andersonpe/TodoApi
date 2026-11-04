# Uso (na raiz da solução, onde está o TodoApi.sln/.slnx):
#   .\buildar-e-rodar.ps1
#   .\buildar-e-rodar.ps1 -Perfil https
#
# Para encerrar a API: Ctrl+C

param(
    [string]$Perfil = "http"   # "http" ou "https" (perfis do launchSettings.json)
)

# Executa sempre a partir da pasta onde o script está
Set-Location $PSScriptRoot

Write-Host "==> Compilando a solução..." -ForegroundColor Cyan
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "Falha no build. A API não será iniciada." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "==> Iniciando a API (perfil '$Perfil')... Ctrl+C para encerrar." -ForegroundColor Cyan
dotnet run --project src/TodoApi.Api --no-build --launch-profile $Perfil