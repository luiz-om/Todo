using Todo.Data;

var builder = WebApplication.CreateBuilder(args);
//adicionar controllers ao projeto
builder.Services.AddControllers();
//Adicionar a classe de contexto permitindo o ef controlar a inicialização e finalização da conexão
builder.Services.AddDbContext<TodoContext>();

var app = builder.Build();

//realizar mapeamento das controllers
app.MapControllers();

app.Run();
