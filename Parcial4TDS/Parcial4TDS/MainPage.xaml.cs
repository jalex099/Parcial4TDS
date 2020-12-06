using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            get { return Data; }
            set { data = value; }
        }
        public MainPage()
        {
            InitializeComponent();
            _URL = "http://juanmajano-002-site5.btempurl.com/api/Datos";
            cli = new HttpClient();
            Data = new ObservableCollection<Models.Data>();

        }

        public async void getData()
        {
            var getData = await cli.GetStringAsync(_URL);
            var items = JsonConvert.DeserializeObject<List<Models.Data>>(getData);
        }
    }
}
