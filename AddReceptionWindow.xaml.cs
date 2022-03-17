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

namespace VetClinic
{
    /// <summary>
    /// Логика взаимодействия для AddReceptionWindow.xaml
    /// </summary>
    public partial class AddReceptionWindow : Window
    {
        public AddReceptionWindow()
        {
            InitializeComponent();

            AnimalNameCmB.ItemsSource = Db.DBClass.GetContext().Animal.ToList();
            ServiceNameCmB.ItemsSource = Db.DBClass.GetContext().Service.ToList();
            UserNameCmB.ItemsSource = Db.DBClass.GetContext().Employee.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (AnimalNameCmB.SelectedItem == null)
                error.AppendLine("Вы не выбрали кличку животного");
            if (DateRecepDP.SelectedDate == null)
                error.AppendLine("Вы не указали дату приема");
            if (TimeRecepTxB.Text == null)
                error.AppendLine("Вы не указали время приема");
            if (AnamnesTxB.Text == null)
                error.AppendLine("Вы не указали анамнез пациента");
            if (ServiceNameCmB.SelectedItem == null)
                error.AppendLine("Вы не выбрали услугу");


            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            Reception reception = new Reception()
            {
                Date = DateRecepDP.SelectedDate.Value,
                Time = TimeSpan.Parse(TimeRecepTxB.Text),
                Anamnesis = AnamnesTxB.Text,
                Animal = AnimalNameCmB.SelectedItem as Animal,
                Employee = UserNameCmB.SelectedItem as Employee,
                Finish = false

            };

            Db.DBClass.GetContext().Reception.Add(reception);

            Db.DBClass.GetContext().ServiceReception.Add(new ServiceReception()
            {
                Reception = reception,
                Service = ServiceNameCmB.SelectedItem as Service

            });
            Db.DBClass.GetContext().SaveChanges();
            ((UserWindow)this.Owner).ReceptionDB();
            MessageBox.Show("Данные успешно добавлены");
            Close();
        }
    }
}
