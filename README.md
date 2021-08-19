# Api Docker
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Requisitos
Baixe e instale o Docker em: https://www.docker.com/products/docker-desktop
## Configuração
Execute o arquivo
```bash
MountDockerImages.bat
```
Aguarde a configuração dos serviços e navegue para http://localhost:5001/swagger/index.html
	
## Cadastrar usuário
```json
{
	"userName": "Usuario de acesso",
    	"fullName": "Nome completo",
    	"password": "Senha",
	"passwordConfirmation": "Confirmação de senha",
	"email": "email valido"
} 
```
## Requisitar token
/api/v1/users/signIn

```json
{
	"userName": "Usuario de acesso",
    	"password": "Senha"
} 
```

## Logando na Api
Copie o token gerado
```json
{
	"success": true,
    	"data": "tokenGerado"
} 
```
Cole o Token na Seção "Authorize" no cabeçalho da api da seguinte forma:
```bash
Bearer <tokenGerado>
```
## Tecnologias
<div style="display: inline_block"><br>
  <img align="center" alt="Jeferson-Netcore" height="30" width="40" src="https://github.com/devicons/devicon/blob/master/icons/dotnetcore/dotnetcore-original.svg">
  <img align="center" alt="Jeferson-Csharp" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg">
 <img align="center" alt="Jeferson-Docker" height="30" width="40" src="https://github.com/devicons/devicon/blob/master/icons/docker/docker-original.svg">
 <img align="center" alt="Jeferson-sqlserver" height="30" width="40" src="https://github.com/devicons/devicon/blob/master/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg">
</div>
