using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CSharpProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool turn = true; //true = X turn; false = O turn
        private int turn_count = 0;

        private int[,] gameArr = new int[,] // array of ints, as array of Strings didnt work for me (couldn't solve null pointer exception error)
        {
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };

        public MainPage()
        {
            this.InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var caller = (Button)sender; // the button currently clicked

            // returns object
            var row = caller.GetValue(Grid.RowProperty);
            var col = caller.GetValue(Grid.ColumnProperty);

            if (turn == true)
            {
                // convert object to int
                gameArr[Convert.ToInt32(row), Convert.ToInt32(col)] = 1;//X wins 
                caller.Content = "X";
                turn_count++;
                checkForWinner();//after each turn check for winner
                turn = false;

            }
            else
            {
                gameArr[Convert.ToInt32(row), Convert.ToInt32(col)] = 2;//O wins
                caller.Content = "O";
                turn_count++;
                checkForWinner();
                turn = true;

            }

            caller.IsEnabled = false;
        }

        private async void checkForWinner()
        {
            bool there_is_a_winner = false;
           
            if (turn_count < 4)
            {
                there_is_a_winner = false;
            }
            // no point in checking for the winner before 4 moves
            if (turn_count > 4)
            {
                //horizontal checks 
                if (gameArr[0, 0] == gameArr[0, 1] && gameArr[0, 1] == gameArr[0, 2] && gameArr[0, 0]!= 0)// as initially array is populated with zeros, it will check for combination of zeros as well but we dont need that (check only ones and twos)
                        {
                            there_is_a_winner = true;

                        }
                        else if (gameArr[1, 0] == gameArr[1, 1] && gameArr[1, 1] == gameArr[1, 2] && gameArr[1, 0] != 0)
                        {

                            there_is_a_winner = true;
                        }
                        else if (gameArr[2, 0] == gameArr[2, 1] && gameArr[2, 1] == gameArr[2, 2] && gameArr[2, 0] != 0)
                        {
                            there_is_a_winner = true;
                        }
                        //vertical checks
                        else if (gameArr[0, 0] == gameArr[1, 0] && gameArr[1, 0] == gameArr[2, 0] && gameArr[0, 0] != 0)
                        {
                            there_is_a_winner = true;
                        }
                        else if (gameArr[0, 1] == gameArr[1, 1] && gameArr[1, 1] == gameArr[2, 1] && gameArr[0, 1] != 0)
                        {
                            there_is_a_winner = true;
                        }
                        else if (gameArr[0, 2] == gameArr[1, 2] && gameArr[1, 2] == gameArr[2, 2] && gameArr[0, 2] != 0)
                        {
                            there_is_a_winner = true;
                        }
                        //diagonal checks
                        else if (gameArr[0, 0] == gameArr[1, 1] && gameArr[1, 1] == gameArr[2, 2] && gameArr[1, 1] != 0)
                        {
                            there_is_a_winner = true;
                        }
                        else if (gameArr[0, 2] == gameArr[1, 1] && gameArr[1, 1] == gameArr[2, 0] && gameArr[1, 1] != 0)
                        {
                            //Debug.WriteLine(there_is_a_winner);
                            there_is_a_winner = true;
                        }
                        else if (turn_count == 9)
                        {
                            await new MessageDialog("DRAW").ShowAsync();
                        }

                    }
              
       
                if (there_is_a_winner)
                {
                    String winner;
                    if (turn == false)
                    {
                        winner = "O";
                        await new MessageDialog(winner + " is the winner").ShowAsync();// add async in method's signature
                    }

                    else if (turn == true)
                    {
                        winner = "X";
                        await new MessageDialog(winner + " is the winner").ShowAsync();
                    }
            }//if turn_count > 4
        }//checkForWinner()

        private void Restart_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        }//namespace
}//page