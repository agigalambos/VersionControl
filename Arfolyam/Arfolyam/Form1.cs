﻿using Arfolyam.Entities;
using Arfolyam.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace Arfolyam
{
    public partial class Form1 : Form
    {

        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();


        public Form1()
        {
            InitializeComponent();

            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);

            var result = response.GetCurrenciesResult;

            var xml1 = new XmlDocument();
            xml1.LoadXml(result);

            //Console.WriteLine(result);

            foreach (XmlElement element in xml1.DocumentElement)
            {
                foreach (XmlElement belso in element.ChildNodes)
                {
                    string currency;
                    currency = belso.InnerText;

                    Currencies.Add(currency);
                }
            }
            comboBox1.DataSource = Currencies;
            //listBox1.DataSource = Currencies;
            
            RefreshData();
        }

        private void GetExchangeRates()
        {

            var mnbservice = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = startDate.Value.ToString(),
                endDate = endDate.Value.ToString()
            };

            var response = mnbservice.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult; ;

            //Console.WriteLine(result);
            result2 = result;
        }

        string result2;

        private void ProcessXml()
        {

            XmlDocument xml = new XmlDocument();

            xml.LoadXml(result2);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");


                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }

        private void CreateVisual()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void RefreshData()
        {
            Rates.Clear();

            GetExchangeRates();
            dataGridView1.DataSource = Rates;
            ProcessXml();
            CreateVisual();
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

    }
}