using System;

class MainClass
{
    static void Main(string[] args)
    {
        //MENU PRINCIPAL
        static void opciones()
        {
            int lol = 0;
            do
            {
                Console.Clear();

                Console.WriteLine("\t\t\t\t\t¡Bienvenido a BATALLA DE BARCOS, tripulante!");

                string barco = @"                 |~
           |/    w
          / (   (|   \
         /( (/   |)  |\
  ____  ( (/    (|   | )  ,
 |----\ (/ |    /|   |'\ /^;
\---*---Y--+-----+---+--/(
 \------*---*--*---*--/
  '~~ ~~~~~~~~~~~~~~~
~~~~~~~~~~~~~~~~~~~`~~~~~~'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
                Console.WriteLine(barco);

                Console.WriteLine("\nJ. JUGAR\nD. Dinámica de juego\nC.¿Cómo jugar?\nS. Salir\n");
                ConsoleKeyInfo op1 = Console.ReadKey(true);

                switch (op1.Key)
                {
                    case ConsoleKey.J:
                        lol = 1;
                        break;

                    case ConsoleKey.D:
                        Console.WriteLine("En cada turno debes intentar destruir mi flota en un tablero cuadrado, yo " +
                            "colocaré cierta cantidad de barcos cuyo tamaño depende de la dificultad seleccionada. Ganarás cuando " +
                            "los destruyas todos y dependiendo de tus aciertos y fallos tendrás cierta cantidad de puntos. " +
                            "\n¡Trata de tener la mayor puntuación posible!");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.C:
                        Console.WriteLine("Al momento de iniciar comenzará la fase de ataque, se presenta un tablero que " +
                            "representa el mar, sin embargo, los barcos están ocultos. Escribe las coordenadas de la casilla " +
                            "que deseas atacar, indica la fila seguida de la columna. \nLos aciertos revelarán la posición " +
                            "del barco y se sumarán puntos, sin embargo, tus fallos te restarán puntos. Ten cuidado en altamar.");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.S:
                        Console.WriteLine("Vuelve cuando quieras hundir un par de barcos, novato.\n>:)");
                        Environment.Exit(0);
                        break;

                    default:
                        opciones();
                        break;
                }
            } while (lol != 1);
        }

        opciones();
        Console.Clear();

        //1. CREA UN TABLERO DE JUEGO BIDIMENSIONAL
        int op2;
        char[] dimensiones = { (char)5, (char)6, (char)7 };

        Console.WriteLine("Selecciona el área que deseas que tenga tu tablero:");
        Console.WriteLine("\n1. Civil\nTablero 5x5 con intentos ilimitados." +
            "\n\n2. Cabo\nTablero 6x6 con 15 intentos." +
            "\n\n3. Almirante\nTablero 7x7 con 15 intentos.\n");

        var stringOint = Console.ReadLine();
        Console.WriteLine();

        while (!int.TryParse(stringOint, out op2) && op2 != 1 && op2 != 2 && op2 != 3)
        {
            stringOint = Console.ReadLine();
        }

        int numDimensiones = dimensiones[op2 - 1];
        char[,] tablero = new char[numDimensiones, numDimensiones];
        char[,] tableroTapado = new char[numDimensiones, numDimensiones];

        for (int i = 0; i < tableroTapado.GetLength(0); i++)
        {
            for (int x = 0; x < tableroTapado.GetLength(1); x++)
            {
                tableroTapado[i, x] = '.';
                tablero[i, x] = '.';
            }
        }

        Random random = new Random();
        int dir = random.Next(0, 2);

        if (dir == 0)
        {
            int x = random.Next(0, 3);
            int y = random.Next(0, 6);

            tablero[x, y] = 'X';
            tablero[x + 1, y] = 'X';
            tablero[x + 2, y] = 'X';
        }

        else if (dir == 1)
        {
            int x = random.Next(0, 6);
            int y = random.Next(0, 3);

            tablero[x, y] = 'X';
            tablero[x, y + 1] = 'X';
            tablero[x, y + 2] = 'X';
        }

        int barcos = 3;
        int golpes = 0;
        int intentos = 0;
        int puntos = 0;

        while (true)
        {
            try
            {
                drawBoard(tableroTapado);
                Console.Write("Ingresa la letra correspondiente a la fila:");
                string columnaLetra = Console.ReadLine();
                columnaLetra = columnaLetra.ToUpper();
                int f = 0;

                Console.Write("Ingresa el número correspondiente a la columna:");
                string fila = Console.ReadLine();
                int col = Int32.Parse(fila) - 1;

                if (columnaLetra == "A")
                {
                    f = 0;
                }
                else if (columnaLetra == "B")
                {
                    f = 1;
                }
                else if (columnaLetra == "C")
                {
                    f = 2;
                }
                else if (columnaLetra == "D")
                {
                    f = 3;
                }
                else if (columnaLetra == "E")
                {
                    f = 4;
                }
                else if (columnaLetra == "F")
                {
                    f = 5;
                }

                if (tableroTapado[f, col] == '.')
                {
                    intentos++;
                    if (tablero[f, col] == 'X')
                    {
                        tableroTapado[f, col] = 'X';
                        golpes++;
                        Console.WriteLine("¡Golpe!");
                        golpes++;
                        puntos += 15;
                        if (golpes == barcos)
                        {
                            break;
                        }
                    }
                    else
                    {
                        tableroTapado[f, col] = 'O';
                        Console.WriteLine("Fallaste...");
                        puntos -= 3;
                    }
                }

            }
            catch
            {
                Console.WriteLine("Deja de intentar de hacer sufrir al programa.");
            }
        }
        drawBoard(tableroTapado);
        Console.WriteLine($"Ganaste a los {intentos} intentos con {puntos} puntos. Bien hecho, camarada.");
    }
    public static void drawBoard(char[,] arr)
    {
        Console.Clear();
        int asciiVal = 65;

        Console.Write(" \t");
        for (int i = 1; i < arr.GetLength(1) + 1; i++)
        {
            Console.Write($"{i}\t");
        }
        Console.WriteLine("\n");

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            Console.Write($"{(char)asciiVal}\t");
            for (int x = 0; x < arr.GetLength(1); x++)
            {
                Console.Write($"{arr[i, x]}\t");
            }
            Console.WriteLine();
            asciiVal++;
        }
    }
}