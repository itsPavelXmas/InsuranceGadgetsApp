using ChartLibrary;
using PavelProj.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PavelProj
{
    public partial class MainForm : Form
    {

        
       
        
       public Client client = new Client();
        Insurance insurance = new Insurance();
        Event ev=new Event();
        private Insurance clientInsurance;
        private Event events;
        private string connectionString = "Data Source=database.db";


        public MainForm()
        {
            InitializeComponent();
              
            
        }

        //
      private void ADDClient(Client client)
        {

            var query = "INSERT INTO Insurance (LastName, FirstName,personalIdentity,phoneNumber,insuranceGoods,Brand,Model,serialNumber,priceValue,yearAquit,theft,accDamage)" +
                "VALUES (@lastName,@firstName,@personalIdentity,@phoneNumber,@insuranceGoods,@Brand,@Model,@serialNumber,@priceValue,@yearAquit,@theft,@accDamage); " +
                "" +
                "SELECT last_insert_rowid()"; 

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@lastName", client.lastName);
                command.Parameters.AddWithValue("@firstName", client.firstName);
                command.Parameters.AddWithValue("@personalIdentity", client.personalIdentity);
                command.Parameters.AddWithValue("@phoneNumber", client.phoneNumber);
                command.Parameters.AddWithValue("@insuranceGoods", client.ClientInsurance.insuranceGoods);
                command.Parameters.AddWithValue("@Brand", client.ClientInsurance.Brand);
                command.Parameters.AddWithValue("@Model", client.ClientInsurance.Model);
                command.Parameters.AddWithValue("@serialNumber", client.ClientInsurance.serialNumber);
                command.Parameters.AddWithValue("@priceValue", client.ClientInsurance.priceValue);
                command.Parameters.AddWithValue("@yearAquit", client.ClientInsurance.yearAquit);
                command.Parameters.AddWithValue("@theft", client.ClientEvent.theft);
                command.Parameters.AddWithValue("@accDamage", client.ClientEvent.accDamage);

                connection.Open();
                Object id=command.ExecuteScalar();
                client.DbID = (long)id;

                ReviewDemand.clients.Add(client);



            }

        }

        private void btReviewDemand_Click(object sender, EventArgs e)
        {
            

            if (string.IsNullOrEmpty(tbLastName.Text) || string.IsNullOrEmpty(tbFirstName.Text)
                || string.IsNullOrEmpty(tbPersonalIN.Text) || string.IsNullOrEmpty(tbPhoneNumber.Text)
                || string.IsNullOrEmpty(tbBrand.Text) || string.IsNullOrEmpty(tbModel.Text)
                || string.IsNullOrEmpty(tbSerialNumber.Text) || string.IsNullOrEmpty(tbPurchaseValue.Text)
                || string.IsNullOrEmpty(tbYearAq.Text))
            {
                MessageBox.Show("Please fill all the fields!", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                client.lastName = tbLastName.Text;
                client.firstName = tbFirstName.Text;
                client.personalIdentity = long.Parse(tbPersonalIN.Text.Trim());
                client.phoneNumber = long.Parse(tbPhoneNumber.Text.Trim());
                insurance.insuranceGoods = cbInsuranceGoods.Text.Trim();
                insurance.Brand = tbBrand.Text;
                insurance.Model = tbModel.Text;
                insurance.serialNumber = Int32.Parse(tbSerialNumber.Text.Trim());
                insurance.priceValue = Double.Parse(tbPurchaseValue.Text.Trim());
                insurance.yearAquit = Int32.Parse(tbYearAq.Text.Trim());
                if (rbTrue.Checked)
                {
                    ev.theft = "Yes";
                }
                else
                {
                    ev.theft = "No";
                }
                if (rbDamageTrue.Checked)
                {
                    ev.accDamage = "Yes";
                }
                else
                {
                    ev.accDamage = "No";
                }
                client.ClientInsurance = insurance;
                client.ClientEvent = ev;


                
                   
                    ReviewDemand rd = new ReviewDemand(client);
                    rd.ShowDialog();
               

               

                
                this.Close();
                


            }
                
        }
        //Validations with Error Provider! 
        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbLastName.Text))
            {
                epLastName.SetError(tbLastName, "Last name must not be Empty!");
                e.Cancel = true;
            }

        }

        private void tbLastName_Validated(object sender, EventArgs e)
        {
            epLastName.Clear();
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbFirstName.Text))
            {
                epFirstName.SetError(tbFirstName, "First Name must not be Empty!");
                e.Cancel = true;
            }

        }

        private void tbFirstName_Validated(object sender, EventArgs e)
        {
            epFirstName.Clear();
        }

        private void tbPersonalIN_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (long.Parse(tbPersonalIN.Text.Trim()) > 9999999999999 || tbPersonalIN.Text.Length != 13)
                {
                    epPersonalIdentityNumber.SetError(tbPersonalIN, "Personal Id must Not be Empty or it must have 13 digits!");
                    e.Cancel = true;

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong Personal Id Number!", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                epPersonalIdentityNumber.SetError(tbPersonalIN, "Personal Id must Not be Empty or it must have 13 digits!");
                epPersonalIdentityNumber.Clear();
            }
        }

        private void tbPersonalIN_Validated(object sender, EventArgs e)
        {
            epPersonalIdentityNumber.Clear();
        }




        private void tbPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (long.Parse(tbPhoneNumber.Text.Trim()) > 9999999999 || tbPhoneNumber.Text.Length != 10)

                {
                    epPhoneNumber.SetError(tbPhoneNumber, "Phone number must not be empty or must contain 10 digits!");

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong phone format!", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                epPhoneNumber.SetError(tbPhoneNumber, "Phone Number must NOT be empty or it must have at most 10 digits!");
                e.Cancel = true;
            }

        }

        private void tbPhoneNumber_Validated(object sender, EventArgs e)
        {
            epPhoneNumber.Clear();
        }




        private void cbInsuranceGoods_Validating(object sender, CancelEventArgs e)
        {
            if (cbInsuranceGoods == null)
            {
                epInsuranceGoods.SetError(cbInsuranceGoods, "Select a good that you want to insure it!");
                e.Cancel = true;
            }
        }

        private void cbInsuranceGoods_Validated(object sender, EventArgs e)
        {
            epInsuranceGoods.Clear();
            
        }

        private void tbBrand_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbBrand.Text))
            {
                epBrand.SetError(tbBrand, "The Brand must not be empty!");
                e.Cancel = true;
            }
        }

        private void tbBrand_Validated(object sender, EventArgs e)
        {
            epBrand.Clear();
        }

        private void tbModel_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbModel.Text))
            {
                epModel.SetError(tbModel, "The Model must not be empty!");
                e.Cancel = true;

            }
        }

        private void tbModel_Validated(object sender, EventArgs e)
        {
            epModel.Clear();
        }

        private void tbYearAq_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (int.Parse(tbYearAq.Text.Trim()) < 2000 || tbYearAq.Text.Length != 4)
                {
                    
                    epYearAq.SetError(tbYearAq, "The Year of Aquisition must not be empty, sooner than 2000 and must be of 4 digits!");
                    e.Cancel = true;
                    


                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Wrong Year number", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                epYearAq.SetError(tbYearAq, "The Year of Aquisition must not be empty, older than 2000 and must be of 4 digits!");
                e.Cancel = true;

            }
        }

        private void tbYearAq_Validated(object sender, EventArgs e)
        {
            epYearAq.Clear();
        }




        private void tbPurchaseValue_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (double.Parse(tbPurchaseValue.Text.Trim()) < 100)
                {
                    epPurchaseValue.SetError(tbPurchaseValue, "The Purchase value must not be empty and must be over 100.00 dollars!");
                    e.Cancel = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong Purchase Value", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                epPurchaseValue.SetError(tbPurchaseValue, "The Purchase value must not be empty and must be over 100.00 dollars!");
                e.Cancel = true;
            }

        }

        private void tbPurchaseValue_Validated(object sender, EventArgs e)
        {
            epPurchaseValue.Clear();
        }

        private void tbSerialNumber_Validating(object sender, CancelEventArgs e)
        {
            try
            {


                if (int.Parse(tbSerialNumber.Text.Trim()) == 0)
                {
                    epSerialNumber.SetError(tbSerialNumber, "The Serial Number must not be Empty and not 0!");
                    e.Cancel = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong Serial Number", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                epSerialNumber.SetError(tbSerialNumber, "The Serial Number must not be Empty and not 0!");
                e.Cancel = true;
            }
        }

        private void tbSerialNumber_Validated(object sender, EventArgs e)
        {
            epSerialNumber.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void barChartControl1_Click(object sender, EventArgs e)
        {
            barChartControl1.Data = new[]
            {
                new BarChartValue("Vegan", 3 ),
            new BarChartValue("NotVegan", 3),

            };
            barChartControl1.Invalidate();
        }
    }
}
