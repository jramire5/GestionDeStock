using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Esta interfaz es para mantener un mismo criterio en todos los ABM,
// luego si se necesitan más métodos, se agregarán dependiendo el caso
namespace GestionDeStock.Services
{
    public interface IServicesABM<T>
    {
        void Add(T entidad);
        T? Get(int id);
        IEnumerable<T>? GetAll();
        void Update(T entidad);
        void Delete(int id);
    }

}
