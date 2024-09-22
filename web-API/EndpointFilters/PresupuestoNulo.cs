

public class PresupuestoNulo : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var resultado = await next(context);
        if (resultado == null) {
            return Results.NotFound("No hay 2 productos de distintas categorias cuya suma sea menor al presupuesto");
        }
        return await next(context);
    }
}
