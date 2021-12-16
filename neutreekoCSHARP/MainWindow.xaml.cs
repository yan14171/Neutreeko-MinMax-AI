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
using System.Runtime.InteropServices;

namespace neutreekoCSHARP
{
    public partial class MainWindow : Window
    {
        bool isPawnSelected = false;
        public MainWindow()
        {
            InitializeComponent();
            AllocConsole();
            var pawnList = new List<Pawn>{
                new Pawn(){Style = (Style)Resources["FirstPlayerPawn"], GridColumn = 2, GridRow = 1},
                new Pawn(){Style = (Style)Resources["FirstPlayerPawn"], GridColumn = 1, GridRow = 4},
                new Pawn(){Style = (Style)Resources["FirstPlayerPawn"], GridColumn = 3, GridRow = 4},
                new Pawn(){Style = (Style)Resources["SecondPlayerPawn"], GridColumn = 3, GridRow = 0},
                new Pawn(){Style = (Style)Resources["SecondPlayerPawn"], GridColumn = 1, GridRow = 0},
                new Pawn(){Style = (Style)Resources["SecondPlayerPawn"], GridColumn = 2, GridRow = 3},
            };
            Pawns.ItemsSource = pawnList; 
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int AllocConsole();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AllocConsole();
        }

        private void OnPawnSelect(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hoorah!");
            isPawnSelected = true;
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Key Down!");
            if(isPawnSelected)
                Console.WriteLine("Pawn Selected");
            isPawnSelected=false;
        }
    }

    public class Pawn
    {
        public System.Windows.Style Style { get; set; }

        public byte GridRow { get; set; }

        public byte GridColumn { get; set; }
    }
}
