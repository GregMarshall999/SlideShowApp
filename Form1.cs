using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SlideShowApp
{
    public partial class Form1 : Form
    {
        private List<string> filteredFiles;
        private FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

        private int counter = -1;
        private int timerInterval = 1000;
        private bool isPlaying = false;

        public Form1()
        {
            InitializeComponent();

            radioButton1.Checked = true;
            slideTimer.Interval = timerInterval;
        }

        private void BrowseForImages(object sender, EventArgs e)
        {
            counter = -1;
            isPlaying = false;
            slideTimer.Stop();
            btnPlay.Text = "Play";

            DialogResult result = folderBrowser.ShowDialog();

            filteredFiles = Directory.GetFiles(folderBrowser.SelectedPath, "*.*")
                .Where(file => file.ToLower().EndsWith("jpg")
                    || file.ToLower().EndsWith("gif")
                    || file.ToLower().EndsWith("png")
                    || file.ToLower().EndsWith("bmp"))
                .ToList();

            lblFileInfo.Text = "Folder loaded - Now Press Play!";
        }

        private void PlayStopSlideShow(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                btnPlay.Text = "Stop";
                slideTimer.Start();
                isPlaying = true;
            }
            else
            {
                btnPlay.Text = "Play";
                slideTimer.Stop();
                isPlaying = false;
            }
        }

        private void PlaySlideShowTimerEvent(object sender, EventArgs e)
        {
            counter += 1;

            if (counter >= filteredFiles.Count)
            {
                counter = -1;
            }
            else
            {
                imageViewer.Image = Image.FromFile(filteredFiles[counter]);
                lblFileInfo.Text = filteredFiles[counter].ToString();
            }
        }

        private void SpeedChangedEvent(object sender, EventArgs e)
        {
            RadioButton tempRadioButton = sender as RadioButton;

            switch (tempRadioButton.Text.ToString())
            {
                case "1x":
                    timerInterval = 3000;
                    break;
                case "2x":
                    timerInterval = 1500;
                    break;
                case "3x":
                    timerInterval = 1000;
                    break;
                default:
                    break;
            }

            slideTimer.Interval = timerInterval;
        }
    }
}
