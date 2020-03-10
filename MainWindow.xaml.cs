using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlgorithmsStage2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Movement flags   
        bool testFlag1X = true;
        bool testFlag1Y = true;

        bool testFlag2X = true;
        bool testFlag2Y = true;
        #endregion
        
        #region Key Pressed Flags

        bool flagA = false;
        bool flagD = false;
        bool flagW = false;
        bool flagS = false;

        #endregion

        #region Random Number

        // Add code here

        #endregion


        public MainWindow()
        {
            InitializeComponent();

            #region Random Number

            // Add code here

            #endregion

            // Set game loop timer
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(10000); // 10000 ticks = 1 milisecond
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Our time event that fires the movement
        /// </summary>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            #region Move using a key press
            Move(testImage1, flagA, flagD, flagW, flagS, 5.00);
            #endregion

            #region Lock_To_Grid

            // add code here

            #endregion

            #region Move_Lock_To_Grid

            // Add code here

            #endregion

            //MAIN
            Move_Lock_To_Grid(testImage1,TestGrid);

            //Bitches
            Follow(testImage2,testImage1,1.50);    //Bitch 1
            Follow(testImage3, testImage1, 1.75);    //Bitch 2



            #region Follow

            // Add code here

            #endregion

            #region Runaway

           Runaway(testImage4,testImage1,0.25);
           Lock_To_Grid(testImage4,TestGrid);

            #endregion

            #region Collide

            // Add code here

            #endregion

            #region Random Number

            // Add code here

            #endregion

        }

        #region Move using a key press
        public void Move(Image anImage, bool left, bool right, bool top, bool bottom, double speed)
        {
            double leftMargin = anImage.Margin.Left;
            double topMargin = anImage.Margin.Top;
            double rightMargin = anImage.Margin.Right;
            double bottomMargin = anImage.Margin.Bottom;


            if (left == true)
            {
                testFlag1X = true;
                right = false;
            }
            else if (right == true) {
                testFlag1X = false;
                left = false;
            }
            if (top == true)
            {
                testFlag1Y = true;
                bottom = false;
            }
            else if (bottom == true) {
                testFlag1Y = false;
                top = false;
            }

            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
        }
        #endregion

        #region Lock_To_Grid
        public void Lock_To_Grid(Image anImage, Grid TestGrid)
        {
            double speed = 1;
            double leftMargin = anImage.Margin.Left;
            double rightMargin = anImage.Margin.Right;
            double topMargin = anImage.Margin.Top;
            double bottomMargin = anImage.Margin.Bottom;



            if (topMargin < (TestGrid.Height - TestGrid.Height))
            {
                topMargin = 0;
            }
            else if ((topMargin + testImage1.Height) > TestGrid.Height)
            {
                topMargin = TestGrid.Height - testImage1.Height;
            }

            if (leftMargin < TestGrid.Width - TestGrid.Width)
            {
                leftMargin = 0;
            }
            else if (leftMargin + anImage.Width > TestGrid.Width)
            {
                leftMargin = TestGrid.Width - testImage1.Width;
            }



            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);

        }

        #endregion
 
        #region Move_Lock_To_Grid

        public void Move_Lock_To_Grid(Image anImage, Grid TestGrid)
        {

            double speed = 2;
            double leftMargin = anImage.Margin.Left;
            double rightMargin = anImage.Margin.Right;
            double topMargin = anImage.Margin.Top;
            double bottomMargin = anImage.Margin.Bottom;

            if (topMargin < (TestGrid.Height - TestGrid.Height))
            {

                topMargin = 0;
                testFlag1Y = !testFlag1Y;

            }
            else if ((topMargin + testImage1.Height) > TestGrid.Height)
            {

                topMargin = TestGrid.Height - testImage1.Height;
                testFlag1Y = !testFlag1Y;
            }
            else if (leftMargin < TestGrid.Width - TestGrid.Width)
            {

                leftMargin = 0;
                testFlag1X = !testFlag1X;

            }
            else if (leftMargin > TestGrid.Width - testImage1.Width)
            {
                leftMargin = TestGrid.Width - testImage1.Width;
                testFlag1X = !testFlag1X;
            }

            if (testFlag1X) leftMargin -= speed;
            else if (!testFlag1X) leftMargin += speed;
            if (testFlag1Y) topMargin -= speed;
            else if (!testFlag1Y) topMargin += speed;

            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);

        }


        #endregion

        #region Follow

        public void Follow(Image anImage, Image target, double velocity)
        {
           // double distance = 200;
            double anImageLeft = anImage.Margin.Left;
            double anImageRight = anImage.Margin.Right;
            double anImageTop = anImage.Margin.Top;
            double anImageBottom = anImage.Margin.Bottom;



            if (anImageLeft + target.Width < target.Margin.Left )
            {
                //Go Right
                Console.WriteLine("Within range");
                anImageLeft += velocity;
            }
            else if (anImageLeft > target.Margin.Left + target.Width)
            {
                //Go Left
                anImageLeft -= velocity;
            }

            if (anImageTop > target.Margin.Top + target.Height)
            {
                //Go Up
                anImageTop -= velocity;
            }
            else if (anImageTop  > target.Margin.Top - target.Height)
            {
                //Go Down
                anImageTop += velocity;

            }

            Collide(anImage,target);


            anImage.Margin = new Thickness(anImageLeft, anImageTop, anImageRight, anImageBottom);

        }

        #endregion

        #region Runaway

        public void Runaway(Image anImage, Image target, double speed)
        {

            double distance = 50;



            double anImageLeft = anImage.Margin.Left;
            double anImageRight = anImage.Margin.Right;
            double anImageTop = anImage.Margin.Top;
            double anImageBottom = anImage.Margin.Bottom;

            if ((anImageLeft + anImage.Width) < target.Margin.Left + distance )
            {
                anImageLeft -= speed;
            }
            else if (anImageLeft > target.Margin.Left + target.Width )
            {
                anImageLeft += speed;
            }

            if ((anImageTop + anImage.Height) < target.Margin.Top + distance )
            {
                anImageTop -= speed;
            }
            else if (anImageTop > target.Margin.Top + target.Height )
            {
                anImageTop += speed;
            }

            anImage.Margin = new Thickness(anImageLeft, anImageTop, anImageRight, anImageBottom);

        }

        #endregion

        #region Collide

        public void Collide(Image anImage, Image target) {

            if ((anImage.Margin.Left + anImage.Width) > target.Margin.Left && anImage.Margin.Left < (target.Margin.Left + target.Width) &&
                  (anImage.Margin.Top + anImage.Height) > target.Margin.Top && anImage.Margin.Top < (target.Margin.Top + target.Height))
            {
                Console.WriteLine("BOOM PARANG NENENG B KANYANG KATAWAN");


            }
            else
            {
                Console.WriteLine("OUT NA SIR OUT NA SIR");
            }
        }

        #endregion

        /// <summary>
        /// Resizes the grid to the screen size
        /// </summary>
        private void TestWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TestGrid.Width = TestWindow.Width - 30;
            TestGrid.Height = TestWindow.Height - 50;
        }

        #region Key Pressed test

        private void TestWindow_KeyDown(object sender, KeyEventArgs e)
        {

            flagA = false;
            flagD = false;
            flagW = false;
            flagS = false;

            if (e.Key == Key.A) flagA = true;
            if (e.Key == Key.D) flagD = true;
            if (e.Key == Key.W) flagW = true;
            if (e.Key == Key.S) flagS = true;
        }

        #endregion
    }
}
