using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Parcial4TDS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public string _URL { get; set; }
        public HttpClient cli { get; set; }
        public EditPage()
        {
            InitializeComponent();
            
            cli = new HttpClient();
            btnEditar.IsEnabled = false;
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            Models.Data send = new Models.Data()
            {
                id_Alumno = int.Parse(lblId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Nota1 = double.Parse(txtNota1.Text),
                Nota2 = double.Parse(txtNota2.Text),
                Nota3 = double.Parse(txtNota3.Text),
                Promedio = double.Parse(lblPromedio.Text),
                Estado = lblEstado.Text
            };
            _URL = "http://hectorpaiz-001-site1.itempurl.com/api/Alumno/" + lblId.Text;
            string jsonData = JsonConvert.SerializeObject(send);
            StringContent sendData = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await cli.PutAsync(_URL, sendData);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Se modifico correctamente", "Ok");
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                await DisplayAlert("Error", "No se modifico", "Ok");
            }

        }

        private async void btnCalcular_Clicked(object sender, EventArgs e)
        {
            double nota1 = 0;
            double nota2 = 0;
            double nota3 = 0;
            if (txtNombre.Text == "" || txtApellido.Text == "")
            {
                btnEditar.IsEnabled = false;
            }
            try
            {
                nota1 = double.Parse(txtNota1.Text);
            }
            catch
            {
                await DisplayAlert("Error", "Nota 1 contiene caracteres erroneos.", "Aceptar");
                btnEditar.IsEnabled = false;
                return;
            }

            try
            {
                nota2 = double.Parse(txtNota2.Text);
            }
            catch
            {
                await DisplayAlert("Error", "Nota 2 contiene caracteres erroneos.", "Aceptar");
                btnEditar.IsEnabled = false;
                return;
            }
            try
            {
                nota3 = double.Parse(txtNota3.Text);
            }
            catch
            {
                await DisplayAlert("Error", "Nota 3 contiene caracteres erroneos.", "Aceptar");
                btnEditar.IsEnabled = false;
                return;
            }
            string estado;
            double promedio = (nota1 + nota2 + nota3) / 3;
            lblPromedio.Text = promedio.ToString();
            if (promedio >= 6)
            {
                estado = "Aprobado";
                lblEstado.Text = estado;
            }
            else
            {
                estado = "Reprobado";
                lblEstado.Text = estado;
            }

            btnEditar.IsEnabled = true;
        }
    }
}