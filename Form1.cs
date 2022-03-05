using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bitmappoc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            /* calc one dot ratio */ 
            ratio_x = (double)(width - 1) / (maxRange_x - minRange_x);
            ratio_y = (double)(height - 1) / (maxRange_y - minRange_y);

            SetBitmapFigure();
        }

        //int width = 2401;    // figure width
        //int height = 1601;   // figure height

        int width = 4801;    // figure width
        int height = 3201;   // figure height

        int gridsize_w = 160;    // grid width
        int gridsize_h = 160;    // grid height

        double maxRange_x = 100000;
        double minRange_x = 0;
        double maxRange_y = 5;
        double minRange_y = 0;

        double ratio_x = 0;
        double ratio_y = 0;

        Bitmap bitmap;

        /* Color Pallet */
        Color[] newLinesColor = {
            Color.Crimson,
            Color.MidnightBlue,
            Color.Goldenrod
        };

        private void SetBitmapFigure()
        {
            bitmap = new Bitmap(width, height); // Create bitmap image for figure
            
            /* Draw grid lines */
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    if (x % gridsize_w == 0 || y % gridsize_h == 0)
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                }
            }


            /* Write Lines */
            var rand = new Random();
            int numOfPoints = 100000;
            double test_x = 0;
            double test_y = 0;
            double prev_x = 0;
            double prev_y = 0;

            for (int ch = 0; ch < 3; ch++)
            {
                for (int i = 1; i < numOfPoints; i++)
                {
                    prev_x = test_x;
                    prev_y = test_y;
                    test_x = (double)i;
                    test_y = rand.NextDouble() + ch * 1.5;
                    
                    drawLine(prev_x, prev_y, test_x, test_y, newLinesColor[ch]);
                }
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = bitmap;
            
        }

        private void drawLine(double x0, double y0, double x1, double y1, Color color)
        {
            int x0CanScale = (int)(ratio_x * x0);
            int x1CanScale = (int)(ratio_x * x1);
            int y0CanScale = (int)(ratio_y * y0);
            int y1CanScale = (int)(ratio_y * y1);

            double grad = (double)(y1CanScale - y0CanScale) / (x1CanScale - x0CanScale);
            double offset = y1CanScale - grad * x1CanScale;

            for (int x = x0CanScale; x < x1CanScale; x++) 
            { 
                for (int y = y0CanScale; y < y1CanScale; y++)
                {
                    bitmap.SetPixel(x, y, color);
                }
            }

        }
    }
}
