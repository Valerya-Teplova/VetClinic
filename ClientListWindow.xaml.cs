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
    /// Логика взаимодействия для ClientListWindow.xaml
    /// </summary>
    public partial class ClientListWindow : Window
    {
        public ClientListWindow()
        {
            InitializeComponent();
            
            ClientList();
        }

        public void ClientList()
        {
            Db.DBClass.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            var client_list = from animalowner in Db.DBClass.GetContext().AnimalOwner
                              join animal in Db.DBClass.GetContext().Animal on animalowner.IdAnimal equals animal.IdAnimal
                              join owner in Db.DBClass.GetContext().Owner on animalowner.IdOwner equals owner.IdOwner
                              select new
                              {
                                  AnName = animal.AnimalName,
                                  Ag = animal.Age,
                                  Gen = animal.Gender.GenderName,
                                  TypeAn = animal.TypeAnimal.TypeName,
                                  OwName = owner.SurName + " " + owner.Name + " " + owner.Patronymic,
                                  Tel = owner.Telephone
                              };
            dataGridClientList.ItemsSource = client_list.ToList();
                              
        }

        private void edBut_Click(object sender, RoutedEventArgs e)
        {
            //comment
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
