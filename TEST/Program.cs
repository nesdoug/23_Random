using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace RNG_Test
{
    class Program
    {
        public const int max_num = 50000;
        public static int seed_low, seed_high, seed0, seed1, seed2, seed3;
        public static int [] N_array = new int[256];


        static void Main(string[] args)
        {
            var path = @"c:\test\rng.bmp";
            Bitmap bm = new Bitmap(256, 256);
            

            // orig uses 0xfd
            seed_low = 0xfd; // neslib seed
            seed_high = 0xfd;
            int current_num = 0;
            int last_num = 0;

            // fill the box with white
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 256; y++)
                {
                    bm.SetPixel(x, y, Color.White);
                }
            }

            for (int i = 0; i < max_num; i++)
            {
                last_num = current_num;
                current_num = RNG_Algorithm();
                N_array[current_num] = N_array[current_num] + 1;

                if (i == 0) continue;
                // one black dot per number pair
                bm.SetPixel(last_num, current_num, Color.Black);
            }
            

            bm.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);

            // print out the distribution of numbers to console
            for(int i = 0; i < 256; i++)
            {
                Console.WriteLine(i + " " + N_array[i]);
            }
        }

        public static int RNG_Algorithm()
        {
            int carry_bit, final_value;

            
            // neslib original

            /*
            seed_low = (seed_low << 1);
            if(seed_low >= 256)
            {
                seed_low = seed_low ^ 0xcf;
            }
            
            seed_low = seed_low & 0xff;

            seed_high = (seed_high << 1);
            if (seed_high >= 256)
            {
                seed_high = seed_high ^ 0xd7;
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed_high = seed_high & 0xff;
            
            final_value = ((seed_low + seed_high + carry_bit) & 0xff);
            
            */
            




            //old cc65 rand Brad

            /*
            seed1 = seed0 + seed1;
            if(seed1 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed1 = seed1 & 0xff;

            seed2 = seed1 + seed2 + carry_bit;
            if (seed2 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed2 = seed2 & 0xff;

            seed3 = seed2 + seed3 + carry_bit;
            
            carry_bit = 0;
            seed3 = seed3 & 0xff;
            
            //add 0x31415927
            seed0 = seed0 + 0x27;
            if (seed0 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed0 = seed0 & 0xff;

            seed1 = seed1 + 0x59 + carry_bit;
            if (seed1 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed1 = seed1 & 0xff;

            seed2 = seed2 + 0x41 + carry_bit;
            if (seed2 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed2 = seed2 & 0xff;

            seed3 = seed3 + 0x31 + carry_bit;
            seed3 = seed3 & 0xff;

            final_value = seed3;
            */






            // new version cc65

            
            seed0 = seed0 + 0xb3;
            if (seed0 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed0 = seed0 & 0xff;

            seed1 = seed1 + seed0 + carry_bit;
            if (seed1 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed1 = seed1 & 0xff;

            seed2 = seed2 + seed1 + carry_bit;
            if (seed2 >= 256)
            {
                carry_bit = 1;
            }
            else
            {
                carry_bit = 0;
            }
            seed2 = seed2 & 0xff;
            
            seed3 = seed3 + seed2 + carry_bit;
            seed3 = seed3 & 0xff;


            final_value = seed3;
            

            
            return final_value;


            
        }
    }
}
