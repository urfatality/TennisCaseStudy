using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeakGames
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string FilePath { get;private set; }
        public string OutputPath { get;private set; }
        public string InputPath { get;private set; }
        OpenFileDialog ofd = new OpenFileDialog();
        private void Button1_Click(object sender, EventArgs e)
        {
            //Filters file extension other than .json
            ofd.Filter = "JSON|*.json";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                FilePath = ofd.FileName;
                InputPath = System.IO.Path.GetDirectoryName(ofd.FileName);
                textBox1.Text = ofd.FileName;
                textBox2.Text = ofd.SafeFileName;
            }  
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void Text_Enter(object sender, EventArgs e)
        {
            if(textBox3.Text == "Output File Name")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void Text_Exit(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
                OutputPath = textBox3.Text;
                if (OutputPath == null || OutputPath == "" || OutputPath == "Output File Name")
                OutputPath = "output";
                textBox4.Text = String.Concat(String.Concat(InputPath, String.Concat(@"\", OutputPath), ".json"));
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OutputPath = textBox3.Text;
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
