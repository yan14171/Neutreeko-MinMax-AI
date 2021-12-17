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
using System.ComponentModel;

namespace neutreekoCSHARP
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        bool isPawnSelected = false;
        byte selectedPawnRow;
        byte selectedPawnColumn;
        int[,] field;

        UIGame game = new UIGame();

        public event PropertyChangedEventHandler PropertyChanged;

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
            new BoardOperations(field).displayBoard();
            field = game.AIMove();
            ShowBoard(field);
        }

        private void OnPawnSelect(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hoorah!");
            isPawnSelected = true;
            selectedPawnColumn = ((e.Source as Button).DataContext as Pawn).GridColumn;
            selectedPawnRow = ((e.Source as Button).DataContext as Pawn).GridRow;
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Key Down!");
            if (isPawnSelected && e.Key.ToString().Contains("NumPad"))
            {
                Console.WriteLine("Pawn Selected");
                var direction = (byte)e.Key.ToString()[6] - (byte)'0';
                field = game.turn(selectedPawnRow, selectedPawnColumn, direction);
                new BoardOperations(field).displayBoard();
                ShowBoard(field);
            }
            isPawnSelected=false;
        }

        private void ShowBoard(int[,] board)
        {
            var newPawnList = new List<Pawn>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j] == 1)
                        newPawnList.Add(new Pawn() { GridRow = (byte)i, GridColumn = (byte)j, Style = (Style)Resources["FirstPlayerPawn"]});
                    else if (board[i, j] == 2)
                        newPawnList.Add(new Pawn() { GridRow = (byte)i, GridColumn = (byte)j, Style = (Style)Resources["SecondPlayerPawn"] });
                }
            }
            Pawns.ItemsSource = newPawnList;
            PropertyChanged?.Invoke(this, new(nameof(Pawns)));
            PropertyChanged?.Invoke(this, new(nameof(Pawn.GridRow)));
            PropertyChanged?.Invoke(this, new(nameof(Pawn.GridColumn)));
        }
    }

    public class Pawn
    {
        public System.Windows.Style Style { get; set; }

        public byte GridRow { get; set; }

        public byte GridColumn { get; set; }
    }
}
