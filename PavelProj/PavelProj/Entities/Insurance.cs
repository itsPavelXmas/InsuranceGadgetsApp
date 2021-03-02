using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavelProj.Entities
{
    [Serializable]
    public   class Insurance
    {
        public String insuranceGoods { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int serialNumber { get; set; }
        public double priceValue { get; set; }
        public int yearAquit { get; set; } 

        public Insurance(string insuranceGoods, string brand, string model, int serialNumber, double priceValue, int yearAquit)
        {
            this.insuranceGoods = insuranceGoods;
            this.Brand = brand;
             this.Model = model;
            this.serialNumber = serialNumber;
            this.priceValue = priceValue;
            this.yearAquit = yearAquit;

            
            




        }

        public Insurance()
        {

        }
    }
}
