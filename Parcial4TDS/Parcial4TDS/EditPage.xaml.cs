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
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            Models.Data send = new Models.Data()
            {
                IdPersona = int.Parse(lblIdPersona.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text
            };
            _URL = "http://juanmajano-002-site5.btempurl.com/api/Datos/"+lblIdPersona.Text;
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
    }
}