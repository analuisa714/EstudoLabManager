namespace EstudoLabManager.Models; //nome do projeto + nome da pasta

class Computer
{
    public int Id { get; set; }
    public string Ram { get; set; }
    public string Processor { get; set; }

    public Computer(int id, string ram, string processor)
    {
        Id = id;
        Ram = ram;
        Processor = processor;
    }

    public Computer () {} //vazio pq o dapper precisa de um construtor assim
}