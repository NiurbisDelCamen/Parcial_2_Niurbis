using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Parcial2.BLL;
using Parcial2.Entidades;

namespace Parcial2.UI
{
    /// <summary>
    /// Interaction logic for rParcial2.xaml
    /// </summary>
    public partial class rParcial2 : Window
    {
        LLamada llamada = new LLamada();
        public List<LlamadaDetalle> detalles { get; set; }
        public rParcial2()
        {
            InitializeComponent();
            this.DataContext = llamada;
            this.detalles = new List<LlamadaDetalle>();
            LlamadaIdTextBox.Text = "0";

        }

        private void Limpiar()
        {
            LlamadaIdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            LlamadaDataGrid.ItemsSource = new List<LlamadaDetalle>();

        }

        private void CargaGrid()
        {
            LlamadaDataGrid.ItemsSource = null;
            LlamadaDataGrid.ItemsSource = this.detalles;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool Validar()
        {
            bool paso = true;
            if (string.IsNullOrWhiteSpace(LlamadaIdTextBox.Text))
                paso = false;
            else
            {
                try
                {
                    int i = Convert.ToInt32(LlamadaIdTextBox.Text);
                }
                catch (FormatException)
                {
                    paso = false;
                }
                if (string.IsNullOrWhiteSpace(DescripcionTextBox.Text))
                    paso = false;
                else
                {
                    foreach (var caracter in DescripcionTextBox.Text)
                    {
                        if (!char.IsLetter(caracter) && !char.IsWhiteSpace(caracter))
                            paso = false;
                    }
                }
                if (LlamadaDataGrid.Columns.Count == 0)
                {
                    MessageBox.Show("Todos Los campos deben estar llenos y debe agregarlos....!!!!!");
                    LlamadaDataGrid.Focus();
                    paso = false;
                }
                if (paso == false)
                {
                    MessageBox.Show("Los Datos son invalidos");

                }
            }
            return paso;
        }

        private bool ExisteBD()
        {
            LLamada LlamadaAnterior = LlamadasBLL.Buscar(llamada.LlamadaId);
            return LlamadaAnterior != null;

        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;
            if (!Validar())
                return;
           if(string.IsNullOrEmpty(LlamadaIdTextBox.Text) || (LlamadaIdTextBox.Text =="0"))
                paso = LlamadasBLL.Guardar(llamada);
            else
            {
                if (ExisteBD())
                {
                    MessageBox.Show("No se puede agregar llamada que no existe");
                    return;
                }
                paso = LlamadasBLL.Modificar(llamada);
            }
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("La llamada no pudo guardarse");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(LlamadaIdTextBox.Text, out id);
            Limpiar();
            if(LlamadasBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo Guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Recargar()
        {
            this.DataContext = null;
            this.DataContext = llamada;
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(LlamadaIdTextBox.Text, out id);
            llamada=LlamadasBLL.Buscar(id);
            if(llamada !=null)
            {
                this.DataContext = llamada;
                Recargar();
            }


        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (LlamadaDataGrid.Items.Count > 0 && LlamadaDataGrid.SelectedItem !=null)
            {
                this.detalles.RemoveAt(LlamadaDataGrid.SelectedIndex);
                CargaGrid();
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (LlamadaDataGrid.Items != null)
                this.detalles = (List<LlamadaDetalle>)LlamadaDataGrid.ItemsSource;
            this.detalles.Add(new LlamadaDetalle()
            {
                Id = 0,
                Problema = ProblemaTextBox.Text,
                Solucion = SolucionTextBox.Text
            });
            CargaGrid();
            ProblemaTextBox.Text = string.Empty;
            SolucionTextBox.Text = string.Empty;

        }


    }
}
