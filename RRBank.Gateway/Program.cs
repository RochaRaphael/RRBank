using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); // Cria e configura uma nova inst�ncia do builder para o aplicativo, usando os argumentos de linha de comando.

var config = builder.Configuration; // Obt�m a inst�ncia da configura��o do aplicativo.
config.AddJsonFile("appsettings.json", true, true) // Adiciona o arquivo "appsettings.json" � configura��o, permitindo o carregamento de configura��es padr�o.
      .AddJsonFile("ocelot.json"); // Adiciona o arquivo "ocelot.json" � configura��o, onde est�o definidas as rotas e configura��es do Ocelot.

var sv = builder.Services; // Obt�m a cole��o de servi�os onde as depend�ncias s�o registradas.

// Configura o SwaggerForOcelot primeiro
sv.AddSwaggerForOcelot(config); // Adiciona e configura o SwaggerForOcelot, que integra o Swagger ao Ocelot, usando as configura��es definidas.

// Configura o Ocelot depois
sv.AddOcelot(config); // Adiciona e configura o Ocelot no pipeline de servi�os, usando as configura��es definidas.

sv.AddControllers(); // Adiciona suporte para controladores MVC ao aplicativo.

var app = builder.Build(); // Constr�i a aplica��o a partir da configura��o feita anteriormente.

app.UseStaticFiles(); // Habilita o suporte a arquivos est�ticos (como HTML, CSS, JS) na aplica��o.
app.UseSwaggerForOcelotUI(); // Configura o UI do Swagger para funcionar com o Ocelot, permitindo a visualiza��o da documenta��o das APIs.

app.UseRouting(); // Habilita o roteamento no pipeline de requisi��es, determinando como as requisi��es s�o direcionadas para os endpoints.

app.UseOcelot().Wait(); // Adiciona o middleware do Ocelot ao pipeline e espera a conclus�o da tarefa ass�ncrona. Ele gerencia o roteamento de requisi��es para outras APIs.

app.Run(); // Inicia o aplicativo, permitindo que ele comece a processar requisi��es.
