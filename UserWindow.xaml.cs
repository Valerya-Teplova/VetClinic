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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User user;
        public UserWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            UserNameLbl.Content = user.Employee.SurName + " " + user.Employee.Name + " " + user.Employee.Patronymic;
            UserRoleLbl.Content = user.Role.RoleName;
            dataGridReception.Columns[1].ClipboardContentBinding.StringFormat = "d";
            ReceptionDB();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void ReceptionDB()
        {
            Db.DBClass.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            var reception_list = from receptionservice in Db.DBClass.GetContext().ServiceReception
                                 join reception in Db.DBClass.GetContext().Reception on receptionservice.IdReception equals reception.IdReception
                                 join service in Db.DBClass.GetContext().Service on receptionservice.IdService equals service.IdService
                                 join animal in Db.DBClass.GetContext().Animal on reception.IdAnimal equals animal.IdAnimal
                                 join employe in Db.DBClass.GetContext().Employee on reception.IdEmployee equals employe.IdEmployee
                                 select new
                                 {
                                     idAnim = receptionservice.IdReception,
                                     idAn = animal.IdAnimal,
                                     nameAni = animal.AnimalName,
                                     dateP = reception.Date,
                                     timeP = reception.Time,
                                     Serv = service.ServiceName + "\n" + service.Price + " рублей",
                                     Employ = employe.SurName + "\n" + employe.Name + " " + employe.Patronymic
                                 };

            dataGridReception.ItemsSource = reception_list.ToList();
        }

        private void AddReception_Click(object sender, RoutedEventArgs e)
        {
            Button addbutton = sender as Button;
            AddReceptionWindow addReceptionWindow = new AddReceptionWindow { Owner = this };
            addReceptionWindow.Show();
        }

        private void edBut_Click(object sender, RoutedEventArgs e)
        {
            Button editbutton = sender as Button;
            //EditReceptionWindow editReceptionWindow = new EditReceptionWindow((Reception)editbutton.Tag);            
            var editReceptionWindow = new EditReceptionWindow(Db.DBClass.GetContext().Reception.Where(p => p.IdReception == (int)editbutton.Tag).First());
            editReceptionWindow.Owner = this;
            editReceptionWindow.Show();

        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            Button clientButton = sender as Button;
            ClientListWindow clientListWindow = new ClientListWindow { Owner = this };
            clientListWindow.Show();
        }
    }
}
