using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YeelightAPI;

namespace yeelight_tests
{
    public partial class Form1 : Form
    {
        public int r = 0;
        public int g = 0;
        public int b = 0;
        public int colorTemp = 2700;
        public int brightness = 100;
        Device yeebulb;

        public async Task LoopForTemp()
        {
            while (true)
            {
                if (yeebulb != null && yeebulb.IsConnected == true)
                {
                    await yeebulb.SetColorTemperature(colorTemp);                    
                }
                await Task.Delay(200);
            }
        }

        public async Task LoopForBrightness()
        {
            while (true)
            {
                if (yeebulb != null && yeebulb.IsConnected == true)
                {
                    await yeebulb.SetBrightness(brightness);
                }
                await Task.Delay(200);
            }
        }


        public async Task Loop()
        {
            while (true)
            {
                if (yeebulb != null && yeebulb.IsConnected == true)
                {
                    try
                    {
                        if (r != 0 || g != 0 || b != 0)
                        {
                            await yeebulb.SetRGBColor(r, g, b);
                        }
                        await yeebulb.SetBrightness(brightness);
                        //await yeebulb.SetColorTemperature(colorTemp);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        yeebulb.Disconnect();
                        await yeebulb.Connect();
                    }
                }
                await Task.Delay(200);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (yeebulb != null)
            {
                yeebulb.Dispose();//Automaticly disconnects
                label2.ForeColor = Color.Red;
                label2.Text = "Yeebulb is Offline";
            }
            if (textBox1.Text == "")
            {
                return;
            }
            yeebulb = new Device(textBox1.Text);
            //Console.WriteLine("Connecting...");
            bool issuccesful = await yeebulb.Connect();//By the way generally your ip is 192.168.1.X . If its not you can go to the -Yeelight app - Select your device - Go to Settings(In right top) - Device info - IP address
            //Console.WriteLine("Connected");
            if (issuccesful == true)
            {
                label2.ForeColor = Color.Green;
                label2.Text = "Yeebulb is Online";
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loop();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            trackBar1.Value = Convert.ToInt32(textBox2.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            trackBar2.Value = Convert.ToInt32(textBox4.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            trackBar3.Value = Convert.ToInt32(textBox3.Text);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar1.Value.ToString();
            r = trackBar1.Value;           
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = trackBar2.Value.ToString();
            g = trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar3.Value.ToString();
            b = trackBar3.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (yeebulb != null && yeebulb.IsConnected )
            {
                yeebulb.Toggle();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            trackBar4.Value = Convert.ToInt32(textBox6.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            trackBar5.Value = Convert.ToInt32(textBox5.Text);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            textBox6.Text = trackBar4.Value.ToString();
            brightness = trackBar4.Value;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = trackBar5.Value.ToString();
            colorTemp = trackBar5.Value;
        }
    }
}
