using System;
using System.Collections.Generic;
using System.IO;
namespace Project_2048_game
{
    /// <summary>
    /// Class Game
    /// </summary>
    public class Game
    {
        int Length { get; set; }
        public int Score { get; set; }
        public int HighScore { get; set; }
        public int Moves { get; set; }
        int[,] Field { get; set; }
        /// <summary>
        /// Game class
        /// </summary>
        /// <param name="length">Length of the grid</param>
        public Game(int length)
        {
            if (length >= 2) Length = length; else Length = 2;
            Field = new int[Length, Length];
            int highscore = 0;
            try
            {
                string line = "";
                using (StreamReader str = new StreamReader("Scores.txt"))
                {
                    while ((line = str.ReadLine()) is not null)
                    {
                        string[] SplitData = line.Split(" ");
                        if (int.Parse(SplitData[SplitData.Length-1]) >= highscore&&int.Parse(SplitData[2])==Length)
                        {
                            
                            highscore = int.Parse(SplitData[SplitData.Length-1]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            HighScore = highscore;
        }

        #region Manage Score Methods
        /// <summary>
        /// Show the Scores History
        /// </summary>
        public void ScoresHistory()
        {
            try
            {
                string line = "";
                int i = 0;
                using (StreamReader str = new StreamReader("Scores.txt"))
                {
                    Console.Clear();
                    while ((line = str.ReadLine()) is not null)
                    {
                        
                        Console.WriteLine("Game " + i+line);
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Write the new score at the end of the game
        /// </summary>
        /// <param name="score">New score to write in the file</param>
        public void WriteScores(int score)
        {
            try
            {
                using (StreamWriter stw = File.AppendText("Scores.txt"))
                {
                    stw.WriteLine(": Size: " + Length+" * "+Length + " Score: " + score);
                    Console.Clear();
                    Console.WriteLine("Score saved");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Modify the current and the HighScore if necessary
        /// </summary>
        /// <param name="value">Value to add to the current score</param>
        public void CurrentScore(int value)
        {
            Score += value;
            if (Score >= HighScore) HighScore = Score;
        }
        #endregion

        #region Display Game
        /// <summary>
        /// Show the grid
        /// </summary>
        public void DisplayField()
        {
            Console.Clear();
            for (int i = 0; i < Length + 1; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Console.Write(" ----");
                }
                Console.WriteLine();

                if (i == Length)
                {
                    break;
                }
                for (int j = 0; j < Length + 1; j++)
                {
                    Console.Write("|    ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Show the cells
        /// </summary>
        public void DisplayCell()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Console.SetCursorPosition((j) * 5 + 1, (i) * 2 + 1);
                    if (Field[i, j] == 0)
                    {

                        Console.Write("    ");
                    }
                    else
                    {
                        SetColor(Field[i, j]);
                        Console.Write(Field[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        #endregion

        #region Direction Methods
        /// <summary>
        /// Four different direction 
        /// </summary>
        enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }

        /// <summary>
        /// Take the keyboard entry and 'transform' into a direction
        /// </summary>
        /// <param name="key">Keyboard entry</param>
        /// <returns>False if the entry is not one of the four arrows; True otherwise</returns>
        public bool KeyBoard(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Move(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    Move(Direction.Down);
                    break;
                case ConsoleKey.RightArrow:
                    Move(Direction.Right);
                    break;
                case ConsoleKey.LeftArrow:
                    Move(Direction.Left);
                    break;
                default:
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Take the direction and call the right method
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="direction">Direction</param>
        void Move(Direction direction)
        {

            switch (direction)
            {
                case Direction.Up:
                    DirectionMoveUp();
                    Moves++;
                    break;
                case Direction.Down:
                    DirectionMoveDown();
                    Moves++;
                    break;
                case Direction.Right:
                    DirectionMoveRight();
                    Moves++;
                    break;
                case Direction.Left:
                    DirectionMoveLeft();
                    Moves++;
                    break;
            }
        }
        #endregion

        #region Manage Cell Methods
        /// <summary>
        /// Set the number to a certain color
        /// </summary>
        /// <param name="value">Cell number</param> 
        public void SetColor(int value)
        {
            switch(value)
            {
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 16:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 32:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 64:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 128:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 256:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 512:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 1024:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 2048:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

            }
        }

        /// <summary>
        /// Double the cell value
        /// </summary>
        /// <param name="i">Row</param>
        /// <param name="j">Column</param>
        void DoubleValue(int i, int j)
        {
            Field[i, j] = Field[i, j]*2;
            CurrentScore(Field[i, j]);
        }
        /// <summary>
        /// Add a new number to the grid
        /// </summary>
        /// <returns>True if adding a number is possible; False otherwise</returns>
        public bool AddCell()
        {
            List<int> Row = new List<int>();
            List<int> Col = new List<int>();
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (Field[i, j] == 0) { Row.Add(i); Col.Add(j); }
                }
            }
            if (Row.Count != 0 && Col.Count != 0)
            {
                Random random = new Random();
                int count = random.Next(0, Row.Count);
                int X = Row[count];
                int Y = Col[count];
                Field[X, Y] = 2;
                return true;
            }
            else
            {
                DisplayCell();
                Console.SetCursorPosition(Length * 5 + 3, Length * 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have lost");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();

                return false;
            }
        }
        #endregion

        #region Move Cells Methods

        /// <summary>
        /// MoveUp method
        /// </summary>
        void DirectionMoveUp()
        {  
            int i = 0;
            int j = 0;
            
            while(j<=Length-1)
            {
                bool redo = false;
                if (i < Length - 1 && Field[i, j] == 0)
                {
                    int count = 1;
                    while(i + count < Length && Field[i+count,j]==0)
                    {
                        count++;
                    }
                    if(count+i<Length)
                    {
                        MoveCellUp(i, j);
                        redo = true;
                    }
                    
                }
                else if (i < Length - 1 && Field[i, j] == Field[i + 1, j] && Field[i, j] != 0)
                {
                    DoubleValue(i, j);

                    Field[i + 1, j] = 0;
                    redo = true;
                }               
                if (redo == true && (i == 0|| i == 1))
                {
                    i = 0;
                }
                if ( i > 1 && i < Length - 1&& redo == true)
                {
                    i--;
                }
                 if(redo==false)
                {
                    i++;
                }
                if (i>Length-1&&j<Length&&redo==false)
                {
                    i = 0;
                    j++;
                }

                //if (i == Length - 1 && j == Length - 1)return;                   
            } 
        }
        /// <summary>
        /// Move up the whole column
        /// </summary>
        /// <param name="i">Row</param>
        /// <param name="j">Column</param>
        void MoveCellUp(int i, int j )
        {
            int z = 0;
            while (Field[i, j] == 0 && z != Length - 1)
            {
                int k;
                    
                for (k = i; k < Length - 1; k++)
                {
                    Field[k, j] = Field[k + 1, j];
                }
                if(k>i) Field[Length - 1, j] = 0;
                z++;
            }
            
        }
        /// <summary>
        /// MoveDown method
        /// </summary>
        void DirectionMoveDown()
        {
            int i = Length-1;
            int j = 0;
            while (j <= Length - 1)
            {
                bool redo = false;
                if (i >0 && Field[i, j] == 0)
                {
                    int count = 1;
                    while (i - count >-1 && Field[i - count, j] == 0)
                    {
                        count++;
                    }
                    if (i-count >-1)
                    {
                        MoveCellDown(i, j);
                        redo = true;
                    }

                }
                else if (i > 0 && Field[i, j] == Field[i - 1, j] && Field[i, j] != 0)
                {
                    DoubleValue(i, j);

                    Field[i - 1, j] = 0;
                    redo = true;
                }
                if (redo == true && (i ==Length-1 || i == Length-2))
                {
                    i = Length-1;
                }
                /*if (redo == true && (i == 1))
                {
                    i = 1;
                }*/
                if (i <Length-2 && i >0 && redo == true)
                {
                    i++;
                }
                if (redo == false)
                {
                    i--;
                }
                if (i <0 && j < Length && redo == false)
                {
                    i = Length-1;
                    j++;
                }

                //if (i == Length - 1 && j == Length - 1)return;                   
            }
        }
        /// <summary>
        /// Move the whole column down
        /// </summary>
        /// <param name="i">Row</param>
        /// <param name="j">Column</param>
        void MoveCellDown(int i, int j)
        {
            int z = 0;
            while (Field[i, j] == 0 && z != Length-1)
            {
                int k;

                for (k = i; k >0; k--)
                {
                    Field[k, j] = Field[k - 1, j];
                }
                if (k < i) Field[0, j] = 0;
                z++;
            }
        }
        /// <summary>
        /// MoveRight method
        /// </summary>
        void DirectionMoveRight()
        {
            int i = 0;
            int j = Length-1;
            while (i<=Length-1)
            {
                bool redo = false;
                if (j > 0 && Field[i, j] == 0)
                {
                    int count = 1;
                    while (j - count > -1 && Field[i , j - count] == 0)
                    {
                        count++;
                    }
                    if (j - count > -1)
                    {
                        MoveCellRight(i, j);
                        redo = true;
                    }

                }
                else if (j > 0 && Field[i, j] == Field[i , j-1] && Field[i, j] != 0)
                {
                    DoubleValue(i, j);

                    Field[i , j - 1] = 0;
                    redo = true;
                }
                if (redo == true && (j == Length-1 || j ==Length-2))
                {
                    j = Length-1;
                }
                /*if (redo == true && (i == 1))
                {
                    i = 1;
                }*/
                if (j < Length - 2 && j > 0 && redo == true)
                {
                    j++;
                }
                if (redo == false)
                {
                    j--;
                }
                if (j < 0 && i < Length && redo == false)
                {
                    j = Length - 1;
                    i++;
                }

                //if (i == Length - 1 && j == Length - 1)return;                   
            }
        }
        /// <summary>
        /// Move the whole row right
        /// </summary>
        /// <param name="i">Row</param>
        /// <param name="j">Column</param>
        void MoveCellRight(int i,int j)
        {
            int z = 0;
            while (Field[i, j] == 0 && z != Length - 1)
            {
                int k;

                for (k = j; k >0; k--)
                {
                    Field[i, k] = Field[i, k - 1];
                }
                if (k < j) Field[i, 0] = 0;
                z++;
            }
        }
        /// <summary>
        /// MoveLeft method
        /// </summary>
        void DirectionMoveLeft()
        {
            int i = 0;
            int j = 0;
            
            while(i<=Length-1)
            {
                bool redo = false;
                if (j < Length - 1 && Field[i, j] == 0)
                {
                    int count = 1;
                    while(j + count < Length && Field[i,j+count]==0)
                    {
                        count++;
                    }
                    if(count+j<Length)
                    {
                        MoveCellLeft(i, j);
                        redo = true;
                    }
                    
                }
                else if (j < Length - 1 && Field[i, j] == Field[i, j+1] && Field[i, j] != 0)
                {
                    DoubleValue(i, j);

                    Field[i , j+1] = 0;
                    redo = true;
                }               
                if (redo == true && (j == 0|| j == 1))
                {
                    j = 0;
                }
                /*if (redo == true && (i == 1))
                {
                    i = 1;
                }*/
                if ( j > 1 && j < Length - 1&& redo == true)
                {
                    j--;
                }
                 if(redo==false)
                {
                   j++;
                }
                if (j>Length-1&&i<Length&&redo==false)
                {
                   j = 0;
                    i++;
                }

                //if (i == Length - 1 && j == Length - 1)return;                   
            } 
        }
        /// <summary>
        /// Move the whole row left
        /// </summary>
        /// <param name="i">Row</param>
        /// <param name="j">Column</param>
        void MoveCellLeft(int i,int j)
        {
            int z = 0;
            while (Field[i, j] == 0 && z != Length - 1)
            {
                int k;

                for (k = j; k < Length - 1; k++)
                {
                    Field[i, k] = Field[i, k+1];
                }
                if (k > j) Field[i, Length-1] = 0;
                z++;
            }
        }

        #endregion

        /// <summary>
        /// Method wich check if the game is over
        /// </summary>
        /// <returns>True if the game is over; False otherwise</returns>
        public bool IsOver()
        {
            int count = 0;
            foreach(int i in Field)
            {
                if (i == 0) count++;
            }
            if (count>=0)
            {
                foreach (int i in Field)
                {
                    if (i>=Length*512)
                    {
                        Console.SetCursorPosition(Length*2+3,Length*2);
                        Console.WriteLine("You have won");
                        Console.ReadKey();
                        return true;
                    }
                }               
            }
            return false;
        }
    }
}