using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PB
{
    public partial class PB : Form
    {
        List<String> imagens = new List<string>();
        static Bitmap newBitmap;
        public PB()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            
            try
            {
                
                listBox1.Items.Clear();
                listBox1.Items.Add("Por favor, aguarde!!!");
                
                imagens.AddRange((String[])e.Data.GetData(DataFormats.FileDrop));

                if (imagens.Count > 30)
                {
                    MessageBox.Show("Por favor, selecione menos de 30 arquivos para um melhor desempenho");
                    imagens.Clear();
                    listBox1.Items.Clear();
                }
                else {

                   listBox1.Items.Clear();
                foreach (String s in imagens)
                {
                    Bitmap temp = new Bitmap(s);
                    MakeGrayscale(temp).Save(s.Substring(0, s.Length - 4) + "PB.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    temp.Dispose();
                    newBitmap.Dispose();
                    listBox1.Items.Add(s);
                    
                }
                imagens.Clear();
                listBox1.Items.Add("Pronto!!!");

                }
            }
            catch{
                MessageBox.Show("Um erro ocorreu, confira o que deu errado, e tente novamente");
            }
            
        }



        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            newBitmap = new Bitmap(original.Width, original.Height);
            newBitmap.SetResolution(300.0F, 300.0F);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
      {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
      });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        private void listBox1_DragLeave(object sender, EventArgs e)
        {



        }
    }
}
