using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pomodoro_Timer
{
    public partial class Form1 : Form
    {
        public TimeSpan time { get; set; }
        public int breakCount { get; set; }
        private Boolean breakFlag = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (breakFlag == true)
            {
                startBreak();
            }
            else
            {
                startPomodoro();
            }
        }

        /// <summary>
        /// Begins a new break. Every fourth break is 15 minutes, otherwise 5 minutes.
        /// </summary>
        private void startBreak()
        {
            breakCount++;
            if (breakCount % 4 == 0)
                time = new TimeSpan(0, 15, 0);
            else
                time = new TimeSpan(0, 5, 0);
            updateTime();
            timerPomodoro.Start();
            btnStart.Enabled = false;
        }

        /// <summary>
        /// Begins a new Pomodoro session.
        /// Updates the title bar and starts the timer.
        /// </summary>
        private void startPomodoro()
        {
            time = new TimeSpan(0, 25, 0);
            updateTime();
            timerPomodoro.Start();
            btnStart.Enabled = false;
            txtTask.Enabled = false;

            this.Text = txtTask.Text + " - " + pomodoroTime(time);
        }


        private void timerPomodoro_Tick(object sender, EventArgs e)
        {
            time = time.Subtract(new TimeSpan(0, 0, 1));
            updateTime();
        }

        /// <summary>
        /// Returns the time left on the clock.
        /// </summary>
        /// <param name="timeLeft">Time left on the clock.</param>
        /// <returns>String format of time left, in minutes and seconds.</returns>
        private string pomodoroTime(TimeSpan timeLeft)
        {
            return time.Minutes.ToString() + ":" + time.Seconds.ToString("D2");
        }

        private void updateTime()
        {
            lblTime.Text = pomodoroTime(time);

            if (breakFlag == true)
                this.Text = "Break - " + pomodoroTime(time);
            else
                this.Text = txtTask.Text + " - " + pomodoroTime(time);
            
            
            if( time.Equals(TimeSpan.Zero) )
            {
                timerPomodoro.Stop();
                btnStart.Enabled = true;
                if (breakFlag == true)
                {
                    txtTask.Enabled = true;
                    txtTask.Text = "";
                    btnStart.Text = "Start";
                    breakFlag = false;
                }
                else
                {
                    btnStart.Text = "Start break";
                    breakFlag = true;
                }
            }
        }
    }
}
