using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace POD_lab7
{
    class Program
    {
        static int[,] dodaj(int[,] udzial1, int[,] udzial2)
        {
            int[,] tab = new int[2,2];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    if (udzial1[i, j] == 0 && udzial2[i, j] == 0)
                        tab[i, j] = 0;
                    else
                        tab[i, j] = 1;
            return tab;
        }
        static int sprawdz_biale(int[,] tab)
        {
            int ile = 0;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    if (tab[i, j] == 0)
                        ile++;
            return ile;
        }
        static public int[,] wylosuj(System.Random x)
        {
            int[,] tab = new int[2,2];
            for(int i=0;i<2;i++)
                for (int j = 0; j < 2; j++)
                {
                    if (x.NextDouble() >= 0.5)
                    {
                        tab[i, j] = 1;
                    }
                    else
                    {
                        tab[i, j] = 0;
                    }
                }
            return tab;
        }
        static public int[,] sub_piksele(int war, System.Random x)
        {
            int[,] tab = new int[2,2];
            int biale=0;
            if (war == 255)
            {
                do
                {
                    tab = wylosuj(x);
                    biale = 0;
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 2; j++)
                            if (tab[i,j] == 0)
                                biale++;
                }
                while (biale != 2);
            }
            else
                do
                {
                    tab = wylosuj(x);
                    biale = 0;
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 2; j++)
                            if (tab[i, j] == 0)
                                biale++;
                }
                while (biale != 2);
            return tab;
        }
        static public int[,] dopelnienie(int war, int[,] udzial1, System.Random x)
        {
            int[,] tab = new int[2, 2];
            if (war == 255)
            {
               tab = udzial1;
            }
            else
            {
                for(int i = 0; i<2;i++)
                {
                    for (int j = 0; j < 2; j++)
                        if (udzial1[i, j] == 0)
                            tab[i, j] = 1;
                        else
                            tab[i, j] = 0;

                }
            }
            return tab;
        }
        static void Main(string[] args)
        {
            Bitmap myBitmap = new Bitmap("i.bmp");
            Bitmap udzial1 = new Bitmap(myBitmap.Width*2, myBitmap.Height*2);
            Bitmap udzial2 = new Bitmap(myBitmap.Width * 2, myBitmap.Height * 2);
            System.Random x = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < myBitmap.Width; i++)
            {
                for (int j = 0; j < myBitmap.Height; j++)
                {
                    Color pixelColor = myBitmap.GetPixel(i, j);
                    int[,] tab = sub_piksele(pixelColor.R, x);
                    int[,] tab2 = dopelnienie(pixelColor.R, tab, x);
                    for (int y = 0; y < 2; y++)
                        for (int z = 0; z < 2; z++)
                        {
                            if (tab[y, z] == 0)
                                udzial1.SetPixel(2*i + y, 2*j + z, Color.White);
                            else
                                udzial1.SetPixel(2*i + y, 2*j + z, Color.Black);
                            if (tab2[y, z] == 0)
                                udzial2.SetPixel(2*i + y, 2*j + z, Color.White);
                            else
                                udzial2.SetPixel(2*i + y, 2*j + z, Color.Black);
                        }
                }
                udzial1.Save("udzial1.bmp");
                udzial2.Save("udzial2.bmp");
            }
            Bitmap result = new Bitmap(myBitmap.Width * 2, myBitmap.Height * 2);
            for (int i = 0; i < udzial1.Width; i++)
            {
                for (int j = 0; j < udzial1.Height; j++)
                {
                    Color pixelColor = udzial1.GetPixel(i, j);
                    Color pixelColor2 = udzial2.GetPixel(i, j);
                    if (pixelColor.R == 255 && pixelColor2.R == 255)
                    {
                        result.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        result.SetPixel(i, j, Color.Black);
                    }
                }
            }
            result.Save("rezultat.bmp");

            Console.ReadKey();
        }
    }
}
