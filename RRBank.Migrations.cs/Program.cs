using RRBank.Migrations.cs;

class Program
{
    static void Main(string[] args)
    {
        // Criação de uma instância do DbContextFactory para obter o DataContext
        var factory = new AppDbContextFactory();
        var context = factory.CreateDbContext(args);

        // Lógica do programa (exemplo de uso do contexto)
        Console.WriteLine("Banco de dados configurado com sucesso!");

    }
}