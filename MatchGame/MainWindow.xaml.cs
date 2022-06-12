using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            List<string> animalEmojis = new List<string>()
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
            Random random = new Random();
            foreach (TextBlock textBlock in gameGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(animalEmojis.Count);
                string animalEmoji = animalEmojis[index];
                animalEmojis.RemoveAt(index);
                textBlock.Text = animalEmoji;
            }
        }

        private TextBlock previousElement;

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock selectedElement = sender as TextBlock;
            if (previousElement == null)
            {
                previousElement = selectedElement;
                previousElement.Visibility = Visibility.Hidden;
            } else if (selectedElement.Text.Equals(previousElement.Text))
            {
                previousElement.Visibility = Visibility.Hidden;
                selectedElement.Visibility = Visibility.Hidden;
                previousElement = null;
            } else
            {
                previousElement.Visibility = Visibility.Visible;
                previousElement = null;
            }
        }
    }
}
