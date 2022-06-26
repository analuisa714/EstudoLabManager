using EstudoLabManager.DataBase;
using EstudoLabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace EstudoLabManager.Repositories;

class ComputerRepository
{ 
    private readonly DatabaseConfig _databaseConfig; //atributo

    //tem que receber qual é a configuração do banco de dados
    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public IEnumerable<Computer> GetAll() //devolver lista de computadores
    {
        var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        var computers = connection.Query<Computer> ("SELECT * FROM Computers");
        connection.Close();

        return computers;
    }

    public void Save (Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString); //pq alguns usa using var connection e outros não?
        connection.Open();

        connection.Execute("INSERT INTO Computers VALUES (@Id, @Ram, @Processor)", computer);

        connection.Close();
    }

    public void Delete (int id)
    {
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id == @Id", new {Id = id}); //pra que esse {}?

        connection.Close();
    }

    public void Update (Computer computer)
    {
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);   
        connection.Open();

        connection.Execute ("UPDATE Computers SET ram = @Ram, processor = @Processor WHERE id == @Id", computer);

        connection.Close();
    }

    public Computer GetById(int id)
    {
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        var computer = connection.QuerySingle<Computer> ("SELECT * FROM Computers WHERE id == @Id", new {Id = id});

        connection.Close();
        return computer;
    }

    public bool ExistsById (int id) //verifica se um computer existe usando o id
    {
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        bool result = connection.ExecuteScalar<bool> ("SELECT count(id) FROM Computers WHERE id == @Id", new {Id = id});

        connection.Close();
        return result;
    }
}