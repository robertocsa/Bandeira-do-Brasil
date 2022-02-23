using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Bandeira_Nacional_Aplicativo_Teste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Print handle
            printDocument1.PrintPage+=new PrintPageEventHandler(printDocument1_PrintPage);           
        }
    
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Graphics myGraphics = this.CreateGraphics();
            //Size s = this.Size;
            Size s = ctl_BandeiraBrasil2.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            //Calcula a altura do título da Janela:
            // Calculates the title height  
            Rectangle screenRectangle = Screen.FromControl(this).Bounds ;
            int AlturaTituloForm1 = this.RectangleToScreen(this.Bounds).Top - this.Top;
            //Copia a imagem da bandeira para a memória:
            //Copies the image to the memory
            memoryGraphics.CopyFromScreen(ctl_BandeiraBrasil2.Location.X+screenRectangle.Location.X, ctl_BandeiraBrasil2.Location.Y+AlturaTituloForm1, 0, 0, s);

            //Chama o diálogo de impressão
            //Calls the print dialog
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;           
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Print in landscape
                printDocument1.DefaultPageSettings.Landscape = true;

                //printDocument1.OriginAtMargins = false;
                //printDocument1.DefaultPageSettings.Margins.Left = 20;
                //printDocument1.DefaultPageSettings.Margins.Right = 20;
                //printDocument1.DefaultPageSettings.Margins.Top  = 20;
                //printDocument1.DefaultPageSettings.Margins.Bottom = 20;
                                     
                printDocument1.Print();
            }
        }

        //Bitmap que guardará a imagem a ser impressa
        //Bitmap that will keep the print image
        Bitmap memoryImage;
        
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //Keeps the proportion of the flag´s width=75% of the window width (just in this example). The control maybe used in different proportions.
            //Mantém a proporção da bandeira em 75% da janela (apenas neste exemplo). O controle pode ser usado em diferentes proporções.
            ctl_BandeiraBrasil2.Size = new Size(Convert.ToInt32(this.Size.Width*0.75F), this.Size.Height);
        }
        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Tamanho do retângulo de impressão - fica entre os limites das margens
            //Size of the print rectangle - remains in the margin bounds
            Rectangle TamanhoImpressão=e.MarginBounds;
            TamanhoImpressão.Size=new Size(TamanhoImpressão.Width-5, TamanhoImpressão.Height-5);
            e.Graphics.DrawImage(memoryImage, TamanhoImpressão);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            toolTip1.Show("Para imprimir, use o botão direito do mouse... / To print, use the mouse's right button...",this);
        }
    }
}
