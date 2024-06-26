using TodoApi.Controllers;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Repository.Implementations;
using NuGet.Protocol.Core.Types;

namespace TodoApi.Tests;

#nullable disable
public class TodoListsControllerTests
{
    private DbContextOptions<TodoContext> DatabaseContextOptions()
    {
        return new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    private void PopulateDatabaseContext(TodoContext context)
    {
        context.TodoList.Add(new Models.TodoList { Id = 1, Name = "Task 1" });
        context.TodoList.Add(new Models.TodoList { Id = 2, Name = "Task 2" });
        context.SaveChanges();
    }

    [Fact]
    public async Task GetTodoList_WhenCalled_ReturnsTodoListList()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        using (var repository = new TodoListRepository(context))
        {
            PopulateDatabaseContext(context);

            var controller = new TodoListsController(repository);

            var result = await controller.GetTodoLists();

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(
              2,
              ((result.Result as OkObjectResult).Value as IList<TodoList>).Count
            );
        }
    }

    [Fact]
    public async Task GetTodoList_WhenCalled_ReturnsTodoListById()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        using (var repository = new TodoListRepository(context))
        {
            PopulateDatabaseContext(context);

            var controller = new TodoListsController(repository);

            var result = await controller.GetTodoList(1);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(
              1,
              ((result.Result as OkObjectResult).Value as TodoList).Id
            );
        }
    }


    [Fact]
    public async Task PutTodoList_WhenTodoListIdDoesntMatch_ReturnsBadRequest()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        using (var repository = new TodoListRepository(context))
        {
            PopulateDatabaseContext(context);

            var controller = new TodoListsController(repository);

            var result = await controller.PutTodoList(1, new TodoList{ Id = 2 });

            Assert.IsType<BadRequestResult>(result);
        }
    }

    [Fact]
    public async Task PutTodoList_WhenTodoListDoesntExist_ReturnsBadRequest()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        using (var repository = new TodoListRepository(context))
        {
            PopulateDatabaseContext(context);

            var controller = new TodoListsController(repository);

            var result = await controller.PutTodoList(3, new TodoList { Id = 3 });

            Assert.IsType<NotFoundResult>(result);
        }
    }

    [Fact]
    public async Task PutTodoList_WhenCalled_UpdatesTheTodoList()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        using (var repository = new TodoListRepository(context))
        {
            PopulateDatabaseContext(context);

            var controller = new TodoListsController(repository);

            var todoList = await repository.Get(2);
            var result = await controller.PutTodoList(todoList.Id, todoList);

            Assert.IsType<NoContentResult>(result);
        }
    }

    [Fact]
    public async Task PostTodoList_WhenCalled_CreatesTodoList()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        using (var repository = new TodoListRepository(context))
        {
            PopulateDatabaseContext(context);

            var controller = new TodoListsController(repository);

            var todoList = new TodoList { Name = "Task 3" };
            var result = await controller.PostTodoList(todoList);

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(
              3,
              repository.GetAll().Result.Count()
            );
        }
    }

    [Fact]
    public async Task DeleteTodoList_WhenCalled_RemovesTodoList()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        using (var repository = new TodoListRepository(context))
        {
            PopulateDatabaseContext(context);

            var controller = new TodoListsController(repository);

            var result = await controller.DeleteTodoList(2);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal(
              1,
              repository.GetAll().Result.Count()
            );
        }
    }
}