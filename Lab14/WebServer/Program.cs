using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.Results;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeDb>(opt => opt.UseInMemoryDatabase("EmployeeList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/employee", async (EmployeeDb db) => await db.Employee.ToListAsync());

app.MapGet("/employee/{id}", async (int id, EmployeeDb db) =>
{
    return await db.Employee.FindAsync(id) is Employee todo ? Ok(todo) : NotFound();
});

app.MapPost("/employee/{id}", async (int id, [FromBody] Employee employee, EmployeeDb db) =>
{
    employee.Id = id;
    await db.Employee.AddAsync(employee);
    await db.SaveChangesAsync();

    return Created($"/employee/{id}", employee);
});

app.MapPut("/employee/{id}", async (int id, [FromBody] Employee inputEmployee, EmployeeDb db) =>
{
    var employee = await db.Employee.FindAsync(id);
    if (employee is null)
        return NotFound();

    employee.Name = inputEmployee.Name;
    employee.Age = inputEmployee.Age;
    employee.Position = inputEmployee.Position;
    await db.SaveChangesAsync();
    return NoContent();
});

app.MapDelete("/employee/{id}", async (int id, EmployeeDb db) =>
{
    if (await db.Employee.FindAsync(id) is Employee employee)
    {
        db.Employee.Remove(employee);
        await db.SaveChangesAsync();
        return Ok(employee);
    }

    return NotFound();
});

app.Run();


public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Position { get; set; }
};

public class EmployeeDb : DbContext
{
    public EmployeeDb(DbContextOptions<EmployeeDb> options) : base(options) { }

    public DbSet<Employee> Employee => Set<Employee>();
}