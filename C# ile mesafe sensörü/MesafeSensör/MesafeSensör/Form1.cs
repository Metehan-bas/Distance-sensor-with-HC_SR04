using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesafeSensör
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                comboBox1.Items.Add(port);
          
            
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string veri = serialPort1.ReadLine().Trim(); // \r\n gibi şeyleri temizler

                if (int.TryParse(veri, out int sayi))
                {
                    Invoke(new Action(() =>
                    {
                        label2.Text = sayi.ToString();

                        if (sayi < 5)
                        {
                            Console.Beep();
                        }
                    }));
                }
                else
                {
                    // Hatalı veya sayısal olmayan veri geldi, istersen logla
                }
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }));
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();

                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                MessageBox.Show("Bağlantı kuruldu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası: " + ex.Message);
            }
            

        }

       
    }
}
