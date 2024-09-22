using GestionDeStock;
using GestionDeStock.Model;
using GestionDeStock.Services;
using Microsoft.IdentityModel.Tokens;

namespace GestionDeStock.Service
{

public class ProductoService : IServicesABM<Producto>
{
    public void Add(Producto producto)
    {
        using var context = new GestionContext();

        context.Productos.Add(producto);
        context.SaveChanges();
    }




    public void Delete(int id)
    {
        using var context = new GestionContext();
        Producto? productoToDelete = context.Productos.Find(id);

        if (productoToDelete != null)
        {
            context.Productos.Remove(productoToDelete);
            context.SaveChanges();
        }
    }

    public Producto? Get(int id)
    {
        using var context = new GestionContext();

        return context.Productos.Find(id);
    }

    public IEnumerable<Producto> GetAll()
    {
        using var context = new GestionContext();

            return context.Productos.ToList();
    }

    public IEnumerable<Producto>? ProductosFiltrados(int presupuesto)
    {

        using var context = new GestionContext();
            List<Producto> ProductosLista = context.Productos.ToList();
            List<Producto> ProductosCategoria1 = new List<Producto>();
            List<Producto> ProductosCategoria2 = new List<Producto>();
            List<Producto> ProductosAEnviar = new List<Producto>();
            foreach (var item in ProductosLista)
            {
                if (item.precio < presupuesto) {
                    if (item.categoria == 1)
                    {
                        ProductosCategoria1.Add(item);
                    }
                    else if (item.categoria == 2)
                    {
                        ProductosCategoria2.Add(item);
                    }
                }
            }
            if (ProductosCategoria1.IsNullOrEmpty() || ProductosCategoria2.IsNullOrEmpty())
            {
                return null;
            }
            int precios = 0;
            foreach (var item in ProductosCategoria1)
            {
                foreach (var item2 in ProductosCategoria2)
                {
                    if (item.precio + item2.precio <= presupuesto && (item.precio + item2.precio) > precios)
                    {
                        
                        precios = item.precio + item2.precio;
                        ProductosAEnviar.Clear();
                        ProductosAEnviar.Add(item);
                        ProductosAEnviar.Add(item2);
                    }
                }
            }

                return ProductosAEnviar;

        }
    public void Update(Producto producto)
    {
        using var context = new GestionContext();
        Producto? productoToUpdate = context.Productos.Find(producto.id);

        if (productoToUpdate != null)
        {
            productoToUpdate.id = producto.id;
            productoToUpdate.precio = producto.precio;
            productoToUpdate.fechaDeCarga = producto.fechaDeCarga;
            productoToUpdate.categoria = producto.categoria;
            context.SaveChanges();
        }
    }

    public void Update(Usuario entidad)
        {
            throw new NotImplementedException();
        }
    }
}