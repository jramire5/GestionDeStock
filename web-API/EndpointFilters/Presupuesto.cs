

public class Presupuesto : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int presupuesto = context.GetArgument<int>(0);
        if (presupuesto < 1 || presupuesto > 1000000) {
            return Results.BadRequest("El presupuesto debe ser entre 1 y un millón (1.000.000)");
        }
        return await next(context);
    }
}
