using Microsoft.Data.Sqlite;
using Dapper;
using EstudoLabManager.Models;
using EstudoLabManager.DataBase;

namespace EstudoLabManager.Repositories;

class LabRepository
{
    //criar um atributo com a configuração com o banco de dados

    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Lab> GetAllLabs()
    {
        //abre a conexão com using pq é dapper
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        //faz a query com dapper e armazena numa variável var
        var labs = connection.Query<Lab>("SELECT * FROM Labs").ToList();

        return labs;
    }

    public void Save(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Lab VALUES(@Id, @Number, @Name, @Block)", lab);
        connection.Close();
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        //executa na tabela a deleção do computador com o id tal
        connection.Execute("DELETE FROM Lab WHERE id = @Id", new{Id = id});
        connection.Close();
    }

    public Lab GetLabById(int id)
    {
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();

        //Query pra quando só retorna UM valor
        var computer = connection.QuerySingle<Lab>("SELECT * FROM Lab WHERE id = @Id", new {Id = id});

        connection.Close();
        return computer;
    }

    public Lab Update(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Lab SET number = @Number, name = @Name, block = @Block @WHERE id = @Id", lab);

        return GetLabById(lab.Id);
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection (_databaseConfig.ConnectionString);
        connection.Open();


        //usar ExecuteScalar pq é boolean
        var result = connection.ExecuteScalar<bool>("SELECT count id = @Id FROM Lab WHERE id = $Id", new {Id = id});

        return result;
    }
}