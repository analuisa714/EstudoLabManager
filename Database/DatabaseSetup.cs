using Microsoft.Data.Sqlite;

namespace EstudoLabManager.DataBase;
 
 class DatabaseSetup
 {
    private readonly DatabaseConfig _databaseConfig; //readonly não pode setar fora do atributo e do próprio construtor.

    public DatabaseSetup(DatabaseConfig databaseConfig) //database sem underline pq aq eh variavel nova pro ctor
    {
        _databaseConfig = databaseConfig;
        CreateComputerTable(); //a tabela é criada no database setup
    }

    private void CreateComputerTable()
    {
        var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Computers (
                    id int null primary key,
                    ram varchar (100) not null,
                    processor varchar (100) not null
                );
        ";

        command.ExecuteNonQuery(); //pq não é uma consulta. (não exibe nada)
        connection.Close();
    }

    private void CreateLabTable()
    {
        var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Labs (
            id int null primary key,
            ram varchar (100) not null,
            processor varchar (100) not null
            );
            ";

            command.ExecuteNonQuery();
            connection.Close();
    }

    private void CreateCadernosTable() //método privado pq o usuário não precisa acessar, tipo de retorno, nome do método e sem parâmetros pq essa tabela só está sendo criada, os dados serão adicionados depois
    {
        var connection = new SqliteConnection (_databaseConfig.ConnectionString);//criar conexão com o banco de dados já com a configuração
        connection.Open();

        var command = connection.CreateCommand(); //criando uma váriavel command com poder, a partir da connection, de realizar comandos


        //fazer um comando de texto
        command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Cadernos
        (id int not null,
        qtidade_folhas int null,
        marca varchar (100) null);
        ";
    }
 }