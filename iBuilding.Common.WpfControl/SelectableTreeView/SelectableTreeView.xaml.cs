using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView
{
    /// <summary>
    /// SelectableTreeView.xaml 的交互逻辑
    /// </summary>
    public partial class SelectableTreeView : UserControl
    {
        public static readonly DependencyProperty QueryWaterMarksProperty = DependencyProperty.Register(
            nameof(QueryWaterMarks), typeof(string), typeof(SelectableTreeView), new PropertyMetadata(string.Empty));

        public string QueryWaterMarks
        {
            get => (string)GetValue(QueryWaterMarksProperty);
            set => SetValue(QueryWaterMarksProperty, value);
        }

        public SelectableTreeView()
        {
            InitializeComponent();
        }
    }
}
