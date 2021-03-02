using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavelProj.Entities
{
    [Serializable]
    public class Client
    {
        public long DbID { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public Insurance ClientInsurance { get; set;}
        public Event ClientEvent { get; set; }
        
        public long personalIdentity { get; set; }
        public long phoneNumber { get; set; }
        public object Insurance { get; internal set; }
        

        public Client(string lastName, string firstName, Insurance clientInsurance, Event clientEvent, long personalIdentity, long phoneNumber, object insurance)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            ClientInsurance = clientInsurance;
            ClientEvent = clientEvent;
            this.personalIdentity = personalIdentity;
            this.phoneNumber = phoneNumber;
            Insurance = insurance;
        }

        public Client(long DbID,string lastName, string firstName, Insurance clientInsurance, Event clientEvent, long personalIdentity, long phoneNumber, object insurance)
        {
            this.DbID = DbID;
            this.lastName = lastName;
            this.firstName = firstName;
            ClientInsurance = clientInsurance;
            ClientEvent = clientEvent;
            this.personalIdentity = personalIdentity;
            this.phoneNumber = phoneNumber;
            Insurance = insurance;
        }



        public Client()
        {

        }
    }
}
