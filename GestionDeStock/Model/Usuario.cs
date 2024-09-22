using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionDeStock.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string usuario { get; set; }

        [PasswordPropertyText(true)]
        public string contrasena { get; set; }
    }
}
