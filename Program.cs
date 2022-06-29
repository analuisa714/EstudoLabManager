using Microsoft.Data.Sqlite;
using EstudoLabManager.DataBase; //importar classes com using 
using EstudoLabManager.Repositories;
using EstudoLabManager.Models;

//program.cs não precisa de namespace pq eh o principal

var databaseConfig = new DatabaseConfig(); //criando os objetos database
var databaseSetup = new DatabaseSetup(databaseConfig); 
var computerRepository = new ComputerRepository(databaseConfig);
var labRepository = new LabRepository(databaseConfig);

var modelName = args[0]; //armazena se o comando é computer ou lab
var modelAction = args[1];

if (modelName == "Computer")
{
    if (modelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine(
                "{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor
            );
        }
    }

    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args [2]);
        var ram = (args [3]);
        var processor = (args [4]);
        var computer = new Computer (id, ram, processor);

        computerRepository.Save(computer);
    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32 (args [2]);


        if (computerRepository.ExistsById(id))
        {
            computerRepository.Delete (id);
        }
        else 
        {
            Console.WriteLine ("O computador com o ID {0} não existe.", id);
        }

    }

    if (modelAction == "Show")
    {
        var id = Convert.ToInt32 (args [2]);

        if (computerRepository.ExistsById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine ("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
        else 
        {
            Console.WriteLine ("O computador com o ID {0} não existe.", id);
        }
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32 (args [2]);

        if (computerRepository.ExistsById(id))
        {
            string ram = (args [3]);
            string processor = (args [4]);
            var computer =  new Computer (id, ram, processor);
            computerRepository.Update(computer);
        }
        else 
        {
            Console.WriteLine ("O computador com o ID {0} não existe.", id);
        }
    }
}

if (modelAction == "Lab")
{
    if (modelName == "List")
    {
        foreach (var lab in labRepository.GetAllLabs())
        {
            Console.WriteLine ("{0}, {1}, {2}, {3}", lab.Id, lab.Number, lab.Name, lab.Block);
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        Console.WriteLine("New Lab");
        Console.WriteLine("{0}, {1}, {2}, {3}", id, number, name, block);

        var lab = new Lab(id, number, name, block);
        labRepository.Save(lab);
    }

    if (modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        var lab = labRepository.GetLabById(id);
        Console.WriteLine("{0}, {1}, {2}, {3}", lab.Id, lab.Number, lab.Name, lab.Block);
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        var lab = new Lab(id, number, name, block);

        labRepository.Update(lab);
    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        labRepository.Delete(id);
    }
}