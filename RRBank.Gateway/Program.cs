using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); // Cria e configura uma nova instância do builder para o aplicativo, usando os argumentos de linha de comando.

var config = builder.Configuration; // Obtém a instância da configuração do aplicativo.
config.AddJsonFile("appsettings.json", true, true) // Adiciona o arquivo "appsettings.json" à configuração, permitindo o carregamento de configurações padrão.
      .AddJsonFile("ocelot.json"); // Adiciona o arquivo "ocelot.json" à configuração, onde estão definidas as rotas e configurações do Ocelot.

var sv = builder.Services; // Obtém a coleção de serviços onde as dependências são registradas.

// Configura o SwaggerForOcelot primeiro
sv.AddSwaggerForOcelot(config); // Adiciona e configura o SwaggerForOcelot, que integra o Swagger ao Ocelot, usando as configurações definidas.

// Configura o Ocelot depois
sv.AddOcelot(config); // Adiciona e configura o Ocelot no pipeline de serviços, usando as configurações definidas.

sv.AddControllers(); // Adiciona suporte para controladores MVC ao aplicativo.

var app = builder.Build(); // Constrói a aplicação a partir da configuração feita anteriormente.

app.UseStaticFiles(); // Habilita o suporte a arquivos estáticos (como HTML, CSS, JS) na aplicação.
app.UseSwaggerForOcelotUI(); // Configura o UI do Swagger para funcionar com o Ocelot, permitindo a visualização da documentação das APIs.

app.UseRouting(); // Habilita o roteamento no pipeline de requisições, determinando como as requisições são direcionadas para os endpoints.

app.UseOcelot().Wait(); // Adiciona o middleware do Ocelot ao pipeline e espera a conclusão da tarefa assíncrona. Ele gerencia o roteamento de requisições para outras APIs.

app.Run(); // Inicia o aplicativo, permitindo que ele comece a processar requisições.
