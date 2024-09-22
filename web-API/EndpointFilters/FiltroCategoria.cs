
using GestionDeStock.Model;

public class FiltroCategoria : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Producto producto = context.GetArgument<Producto>(0);
        if (producto.categoria != 1 && producto.categoria != 2)
        {
            return Results.BadRequest("La categoria debe ser 1 o 2");
        }
        return await next(context);
    }
}
