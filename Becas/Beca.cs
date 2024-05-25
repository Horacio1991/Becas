using System;

namespace Becas
{
    public class Beca
    {
        //Parametros de la beca
        public string Codigo { get; set; }
        public decimal Monto { get; set; }

        // Constructor 
        public Beca(string codigo, decimal monto)
        {
            Codigo = codigo;
            Monto = monto;
        }

        // Metodo para asignar una valor a cada codigo de beca
        public static decimal ObtenerMontoBeca(string codigoBeca)
        {
            switch (codigoBeca)
            {
                case "Beca A":
                    return 10000; 
                case "Beca B":
                    return 20000; 
                case "Beca C":
                    return 30000; 
                default:
                    throw new ArgumentException("Código de Beca inválido.");
            }
        }
    }
}
