using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pizzeria.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "DataSource=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddDbContext<PizzaDDDB>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddSqlite<PizzaDDDB>(connectionString);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Pizzeria",
        Description = "Faire les pizzas que vous aimez",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzeria API V1");
});

app.MapGet("/", () => "Diery DIA DIC3 Informatique 2025");

app.MapGet("/pizzas", async (PizzaDDDB db) => await db.Pizzas.ToListAsync());

app.MapPost("/pizza", async (PizzaDDDB db, PizzaDD pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

app.MapGet("/pizza/{id}", async (PizzaDDDB db, int id) => await db.Pizzas.FindAsync(id));

app.MapPut("/pizza/{id}", async (PizzaDDDB db, PizzaDD updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Nom = updatepizza.Nom;
    pizza.Description = updatepizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/pizza/{id}", async (PizzaDDDB db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) { return Results.NotFound(); }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();