using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{

    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[5, 5];
        private bool[,] isBlack = new bool[5, 5];
        private bool[,] hasBeenRevealed = new bool[5, 5];
        private int score = 0;
        private Random rand = new Random();
        private Label scoreLabel = new Label();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        public Form1()
        {
            InitializeComponent();
            InitializeGameGrid();
            InitializeStartButton();
            InitializeScoreLabel();
            InitializeTimer();
        }

        private void InitializeGameGrid()
        {
            int buttonSize = 40;
            int padding = 50; // Padding at the top for other controls
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(i * buttonSize, j * buttonSize + padding),
                        BackColor = Color.Gray
                    };
                    buttons[i, j].Click += Button_Click;
                    Controls.Add(buttons[i, j]);
                }
            }
        }

        private void InitializeStartButton()
        {
            Button startButton = new Button
            {
                Text = "Start Game",
                Location = new Point(10, 10),
                Size = new Size(100, 30)
            };
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);
        }

        private void InitializeScoreLabel()
        {
            scoreLabel.Location = new Point(200, 10);
            scoreLabel.Size = new Size(100, 30);
            scoreLabel.Text = "Score: 0";
            Controls.Add(scoreLabel);
        }

        private void InitializeTimer()
        {
            timer.Interval = 5000; 
            timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Array.Clear(hasBeenRevealed, 0, hasBeenRevealed.Length);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    isBlack[i, j] = rand.NextDouble() > 0.5;
                    buttons[i, j].BackColor = isBlack[i, j] ? Color.Black : Color.White;
                }
            }
            score = 0;
            UpdateScore();
            timer.Start(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (isBlack[i, j]) buttons[i, j].BackColor = Color.White;
                }
            }
            timer.Stop();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int i = clickedButton.Location.X / 40;
            int j = (clickedButton.Location.Y - 50) / 40;

            if (isBlack[i, j] && !hasBeenRevealed[i, j])
            {
                clickedButton.BackColor = Color.Black;
                hasBeenRevealed[i, j] = true;
                score++;
            }
            else if (!isBlack[i, j])
            {
                MessageBox.Show("Incorrect! Try again.");
                score--;
            }
            UpdateScore();
        }

        private void UpdateScore()
        {
            scoreLabel.Text = $"Score: {score}";
        }
    }
}
