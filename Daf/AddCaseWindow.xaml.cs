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
using System.Windows.Shapes;

namespace Daf
{
    /// <summary>
    /// Логика взаимодействия для AddCaseWindow.xaml
    /// </summary>
    public partial class AddCaseWindow : Window
    {
        public AddCaseWindow()
        {
            InitializeComponent();
        }

        private void SaveCase(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            string case_name = titleToDo.Text;
            DateTime? date_case = dateToDo.SelectedDate;
            string case_description = descriptionToDo.Text;



            if (this.Owner is MainWindow main)
            {
                if (case_name == "")
                {
                    case_name = "Нет названия";
                }
                if (date_case == null)
                {
                    date_case = new DateTime(0001, 01, 01);
                }
                if (case_description == "")
                {
                    case_description = "Нет описания";
                }
                main.CasesList.Add(new ToDo(case_name, date_case, case_description));

                titleToDo.Text = null;
                dateToDo.SelectedDate = null;
                descriptionToDo.Text = null;
                main.UpdateList();
                main.compleatedCases = 0;
                main.CasesProgress.Value = 0;
                main.Val.Text = main.compleatedCases.ToString();
                main.CasesProgress.Maximum = main.casesCount;
                main.Max.Text = main.casesCount.ToString();
                this.Close();
            }



        }

        private void EndToDo()
        {

        }
    }
}
