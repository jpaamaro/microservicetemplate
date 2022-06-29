# banco de dados da aplicação deve estar em execução:
docker run --name postgresql -e POSTGRES_USER=myusername -e POSTGRES_PASSWORD=mypassword -e POSTGRES_DB=Atlasdb -p 5432:5432 -d postgres


# banco de dados para os testes integrados:
docker run --name postgresql-test -e POSTGRES_USER=myusername -e POSTGRES_PASSWORD=mypassword -e POSTGRES_DB=Atlasdb-test -p 5433:5432 -d postgres

