using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_To_Text
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                fileDialog.RestoreDirectory = true;
                fileDialog.Title = "Select config file to convert";
                fileDialog.DefaultExt = ".png";
                fileDialog.Filter = "png files (*.png)|*.properties|jpeg files (*.jpg)|*.txt|All files (*.*)|*.*";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                    textBox1.Text = fileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            textBox1.Text = "";

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return;

            Bitmap bitmap = new Bitmap(path);
            if (bitmap == null)
                return;

            string text = "";
            MessageBox.Show("Height: " + bitmap.Height + " Width: " + bitmap.Width);
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    int pixel = bitmap.GetPixel(j, i).ToArgb();
                    if (pixel == -1)
                        pixel = 0;
                    else
                        pixel = 1;
                    text += pixel;
                }
                text += "\n";
            }

            string file = "";
            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                fileDialog.RestoreDirectory = true;
                fileDialog.Title = "Save converted file";
                fileDialog.DefaultExt = "txt file (*.txt)|All files (*.*)|*.*";
                fileDialog.FilterIndex = 1;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                    file = fileDialog.FileName;

            }

            if (string.IsNullOrEmpty(file) || File.Exists(file))
                return;

            File.WriteAllText(file, text);
            Process.Start(new ProcessStartInfo
            {
                Arguments = Path.GetDirectoryName(file),
                FileName = "explorer.exe"
            });
        }
    }
}