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
 }