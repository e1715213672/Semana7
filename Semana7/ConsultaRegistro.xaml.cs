using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semana7.Modelos;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> tablaEstudiantes;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
            get();

        }

        public async void get() {
            try
            {
                var Resultado = await con.Table<Estudiante>().ToListAsync();
                tablaEstudiantes = new ObservableCollection<Estudiante>(Resultado);
                listaUsuarios.ItemsSource = tablaEstudiantes;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void listaUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Estudiante)e.SelectedItem;
            var item = obj.Id.ToString();
            var ID = Convert.ToInt32(item);
            
            var nombreItem = obj.Nombre;
            string nombre = nombreItem.ToString();
            var usuarioItem = obj.Usuario.ToString();
            string usuario = usuarioItem.ToString();
            var contrasenaItem = obj.Contrasena.ToString();
            string contrasena = contrasenaItem.ToString();

            Navigation.PushAsync(new Elemento(ID,nombre,usuario,contrasena));
            
        }
    }
}