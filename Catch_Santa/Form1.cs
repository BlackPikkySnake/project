using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catch_Santa
{
    public partial class Form1 : Form
    {

        private int score = 0;
        private Random random = new Random();
        private List<Snowflake> snowflakes = new List<Snowflake>();
        private int gameSpeed = 5;
        private int blueSnowflakeEffectTime = 0;
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }
        public class Snowflake{
        public PictureBox PictureBox { get; set; }
        public int Speed { get; set; }

        public int Type { get; set; }
        public Snowflake(PictureBox pictureBox, int speed, int type)
        {
            PictureBox = pictureBox;
            Speed = speed;
            Type = type;
        }

    }
        private void InitializeGame()
        {
           
            Image normalSnowflakeImage = Image.FromFile("C:\\Users\\Admin\\Downloads\\Snez_simple.jpg");  
            Image blueSnowflakeImage = Image.FromFile("C:\\Users\\Admin\\Downloads\\Snez_blue.png");  
            Image blackSnowflakeImage = Image.FromFile("C:\\Users\\Admin\\Downloads\\Snez_black.jpg");

            for (int i = 0; i < 3; i++)
            {
                AddSnowflake(normalSnowflakeImage);
            }
            gameTimer.Interval = 50;
            gameTimer.Start();
            scoreLabel.Text = "Счет: 0";
        }

        private void AddSnowflake(Image image)
        {
            PictureBox newSnowflakePictureBox = new PictureBox();
            newSnowflakePictureBox.Image = image;
            newSnowflakePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            newSnowflakePictureBox.Size = new Size(50, 50);
            newSnowflakePictureBox.Location = new Point(random.Next(0, this.ClientSize.Width - newSnowflakePictureBox.Width), -newSnowflakePictureBox.Height);
            newSnowflakePictureBox.Click += SnowflakePictureBox_Click;
            this.Controls.Add(newSnowflakePictureBox);
            Snowflake newSnowflake = new Snowflake(newSnowflakePictureBox, random.Next(1, 5), random.Next(1, 10)); 
            snowflakes.Add(newSnowflake);
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            int currentSpeed = gameSpeed; 

            if (blueSnowflakeEffectTime > 0)
            {
                currentSpeed = 5; 
                blueSnowflakeEffectTime--; 
            }

            for (int i = 0; i < snowflakes.Count; i++)
            {

                snowflakes[i].PictureBox.Top += currentSpeed;

                if (snowflakes[i].PictureBox.Top > this.ClientSize.Height)
                {

                    snowflakes[i].PictureBox.Location = new Point(random.Next(0, this.ClientSize.Width - snowflakes[i].PictureBox.Width), -snowflakes[i].PictureBox.Height);
                    snowflakes[i].Speed = random.Next(1, 5);
                    if (random.Next(1, 10) == 5)
                    {
                        snowflakes.RemoveAt(i);
                        score = score - 1;
                        scoreLabel.Text = "Счет: " + score;
                        Image blackSnowflakeImage = Image.FromFile("C:\\Users\\Admin\\Downloads\\Snez_black.jpg");
                        AddSnowflake(blackSnowflakeImage);
                    }
                    else if (random.Next(1, 10) == 4)
                    {
                        snowflakes.RemoveAt(i);
                        Image blueSnowflakeImage = Image.FromFile("C:\\Users\\Admin\\Downloads\\Snez_blue.png");
                        AddSnowflake(blueSnowflakeImage);
                    }


                }

            }


        }
        private void SnowflakePictureBox_Click(object sender, EventArgs e)
        {

            PictureBox clickedPictureBox = (PictureBox)sender;

            for (int i = 0; i < snowflakes.Count; i++)
            {

                if (snowflakes[i].PictureBox == clickedPictureBox)
                {

                    if (snowflakes[i].Speed > 5)
                    {
                        score -= 5;
                    }
                    else if (snowflakes[i].Speed < 0)
                    {
                        blueSnowflakeEffectTime = 100; 

                    }
                    else
                    {
                        score++;
                    }

                    scoreLabel.Text = "Счет: " + score;

                    snowflakes[i].PictureBox.Location = new Point(random.Next(0, this.ClientSize.Width - snowflakes[i].PictureBox.Width), -snowflakes[i].PictureBox.Height);
                    snowflakes[i].Speed = random.Next(1, 5);
                    if (random.Next(1, 10) == 5)
                    {
                        snowflakes.RemoveAt(i);
                        if (score < 5) { score = 0; } else { score -= 5; } 
                        scoreLabel.Text = "Счет: " + score;
                        Image blackSnowflakeImage = Image.FromFile("C:\\Users\\Admin\\Downloads\\Snez_black.jpg");
                        AddSnowflake(blackSnowflakeImage);
                    }
                    else if (random.Next(1, 10) == 4)
                    {
                        snowflakes.RemoveAt(i);
                        Image blueSnowflakeImage = Image.FromFile("C:\\Users\\Admin\\Downloads\\Snez_blue.png");
                        AddSnowflake(blueSnowflakeImage);
                    }
                    break;
                }
            }


        }
    }
    
   
}

