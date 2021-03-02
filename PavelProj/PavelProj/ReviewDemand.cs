using PavelProj.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PavelProj
{
    public partial class ReviewDemand : Form
    {

        public static List<Client> clients = new List<Client>();

       

        public ReviewDemand(Client client)
        {
            InitializeComponent();
            clients.Add(client);
            
            DisplayInListView();
        }

        public ReviewDemand()
        {
        }

       private void DisplayInListView()
        {
            lvClients.Items.Clear();

            foreach(Client c in clients)
            {
                ListViewItem newItem = new ListViewItem(c.lastName);
                newItem.SubItems.Add(c.firstName);
                newItem.SubItems.Add(c.personalIdentity.ToString());
                newItem.SubItems.Add(c.phoneNumber.ToString());
                newItem.SubItems.Add(c.ClientInsurance.insuranceGoods);
                newItem.SubItems.Add(c.ClientInsurance.Brand);
                newItem.SubItems.Add(c.ClientInsurance.Model.ToString());
                newItem.SubItems.Add(c.ClientInsurance.serialNumber.ToString());
                newItem.SubItems.Add(c.ClientInsurance.priceValue.ToString());
                newItem.SubItems.Add(c.ClientEvent.theft.ToString());
                newItem.SubItems.Add(c.ClientEvent.accDamage.ToString());
                newItem.SubItems.Add(c.ClientInsurance.yearAquit.ToString());

                newItem.Tag = c;


                lvClients.Items.Add(newItem);


                
            }
        }

        // TOOL STRIP Menu- Serialize, Deserialize, Text Report
        public void toolStripButton1_Click(object sender, EventArgs e)
        {
            SerializeObj();
        }

        private void SerializeObj()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream("clients.dat", FileMode.Create);

            formatter.Serialize(file, clients);
            file.Close();
            lvClients.Items.Clear();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            DeserializeObj();
        }


        private void DeserializeObj()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream("clients.dat", FileMode.Open);
            clients = (List<Client>)formatter.Deserialize(file);
            file.Close();
            DisplayInListView();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(lvClients.SelectedItems.Count != 0)
            {
                lvClients.Items.Clear();
                Client client = lvClients.SelectedItems[0].Tag as Client;
                MainForm edit = new MainForm();

                edit.ShowDialog();
                DisplayInListView();


            }
        }

        private void lvClients_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
               contextMenuStrip1.Show(Cursor.Position.X + 5, Cursor.Position.Y + 5);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(lvClients.SelectedItems.Count != 0)
            {
                Client clientt = lvClients.SelectedItems[0].Tag as Client;
                clients.Remove(clientt);
                DisplayInListView();
            }
        }

        //Text Report
        private void textReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Export list in txt File";
            saveFile.Filter = "All text Files(*.txt)|*.txt";
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFile.FileName);
                foreach (Client c in clients)
                {
                    writer.Write("Last Name: " + c.lastName + "\n" + "First Name: " + c.firstName + "\n" + "Personal Identity Number: " + c.personalIdentity
                       + "\n" + "Phone number: " + c.phoneNumber.ToString() + "\n" + "Insurance Good: " + c.ClientInsurance.insuranceGoods + "\n" +
                       "Brand: " + c.ClientInsurance.Brand + "\n" + "Model: " + c.ClientInsurance.Model + "\n" +
                       "Year of Aquisition" + c.ClientInsurance.yearAquit.ToString() + "\n" +
                       "Purchase Value: " + c.ClientInsurance.priceValue.ToString() + "\n" +
                       "Serial Number: " + c.ClientInsurance.serialNumber.ToString() + "\n" +
                       "theft? -" + c.ClientEvent.theft.ToString() + "\n" +
                       "Accidentaly Damaging? -" + c.ClientEvent.accDamage.ToString());
                }
                writer.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.Width = 130;
            button1.BringToFront();
            this.Close();
        }

        //ALT Shortcuts
        private void ReviewDemand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.S)
            {
                SerializeObj();
            }
            else if (e.Alt && e.KeyCode == Keys.D)
            {
                DeserializeObj();
            }
           
        }

        


        






    }

}
