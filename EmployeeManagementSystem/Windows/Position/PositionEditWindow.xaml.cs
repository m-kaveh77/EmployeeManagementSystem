using EmployeeManagementSystem.ViewModels.Position;
using System.Windows;

namespace EmployeeManagementSystem.Windows.Position
{
    /// <summary>
    /// Interaction logic for PositionEditWindow.xaml
    /// </summary>
    public partial class PositionEditWindow : Window
    {
        private readonly PositionEditViewModel _viewModel;
        public Models.Entities.Position? Position { get; set; }
        public bool IsReadOnlyMode { get; set; }

        public PositionEditWindow(PositionEditViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

            _viewModel.OnSubmitChangesSuccess += OnSubmitChangesSuccess;
        }

        private void OnSubmitChangesSuccess(object? sender, EventArgs e)
        {
            if (Position == null)
                Position = new Models.Entities.Position();

            Position.Id = _viewModel.Id ?? 0;
            Position.Title = _viewModel.Title;

            DialogResult = true;
        }

        protected override void OnActivated(EventArgs e)
        {
            if (Position != null)
            {
                this.Title = "Edit Position";
                _viewModel.Id = Position.Id;
                _viewModel.Title = Position.Title;
            }
            else
            {
                this.Title = "New Position";
            }
            base.OnActivated(e);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
