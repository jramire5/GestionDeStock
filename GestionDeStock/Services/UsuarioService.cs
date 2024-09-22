using GestionDeStock.Model;
using GestionDeStock;
using GestionDeStock.Services;
using GestionDeStock.Tools;

namespace GestionDomain.Services
{
    public class UsuarioService : IServicesABM<Usuario>
    {
        public void Add(Usuario usuario)
        {
            Usuario usuarioToCreate = new Usuario();
            usuarioToCreate.usuario = usuario.usuario;
            usuarioToCreate.Id = usuario.Id;
            usuarioToCreate.contrasena = Encrypt.GetSHA256(usuario.contrasena);
           
            using var context = new GestionContext();
            context.Usuarios.Add(usuarioToCreate);
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

        public Usuario? Get(int id)
        {
            using var context = new GestionContext();
            return context.Usuarios.Find(id);
        }

        public IEnumerable<Usuario>? GetAll()
        {
            using var context = new GestionContext();

            return context.Usuarios.ToList();
        }

        public void Update(Usuario entidad)
        {
            using var context = new GestionContext();
            Usuario? usuarioToUpdate = context.Usuarios.Find(entidad.Id);
            if (usuarioToUpdate != null)
            {
                usuarioToUpdate.usuario = entidad.usuario;
                usuarioToUpdate.contrasena = Encrypt.GetSHA256(entidad.contrasena);
                context.SaveChanges();
            }
        }

        public Usuario? login(string username, string password)
        {
            int idADevolver = 0;

            using var context = new GestionContext();
            context.Usuarios.ToList().ForEach(usuario =>
            {
                if (usuario.usuario == username && Encrypt.GetSHA256(password) == usuario.contrasena)
                {
                    idADevolver = usuario.Id;
                    Console.WriteLine(usuario.Id.ToString());
                }

            });
            if (idADevolver == 0)
            { return null; }
            return context.Usuarios.Find(idADevolver);
        }
    }
}


