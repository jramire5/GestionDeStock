
using GestionDeStock.Model;
using GestionDeStock.Service;
using GestionDomain.Services;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string key = "cncaskjnksoncasklnncalktnelkrnklaatlkanklme";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signingKey,
    };
});
builder.Services.AddHttpLogging(o => { });

var app = builder.Build();
await SeedData();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseHttpLogging();
}

app.UseAuthorization();


// # PRODUCTOS

app.MapPost("/productos", (Producto producto) =>
{
    ProductoService productoService = new ProductoService();

    productoService.Add(producto);
    return Task.CompletedTask;
})
.WithName("AddProducto").AddEndpointFilter<FiltroCategoria>();

app.MapGet("/productos", () =>
{
    ProductoService productoService = new ProductoService();

    return productoService.GetAll();
})
.WithName("GetAllProductos").RequireAuthorization()
.WithOpenApi();



app.MapPut("/productos", (Producto producto) =>
{
    ProductoService productoService = new ProductoService();

    productoService.Update(producto);
})
.WithName("UpdateProducto")
.WithOpenApi().RequireAuthorization().AddEndpointFilter<FiltroCategoria>();

app.MapDelete("/productos/{id}", (int id) =>
{
    ProductoService productoService = new ProductoService();

    productoService.Delete(id);

})
.WithName("DeleteProducto").RequireAuthorization()
.WithOpenApi();

app.MapGet("ProductosFiltrados/{presupuesto}", (int presupuesto) =>
    {
        ProductoService productoService = new ProductoService();

        var response =  productoService.ProductosFiltrados(presupuesto);


        return response;
    })
    .WithName("Presupuesto").WithOpenApi().RequireAuthorization().AddEndpointFilter<Presupuesto>().AddEndpointFilter<PresupuestoNulo>().RequireAuthorization();


// # Usuarios / Login

app.MapPost("auth/login", (Usuario usuario) =>
{
   UsuarioService usuarioService = new UsuarioService();
  
    Usuario? usuarioAuth = usuarioService.login(usuario.usuario, usuario.contrasena);

    if (usuarioAuth != null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes(key);
        var tokenDes = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuarioAuth.usuario),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDes);
        return tokenHandler.WriteToken(token);
    }
    else
    {
        return "El usuario no ha sido encontrado";
    }
}).WithName("login").WithOpenApi().AddEndpointFilter<Login>();


//Esto es simplemente para probar un usuario sin la necesidad de crearlo. 
async Task SeedData()
{
    UsuarioService usuarioService = new UsuarioService();
    if (!usuarioService.GetAll().Any())
    {
        var nuevoUsuario = new Usuario
        {

            usuario = "admin",
            contrasena = "Admin123456",

        };
        usuarioService.Add(nuevoUsuario);
    }
    ProductoService productoService = new ProductoService();
    if (!productoService.GetAll().Any())
    {
        DateTime fecha = new DateTime(2024,09,19);
        var categoria = 1;
        var precio = 500;
        for (global::System.Int32 i = 0; i < 10; i++)
        {
            var producto = new Producto
            {

                precio = precio,
                categoria = categoria,
                fechaDeCarga = fecha

            };
            if (categoria == 1)
            {
                categoria = 2;
            }
            else
            {
                categoria = 1;
            }
            precio = precio + 100;
            fecha = fecha.AddDays(1);
            productoService.Add(producto);
        }

    }

}

app.Run();

