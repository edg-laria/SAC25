
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace SAC.QR
{

    public class GeneradorQR
    {
        public Image pImgLOGO { get; set; }
        public Image pImgFondo { get; set; }
        public Image pImgValidador1 { get; set; }
        public Image pImgValidador2 { get; set; }
        public Image pImgValidador3 { get; set; }
        public Image pImgAislados { get; set; }

        public bool pUsaAislado;


        public Image pCenter { get; set; }

        public Image pImgPathBot { get; set; }
        public Image pImgPathTop { get; set; }
        public Image pImgPathLeft { get; set; }
        public Image pImgPathRight { get; set; }

        public Image pImgPathCorner { get; set; }
        public Image pImgPathPasante { get; set; }
        public Image pImgPathPunta { get; set; }




        public Bitmap GetQRBitmap(string content)
        {
            var qrEncoder = new QrEncoder();
            var code = qrEncoder.Encode(content);
           
            var bitmapImage = new Bitmap(code.Matrix.Width, code.Matrix.Height);

            for (int j = 0; j < code.Matrix.Height; j++)
                for (int i = 0; i < code.Matrix.Width; i++)
                    bitmapImage.SetPixel(i, j, code.Matrix[i, j] ? System.Drawing.Color.Black : System.Drawing.Color.White);

            return bitmapImage;
        }

        public string convertirBase64 (Bitmap imagen)
        {
            Bitmap bImage = imagen;  // Your Bitmap Image
            System.IO.MemoryStream ms = new MemoryStream();
            bImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage); // Get Base64
            return SigBase64;
        }
     



        public Image GetQRFromText(string URL)
        {
            if (pUsaAislado)
            {
                pCenter = pImgAislados;

                pImgPathBot = pImgAislados;
                pImgPathTop = pImgAislados;
                pImgPathLeft = pImgAislados;
                pImgPathRight = pImgAislados;

                pImgPathCorner = pImgAislados;
                pImgPathPasante = pImgAislados;
                pImgPathPunta = pImgAislados;
            }


            Brush brush = new System.Drawing.SolidBrush(Color.FromArgb(255, 223, 2, 9));

            Brush linGrBrush = new LinearGradientBrush(
                   new Point(0, 500),
                   new Point(500, 0),
                   Color.FromArgb(255, 11, 69, 168),   // Opaque red
                   Color.FromArgb(255, 10, 10, 10));

            Image backImg = GenerateQRCodeRedondeado(URL, linGrBrush, Brushes.Transparent, 500);

            var bmpFondo = new Bitmap(pImgFondo, new Size(500, 500));
            Image mrkFondo = (Image)bmpFondo;

            Graphics g1 = Graphics.FromImage(mrkFondo);
            g1.DrawImage(backImg, 0, 0);

            return mrkFondo;
        }


        private Bitmap GenerateQrCode(string url, Brush darkColor, Brush lightColor, int size)
        {
            var encoder = new QrEncoder(ErrorCorrectionLevel.H);
            var code = encoder.Encode(url);
            var renderer = new GraphicsRenderer(new FixedCodeSize(size, QuietZoneModules.Zero), darkColor, lightColor);
            using (var ms = new MemoryStream())
            {
                renderer.WriteToStream(code.Matrix, ImageFormat.Png, ms);
                return new Bitmap(ms);
            }
        }


        private Bitmap GenerateQRCodeRedondeado(string url, Brush darkColor, Brush lightColor, int size)
        {

            var encoder = new QrEncoder(ErrorCorrectionLevel.H);
            var code = encoder.Encode(url);

            var TamañoQR = code.Matrix.Width;

            var tIMGPixel = size / TamañoQR;

            var tempBmp = new Bitmap(size, size);

            Graphics g1 = Graphics.FromImage(tempBmp);

            for (int x = 0; x < TamañoQR; x++)
            {
                for (int y = 0; y < TamañoQR; y++)
                {
                    var originalX = x;
                    var originalY = y;


                    if (code.Matrix.InternalArray[originalX, originalY])
                            g1.DrawImage(imagenPorTipoDePath(code.Matrix, x, y, tIMGPixel), tIMGPixel * x, tIMGPixel * y);

                    /*
                    if (code.Matrix.InternalArray[originalX, originalY])
                        tempBmp.SetPixel(x, y, Color.FromArgb(255, 11, 69, 168));
                    else
                        tempBmp.SetPixel(x, y, Color.Transparent);
                     */

                }
            }


            //COLOCAPUNTAS
            var tpunta = TamañoPUNTAS(code.Matrix) * tIMGPixel;

            /*
            var bmpPuntas = new Bitmap(QRGen.Properties.Resources.PUNTAS, tpunta, tpunta);
            Image mrkImgbmpPuntas = (Image)bmpPuntas;
            */

            g1.DrawImage(new Bitmap(pImgValidador1, tpunta, tpunta), 0, 0);
            g1.DrawImage(new Bitmap(pImgValidador2, tpunta, tpunta), tIMGPixel * TamañoQR - tpunta, 0);
            g1.DrawImage(new Bitmap(pImgValidador3, tpunta, tpunta), 0, tIMGPixel * TamañoQR - tpunta);



            int tamaño = Convert.ToInt32((tIMGPixel * TamañoQR) * 0.3f);

            var tamañoW = tamaño;
            var tamañoH = Convert.ToInt32((Convert.ToDecimal(tamaño) / pImgLOGO.Width) * pImgLOGO.Height);



            var bmp = new Bitmap(pImgLOGO, tamañoW, tamañoH);



            g1.DrawImage(bmp, tempBmp.Width / 2 - tamañoW / 2, tempBmp.Height / 2 - tamañoH / 2);


            var bmpFondo = new Bitmap(tempBmp, new Size(500, 500));

            return bmpFondo;

        }


        private Image imagenPorTipoDePath(BitMatrix DATA, int X, int Y, int Pixeles)
        {
            var Resultado = new Bitmap(pImgAislados, Pixeles, Pixeles);


            if (pUsaAislado) return Resultado;

            //BORRO LOS VALIDADORES

            Bitmap returnBitmap = new Bitmap(1, 1);
            var tpunta = TamañoPUNTAS(DATA);

            if (X <= tpunta && Y <= tpunta)
                return returnBitmap;

            if (X >= DATA.Width - tpunta && Y <= tpunta)
                return returnBitmap;


            if (X <= tpunta && Y >= DATA.Width - tpunta)
                return returnBitmap;





            //CHEQUEA 4 LADOS
            if (ChequeaPosicion(DATA, X + 1, Y) && ChequeaPosicion(DATA, X - 1, Y) && ChequeaPosicion(DATA, X, Y + 1) && ChequeaPosicion(DATA, X, Y - 1))
                return new Bitmap(pCenter, Pixeles, Pixeles);


            //CHEQUEA 3 LADOS
            //LEFT
            if (ChequeaPosicion(DATA, X + 1, Y) && ChequeaPosicion(DATA, X, Y + 1) && ChequeaPosicion(DATA, X, Y - 1))
                return new Bitmap(pImgPathLeft, Pixeles, Pixeles);

            //RIGHT
            if (ChequeaPosicion(DATA, X - 1, Y) && ChequeaPosicion(DATA, X, Y + 1) && ChequeaPosicion(DATA, X, Y - 1))
                return new Bitmap(pImgPathRight, Pixeles, Pixeles);


            //TOP
            if (ChequeaPosicion(DATA, X + 1, Y) && ChequeaPosicion(DATA, X - 1, Y) && ChequeaPosicion(DATA, X, Y + 1))
                return new Bitmap(pImgPathTop, Pixeles, Pixeles);

            //BOT
            if (ChequeaPosicion(DATA, X + 1, Y) && ChequeaPosicion(DATA, X - 1, Y) && ChequeaPosicion(DATA, X, Y - 1))
                return new Bitmap(pImgPathBot, Pixeles, Pixeles);




            //CHEQUEA 2 LADOS CORNERS

            if (ChequeaPosicion(DATA, X + 1, Y) && ChequeaPosicion(DATA, X, Y - 1))
                return rotateImage(new Bitmap(pImgPathCorner, Pixeles, Pixeles), 0);


            if (ChequeaPosicion(DATA, X + 1, Y) && ChequeaPosicion(DATA, X, Y + 1))
                return rotateImage(new Bitmap(pImgPathCorner, Pixeles, Pixeles), 90);


            if (ChequeaPosicion(DATA, X - 1, Y) && ChequeaPosicion(DATA, X, Y + 1))
                return rotateImage(new Bitmap(pImgPathCorner, Pixeles, Pixeles), 180);


            if (ChequeaPosicion(DATA, X - 1, Y) && ChequeaPosicion(DATA, X, Y - 1))
            {
                var a = rotateImage(new Bitmap(pImgPathCorner, Pixeles, Pixeles), 90);
                a = rotateImage(a, 90);
                return rotateImage(a, 90);
            }




            //CHEQUE SOLO 2 LADOS FIJOS
            if (ChequeaPosicion(DATA, X + 1, Y) || ChequeaPosicion(DATA, X - 1, Y))
                return new Bitmap(pImgPathPasante, Pixeles, Pixeles);

            if (ChequeaPosicion(DATA, X, Y + 1) || ChequeaPosicion(DATA, X, Y - 1))
                return rotateImage(new Bitmap(pImgPathPasante, Pixeles, Pixeles), 90);




            return Resultado;

        }

        private Bitmap rotateImage(Bitmap b, int Grades)
        {
            Bitmap returnBitmap = new Bitmap(b.Height, b.Width);
            Graphics g = Graphics.FromImage(returnBitmap);

            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            g.RotateTransform(Grades);


            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);


            g.DrawImage(b, new Point(0, 0));


            returnBitmap = new Bitmap(returnBitmap, b.Height, b.Width);


            return returnBitmap;
        }


        private bool ChequeaPosicion(BitMatrix DATA, int X, int Y)
        {
            try
            {
                return DATA.InternalArray[X, Y];
            }
            catch (Exception)
            {
                return false;
            }
        }


        private int TamañoPUNTAS(BitMatrix DATA)
        {

            int contador = 0;

            for (int i = 0; i < DATA.Height; i++)
            {
                if (DATA.InternalArray[0, i])
                    contador++;
                else
                    return contador;


            }

            return 1;

        }


    }


}
