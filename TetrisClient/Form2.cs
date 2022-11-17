using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisClient
{
    public partial class Form2 : Form
    {
        byte[] bytes = new byte[256];
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Form1 = new Form1();
            Form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connector connector= new Connector();
            int port = Convert.ToInt32(textBox2.Text);
            bytes = Encoding.Unicode.GetBytes(textBox3.Text);
            connector.ExecuteClient(textBox1.Text,port);
        }
    }
}
