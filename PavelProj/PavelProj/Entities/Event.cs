using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavelProj.Entities
{
    [Serializable]
    public class Event
    {
        public string theft { get; set; }
        public string accDamage { get; set; }

        public Event(string theft, string accDamage)
        {
            this.theft = theft;
            this.accDamage = accDamage;
        }

        public Event()
        {

        }
    }
}
