using System;

namespace Project_2048_game
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            bool redo = true;
            do
            {
                string ans = "";
                do
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.WriteLine("1-New Game\n" +
                        "2-Rules\n" +
                        "3-Score History\n" +
                        "4-Quit");
                    ans = Console.ReadLine();
                } while (ans != "1" && ans != "2" && ans != "3"&&ans!="4");
                Console.Clear();
                switch (ans)
                {
                    case "1":
                        int size = 0;
                        bool check = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("What size do you prefer? (the size need to be a number >= 2)");
                            check = int.TryParse(Console.ReadLine(), out size);
                        } while (size < 2 || check == false);
                        Game game = new Game(size);
                        game.AddCell();
                        
                        Console.CursorVisible = false;
                        game.DisplayField();
                        game.DisplayCell();
                        bool entry = false;
                        bool isover = false;
                        bool addcell = true;
                        int highestscore = (size *512);
                        do
                        {
                            Console.SetCursorPosition(0, 0);
                            game.DisplayField();
                            game.DisplayCell();
                            Console.SetCursorPosition(0, size * 2 + 1);
                            Console.WriteLine($"Score: {game.Score}\tScore to win: {highestscore} \n" +
                                $"Moves: {game.Moves}\n" +
                                $"High Score: {game.HighScore}");
                            Console.SetCursorPosition(size * 6 + 4, size + 2);
                            Console.Write("By Antoine Schmitt");
                            Console.SetCursorPosition(0, 0);
                            entry = game.KeyBoard(Console.ReadKey(true).Key);
                            addcell=game.AddCell();
                            isover = game.IsOver();
                        } while ((isover == false||entry==false)&&addcell==true);
                        game.WriteScores(game.Score);
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("The goal of the game is to slide tiles on a grid, to combine tiles of the same values.\n" +
                            "To win the player has to create the number written as the score to win (size of the board multiplied by 512).\n" +
                            "You mustn't fill the entire grid otherwise you will lose\n" +
                            "The High Score written depend of the size of the game");
                        Console.ReadKey();
                        break;

                    case "3":
                        Game game1 = new Game(2);
                        game1.ScoresHistory();
                        Console.ReadKey();
                        break;

                    default:
                        redo = false;
                        break;
                }
            } while (redo==true);
        }
    }
}
