using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GestionDeStock.Model
{
    public class Producto
    {
        public int id {  get; set; }
        
        public int precio { get; set; }

       public int categoria { get; set; }

        public DateTime fechaDeCarga { get; set; }

    }

}
