using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using c_minial_api.Models;


var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("Shop") ?? "Data Source=Shop.db";
// builder.Services.AddSqlite<ShopDb>(connectionString);

var connectionStringPostgres = builder.Configuration.GetConnectionString("shop");
builder.Services.AddDbContext<ShopDb>(options =>  options.UseNpgsql("Host=localhost:5432;Database=shop;Username=postgres;Password=123456"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "UserStore API",
         Description = "Making the Users you love",
         Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserStore API V1");
});

var userController = new UserController();

app.MapGet("/", () => "User");

app.MapGet("/u", async (ShopDb db) => await db.Users.ToListAsync());

app.MapPost("/u", async (ShopDb db, User user) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Created($"/user/{user.Id}", user);
});
app.MapGet("/u/{id}", async (ShopDb db, int id) => await db.Users.FindAsync(id));

app.MapPut("/u/{id}", async (ShopDb db, User updateuser, int id) => {
    var user = await db.Users.FindAsync(id);

    if (user is null) return Results.NotFound();
    user.name = updateuser.name;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/u/{id}", async (ShopDb db,  int id) => {
    var user = await db.Users.FindAsync(id);

    if (user is null) return Results.NotFound();
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/user", () =>
{
    return userController.getuser();
}
);


app.Run();

// record Test(string Name);
