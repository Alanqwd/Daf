using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Daf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ToDo> CasesList = new List<ToDo>();

        public int compleatedCases { get; set; }

        public int casesCount { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            CasesList.Add(new ToDo("Приготовить покушать", new DateTime(2024, 1, 15), "Нет описания"));
            CasesList.Add(new ToDo("Поработать", new DateTime(2024, 1, 20), "Съездить на совещание в Москву"));
            CasesList.Add(new ToDo("Отдохнуть", new DateTime(2024, 2, 1), "Съездить в отпуск в Сочи"));

            ListBoxToDo.ItemsSource = CasesList;

            casesCount = CasesList.Count;

            CasesProgress.Maximum = casesCount;


            Max.Text = casesCount.ToString();
            Val.Text = compleatedCases.ToString();


        }



        private void AddCase(object sender, RoutedEventArgs e)
        {
            AddCaseWindow addCaseWindow = new AddCaseWindow();

            addCaseWindow.Owner = this;

            addCaseWindow.Show();

            casesCount = CasesList.Count;
            CasesProgress.Maximum = casesCount;
            Max.Text = casesCount.ToString();
        }

        public void UpdateList()
        {
            ListBoxToDo.ItemsSource = null;

            ListBoxToDo.ItemsSource = CasesList;

            casesCount = CasesList.Count;

            Max.Text = casesCount.ToString();
        }


        private void DelCase(object sender, RoutedEventArgs e)
        {



            CasesList.Remove(ListBoxToDo.SelectedItem as ToDo);
            UpdateList();
            compleatedCases = 0;
            CasesProgress.Value = compleatedCases;
            CasesProgress.Maximum = casesCount;
            Val.Text = compleatedCases.ToString();
            Max.Text = casesCount.ToString();


        }


        private void CasesPlus(object sender, RoutedEventArgs e)
        {
            compleatedCases++;
            CasesProgress.Value = compleatedCases;
            Val.Text = compleatedCases.ToString();
            Max.Text = casesCount.ToString();

            var todo = (sender as CheckBox)?.DataContext as ToDo;

            todo.IsCompleted = true;


        }

        private void CasesMin(object sender, RoutedEventArgs e)
        {
            compleatedCases--;
            CasesProgress.Value = compleatedCases;
            Val.Text = compleatedCases.ToString();
            Max.Text = casesCount.ToString();

            var todo = (sender as CheckBox)?.DataContext as ToDo;

            todo.IsCompleted = false;
        }
        private void SaveCase(object sender, RoutedEventArgs e)
{
    if (CasesList.Count == 0)
    {
        MessageBox.Show("В списке нет дел");
    }
    else
    {

        var dialog = new Microsoft.Win32.SaveFileDialog();
        dialog.FileName = "Saved_list"; 
        dialog.DefaultExt = ".txt"; 
        dialog.Filter = "Text File|*.txt"; 

     
        bool? result = dialog.ShowDialog();

        
        if (result == true)
        {
            
            string filePath = dialog.FileName;
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                foreach (var i in CasesList)
                {
                    DateOnly d = DateOnly.FromDateTime(i.TimeOfCompleating.Value);
                    if (i.IsCompleted == true)
                    {
                        writer.Write("✓");
                    }
                    else
                    {
                        writer.Write(" ");
                    }
                    writer.WriteLine(i.CaseName);
                    writer.WriteLine("");
                    writer.WriteLine(i.Description);
                    writer.WriteLine("");
                    writer.WriteLine(d.ToString());
                    writer.WriteLine("");
                    writer.WriteLine("");
                }
            }
        }

        var dia = new Microsoft.Win32.SaveFileDialog();
        dia.FileName = "Saved_list";
        dia.DefaultExt = ".json"; 
        dia.Filter = "Text File|*.json"; 

  bool? result2 = dia.ShowDialog();

        if (result2 == true)
        {
            string filePath2 = dia.FileName;
            
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(CasesList, settings);
            File.WriteAllText(filePath2, json);
        }
    }
}
    }
}
