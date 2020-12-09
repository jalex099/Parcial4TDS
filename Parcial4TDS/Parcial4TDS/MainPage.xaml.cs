using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Parcial4TDS
{
    public partial class MainPage : ContentPage
    {
        public string _URL { get; set; }

        public HttpClient cli { get; set; }
        private ObservableCollection<Models.Data> data;
        private ObservableCollection<Models.Data> Data
        {
            get { return data; }
            set { data = value; }
        }
        public MainPage()
        {
            InitializeComponent();
            _URL = "http://juanmajano-002-site5.btempurl.com/api/Datos/";
            cli = new HttpClient();
            Data = new ObservableCollection<Models.Data>();
            getData();
        }

        public async void getData()
        {
            Data = new ObservableCollection<Models.Data>();
            var getData = await cli.GetStringAsync(_URL);
            var items = JsonConvert.DeserializeObject<List<Models.Data>>(getData);

            foreach (var element in items)
            {
                Data.Add(new Models.Data()
                {
                    IdPersona = element.IdPersona,
                    Nombre = element.Nombre,
                    Apellido = element.Apellido,
                    Direccion = element.Direccion,
                    Telefono = element.Telefono
                });
            }

            listItems.ItemsSource = Data;
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var objetoBoton = (Button)sender;
            var contenido = (Models.Data)objetoBoton.CommandParameter;
            string _URLDelete = _URL + contenido.IdPersona;
            bool confirm = await DisplayAlert("DELETE", "Eliminara registro "+contenido.IdPersona, "Si", "No");
            

            if (confirm)
            {
                HttpResponseMessage response = await cli.DeleteAsync(_URLDelete);
                if (response.IsSuccessStatusCode)
                {
                    getData();
                }
                else
                {
                    await DisplayAlert("Error", "No se elimino al registro " + contenido.IdPersona, "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Se cancelo la eliminacion", "Ok");
            }
        }

        private void btnEditar_Clicked(object sender, EventArgs e)
        {
            var objetoBoton = (Button)sender;
            var contenido = (Models.Data)objetoBoton.CommandParameter;

            Navigation.PushAsync(new EditPage()
            {
                BindingContext = contenido
            });
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Insert());
        }
    }
}
