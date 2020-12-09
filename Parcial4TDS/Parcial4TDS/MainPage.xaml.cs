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
            _URL = "http://hectorpaiz-001-site1.itempurl.com/api/Alumno/";
            cli = new HttpClient();
            Data = new ObservableCollection<Models.Data>();
            getData();
        }

        public async void getData()
        {
            Data = new ObservableCollection<Models.Data>();
            var get = await cli.GetStringAsync(_URL);
            var items = JsonConvert.DeserializeObject<List<Models.Data>>(get);
            
            if (items!= null) { 
            foreach (var element in items)
            {
                Data.Add(new Models.Data()
                {
                    id_Alumno = element.id_Alumno,
                    Nombre = element.Nombre,
                    Apellido = element.Apellido,
                    Nota1 = element.Nota1,
                    Nota2 = element.Nota2,
                    Nota3 = element.Nota3,
                    Promedio = element.Promedio,
                    Estado = element.Estado
                });
            }
            }
            listItems.ItemsSource = Data;
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var objetoBoton = (Button)sender;
            var contenido = (Models.Data)objetoBoton.CommandParameter;
            string _URLDelete = _URL + contenido.id_Alumno;
            bool confirm = await DisplayAlert("DELETE", "¿Eliminar registro "+contenido.id_Alumno+"?", "Si", "No");
            

            if (confirm)
            {
                HttpResponseMessage response = await cli.DeleteAsync(_URLDelete);
                if (response.IsSuccessStatusCode)
                {
                    getData();
                }
                else
                {
                    await DisplayAlert("Error", "No se elimino al registro " + contenido.id_Alumno, "Ok");
                }
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

        private void btnInformacion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Datos());
        }
    }
}
