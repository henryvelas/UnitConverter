using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitConverter
{
    public partial class Form1 : Form
    {

        private ConverterService _converterService;
        public Form1()
        {
            InitializeComponent();

            _converterService = new ConverterService();

            cbtype.DataSource =Enum.GetValues(typeof(TypeEnum));
            cbtype.DropDownStyle = ComboBoxStyle.DropDownList;

            ltbfrom.DataSource = Enum.GetValues(typeof(MassEnum));
            ltbto.DataSource = Enum.GetValues(typeof(MassEnum));
        }

        private void cbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAmount.Clear();
            txtconverted.Clear();

            var type = (TypeEnum)cbtype.SelectedItem;
            switch (type)
            {
                case TypeEnum.Mass:
                    ltbfrom.DataSource = Enum.GetValues(typeof(MassEnum));
                    ltbto.DataSource = Enum.GetValues(typeof(MassEnum));
                    break;
                case TypeEnum.Temperature:
                    ltbfrom.DataSource = Enum.GetValues(typeof(TemperatureEnum));
                    ltbto.DataSource = Enum.GetValues(typeof(TemperatureEnum));
                    break;
                case TypeEnum.Time:
                    ltbfrom.DataSource = Enum.GetValues(typeof(TimeEnum));
                    ltbto.DataSource = Enum.GetValues(typeof(TimeEnum));
                    break;
                default: break;
                 
            }

        }

       
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text.Length <= 0)
                return;

            if (ltbfrom.SelectedItems == ltbto.SelectedItems)
            {
                txtconverted.Text = txtAmount.Text;
            }

            var type = (TypeEnum)cbtype.SelectedItem;
            double amount = double.Parse(txtAmount.Text);
            double convertedAmout = 0;

            switch (type)
            {
                case TypeEnum.Mass:
                    var massFrom = (MassEnum)ltbfrom.SelectedItems[0];
                    var massTo = (MassEnum)ltbto.SelectedItems[0];
                    convertedAmout= _converterService.ConvertMassUnit(massFrom, massTo,amount);
                    break; 
                case TypeEnum.Temperature:
                    var temperatureFrom = (TemperatureEnum)ltbfrom.SelectedItems[0];
                    var temperatureTo = (TemperatureEnum)ltbto.SelectedItems[0];
                    convertedAmout =  _converterService.ConvertTemperatureUnit(temperatureFrom, temperatureTo, amount);
                    break; 
                case TypeEnum.Time:
                    var TimeFrom = (TimeEnum)ltbfrom.SelectedItems[0];
                    var TimeTo = (TimeEnum)ltbto.SelectedItems[0];
                    convertedAmout = _converterService.ConvertTimeUnit(TimeFrom, TimeTo, amount);
                    break;
                default: break;
            }

            txtconverted.Text = convertedAmout.ToString();



        }
    }
}
