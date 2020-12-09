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
    public partial class Insert : ContentPage
    {
        public string _URL { get; set; }
        public HttpClient cli { get; set; }
        public Insert()
        {
            InitializeComponent();

            _URL = "http://juanmajano-002-site5.btempurl.com/api/Datos/";
            cli = new HttpClient();
        }

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            Models.Data send = new Models.Data()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text
            };
            string jsonData = JsonConvert.SerializeObject(send);
            StringContent sendData = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await cli.PostAsync(_URL, sendData);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Se inserto correctamente", "Ok");
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                await DisplayAlert("Error", "No se inserto", "Ok");
            }
        }
    }
}