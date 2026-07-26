using System;
using System.Data;
using System.Net.Http.Headers;
namespace Projekt1
{
    class Program
    {
        static void PlanszaRysowanie(int centerX, int centerY, string[,] plansza)
        {
            //Generowanie miejsca na planszy
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(centerX, centerY + i);
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(plansza[i, j]);
                }
            }

        }
        static void Main(string[] args)
        {
            Console.Title = "Kółko i Krzyżyk by Berni";
            bool menu = true;
            bool gra = true;
            bool wygrana = false;

            int x = 0;
            int y = 0;

            int tura = 2;

            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            int centerX = windowWidth / 2 - 2;
            int centerY = windowHeight / 2 - 2;

            string zwyciezca = " ";

            //Plansza gry
            string[,] plansza = new string[5, 5]
            {
                {" ", "█", " ", "█", " "},
                {"█", "█", "█", "█", "█"},
                {" ", "█", " ", "█", " "},
                {"█", "█", "█", "█", "█"},
                {" ", "█", " ", "█", " "},
            };

            //Menu wyboru gracza
            while (menu == true)
            {
                Console.Clear();
                Console.WriteLine("Gra w Kółko i Krzyżyk v. 1.2\nAutor: Berni\nData powstania: 13.10.2024\nNajnowsza aktualizacja: 26.07.2026\n\nWciśnij X lub O by wybrać który gracz zaczyna pierwszy:");
                ConsoleKey input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.X:
                        tura = 0;
                        break;

                    case ConsoleKey.O:
                        tura = 1;
                        break;

                    default:
                        break;
                }

                if (tura == 1 || tura == 0)
                {
                    Console.Clear();
                    menu = false;
                }
            }

            //Główna pętla gry
            while (gra == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                //Umieszcza planszę na środku ekranu
                Console.SetCursorPosition(centerX, centerY);
                PlanszaRysowanie(centerX, centerY, plansza);

                //Tekst informacyjna czyja jest tura
                Console.WriteLine();
                Console.Write("Tura: ");
                if (tura == 1)
                    Console.Write("O");
                else
                    Console.Write("X");

                Console.SetCursorPosition(centerX + x, centerY + y);



                if (plansza[y, x] == "X" || plansza[y, x] == "O")
                    //Jeśli miejsce jest zajęte, zmienia tło zaznaczenia na czerwono
                    Console.BackgroundColor = ConsoleColor.Red;
                else
                    //Jeśli miejsce jest wolne, zamienia tło zaznaczenia na zielono
                    Console.BackgroundColor = ConsoleColor.Green;

                //Wstawia znaki na planszy
                Console.Write(plansza[y, x]);

                //Wybieranie miejsca za pomocą klawiszy WASD i strzałek
                ConsoleKey input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.W:
                        if (y > 0)
                            y = y - 2;
                        break;

                    case ConsoleKey.A:
                        if (x > 0)
                            x = x - 2;
                        break;

                    case ConsoleKey.S:
                        if (y < 4)
                            y = y + 2;
                        break;

                    case ConsoleKey.D:
                        if (x < 4)
                            x = x + 2;
                        break;

                    case ConsoleKey.UpArrow:
                        if (y > 0)
                            y = y - 2;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (x > 0)
                            x = x - 2;
                        break;

                    case ConsoleKey.DownArrow:
                        if (y < 4)
                            y = y + 2;
                        break;

                    case ConsoleKey.RightArrow:
                        if (x < 4)
                            x = x + 2;
                        break;

                    //Potwierdzenie wyboru w pustej pozycji
                    case ConsoleKey.Enter:
                        if (plansza[y, x] == " ")
                        {
                            //Sprawdza czyja jest tura i na tej podstawie wstawia odpowiedni znak do tablicy
                            if (tura == 0)
                                plansza[y, x] = "X";
                            else
                                plansza[y, x] = "O";

                            //W przypadku dokonania wyboru zmienia się tura
                            if (tura == 1)
                                tura--;
                            else
                                tura++;
                        }
                        break;

                    default:
                        break;
                }
                //Warunki zwycięstwa dla X
                if ((plansza[0, 0] == "X" && plansza[0, 2] == "X" && plansza[0, 4] == "X") ||
                    (plansza[2, 0] == "X" && plansza[2, 2] == "X" && plansza[2, 4] == "X") ||
                    (plansza[4, 0] == "X" && plansza[4, 2] == "X" && plansza[4, 4] == "X") ||

                    (plansza[0, 0] == "X" && plansza[2, 0] == "X" && plansza[4, 0] == "X") ||
                    (plansza[0, 2] == "X" && plansza[2, 2] == "X" && plansza[4, 2] == "X") ||
                    (plansza[0, 4] == "X" && plansza[2, 4] == "X" && plansza[4, 4] == "X") ||

                    (plansza[0, 0] == "X" && plansza[2, 2] == "X" && plansza[4, 4] == "X") ||
                    (plansza[4, 0] == "X" && plansza[2, 2] == "X" && plansza[0, 4] == "X"))
                {
                    zwyciezca = "X";
                    wygrana = true;
                }
                //Warunki zwycięstwa dla O
                if ((plansza[0, 0] == "O" && plansza[0, 2] == "O" && plansza[0, 4] == "O") ||
                    (plansza[2, 0] == "O" && plansza[2, 2] == "O" && plansza[2, 4] == "O") ||
                    (plansza[4, 0] == "O" && plansza[4, 2] == "O" && plansza[4, 4] == "O") ||

                    (plansza[0, 0] == "O" && plansza[2, 0] == "O" && plansza[4, 0] == "O") ||
                    (plansza[0, 2] == "O" && plansza[2, 2] == "O" && plansza[4, 2] == "O") ||
                    (plansza[0, 4] == "O" && plansza[2, 4] == "O" && plansza[4, 4] == "O") ||

                    (plansza[0, 0] == "O" && plansza[2, 2] == "O" && plansza[4, 4] == "O") ||
                    (plansza[4, 0] == "O" && plansza[2, 2] == "O" && plansza[0, 4] == "O"))
                {
                    zwyciezca = "O";
                    wygrana = true;
                }

                // Warunki do remisu
                if (plansza[0, 0] != " " && plansza[0, 2] != " " && plansza[0, 4] != " " &&
                    plansza[2, 0] != " " && plansza[2, 2] != " " && plansza[2, 4] != " " &&
                    plansza[4, 0] != " " && plansza[4, 2] != " " && plansza[4, 4] != " ")
                {
                    wygrana = false;
                    gra = false;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(centerX, centerY);
                    PlanszaRysowanie(centerX, centerY, plansza);

                    Console.SetCursorPosition(centerX - 3, centerY + 10);
                    Console.WriteLine("Remis");
                    Console.ReadKey(true);
                }

                // Tekst zwycięstwa
                if (wygrana == true)
                {
                    gra = false;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(centerX, centerY);
                    PlanszaRysowanie(centerX, centerY, plansza);

                    Console.SetCursorPosition(centerX - 3, centerY + 10);
                    Console.WriteLine("Gracz " + zwyciezca + " wygrał");
                    Console.ReadKey(true);
                }
            }

        }
    }
}