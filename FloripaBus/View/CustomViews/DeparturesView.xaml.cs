using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FloripaBusService.Model;
using Xamarin.Forms;

namespace FloripaBus.View.CustomViews
{
    public partial class DeparturesView : ContentView
    {
        #region Properties
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public ObservableCollection<Departure> Departures
        {
            get { return (ObservableCollection<Departure>)GetValue(DeparturesProperty); }
            set { SetValue(DeparturesProperty, value); }
        }

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        #endregion

        #region Bindable properties
        public static readonly BindableProperty DeparturesProperty = BindableProperty.Create<DeparturesView, ObservableCollection<Departure>>(
            d => d.Departures,
            new ObservableCollection<Departure>(),
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<ObservableCollection<Departure>>(DeparturesChanged));

        public static readonly BindableProperty HeaderProperty = BindableProperty.Create<DeparturesView, string>(
            d => d.Header,
            string.Empty,
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<string>(HeaderChanged));

        public static readonly BindableProperty ColumnsProperty = BindableProperty.Create<DeparturesView, int>(
            d => d.Columns,
            1,
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<int>(ColumnsChanged));
        #endregion

        #region Delegate methods
        private static void HeaderChanged(BindableObject bindable, string oldValue, string newValue)
        {
            ((DeparturesView)bindable).UpdateHeader();
        }

        private static void DeparturesChanged(BindableObject bindable, ObservableCollection<Departure> oldValue, ObservableCollection<Departure> newValue)
        {
            ((DeparturesView)bindable).UpdateContent();
        }

        private static void ColumnsChanged(BindableObject bindable, int oldValue, int newValue)
        {
            ((DeparturesView)bindable).UpdateContent();
        }
        #endregion

        #region Update methods
        private void UpdateHeader()
        {
            HeaderLabel.Text = Header;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => { ChangeContentVisibility(); };

            HeaderLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void ChangeContentVisibility()
        {
            _viewWithContent.IsVisible = !_viewWithContent.IsVisible;
        }

        private void UpdateContent()
        {
            if (Departures != null && Departures.Count > 0)
            {
                UpdateGrid();
                _viewWithContent = Grid;
            }
            else
            {
                UpdateMessageLabel();
                _viewWithContent = MessageLabel;
            }
        }

        private void UpdateMessageLabel()
        {
            MessageLabel.Text = "No departures for this day.";
        }
        #endregion

        #region Grid
        private void UpdateGrid()
        {
            Grid.Children.Clear();
            SetRowsAndColumnDefinitions();
            FillGridWithDepartures();
        }

        private void SetRowsAndColumnDefinitions()
        {
            Grid.ColumnDefinitions = GetColumns();
            Grid.RowDefinitions = GetRows();
        }

        private ColumnDefinitionCollection GetColumns()
        {
            var columns = new ColumnDefinitionCollection();
            for (int i = 0; i < Columns; i++)
            {
                columns.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            return columns;
        }

        private RowDefinitionCollection GetRows()
        {
            var rows = new RowDefinitionCollection();
            int rowsNeeded = CalculateRowsNeeded();
            for (int i = 0; i < rowsNeeded; i++)
            {
                rows.Add(new RowDefinition() { Height = GridLength.Auto });
            }
            return rows;
        }

        private int CalculateRowsNeeded()
        {
            return (Departures.Count + Columns - 1) / Columns;
        }

        private void FillGridWithDepartures()
        {
            int row = 0;
            int col = 0;

            foreach (Departure dep in Departures)
            {
                Grid.Children.Add(CreateCellLabel(dep.Time), col, row);
                IncrementRowsAndCols(ref row, ref col);
            }
        }

        private Label CreateCellLabel(string text)
        {
            return new Label()
            {
                Text = text,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
        }

        private void IncrementRowsAndCols(ref int row, ref int col)
        {
            if (++col == Grid.ColumnDefinitions.Count)
            {
                col = 0;
                row++;
            }
        }
        #endregion

        private Xamarin.Forms.View _viewWithContent;

        public DeparturesView()
        {
            InitializeComponent();
        }
    }
}