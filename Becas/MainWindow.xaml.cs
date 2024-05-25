using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Becas
{
    public partial class MainWindow : Window
    {
        //Cada instancia de Alumno se va a guardar en esta lista
        public List<Alumno> Alumnos { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Alumnos = new List<Alumno>();
            dgAlumnos.ItemsSource = Alumnos;
            // Aca vamos a mostrar como valor por defecto en beca, el valor correspondiente
            // a la beca que aparece por defecto en el comboBox
            txtMontoBeca.Text = "10000";

        }


        private void btn_AgregarAlumno_Click(object sender, RoutedEventArgs e)
        {
            // En esta función se va a intentar agregar un alumno con los datos cargados en los "Text Box"
            try
            {
                Alumno.AgregarAlumno(Alumnos, txtLegajo.Text, txtApellido.Text, txtNombre.Text, txtDNI.Text);
                // Limpia los campos para que deje lista la interfaz para agregar otro alumno
                LimpiarCamposAlumno(); 
                // Muestra la Grid view con los datos actualizados
                ActualizarDataGrid();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Funcion para mostrar el monto que corresponde al tipo de Beca
        private void cmbCodigoBeca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbCodigoBeca != null && txtMontoBeca != null && cmbCodigoBeca.SelectedItem is ComboBoxItem selectedItem)
            {
                // capturo el codigo de beca del combo box
                string codigoBeca = selectedItem.Content.ToString();
                // convierto el codigo de beca a decimal (la idea es por si llegara a ser necesario hacer algun calculo en algun momento)
                decimal montoBeca = Beca.ObtenerMontoBeca(codigoBeca);
                // En el campo Monto de Beca pongo el valor asociado al codigo de beca
                txtMontoBeca.Text = montoBeca.ToString();
            }
        }



        // Asignar beca a alumno
        private void btn_OtorgarBeca_Click(object sender, RoutedEventArgs e)
        {
            // intento agregar la beca, para ello se corrobora que el alumno este seleccionado en la grid
            // que haya algo seleccionado en el codigo de beca y que haya una fecha puesta
            // si todo esto se cumple se agrega en la grid el codigo de beca y la fecha correspondiente
            try
            {
                if (dgAlumnos.SelectedItem is Alumno selectedAlumno)
                {
                    if (cmbCodigoBeca.SelectedItem != null)
                    {
                        if (dpFechaBeca.SelectedDate.HasValue)
                        {
                            string codigoBeca = ((ComboBoxItem)cmbCodigoBeca.SelectedItem).Content.ToString();
                            decimal montoBeca = Beca.ObtenerMontoBeca(codigoBeca);
                            selectedAlumno.OtorgarBeca(codigoBeca, montoBeca, dpFechaBeca.SelectedDate.Value);
                            LimpiarCamposBeca();
                            ActualizarDataGrid();
                        }
                        else
                        {
                            MessageBox.Show("Por favor, selecciona una fecha de otorgamiento para la beca.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, selecciona un tipo de beca.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un alumno para otorgar la beca.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Funcion para limpiar los text box, para una mejor usabilidad 
        private void LimpiarCamposAlumno()
        {
            txtLegajo.Clear();
            txtApellido.Clear();
            txtNombre.Clear();
            txtDNI.Clear();
        }

        // Funcion para limpiar los cambos de beca, para una mejor usabilidad
        private void LimpiarCamposBeca()
        {
            cmbCodigoBeca.SelectedIndex = 0;
            dpFechaBeca.SelectedDate = DateTime.Today;
        }

        // Funcion para actuliazar los datos en el grid view
        private void ActualizarDataGrid()
        {
            dgAlumnos.ItemsSource = null;
            dgAlumnos.ItemsSource = Alumnos;
        }
    }
}
