using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmployeeManagementSystem.Windows.Components
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        public enum DialogIconType
        {
            Info, Error, Success, Warning
        }

        public enum DialogButtonType
        {
            OK, YesNo
        }

        public MessageBoxResult Result { get; private set; } = MessageBoxResult.None;

        public CustomDialog(string message, DialogIconType icon, DialogButtonType button)
        {
            InitializeComponent();

            MessageText.Text = message;

            switch (icon)
            {
                case DialogIconType.Info:
                    IconType.Text = "\uE946";
                    IconType.Foreground = new SolidColorBrush(Color.FromRgb(72, 133, 237));
                    break;
                case DialogIconType.Success:
                    IconType.Text = "\uE73E";
                    IconType.Foreground = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                    break;
                case DialogIconType.Error:
                    IconType.Text = "\uEA39";
                    IconType.Foreground = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                    break;
                case DialogIconType.Warning:
                    IconType.Text = "\uE7BA";
                    IconType.Foreground = new SolidColorBrush(Color.FromRgb(243, 156, 18));
                    break;
            }

            AddButtons(button);
        }

        private void AddButtons(DialogButtonType button)
        {
            ButtonType.Children.Clear();

            if (button == DialogButtonType.OK)
            {
                var okButton = CreateButton("OK", () =>
                {
                    Result = MessageBoxResult.OK;
                    Close();
                });
                ButtonType.Children.Add(okButton);
            }
            else if (button == DialogButtonType.YesNo)
            {
                var yesButton = CreateButton("Yes", () =>
                {
                    Result = MessageBoxResult.Yes;
                    Close();
                });

                var noButton = CreateButton("No", () =>
                {
                    Result = MessageBoxResult.No;
                    Close();
                });

                ButtonType.Children.Add(yesButton);
                ButtonType.Children.Add(noButton);
            }
        }

        private Button CreateButton(string text, Action onClick)
        {
            return new Button
            {
                Content = text,
                Width = 80,
                Height = 35,
                Margin = new Thickness(10, 0, 10, 0),
                Background = new SolidColorBrush(Color.FromRgb(142, 45, 226)),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                Cursor = Cursors.Hand,
                BorderThickness = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            }.Apply(btn => btn.Click += (s, e) => onClick());
        }

        public static MessageBoxResult Show(string message, DialogIconType icon, DialogButtonType button)
        {
            var dialog = new CustomDialog(message, icon, button);
            dialog.ShowDialog();
            return dialog.Result;
        }
    }

    public static class Extensions
    {
        public static T Apply<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }
    }
}
