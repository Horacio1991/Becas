using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Becas
{
    public class Alumno
    {
        // atributos de la clase Alumno
        public string Legajo { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string CodigoBeca { get; set; }
        public DateTime? FechaOtorgamiento { get; set; }

        // Constructor para la clase alumno, cada instancia se va a armar a partir de este constructor
        public Alumno(string legajo, string apellido, string nombre, string dni)
        {
            Legajo = legajo;
            Apellido = apellido;
            Nombre = nombre;
            DNI = dni;
            // Inicialmente sin beca
            CodigoBeca = string.Empty;  
            FechaOtorgamiento = null;
        }

        public void OtorgarBeca(string codigoBeca, decimal montoBeca, DateTime? fechaOtorgamiento)
        {
            CodigoBeca = codigoBeca;
            FechaOtorgamiento = fechaOtorgamiento;
        }

        // Metodo para agregar alumno al grid view
        // En la lista alumnos voy a agregar cada instancia de Alumno con su constructor
        public static void AgregarAlumno(List<Alumno> alumnos, string legajo, string apellido, string nombre, string dni)
        {
            // Validación de datos
            if (string.IsNullOrWhiteSpace(legajo) || !Regex.IsMatch(legajo, @"^\d+$"))
                throw new ArgumentException("Legajo debe ser un número y no estar vacío.");

            if (string.IsNullOrWhiteSpace(apellido) || !Regex.IsMatch(apellido, @"^[a-zA-Z]+$"))
                throw new ArgumentException("Apellido debe contener solo letras y no estar vacío.");

            if (string.IsNullOrWhiteSpace(nombre) || !Regex.IsMatch(nombre, @"^[a-zA-Z]+$"))
                throw new ArgumentException("Nombre debe contener solo letras y no estar vacío.");

            if (string.IsNullOrWhiteSpace(dni) || !Regex.IsMatch(dni, @"^\d+$"))
                throw new ArgumentException("DNI debe ser un número y no estar vacío.");
            // Guardo la instancia de alumno en una variable para despues agregarlo a la lista
            var alumno = new Alumno(legajo, apellido, nombre, dni);
            alumnos.Add(alumno);
        }
    }
}



