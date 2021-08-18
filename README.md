# Api Docker
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

# Requisitos
Baixe e instale o Docker em: https://www.docker.com/products/docker-desktop
# Configuração
Execute o arquivo
```bash
MountDockerImages.bat
```
Aguarde a configuração dos serviços e navegue para http://localhost:5001/swagger/index.html
	
# Cadastrar usuário
```json
{
	"userName": "Usuario de acesso",
    	"fullName": "Nome completo",
    	"password": "Senha",
	"passwordConfirmation": "Confirmação de senha",
	"email": "email valido"
} 
```
# Requisitar token
/api/v1/users/signIn

```json
{
	"userName": "Usuario de acesso",
    	"password": "Senha"
} 
```

# Logando na Api
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

