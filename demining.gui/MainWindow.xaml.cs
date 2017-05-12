namespace demining.gui
{
    using System.Linq;

    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    using demining.bl;
    using System.Windows.Media.Imaging;
    using System;

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _spiel;

        public MainWindow()
        {
            InitializeComponent();
            lblGameMessage.Content = string.Empty;
            pnlOptions.Visibility = Visibility.Collapsed;
            lbViewOption.Content = "(+)";
        }

        public void LoadGame()
        {
            _spiel.CreateGameField();

            var count = Spielfeld.Children.Count;
            Spielfeld.Children.RemoveRange(0, count);

            Spielfeld.Columns = _spiel.Fields.GetLength(0);
            Spielfeld.Rows = _spiel.Fields.GetLength(1);

            for (var y = 0; y < _spiel.Fields.GetLength(0); y++)
            {
                for (int x = 0; x < _spiel.Fields.GetLength(1); x++)
                {
                    var button = new Button()
                    {
                        BorderThickness = new Thickness(1, 1, 0, 0),
                        BorderBrush = new SolidColorBrush(Color.FromArgb(0x00, 0x77, 0x77, 0x6D)),
                        Background = new SolidColorBrush(Color.FromArgb(0xBF, 0x77, 0x78, 0x6D)),
                        Foreground = Brushes.White,
                        FontSize = 20,
                        FontWeight = FontWeights.Bold,
                        Name = "bt" + y + "_" + x,
                        Tag = _spiel.Fields[y, x]
                    };
                    button.Click += Feld_Click;
                    button.MouseRightButtonUp += Feld_MouseRightButtonUp;

                    Spielfeld.Children.Add(button);
                }
            }

            SetLabelMines();
        }

        private void Feld_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_spiel.Status != Status.Running)
            {
                return;
            }

            var button = sender as Button;

            if (button != null)
            {
                button.Content = _spiel.SwitchFieldmarkerForMine(button.Tag as Field);
            }

            SetLabelMines();
        }

        private void Feld_Click(object sender, RoutedEventArgs e)
        {
            if (_spiel.Status != Status.Running)
            {
                return;
            }

            var button = sender as Button;

            var tag = button?.Tag as Field;
            if (tag != null)
            {
                var position = tag.Position;
                var feld = _spiel.Fields[position.Y, position.X];

                feld.Detected = true;
                button.Content = feld.Value;

                if (feld.Type == demining.bl.Type.Mine)
                {
                    button.Background = new ImageBrush(new BitmapImage(new Uri(@"Resources\boom.png", UriKind.Relative)));
                    _spiel.Status = Status.Lose;
                }
                else
                {
                    button.Background = new SolidColorBrush(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF));
                }

                if (string.IsNullOrWhiteSpace(feld.Value))
                {
                    NachbarnAufdecken(feld);
                }
            }

            SetLabelMines();
        }

        private void NachbarnAufdecken(Field ausgangsfeld)
        {
            foreach (var feld in ausgangsfeld.Neighbours)
            {
                if (feld.Flag != Mark.no)
                {
                    continue;
                }

                if (feld.Detected == false)
                {
                    var thisButton = Spielfeld.Children.OfType<Button>().Single(w =>
                    {
                        var tag = w.Tag as Field;
                        return tag != null && tag.Position == feld.Position;
                    });

                    thisButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, thisButton));
                }
            }
        }

        private void SetLabelMines()
        {
            if (_spiel.IsThereAMine() && _spiel.Status != Status.Lose)
            {
                _spiel.Status = Status.Won;
            }

            switch (_spiel.Status)
            {
                case Status.Running:
                    lblGameMessage.Content = "Du hast " + _spiel.NumberOfFlags + " von " + _spiel.NumberOfMines + " Minen markiert.";
                    lblGameMessage.Foreground = Brushes.White;
                    break;
                case Status.Lose:
                    AllesAufdecken(true);
                    lblGameMessage.Content = "Sorry, du bist gestorben!";
                    lblGameMessage.Foreground = Brushes.Red;
                    break;
                case Status.Won:
                    AllesAufdecken();
                    lblGameMessage.Content = "Hurra, du hast gewonnen!";
                    lblGameMessage.Foreground = Brushes.Green;
                    break;
            }

            lblGameMessage.HorizontalContentAlignment = HorizontalAlignment.Center;
        }

        private void AllesAufdecken(bool onlyMines = false)
        {
            foreach (var btn in Spielfeld.Children)
            {
                var button = btn as Button;

                var feld = button?.Tag as Field;

                if (feld != null)
                {
                    if (feld.Type == demining.bl.Type.Mine)
                    {
                        if (button.Background.GetType() != typeof(ImageBrush))
                        {
                            feld.Detected = true;
                            button.Background = new ImageBrush(new BitmapImage(new Uri(@"Resources\mine.png", UriKind.Relative)));
                        }
                    }
                    else
                    {
                        if (onlyMines == false)
                        {
                            feld.Detected = true;
                            button.Background = new SolidColorBrush(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF));
                            button.Content = feld.Value;
                        }
                    }
                }
            }
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            var x = int.Parse(tbxRow.Text);
            var y = int.Parse(tbxColumn.Text);
            var m = int.Parse(tbxMines.Text);

            x = x >= 20 ? 20 : x;
            y = y >= 20 ? 20 : y;
            m = m >= x * y ? (x * y) / 2 : m;

            _spiel = new Game(x, y, m);
            pnlOptions.Visibility = Visibility.Collapsed;
            lbViewOption.Content = "(+)";
            LoadGame();
        }

        private void LbViewOption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (pnlOptions.Visibility == Visibility.Collapsed)
            {
                pnlOptions.Visibility = Visibility.Visible;
                (sender as Label).Content = "(-)";
            }
            else
            {
                pnlOptions.Visibility = Visibility.Collapsed;
                (sender as Label).Content = "(+)";
            }
        }
    }
}
