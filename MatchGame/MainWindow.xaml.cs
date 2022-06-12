using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random random = new Random();
        private DispatcherTimer timer;
        private int elapsedTicks;
        private int matchedPairs;
        private TextBlock previousElement;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeClock()
        {
            if (elapsedTicks > 0)
            {
                elapsedTicks = 0;
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            clockComponent.Text = (++elapsedTicks * .1).ToString("0.0s");
            if (matchedPairs == 8)
            {
                timer.Stop();
                clockComponent.Text = clockComponent.Text + " - " + " Jeszcze raz?";
            }
        }

        private void InitializeGrid()
        {
            InitializeClock();
            foreach (TextBlock textBlock in gameGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Visibility != Visibility.Visible)
                {
                    textBlock.Visibility = Visibility.Visible;
                }
            }

            List<string> emojis = new List<string>()
            {
                    "🦊", "🦊",
                    "🦍", "🦍",
                    "🦁", "🦁",
                    "🦝", "🦝",
                    "🦧", "🦧",
                    "🦄", "🦄",
                    "🦌", "🦌",
                    "🐗", "🐗",
            };
            foreach (TextBlock textBlock in gameGrid.Children.OfType<TextBlock>())
            {
                int remainingEmojis = emojis.Count;
                if (remainingEmojis == 0)
                {
                    break;
                }
                int index = random.Next(emojis.Count);
                string emoji = emojis[index];
                emojis.RemoveAt(index);
                textBlock.Text = emoji;
            }
        }
        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock selectedElement = sender as TextBlock;
            if (selectedElement.Name.Equals(clockComponent.Name))
            {
                if (matchedPairs == 8)
                {
                    elapsedTicks = 0;
                    matchedPairs = 0;
                    InitializeGrid();
                }
            }
            else
            {

                if (previousElement == null)
                {
                    previousElement = selectedElement;
                    previousElement.Visibility = Visibility.Hidden;
                }
                else if (selectedElement.Text.Equals(previousElement.Text))
                {
                    previousElement.Visibility = Visibility.Hidden;
                    selectedElement.Visibility = Visibility.Hidden;
                    previousElement = null;
                    matchedPairs++;
                }
                else
                {
                    previousElement.Visibility = Visibility.Visible;
                    previousElement = null;
                }
            }
        }
    }
}
