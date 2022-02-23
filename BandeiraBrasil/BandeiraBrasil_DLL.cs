using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

///
// Este material tem propósito único de estudo
// Autor: Roberto Carlos dos Santos
// Ocupação: Analista-Tributário da Receita Federal do Brasil
// Hobby: programação 
// Sinta-se à vontade para me enviar mensagens a respeito deste material
// email: robertoc_santos@yahoo.com.br


// This material is just for study purposes
// author: Roberto Carlos dos Santos
// Ocupation: Analista-Tributário da Receita Federal do Brasil (Tax Analist in the Brazilian Government Tax Administration)
// Hobby: programming
// Feel free to send-me any messages about this material.
// email: robertoc_santos@yahoo.com.br
// 
///

namespace BandeiraBrasil
{
    public partial class Ctl_BandeiraBrasil : UserControl
    {

        //Fontes (sources of the patterns - dimensions- of the brazilian flag)
        //Dimensões da bandeira (conforme LEI No 5.700, DE 1 DE SETEMBRO DE 1971):
        // http://upload.wikimedia.org/wikipedia/commons/b/be/Flag_of_Brazil_%28dimensions%29.svg
        // e
        // http://pt.wikipedia.org/wiki/Bandeira_do_Brasil
        //
       
        //Cores padrão
        //Pattern Colors
      
        private Color m_CorRetangulo = Color.FromArgb(255, 0, 168, 89);  //Área verde da bandeira (retângulo e letras) //Green area
        private Color m_CorLosango = Color.FromArgb(255, 255, 204, 41); //Área amarela (losango) //yellow area
        private Color m_CorCirculoCentral = Color.FromArgb(255, 62, 64, 149); //Área azul (Circulo central) //blue area
        private Color m_CorFaixaEdasEstrelas = Color.FromArgb(255, 255, 255, 255); //Faixa central (branca) //white area
        
        //Número de estrelas
        //number of stars
        const int NumEstrelas = 27;                         
        
        //Brushes
        SolidBrush bCorCirculo;
        SolidBrush bCorFaixa;
        SolidBrush bCorLosango;
        SolidBrush bCorRetangulo;     

        st_Bandeira oBandeira; //Dimensoes da bandeira

        public Ctl_BandeiraBrasil()  //Construtores  //Constructors
        {
       
            InitializeComponent();

            //Atualiza Metricas
            //Metric´s refresh
            AtualizaMetricas();

            //Color of the brushs
            bCorCirculo = new SolidBrush(m_CorCirculoCentral);
            bCorFaixa = new SolidBrush(m_CorFaixaEdasEstrelas);
            bCorLosango = new SolidBrush(m_CorLosango);
            bCorRetangulo = new SolidBrush(m_CorRetangulo);

            InitArrayTamEstrelas();       

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.Opaque, true);

            Invalidate();
        }
       
        /// <summary> DesenhaABandeira
        /// Eventos de pintura (PAINT)
        /// Desenha os gráficos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DesenhaABandeira(Object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.Width == 0) return;

            ///
            // Aqui são feitos os desenhos
            // Here are made the draws and paintings
            ///
            #region DESENHOS 
            Graphics g = e.Graphics;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.SmoothingMode =  SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        
            //Paths:
            
            GraphicsPath path_Retangulo = new GraphicsPath();           
            GraphicsPath path_Losango = new GraphicsPath();
            GraphicsPath path_Circulo = new GraphicsPath();
            GraphicsPath path_Faixa = new GraphicsPath();
            GraphicsPath path_Estrelas = new GraphicsPath();
            
            //SolidBrushs
            SolidBrush Pincel_Retangulo = new SolidBrush(m_CorRetangulo);
            SolidBrush Pincel_Losango = new SolidBrush(m_CorLosango);
            SolidBrush Pincel_Circulo = new SolidBrush(m_CorCirculoCentral);
            Pen Pincel_Faixa = new Pen(m_CorFaixaEdasEstrelas);
            SolidBrush Pincel_Estrelas = new SolidBrush(m_CorFaixaEdasEstrelas);
            
            //Pinta o retângulo verde
            //Paints the green rectangle
            path_Retangulo.AddRectangle(oBandeira.Retangulo);
            g.FillPath(bCorRetangulo, path_Retangulo);

            //Cria Path e Região do Losango (a região marca onde ocorrerão os cortes (por exemplo) - utilizada, no caso, para cortar o excesso de faixa branca)
            //Creates path and region of the yellow polygon            
            path_Losango.AddPolygon(oBandeira.VerticesLosango);
            Region RgLosango = new Region(path_Losango);

            // Cria path e região do círculo
            // Creates path and circle region
            path_Circulo.AddEllipse(oBandeira.Circulo);
            Region RgCirculo = new Region(path_Circulo);

            //Exclui a região do círculo
            //Excludes the circle region
            RgLosango.Exclude(RgCirculo);

            // Adiciona o círculo central azul
            // Adds the central blue circle
            g.FillRegion(bCorCirculo, RgCirculo);
            
            // Cria a grade (temporária) para posicionamento das estrelas. Este trecho deve ser posteriomente comentado
            // Creates the grid for stars positioning (just for developing tests).
            #region GRADE DE POSICIONAMENTO DAS ESTRELAS (COMENTAR ESTE TRECHO DEPOIS DOS TESTES)

            //float _X=oBandeira.Circulo.X;
            //float _Y=oBandeira.Circulo.Y;
            //float _TamGrade=oBandeira.TamGradeCirculo;
            //float _TamMod=oBandeira.TamModulo;
            //SolidBrush Verde = bCorRetangulo;
            
            //for (int i = 1; i < 20; i++)
            //{
            //    Pen Pincel;
            //    if (i == 10)
            //    { Pincel=new Pen(new SolidBrush(Color.Violet)) ;}
            //    else
            //    { Pincel=new Pen(Verde) ;}

            //    PointF PontoSuperior=new PointF (_X+(i *_TamGrade), _Y);
            //    PointF PontoInferior=new PointF (_X+(i *_TamGrade), _Y+(7F * _TamMod ));
            //    g.DrawLine(Pincel, PontoSuperior, PontoInferior);
            //}
            //for (int i = 1; i < 20; i++)
            //{
            //    Pen Pincel;
            //    if (i == 10)
            //    { Pincel=new Pen(new SolidBrush(Color.Violet)) ;}
            //    else
            //    { Pincel=new Pen(Verde) ;}
            //    PointF PontoEsquerdo = new PointF(_X, _Y + (i * _TamGrade));
            //    PointF PontoDireito = new PointF(_X + (7F * _TamMod),_Y + (i * _TamGrade));
            //    g.DrawLine(Pincel, PontoEsquerdo, PontoDireito);
            //}
            #endregion GRADE DE TESTES
            

            #region Desenha a faixa branca
            //Desenha a faixa branca   
            //Draws the white ribbon

            String Texto = "";

            Font Fonte = new Font("HELVETICA", 0.33F * oBandeira.TamModulo, FontStyle.Bold);

            path_Faixa.AddArc(new RectangleF(oBandeira.Posicao.X, (oBandeira.Posicao.Y + ((5.775F) * oBandeira.TamModulo)), (2 * 8F * oBandeira.TamModulo),
                                      (2 * 8F * oBandeira.TamModulo)), 259, 55);
            Pincel_Faixa = new Pen(bCorFaixa, oBandeira.TamModulo * 0.5F);

            g.DrawPath(Pincel_Faixa, path_Faixa);

            //Novo fill no Losango, para apagar o excesso de faixa branca (a parte da faixa que extrapola a região do círculo).
            //New fill on the yellow polygon, just to erase the excessive white ribbon (the part that overpasses the circle region)
            g.FillRegion(bCorLosango, RgLosango);

            #endregion Desenha a faixa

            #region DESENHA A FRASE "ORDEM E PROGRESSO"
            //Desenha as palavras
            //Draw the words "Ordem e Progresso" (in the middle of the circle of the brazilian flag)

            Texto = "ORDEM";
            Fonte = new Font("CALIBRI", 0.32F * oBandeira.TamModulo, FontStyle.Bold);
            GraphicsPath path_Texto_Ordem = new GraphicsPath();
            path_Texto_Ordem.AddArc(new RectangleF(oBandeira.Posicao.X, (oBandeira.Posicao.Y + (5.425F * oBandeira.TamModulo)), (2 * 8.07F * oBandeira.TamModulo),
                                      (2 * 8F * oBandeira.TamModulo)), 260.5F, 30);

            DrawTextOnPath(g, bCorRetangulo, Fonte, Texto, path_Texto_Ordem, false);

            //
            Texto = "E";
            Fonte = new Font("CALIBRI", 0.27F * oBandeira.TamModulo, FontStyle.Bold);
            GraphicsPath path_Texto_E = new GraphicsPath();
            path_Texto_E.AddArc(new RectangleF(oBandeira.Posicao.X, (oBandeira.Posicao.Y + (5.505F * oBandeira.TamModulo)), (2 * 8.07F * oBandeira.TamModulo),
                                      (2 * 8F * oBandeira.TamModulo)), 279.72F, 5);

            DrawTextOnPath(g, bCorRetangulo, Fonte, Texto, path_Texto_E, false);

            //
            Texto = "PROGRESSO";
            Fonte = new Font("CALIBRI", 0.32F * oBandeira.TamModulo, FontStyle.Bold);
            GraphicsPath path_Texto_Progresso = new GraphicsPath();
            path_Texto_Progresso.AddArc(new RectangleF(oBandeira.Posicao.X, (oBandeira.Posicao.Y + (5.425F * oBandeira.TamModulo)), (2 * 8.07F * oBandeira.TamModulo),
                                      (2 * 8F * oBandeira.TamModulo)), 282.8F, 68);

            DrawTextOnPath(g, bCorRetangulo, Fonte, Texto, path_Texto_Progresso, false);

            #endregion FRASE ORDEM E PROGRESSO
                       

            //DISPOSES:
            
            //path_Fundo.Dispose();
            path_Retangulo.Dispose();    
            path_Losango.Dispose();
            path_Circulo.Dispose();
            path_Faixa.Dispose();
            path_Estrelas.Dispose();
            //
            //Pincel_Fundo.Dispose();
            Pincel_Retangulo.Dispose();
            Pincel_Losango.Dispose(); 
            Pincel_Circulo.Dispose(); 
            Pincel_Faixa.Dispose();
            Pincel_Estrelas.Dispose();

            RgCirculo.Dispose();
            RgLosango.Dispose();
            #endregion DESENHOS
 
            #region calcula vértices da estrela
            //Calculates the star´s point's locations (the inners and the outters)

            //************
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            //The proportion 3/8 is the relation between the inner circle and the outter circle of the stars
            PointF[] Estrela1 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela1.X, oBandeira.Estrela1.Y),
                                  aTamEstrelas[0], aTamEstrelas[0] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela2 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela2.X, oBandeira.Estrela2.Y),
                                  aTamEstrelas[1], aTamEstrelas[1] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela3 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela3.X, oBandeira.Estrela3.Y),
                                  aTamEstrelas[2], aTamEstrelas[2] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela4 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela4.X, oBandeira.Estrela4.Y),
                                  aTamEstrelas[3], aTamEstrelas[3] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela5 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela5.X, oBandeira.Estrela5.Y),
                                  aTamEstrelas[4], aTamEstrelas[4] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela6 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela6.X, oBandeira.Estrela6.Y),
                                  aTamEstrelas[5], aTamEstrelas[5] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela7 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela7.X, oBandeira.Estrela7.Y),
                                  aTamEstrelas[6], aTamEstrelas[6] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela8 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela8.X, oBandeira.Estrela8.Y),
                                  aTamEstrelas[7], aTamEstrelas[7] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela9 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela9.X, oBandeira.Estrela9.Y),
                                  aTamEstrelas[8], aTamEstrelas[8] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela10 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela10.X, oBandeira.Estrela10.Y),
                                  aTamEstrelas[9], aTamEstrelas[9] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela11 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela11.X, oBandeira.Estrela11.Y),
                                  aTamEstrelas[10], aTamEstrelas[10] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela12 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela12.X, oBandeira.Estrela12.Y),
                                  aTamEstrelas[11], aTamEstrelas[11] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela13 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela13.X, oBandeira.Estrela13.Y),
                                  aTamEstrelas[12], aTamEstrelas[12] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela14 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela14.X, oBandeira.Estrela14.Y),
                                  aTamEstrelas[13], aTamEstrelas[13] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela15 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela15.X, oBandeira.Estrela15.Y),
                                  aTamEstrelas[14], aTamEstrelas[14] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela16 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela16.X, oBandeira.Estrela16.Y),
                                  aTamEstrelas[15], aTamEstrelas[15] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela17 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela17.X, oBandeira.Estrela17.Y),
                                  aTamEstrelas[16], aTamEstrelas[16] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela18 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela18.X, oBandeira.Estrela18.Y),
                                  aTamEstrelas[17], aTamEstrelas[17] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela19 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela19.X, oBandeira.Estrela19.Y),
                                  aTamEstrelas[18], aTamEstrelas[18] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela20 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela20.X, oBandeira.Estrela20.Y),
                                  aTamEstrelas[19], aTamEstrelas[19] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela21 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela21.X, oBandeira.Estrela21.Y),
                                  aTamEstrelas[20], aTamEstrelas[20] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela22 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela22.X, oBandeira.Estrela22.Y),
                                  aTamEstrelas[21], aTamEstrelas[21] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela23 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela23.X, oBandeira.Estrela23.Y),
                                  aTamEstrelas[22], aTamEstrelas[22] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela24 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela24.X, oBandeira.Estrela24.Y),
                                  aTamEstrelas[23], aTamEstrelas[23] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela25 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela25.X, oBandeira.Estrela25.Y),
                                  aTamEstrelas[24], aTamEstrelas[24] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela26 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela26.X, oBandeira.Estrela26.Y),
                                  aTamEstrelas[25], aTamEstrelas[25] * 3 / 8);
            //A razão de 3/8 é a relação entre o círculo interno e o círculo externo onde se apóiam os vértices da estrela
            PointF[] Estrela27 = CalculaPontosDaEstrela(new PointF(oBandeira.Estrela27.X, oBandeira.Estrela27.Y),
                                  aTamEstrelas[26], aTamEstrelas[26] * 3 / 8);            


            #endregion

            #region Fill estrelas  //Fill stars
            //path_Estrelas.AddPolygon(Estrela1);
            g.FillPolygon(bCorFaixa, Estrela1);
            g.FillPolygon(bCorFaixa, Estrela2);
            g.FillPolygon(bCorFaixa, Estrela3);
            g.FillPolygon(bCorFaixa, Estrela4);
            g.FillPolygon(bCorFaixa, Estrela5);
            g.FillPolygon(bCorFaixa, Estrela6);
            g.FillPolygon(bCorFaixa, Estrela7);
            g.FillPolygon(bCorFaixa, Estrela8);
            g.FillPolygon(bCorFaixa, Estrela9);
            g.FillPolygon(bCorFaixa, Estrela10);
            g.FillPolygon(bCorFaixa, Estrela11);
            g.FillPolygon(bCorFaixa, Estrela12);
            g.FillPolygon(bCorFaixa, Estrela13);
            g.FillPolygon(bCorFaixa, Estrela14);
            g.FillPolygon(bCorFaixa, Estrela15);
            g.FillPolygon(bCorFaixa, Estrela16);
            g.FillPolygon(bCorFaixa, Estrela17);
            g.FillPolygon(bCorFaixa, Estrela18);
            g.FillPolygon(bCorFaixa, Estrela19);
            g.FillPolygon(bCorFaixa, Estrela20);
            g.FillPolygon(bCorFaixa, Estrela21);
            g.FillPolygon(bCorFaixa, Estrela22);
            g.FillPolygon(bCorFaixa, Estrela23);
            g.FillPolygon(bCorFaixa, Estrela24);
            g.FillPolygon(bCorFaixa, Estrela25);
            g.FillPolygon(bCorFaixa, Estrela26);
            g.FillPolygon(bCorFaixa, Estrela27);
            //
            Pincel_Estrelas = new SolidBrush(m_CorFaixaEdasEstrelas);
            //g.FillPath(Pincel_Estrelas, path_Estrelas);
            #endregion
                    
        }

        #region Draw some text
        // Draw some text along a GraphicsPath.        
        // Obtido em (source of the base code):http://blog.csharphelper.com/2011/06/08/draw-text-that-follows-a-curve-in-c.aspx
        private void DrawTextOnPath(Graphics gr, Brush brush, Font font, string txt, GraphicsPath path, bool text_above_path)
        {
            // Make a copy so we don't mess up the original.
            path = (GraphicsPath)path.Clone();

            // Flatten the path into segments.
            path.Flatten();

            // Draw characters.
            int start_ch = 0;
            PointF start_point = path.PathPoints[0];
            for (int i = 1; i < path.PointCount; i++)
            {
                PointF end_point = path.PathPoints[i];
                DrawTextOnSegment(gr, brush, font, txt, ref start_ch, ref start_point, end_point, text_above_path);
                if (start_ch >= txt.Length) break;
            }
        }

        // Draw some text along a line segment.

        // Obtido em :http://blog.csharphelper.com/2011/06/08/draw-text-that-follows-a-curve-in-c.aspx
        // Leave char_num pointing to the next character to be drawn.
        // Leave start_point holding the coordinates of the last point used.
        private void DrawTextOnSegment(Graphics gr, Brush brush, Font font, string txt, ref int first_ch, ref PointF start_point, PointF end_point, bool text_above_segment)
        {
            float dx = end_point.X - start_point.X;
            float dy = end_point.Y - start_point.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            dx /= dist;
            dy /= dist;

            // See how many characters will fit.
            int last_ch = first_ch;
            while (last_ch < txt.Length)
            {
                string test_string = txt.Substring(first_ch, last_ch - first_ch + 1);
                if (gr.MeasureString(test_string, font).Width > dist)
                {
                    // This is one too many characters.
                    last_ch--;
                    break;
                }
                last_ch++;
            }
            if (last_ch < first_ch) return;
            if (last_ch >= txt.Length) last_ch = txt.Length - 1;
            string chars_that_fit = txt.Substring(first_ch, last_ch - first_ch + 1);

            // Rotate and translate to position the characters.
            GraphicsState state = gr.Save();
            if (text_above_segment)
            {
                gr.TranslateTransform(0,
                    -gr.MeasureString(chars_that_fit, font).Height,
                    MatrixOrder.Append);
            }
            float angle = (float)(180 * Math.Atan2(dy, dx) / Math.PI);
            gr.RotateTransform(angle, MatrixOrder.Append);
            gr.TranslateTransform(start_point.X, start_point.Y, MatrixOrder.Append);

            // Draw the characters that fit.
            gr.DrawString(chars_that_fit, font, brush, 0, 0);

            // Restore the saved state.
            gr.Restore(state);

            // Update first_ch and start_point.
            first_ch = last_ch + 1;
            float text_width = gr.MeasureString(chars_that_fit, font).Width;
            start_point = new PointF(
                start_point.X + dx * text_width,
                start_point.Y + dy * text_width);
        }


        #endregion
                     
        ///
        /// <summary> CalculaPontosDaEstrela (Calcula os pontos (locais) de cada vértice da estrela)
        /// Código fonte obtido em - Source of the code : http://www.daniweb.com/software-development/csharp/code/360165/draw-any-star-you-want
        /// Return an array of 10 points to be used in a Draw- or FillPolygon method
        /// </summary>
        /// <param name="Orig"> The origin is the middle of the star.(ponto de origem fica no meio da estrela)</param>
        /// <param name="RaioExterno">Radius of the surrounding circle. (raio do círculo de fora - dos pontos externos da estrela)</param>
        /// <param name="RaioInterno">Radius of the circle for the "inner" points (raio do círculo de dentro - dos pontos internos)</param>
        /// 
        ///  Exemplo de chamada: Example of use:
        ///  PointF[] Star5 = Calculate5StarPoints(new PointF(200f, 500f), 80f, 30f);
        ///  G.DrawPolygon(new Pen(Color.Gold , 3), Star5);
        ///  
        /// <returns>Array of 10 PointF structures</returns>
        /// *******************************************************************************
        /// Breve lembrança sobre trigonometria: Some trig remembering:
        /// Seno=Cateto oposto / hipotenusa   
        /// Cosseno=Cateto adjacente / hipotenusa
        /// No caso, o raio do círculo (exterior ou interior) da estrela coincide com a hipotenusa. Logo, se quero conhecer os pontos X e Y,
        /// tenho de usar (por exemplo) :  X (cateto adjacente) = raio (hipotenusa) x cosseno (do ângulo que o raio faz em relação ao eixo de X).
        /// Isso porque o cosseno é a relação entre cateto adjacente e hipotenusa. Então, se multiplico Hipotenusa (raio) x Cateto Adj / Hipotenusa (raio),
        /// fico com o valor do cateto adjacente, que é o mesmo valor de X no círculo trigonométrico
        /// O círculo trigonométrico é formado por quatro regiões (quadrantes): de 0 a 90 graus (1ºQd), seno e cosseno dão resultados positivos, 
        /// de 90 a 180 graus (2º Qd), seno dá resultado positivo, e cosseno, negativo; de 180 a 270 graus(3ºQd), seno e cosseno dão resultados negativos; 
        /// de 270 a 360 graus(4ºQd), seno dá resultado negativo e cosseno, positivo.
        

        private PointF[] CalculaPontosDaEstrela(PointF Orig, float RaioExterno, float RaioInterno)
        {

            
            
            // Define some variables to avoid as much calculations as possible
            // conversions to radians
            double Ang36 = Math.PI / 5.0;   // 36° x PI/180 (ao variar este divisor (5 para 4, por exemplo), obteremos interessantes formas de estrela).
            double Ang72 = 2.0 * Ang36;     // 72° x PI/180
            // some sine and cosine values we need
            float Sin36 = (float)Math.Sin(Ang36);
            float Sin72 = (float)Math.Sin(Ang72);
            float Cos36 = (float)Math.Cos(Ang36);
            float Cos72 = (float)Math.Cos(Ang72);
            // Fill array with 10 origin points
            PointF[] pnts = { Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig };


            //Primeiro ponto começa no topo, portanto, X=raio * cosseno de 90 graus (que é 0). Portanto, X=0
            // Y= raio * seno de 90 graus (que é 1). Portanto, Y = raio (negativo porque os valores de Y crescem de cima para baixo);
            //The first point begins at the top.
            pnts[0].Y -= RaioExterno;  // top off the star, or on a clock this is 12:00 or 0:00 hours
            
            // O segundo ponto fica a 54 graus em relação ao eixo de X. Portanto, em relação ao eixo Y fica a 36 graus.
            // The second point stands 54 degrees in relation to the X axis. So, stays 36 degrees in relation to Y.
            // Y está negativo porque os valores crescem de cima para baixo. Isso é uma característica do GDI (o ponto X,Y de determinado retângulo fica no canto superior esquerdo).
            // It's negative because the values grow from top to bottom.
            pnts[1].X += RaioInterno * Sin36; pnts[1].Y -= RaioInterno * Cos36; // 0:06 hours

            //O terceiro ponto fica a 18 graus em relação ao eixo de X e a 72 do de Y. 
            pnts[2].X += RaioExterno * Sin72; pnts[2].Y -= RaioExterno * Cos72; // 0:12 hours

            //O quarto ponto fica já no quarto quadrante, e o raio fica inclinado para baixo, a 18º em relação a X e a 72 em relação a Y.
            pnts[3].X += RaioInterno * Sin72; pnts[3].Y += RaioInterno * Cos72; // 0:18

            //O quinto ponto igualmente fica no quarto quadrante trigonométrico, com o raio voltado para baixo a 54 graus em relação ao eixo de X (36 graus de Y).
            pnts[4].X += RaioExterno * Sin36; pnts[4].Y += RaioExterno * Cos36; // 0:24 
            //
            pnts[5].Y += RaioInterno;
            //  A simetria da estrela é utilizada para preencher estes pontos:
            pnts[6].X += pnts[6].X - pnts[4].X; pnts[6].Y = pnts[4].Y;  // (ponto simétrico (espelhado)) // mirrowed points
            pnts[7].X += pnts[7].X - pnts[3].X; pnts[7].Y = pnts[3].Y;  // idem
            pnts[8].X += pnts[8].X - pnts[2].X; pnts[8].Y = pnts[2].Y;  // idem
            pnts[9].X += pnts[9].X - pnts[1].X; pnts[9].Y = pnts[1].Y;  // idem
            return pnts;
        }

        //Estrutura da bandeira
        //Flags struct
        struct st_Bandeira
        {
            public float TamModulo;
            public PointF Posicao;
            public SizeF Tamanho;
            public RectangleF Retangulo;
            public RectangleF Circulo;
            public float TamGradeCirculo;
            public float RaioInferiorFaixa;
            public float RaioSuperiorFaixa;
            public float RaioCirculo;
            public PointF OrigemRaioCirculo;
            public PointF OrigemTexto;
            public PointF OrigemRaioFaixa;
            public float RaioExternoEstrelaDimensao1;
            public float RaioExternoEstrelaDimensao2;
            public float RaioExternoEstrelaDimensao3;
            public float RaioExternoEstrelaDimensao4;
            public float RaioExternoEstrelaDimensao5;
            public PointF VerticeEsquerdoLosango;
            public PointF VerticeSuperiorLosango;
            public PointF VerticeDireitoLosango;
            public PointF VerticeInferiorLosango;
            public PointF[] VerticesLosango;
            public PointF Estrela1;
            public PointF Estrela2;
            public PointF Estrela3;
            public PointF Estrela4;
            public PointF Estrela5;
            public PointF Estrela6;
            public PointF Estrela7;
            public PointF Estrela8;
            public PointF Estrela9;
            public PointF Estrela10;
            public PointF Estrela11;
            public PointF Estrela12;
            public PointF Estrela13;
            public PointF Estrela14;
            public PointF Estrela15;
            public PointF Estrela16;
            public PointF Estrela17;
            public PointF Estrela18;
            public PointF Estrela19;
            public PointF Estrela20;
            public PointF Estrela21;
            public PointF Estrela22;
            public PointF Estrela23;
            public PointF Estrela24;
            public PointF Estrela25;
            public PointF Estrela26;
            public PointF Estrela27;            

        }
        public float[] aTamEstrelas;

        //inicializa o array de tamanho das estrelas
        //initialize the array of the stars dimension.
        //Na bandeira brasileira, as estrelas são de cinco tipos (dimensões) diferentes. Todas em relação à medida básica (1/20 da largura)
        //In the brazilian flag, the stars are classified in five different types (different dimensions). All of them in relation to the basic mesure (1/20 of the width).
        public void InitArrayTamEstrelas()
        {
            aTamEstrelas = new float[NumEstrelas];

            aTamEstrelas[0] = oBandeira.RaioExternoEstrelaDimensao1;//Estrela 1
            aTamEstrelas[1] = oBandeira.RaioExternoEstrelaDimensao1;//Estrela 2
            aTamEstrelas[2] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 3
            aTamEstrelas[3] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 4
            aTamEstrelas[4] = oBandeira.RaioExternoEstrelaDimensao1;//Estrela 5
            aTamEstrelas[5] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 6
            aTamEstrelas[6] = oBandeira.RaioExternoEstrelaDimensao4;//Estrela 7
            aTamEstrelas[7] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 8
            aTamEstrelas[8] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 9
            aTamEstrelas[9] = oBandeira.RaioExternoEstrelaDimensao1;//Estrela 10
            aTamEstrelas[10] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 11
            aTamEstrelas[11] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 12
            aTamEstrelas[12] = oBandeira.RaioExternoEstrelaDimensao4;//Estrela 13
            aTamEstrelas[13] = oBandeira.RaioExternoEstrelaDimensao1;//Estrela 14
            aTamEstrelas[14] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 15
            aTamEstrelas[15] = oBandeira.RaioExternoEstrelaDimensao1;//Estrela 16
            aTamEstrelas[16] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 17
            aTamEstrelas[17] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 18
            aTamEstrelas[18] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 19
            aTamEstrelas[19] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 20
            aTamEstrelas[20] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 21
            aTamEstrelas[21] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 22
            aTamEstrelas[22] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 23
            aTamEstrelas[23] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 24
            aTamEstrelas[24] = oBandeira.RaioExternoEstrelaDimensao2;//Estrela 25
            aTamEstrelas[25] = oBandeira.RaioExternoEstrelaDimensao3;//Estrela 26
            aTamEstrelas[26] = oBandeira.RaioExternoEstrelaDimensao5;//Estrela 27
        }

        #region metricas
        public void AtualizaMetricas()
        {              
            //Dimensões da bandeira (Todas dependem do comprimento do módulo):
            //Flags dimension (all of them depends on the basic mesure extent).
            oBandeira.TamModulo = Convert.ToInt32((this.Size.Width / 20F));                
            // O módulo deve ser um número inteiro qualquer, que servirá de base para todas as outras medidas.
            // The basic mesure may be any integer, which will serve to the others mesures.           

            this.Size=new Size(this.Size.Width, Convert.ToInt32(this.Size.Width * 0.7F));

            oBandeira.Tamanho.Width = 20F * oBandeira.TamModulo;
            oBandeira.Tamanho.Height = 14F * oBandeira.TamModulo;
            oBandeira.Posicao.X = 0;
            oBandeira.Posicao.Y = 0;

            oBandeira.Retangulo = new RectangleF(oBandeira.Posicao, oBandeira.Tamanho);

            oBandeira.TamGradeCirculo = oBandeira.TamModulo * 0.35F;
            oBandeira.OrigemRaioFaixa.X = oBandeira.Posicao.X + (8F * oBandeira.TamModulo);
            oBandeira.OrigemRaioFaixa.Y = oBandeira.Posicao.Y + (14F * oBandeira.TamModulo);
            oBandeira.OrigemRaioCirculo.X = oBandeira.Posicao.X + (10F * oBandeira.TamModulo);
            oBandeira.OrigemRaioCirculo.Y = oBandeira.Posicao.Y + (7F * oBandeira.TamModulo);
            oBandeira.OrigemTexto.X = oBandeira.OrigemRaioCirculo.X - (9F * oBandeira.TamGradeCirculo);
            oBandeira.OrigemTexto.Y = oBandeira.OrigemRaioCirculo.Y - (3F * oBandeira.TamGradeCirculo);

            oBandeira.RaioCirculo = oBandeira.TamModulo * 3.5F;

            oBandeira.Circulo = new RectangleF(oBandeira.OrigemRaioCirculo.X - oBandeira.RaioCirculo, oBandeira.OrigemRaioCirculo.Y - oBandeira.RaioCirculo,
                                             oBandeira.TamModulo * 7F, oBandeira.TamModulo * 7F);

            oBandeira.RaioInferiorFaixa = 8F * oBandeira.TamModulo;
            oBandeira.RaioSuperiorFaixa = 8.5F * oBandeira.TamModulo;
            oBandeira.RaioExternoEstrelaDimensao1 = 0.15F * oBandeira.TamModulo ;
            oBandeira.RaioExternoEstrelaDimensao2 = 0.125F * oBandeira.TamModulo;
            oBandeira.RaioExternoEstrelaDimensao3 = 0.10F * oBandeira.TamModulo;
            oBandeira.RaioExternoEstrelaDimensao4 = 0.07F * oBandeira.TamModulo;
            oBandeira.RaioExternoEstrelaDimensao5 = 0.05F * oBandeira.TamModulo;
            oBandeira.VerticeEsquerdoLosango.X = oBandeira.Posicao.X + (1.7F * oBandeira.TamModulo);
            oBandeira.VerticeEsquerdoLosango.Y = oBandeira.Posicao.Y + (7F * oBandeira.TamModulo);
            oBandeira.VerticeSuperiorLosango.X = oBandeira.Posicao.X + (10F * oBandeira.TamModulo);
            oBandeira.VerticeSuperiorLosango.Y = oBandeira.Posicao.Y + (1.7F * oBandeira.TamModulo); ;
            oBandeira.VerticeDireitoLosango.X = oBandeira.Posicao.X + (20F * oBandeira.TamModulo) - (1.7F * oBandeira.TamModulo);
            oBandeira.VerticeDireitoLosango.Y = oBandeira.VerticeEsquerdoLosango.Y;
            oBandeira.VerticeInferiorLosango.X = oBandeira.VerticeSuperiorLosango.X;
            oBandeira.VerticeInferiorLosango.Y = oBandeira.Posicao.Y + (14F * oBandeira.TamModulo) - (1.7F * oBandeira.TamModulo);

            //Array com os pontos (Vertices) do losango:
            //Array with yellow polygon points:
            oBandeira.VerticesLosango = new PointF[4];
            oBandeira.VerticesLosango[0] = oBandeira.VerticeEsquerdoLosango;
            oBandeira.VerticesLosango[1] = oBandeira.VerticeSuperiorLosango;
            oBandeira.VerticesLosango[2] = oBandeira.VerticeDireitoLosango;
            oBandeira.VerticesLosango[3] = oBandeira.VerticeInferiorLosango;

            //Posições das estrelas: 
            //Stars position:
            oBandeira.Estrela1.X = oBandeira.OrigemRaioCirculo.X + (3F * (oBandeira.TamGradeCirculo));
            oBandeira.Estrela1.Y = oBandeira.OrigemRaioCirculo.Y - (3F * (oBandeira.TamGradeCirculo));

            oBandeira.Estrela2.X = oBandeira.OrigemRaioCirculo.X - (8F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela2.Y = oBandeira.OrigemRaioCirculo.Y - (1.5F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela3.X = oBandeira.OrigemRaioCirculo.X - (3.5F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela3.Y = oBandeira.OrigemRaioCirculo.Y + (0.5F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela4.X = oBandeira.OrigemRaioCirculo.X + (2.5F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela4.Y = oBandeira.OrigemRaioCirculo.Y - (0.5F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela5.X = oBandeira.OrigemRaioCirculo.X - (7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela5.Y = oBandeira.OrigemRaioCirculo.Y + (3F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela6.X = oBandeira.OrigemRaioCirculo.X - (8F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela6.Y = oBandeira.OrigemRaioCirculo.Y + (4F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela7.X = oBandeira.OrigemRaioCirculo.X - (6F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela7.Y = oBandeira.OrigemRaioCirculo.Y + (2F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela8.X = oBandeira.OrigemRaioCirculo.X - (5F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela8.Y = oBandeira.OrigemRaioCirculo.Y + (3.8F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela9.X = oBandeira.OrigemRaioCirculo.X - (5.3F * (oBandeira.TamGradeCirculo));
            oBandeira.Estrela9.Y = oBandeira.OrigemRaioCirculo.Y + (4.8F * (oBandeira.TamGradeCirculo));

            oBandeira.Estrela10.X = oBandeira.OrigemRaioCirculo.X - (4F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela10.Y = oBandeira.OrigemRaioCirculo.Y + (5.8F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela11.X = oBandeira.OrigemRaioCirculo.X;
            oBandeira.Estrela11.Y = oBandeira.OrigemRaioCirculo.Y + (1F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela12.X = oBandeira.OrigemRaioCirculo.X -(1F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela12.Y = oBandeira.OrigemRaioCirculo.Y +(2F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela13.X = oBandeira.OrigemRaioCirculo.X - (0.5F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela13.Y = oBandeira.OrigemRaioCirculo.Y + (2.5F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela14.X = oBandeira.OrigemRaioCirculo.X;
            oBandeira.Estrela14.Y = oBandeira.OrigemRaioCirculo.Y + (4F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela15.X = oBandeira.OrigemRaioCirculo.X + (1F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela15.Y = oBandeira.OrigemRaioCirculo.Y + (2F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela16.X = oBandeira.OrigemRaioCirculo.X + (6.7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela16.Y = oBandeira.OrigemRaioCirculo.Y + (3.7F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela17.X = oBandeira.OrigemRaioCirculo.X + (8.2F * (oBandeira.TamGradeCirculo));
            oBandeira.Estrela17.Y = oBandeira.OrigemRaioCirculo.Y + (3.8F * (oBandeira.TamGradeCirculo));

            oBandeira.Estrela18.X = oBandeira.OrigemRaioCirculo.X + (7.2F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela18.Y = oBandeira.OrigemRaioCirculo.Y + (4.5F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela19.X = oBandeira.OrigemRaioCirculo.X + (6.7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela19.Y = oBandeira.OrigemRaioCirculo.Y + (5.5F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela20.X = oBandeira.OrigemRaioCirculo.X + (5.7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela20.Y = oBandeira.OrigemRaioCirculo.Y + (5.8F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela21.X = oBandeira.OrigemRaioCirculo.X + (4.7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela21.Y = oBandeira.OrigemRaioCirculo.Y + (5.6F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela22.X = oBandeira.OrigemRaioCirculo.X + (4.7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela22.Y = oBandeira.OrigemRaioCirculo.Y + (6.8F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela23.X = oBandeira.OrigemRaioCirculo.X + (4.7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela23.Y = oBandeira.OrigemRaioCirculo.Y + (7.8F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela24.X = oBandeira.OrigemRaioCirculo.X + (3.7F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela24.Y = oBandeira.OrigemRaioCirculo.Y + (6.2F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela25.X = oBandeira.OrigemRaioCirculo.X + (3F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela25.Y = oBandeira.OrigemRaioCirculo.Y + (7F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela26.X = oBandeira.OrigemRaioCirculo.X + (2F * oBandeira.TamGradeCirculo);
            oBandeira.Estrela26.Y = oBandeira.OrigemRaioCirculo.Y + (6F * oBandeira.TamGradeCirculo);

            oBandeira.Estrela27.X = oBandeira.OrigemRaioCirculo.X; 
            oBandeira.Estrela27.Y = oBandeira.OrigemRaioCirculo.Y + (7F * oBandeira.TamGradeCirculo);

            InitArrayTamEstrelas();

        #endregion metricas

        }                
        
        /// <summary> Variável de sistema (não mexer) //System (do not alter)
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> DISPOSE (Não mexer)
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Ctl_BandeiraBrasil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Ctl_BandeiraBrasil";
            this.Size = new System.Drawing.Size(1000, 700);
            this.SizeChanged += new System.EventHandler(this.Ctl_BandeiraBrasil_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DesenhaABandeira);
            this.ResumeLayout(false);

        }

        #endregion

        private void Ctl_BandeiraBrasil_SizeChanged(object sender, EventArgs e)
        {
            //Call to refresh mesures (just in case the control is modified)
            AtualizaMetricas();

            //force repainting
            Invalidate();
        }
    }
}
