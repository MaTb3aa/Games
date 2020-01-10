using System;
using System.IO;
using System.Text;
using System.Threading;
namespace Game
{
    class Program
    {   
        static void RemoveScrollBars()
        {   // seaching ^^
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static int my_position,my_size=3,difficulity=1,speed=100,score=0,topscore=0;
        static int windowlen = Console.WindowHeight-3;
        static String[] stones = new string[windowlen];
        static char [] rockes = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';', '-' };
     
        static void moveme()
        {
            if (Console.KeyAvailable == true)
            {
                ConsoleKeyInfo input= Console.ReadKey();
                if (input.Key == ConsoleKey.LeftArrow && my_position > 0) my_position--;
                if (input.Key == ConsoleKey.RightArrow && my_position + my_size < Console.WindowWidth) my_position++;
                if (input.Key == ConsoleKey.UpArrow && speed >100) speed -= 10;
                if (input.Key == ConsoleKey.DownArrow &&speed<300) speed += 10;
               
            }
        }
        static void shiftrwos()
        {
            for (int i = windowlen - 1; i > 0; i--)
                stones[i] = stones[i - 1];
        }

        static void createrow()
        {
            stones[0] = new string(' ', Console.WindowWidth);
            //without blank line %2
            for(int i = 0; i < difficulity; i++)
            {
                Random randd = new Random();
                int idxgeneration = randd.Next(0, Console.WindowWidth - 2);
                int lenofgenerate = randd.Next(1, 4);
                int idxrock = randd.Next(0, 12);
                StringBuilder builder = new StringBuilder(stones[0]);
                builder.Replace(' ', rockes[idxrock], idxgeneration, lenofgenerate);
                stones[0] = builder.ToString();
            }

        }
        static void hit()
        {
            int flag = 1;
            if (stones[Console.WindowHeight - 4].Substring(my_position, my_size).Contains("   ")) flag = 0;
            if (flag==1)
            {
                if (score > topscore) topscore = score;
                score = 0;speed = 200;
                my_position =(Console.WindowWidth-my_size)/ 2;
                difficulity = 1;
                clearr();
                ConsoleKeyInfo input;
                do
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 1);
                    Console.Write("GAME OVER *_*");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.WindowHeight / 2 + 1);
                    Console.Write("Press ENTER to start a new game!");
                    input = Console.ReadKey();

                } while (input.Key != ConsoleKey.Enter);

            } 
        }

        static void draw()
        {
            for (int i = 0; i < windowlen; i++)
                Console.WriteLine(stones[i]);
            for(int i = 0; i < my_size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(my_position+i, Console.WindowHeight-4);
                Console.Write('O');
                Console.SetCursorPosition(0, Console.WindowHeight - 3);
                Console.Write(new string('-', Console.WindowWidth));
            }
        }
        static void clearr()
        {
            for (int i = 0; i < windowlen; i++)
                stones[i] = new string(' ', Console.WindowWidth);      
        }
        static void ShowScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write("Score: {0}", score);
            Console.SetCursorPosition(12, Console.WindowHeight - 1);
            Console.Write("Difficulty: {0}", difficulity);
            Console.SetCursorPosition(12, Console.WindowHeight - 2);
            Console.Write("Top Score: {0}", topscore);
            Console.SetCursorPosition(30, Console.WindowHeight - 2);
            Console.WriteLine("Left Arrow: Move Left");
            Console.SetCursorPosition(30, Console.WindowHeight - 1);
            Console.Write("Right Arrow: Move Right");
            Console.SetCursorPosition(60, Console.WindowHeight - 2);
            Console.WriteLine("Up Arrow: + Speed");
            Console.SetCursorPosition(60, Console.WindowHeight - 1);
            Console.Write("Down Arrow: - Speed");
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Speed: {0}", 300-speed);
            Console.ForegroundColor = ConsoleColor.Red;
            score++;
            if (score % 100 == 0)difficulity++; 
        }
        static void Main(string[] args)
        {
            Console.Title = "MaTb3aa Game";
            RemoveScrollBars();clearr();
            my_position = (Console.WindowWidth - my_size) / 2;
            while (true)
            { 
                moveme();
                shiftrwos();createrow();
                hit(); Console.Clear();
                draw();ShowScore();
                Thread.Sleep(speed);
            }

        }
    }
}
