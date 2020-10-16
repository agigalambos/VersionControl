using Arfolyam.Entities;
using Arfolyam.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arfolyam
{
    public partial class Form1 : Form
    {

        BindingList<RateData> Rates = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();
            GetExchangeRates();
            dataGridView1.DataSource = Rates;
        }

        private void GetExchangeRates()
        {

            var mnbservice = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbservice.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult; ;

            Console.WriteLine(result);
        }

        







    }
}
