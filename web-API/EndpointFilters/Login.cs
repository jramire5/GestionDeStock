

using GestionDeStock.Model;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

public class Login : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        
        var resultado = await next(context);
        if (resultado == null) {
            return Results.NotFound("Usuario o Contraseña inexistente");
        }
        return await next(context);
    }
}
