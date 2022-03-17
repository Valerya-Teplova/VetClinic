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
    /// Логика взаимодействия для EditReceptionWindow.xaml
    /// </summary>
    public partial class EditReceptionWindow : Window
    {
        public Reception receptions { get; set; }
        public EditReceptionWindow(Reception reception)
        {
            InitializeComponent();
            receptions = reception;
            DataContext = reception;
            AnimalNameCmB.ItemsSource = Db.DBClass.GetContext().Animal.ToList();            
            UpdateServiceList();
        }

        private void UpdateServiceList()
        {
            ServiceNameCmB.ItemsSource = Db.DBClass.GetContext().Service.ToList().Except(receptions.ServiceReception.Select(p => p.Service)).ToList();
            serviceListView.ItemsSource = null;
            serviceListView.ItemsSource = receptions.ServiceReception;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Db.DBClass.GetContext().SaveChanges();
            ((UserWindow)this.Owner).ReceptionDB();
            MessageBox.Show("Информация изменена");
            this.Close();
        }

        private void addServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            Service service = ServiceNameCmB.SelectedItem as Service;
            if(service != null)
            {
                Db.DBClass.GetContext().ServiceReception.Add(new ServiceReception()
                {
                    Service = service,
                    Reception = receptions
                });
                UpdateServiceList();
            }
        }

        private void deleteServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if(serviceListView.SelectedItem != null)
            {
                receptions.ServiceReception.Remove(serviceListView.SelectedItem as ServiceReception);
                UpdateServiceList();
            }
            else
            {
                MessageBox.Show("Ошибка удаления услуги.");
            }
        }
    }
}
