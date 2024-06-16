using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using Catalogo;
//Nose si había que agregar el dominio y el catalogo acá también, no creo que la directiva se use, pero a mi me da miedo ಥ‿ಥ

namespace winform
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormArticulos());
        }
    }
}
