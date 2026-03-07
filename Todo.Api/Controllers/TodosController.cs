using System.Collections.Concurrent;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Models;
using System.Text.Json;

namespace Todo.Api.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    // email -> (id -> todo)
    private static readonly ConcurrentDictionary<string, ConcurrentDictionary<int, TodoItem>> Store = new();
    private static int _nextId = 0;

    private readonly ILogger<TodosController> _logger;

    public TodosController(ILogger<TodosController> logger)
    {
        _logger = logger;
    }

    [HttpGet("get-all-external")]
    public IActionResult Get()
    {
        return Ok(Store);
    }


    private string GetUserEmail()
    {
        return "abc@gmail.com";
        if (!Request.Cookies.TryGetValue("TODO_custom_cookies", out var raw))
            throw new UnauthorizedAccessException("Auth cookie missing");

        var ctx = JsonSerializer.Deserialize<AppContextCookie>(raw);

        if (string.IsNullOrWhiteSpace(ctx?.Email))
            throw new UnauthorizedAccessException("Email missing in cookie");

        return ctx.Email;
    }


    private ConcurrentDictionary<int, TodoItem> GetUserStore()
    {
        var email = GetUserEmail();
        if (string.IsNullOrWhiteSpace(email))
            throw new UnauthorizedAccessException("Email cookie is missing");

        return Store.GetOrAdd(email, _ => new ConcurrentDictionary<int, TodoItem>());
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var userStore = GetUserStore();
        return Ok(userStore.Values.OrderBy(x => x.Id));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var userStore = GetUserStore();
        return userStore.TryGetValue(id, out var item) ? Ok(item) : NotFound();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteById(int id)
    {
        var userStore = GetUserStore();
        return userStore.TryRemove(id, out _)
            ? Ok(new { status = "Success", value = "No worry" })
            : NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTodoRequest req)
    {
        if (req is null || string.IsNullOrWhiteSpace(req.Title))
            return BadRequest(new { message = "Title is required" });

        var userStore = GetUserStore();
        var id = Interlocked.Increment(ref _nextId);

        var item = new TodoItem
        {
            Id = id,
            Title = req.Title.Trim(),
            IsDone = false,
            CreatedAt = DateTimeOffset.UtcNow
        };

        userStore[id] = item;
        return Created($"/api/todos/{item.Id}", item);
    }
}
