using System.Data;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Model;

namespace Todo.Controllers
{
    [ApiController]

    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/todos")]
        public IActionResult Get([FromServices] TodoContext context)
        => Ok(context.Todos.ToList());



        [HttpGet]
        [Route("/buscaid/{id:int}")]
        public IActionResult BuscaId([FromRoute] int id,
        [FromServices] TodoContext context)
        {
            var tarefa = context.Todos.FirstOrDefault(x => x.Id == id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);


        }
        [HttpGet("/titulo/{tarefa}")]
        public List<TodoModel> NomeTarefa(
            [FromRoute] string tarefa,
            [FromServices] TodoContext context)
        {
            if (tarefa is null)
            {
                throw new ArgumentNullException(nameof(tarefa));
            }

            return context.Todos.Where(x => x.Title.Contains(tarefa)).ToList();
        }

        [HttpPost("/add")]
        public IActionResult addTodo([FromBody] TodoModel todos, [FromServices] TodoContext context)
        {
            //var todo = new TodoModel { CreatedAt = DateTime.Now, Done = todos.Done, Title = todos.Title };

            context.Todos.Add(todos);
            context.SaveChanges();
            return Created($"/add/{todos.Id}",todos);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Editar([FromRoute] int id,
         [FromBody] TodoModel model,
          [FromServices] TodoContext context)
        {
            var tarefa = context.Todos.FirstOrDefault(x => x.Id == id);
            if (tarefa.Id == null)
            {
                return NotFound();
            }
            tarefa.Title = model.Title;
            tarefa.Done = model.Done;


            context.Todos.Update(tarefa);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("/deletar/{id:int}")]
        public IActionResult Deletar([FromRoute] int id, [FromServices] TodoContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
if (model ==null)
return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}