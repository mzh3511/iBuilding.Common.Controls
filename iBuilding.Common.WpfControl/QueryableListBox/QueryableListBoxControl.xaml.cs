using System.Windows;
using System.Windows.Controls;

namespace RanOpt.iBuilding.Common.WpfControl.QueryableListBox
{
    /// <summary>
    /// QueryableListBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class QueryableListBoxControl : UserControl
    {
        public static readonly DependencyProperty QueryWaterMarksProperty = DependencyProperty.Register(
            nameof(QueryWaterMarks), typeof(string), typeof(QueryableListBoxControl), new PropertyMetadata("WaterMarks"));

        public string QueryWaterMarks
        {
            get => (string)GetValue(QueryWaterMarksProperty);
            set => SetValue(QueryWaterMarksProperty, value);
        }

        public QueryableListBoxControl()
        {
            InitializeComponent();
        }
    }
}
