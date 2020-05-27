using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fft
{
    struct COMPLEX
    {
        public double real, imag;
        public COMPLEX(double x, double y)
        {
            real = x;
            imag = y;
        }
        public float Magnitude()
        {
            return ((float)Math.Sqrt(real * real + imag * imag));
        }
        public float Phase()
        {
            return ((float)Math.Atan(imag / real));
        }
    }

    


    class Program
    {

        static void read(int pixelSize, double[,] array, BinaryReader br, FileStream fs)
        {

            for (int j = 0; j < pixelSize; j++)
            {
                for (int k = 0; k < pixelSize; k++)
                {

                    array[j, k] = (double)br.ReadByte();//8 bit                                               

                }
            }

            br.Close();
            fs.Close();
        }


        static void Main(string[] args)
        {

            FileStream fs = new FileStream("C:\\Users\\poh12\\OneDrive\\Desktop\\MR_data.raw", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            //Program nana = new Program();

            COMPLEX[,] Fourier = new COMPLEX[256, 512];

            // read(256, Fourier, br, fs);
            int pixelSize = 256;

            for (int j = 0; j < pixelSize; j++)
            {
                for (int k = 0; k < pixelSize*2; k++)
                {

                   Fourier[j, k].real = (double)br.ReadSingle();//8 bit                                               
                    Fourier[j, k].imag = (double)br.ReadSingle();
                   // Fourier[j, k].real = (double)br.ReadByte();//8 bit                                               
                  //  Fourier[j, k].imag = (double)br.ReadByte();
                    // Console.WriteLine(Fourier[j, k].real + " " + Fourier[j, k].imag);
                }
            }

            br.Close();
            fs.Close();

            

            FileStream fs0 = new FileStream("D:/data5.raw", FileMode.CreateNew, FileAccess.Write);
            BinaryWriter bw0 = new BinaryWriter(fs0);


            for (int j = 0; j < pixelSize; j++)
            {
                for (int k = 0; k < pixelSize*2; k++)
                {

                    bw0.Write(Fourier[j, k].Magnitude());
                   // bw0.Write((float)Fourier[j, k].real);
                    //bw0.Write((float)Fourier[j, k].imag);
                    // Fourier[j, k].real = (double)br.ReadByte();//8 bit                                               
                    //  Fourier[j, k].imag = 0;
                    Console.WriteLine(Fourier[j, k].real + " " + Fourier[j, k].imag + " " + Fourier[j, k].Magnitude());
                }
            }

            bw0.Close();
            fs0.Close();



            
        }
    }
}
