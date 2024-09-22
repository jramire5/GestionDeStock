using GestionDeStock.Model;


public class FiltroPrecio : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Producto producto = context.GetArgument<Producto>(0);
        if (producto.precio < 1 || producto.precio > 1000000)
        {
            return Results.BadRequest("El precio debe ser entre 1 y un millón (1.000.000)");
        }
        return await next(context);
    }
}

