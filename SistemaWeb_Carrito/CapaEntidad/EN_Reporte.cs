using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_Reporte
    {
        public string FechaRegistro { get; set; }

        public string Libro { get; set; }

        public string Autor { get; set; }

        public string Categoria { get; set; }

        public decimal Paginas { get; set; }

        public int Stock { get; set; }

        public int IdLibro { get; set; }
    }
}
