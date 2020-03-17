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
            if (llamada.LlamadaId == 0)
                paso = LlamadasBLL.Guardar(llamada);
            else
            {
                if (ExisteBD())
                {
                    paso = LlamadasBLL.Modificar(llamada);
                }
                else
                {
                    MessageBox.Show("No se puede modificar una llamada que no existe");
                    return;
                }
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
            if (LlamadasBLL.Eliminar(llamada.LlamadaId))
            {
                MessageBox.Show("Eliminado");
                Limpiar();
            }
            else
                MessageBox.Show("No se puede eliminar una llamada que no existe");
        }
        private void Recargar()
        {
            this.DataContext = null;
            this.DataContext = llamada;
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            LLamada LlamadaAnterior = LlamadasBLL.Buscar(llamada.LlamadaId);
            if (LlamadaAnterior != null)
            {
                llamada = LlamadaAnterior;
                Recargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Llamada No Encontrada");
            }

        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (LlamadaDataGrid.Items.Count > 1 && LlamadaDataGrid.SelectedIndex < LlamadaDataGrid.Items.Count - 1)
            {
                llamada.Llamadas.RemoveAt(LlamadaDataGrid.SelectedIndex);
                Recargar();
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
