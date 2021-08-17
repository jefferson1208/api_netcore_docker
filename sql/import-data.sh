#aguardando 30 segundos para aguardar o provisionamento e start do banco
sleep 30s
#rodar o comando para criar o banco
/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P "Teste@123" -i criacao_banco_docker.sql