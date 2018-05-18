using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RanOpt.iBuilding.Common.WpfControl.QueryableListBox.ViewModels;
using RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels;

namespace Test.WpfControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var students = new List<Student>();
            students.Add(new Student { Name = "王伟", City = "济南", Area = "历下区", Sex = "男", Age = 15 });
            students.Add(new Student { Name = "王微", City = "济南", Area = "历下区", Sex = "女", Age = 16 });
            students.Add(new Student { Name = "王维", City = "济南", Area = "历城区", Sex = "男", Age = 17 });
            students.Add(new Student { Name = "李薇薇", City = "济南", Area = "历城区", Sex = "女", Age = 15 });
            students.Add(new Student { Name = "李向北", City = "青岛", Area = "市北区", Sex = "男", Age = 16 });
            students.Add(new Student { Name = "李楠楠", City = "青岛", Area = "市北区", Sex = "女", Age = 17 });
            students.Add(new Student { Name = "王娜娜", City = "青岛", Area = "市南区", Sex = "男", Age = 16 });
            students.Add(new Student { Name = "张霞", City = "青岛", Area = "市南区", Sex = "女", Age = 15 });

            var viewModel = new ContainerViewModel();
            viewModel.AddRange(students, student => student.Name);

            viewModel.Selection = students.GetRange(2, 1);
            StuListBox.DataContext = viewModel;

            var groupList = new List<IGroupBy<Student>>
            {
                new GroupBy<Student,string>(student=>student.City,city=>city,(cityX,cityY)=> Comparer<string>.Default.Compare(cityY, cityX)),
                new GroupBy<Student,string,string>((student,city)=>student.Area,area=>area,(areaX,areaY)=> Comparer<string>.Default.Compare(areaY, areaX)),
                new GroupBy<Student,string,int>((student,area)=>student.Age,age=>age.ToString("D5"),(ageX,ageY)=> Comparer<int>.Default.Compare(ageX, ageY)),
            };

            var groupByExecutor = new GroupExecutor<Student>(groupList, new GroupBy<Student, Student>(student => student, student => student.Name));
            var treeViewViewModel = groupByExecutor.Execute(students);
            StuTreeView.DataContext = treeViewViewModel;
        }

        public class Student
        {
            public string Name { get; set; }

            public string City { get; set; }

            public string Area { get; set; }

            public string Sex { get; set; }

            public int Age { get; set; }
        }
    }
}
