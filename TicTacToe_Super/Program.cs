using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace TicTacToe
{
    class Program
    {
        static int[,] table = new int[3, 3];
        static char[] symbol = { ' ', 'O', 'X', '♫', '♠', '♥' };
        static string p1 = " ";
        static string p2 = " ";
        static int playerT;
        static int symbolP1;
        static int symbolP2 = 0;
        static bool end = false;
        static int winWidth = 48;
        static string fase = "start";
        static bool valid = true;
        static bool notEmpty = false;
        static string square = "";
        static int lang = 1;//1 is for English and 2 is for Spanish.

        static void Main(string[] args)
        {
            setWindow();
            ChangeTitleName();
            Language();
            Console.Clear();

            PlayMusic();
            Welcome();
            InstructionFuntion("PlayerNameInstructions");
            playerName();

            Console.Clear();
            Welcome();
            Simbols();
            InstructionFuntion("symbolInstrucction");
            PlayerSymbol();

            Console.Clear();
            Load();

            do
            {
                playerT = 1;
                Console.Clear();
                EmojiPlayer();
                renderTable();
                TurnPlayer();
                InstructionFuntion("coordinateInstrucction");
                askCoordinate(playerT);

                end = checkWin();
                if (end == true)
                {
                    Console.Clear();
                    EmojiPlayer();
                    renderTable();
                    winMessage();

                }
                else
                {
                    end = checkTie();
                    if (end == true)
                    {
                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        tieMessage();
                    }
                    else
                    {
                        playerT = 2;
                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        TurnPlayer();
                        InstructionFuntion("coordinateInstrucction");
                        askCoordinate(playerT);


                        end = checkWin();
                        if (end == true)
                        {
                            Console.Clear();
                            EmojiPlayer();
                            renderTable();
                            winMessage();
                        }
                    }
                }

            } while (end == false);

            if (lang == 1)
            {
                WriteGrayLine(" Press enter to exit...");
            }
            else
            {
                WriteGrayLine(" Presiona enter para salir...");
            }
            
            Console.ReadLine();
        }

        //Pre Game
        static void setWindow()
        {
            Console.SetWindowSize(winWidth, 30);
            Console.SetBufferSize(winWidth, 80);

        }
        static void ChangeTitleName()
        {
            Console.Title = "Super Michi";
        }
        static void Language()
        {
            Console.WriteLine("\n\n\n\n\n");
            WriteRed("          |");
            WriteYellow(" 1: English ");
            WriteRed("|");
            WriteYellow(" 2: Spanish ");
            WriteRed("|");
            Console.WriteLine();
            InstructionFuntion("Languageinstruction");


            do
            {
                try
                {
                    Console.Write("\n\n Please select your language: ");
                    lang = Convert.ToInt32(Console.ReadLine());
                    Console.Beep();
                }

                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n");
                    WriteRed("          |");
                    WriteYellow(" 1: English ");
                    WriteRed("|");
                    WriteYellow(" 2: Spanish ");
                    WriteRed("|");
                    Console.WriteLine();
                    InstructionFuntion("Languageinstruction");

                    WriteRedLine("\n Enter a valid symbol number!");
                    
                    lang = 6;
                }

                if (lang < 1 || lang > 2)
                {
                    Console.Clear();

                    Console.WriteLine("\n\n\n\n\n");
                    WriteRed("          |");
                    WriteYellow(" 1: English ");
                    WriteRed("|");
                    WriteYellow(" 2: Spanish ");
                    WriteRed("|");
                    Console.WriteLine();
                    InstructionFuntion("Languageinstruction");

                    WriteRedLine("\n Enter a valid number!");
                }

            } while (lang < 1 || lang > 2);


        }
        static void PlayMusic()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\BGMusic.wav";
            player.PlayLooping();

        }
        static void Welcome()
        {
            if (lang == 1)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                WriteRedLine("\n\t#############################");
                WriteYellowLine("\t   (= Φ ω Φ =) Welcome to:");
                WriteRedLine("\t#############################\n");
                WriteYellowLine(
                    "\t##    ### ## ###### ##  ## ##\n" +
                    "\t#### #### ## ##     ##  ## ##\n" +
                    "\t## ### ## ## ##     ###### ##\n" +
                    "\t##     ## ## ###### ##  ## ##\n");
                WriteRedLine("\t#############################\n");

            }
            else
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                WriteRedLine("\n\t#############################");
                WriteYellowLine("\t  (= Φ ω Φ =) Bienvenido a:");
                WriteRedLine("\t#############################\n");
                WriteYellowLine(
                    "\t##    ### ## ###### ##  ## ##\n" +
                    "\t#### #### ## ##     ##  ## ##\n" +
                    "\t## ### ## ## ##     ###### ##\n" +
                    "\t##     ## ## ###### ##  ## ##\n");
                WriteRedLine("\t#############################\n");

            }

        }
        static void InstructionFuntion(string value)
        {
            switch (value)
            {
                case "Languageinstruction":

                    {
                        Instructions("Introduce 1 or 2 and press enter");
                        break;
                    }

                case "PlayerNameInstructions":

                    {
                        if (lang == 1)
                        {
                            Instructions("Enter a name and press enter");
						}
                        else
                        {
                            Instructions("Escribe un nombre y presiona enter");
                        }
                        break;
                    }

                case "symbolInstrucction":

                    {
                        if (lang == 1)
                        {
                            Instructions("Enter your simbol number and press enter");
                        }
                        else
                        {
                            Instructions("Escribe el número de tu símbolo y da enter");
                        }
                        break;
                    }
                case "coordinateInstrucction":

                    {
                        if (lang == 1)
                        {
                            Instructions("Enter a coordinate and press enter (e.g. A1)");
                        }
                        else
                        {
                            Instructions("Introduce tu coordenada y da enter (ej. A1)");
                        }
                        break;
                    }

                default:
                    break;
            }
        }
        static void Instructions(string value)
        {
            string border = new string('-', value.Length);
           
            Console.WriteLine();

            Console.SetCursorPosition
            ((Console.WindowWidth - value.Length) / 2, Console.CursorTop);
            WriteDarkRedLine(border);

            WriteDarkRedLine
            (String.Format("{0,"
            + ((Console.WindowWidth / 2) +
            (value.Length / 2)) + "}", value));

            Console.SetCursorPosition
            ((Console.WindowWidth - value.Length) / 2, Console.CursorTop);
            WriteDarkRedLine(border);
        }
        static void playerName()
        {
            do
            {
                ask:
                
                if (lang == 1)
                {
                    WriteCyan("\n Player 1 ");
                    Console.Write("name: ");
                }
                else
                {
                    WriteCyan("\n Jugador 1 ");
                    Console.Write("nombre: ");
                }


                p1 = Console.ReadLine();
                Console.Beep();

                if ( p1.Length < 1 )
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                }
                 if (p1.Length > 16)
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                }
                if (p1 == " ")
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                    goto ask;
                }
                if (p1 == "  ")
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                    goto ask;
                }
                if (p1 == "   ")
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                    goto ask;
                }

            } while ( p1.Length < 1 || p1.Length > 16);

            Console.Clear();
            Welcome();
            InstructionFuntion("PlayerNameInstructions");

            do
            {
                ask:

                if (lang == 1)
                {
                    WriteGreen("\n Player 2 ");
                    Console.Write("name: ");
                }
                else
                {
                    WriteGreen("\n Jugador 2 ");
                    Console.Write("nombre: ");
                }


                p2 = Console.ReadLine();
                Console.Beep();

                if (p2.Length < 1)
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");

                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }

                }
                if (p2.Length > 16)
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                }
                if (p2 == " ")
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                    goto ask;
                }
                if (p2 == "  ")
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                    goto ask;
                }
                if (p2 == "   ")
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid name!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un nombre valido!");
                    }
                    goto ask;
                }

                if (p2 == p1)
                {
                    Console.Clear();
                    Welcome();
                    InstructionFuntion("PlayerNameInstructions");
                    if (lang == 1)
                    {
                        WriteRedLine($"\n {p1} is alredy in use!");
                    }
                    else
                    {
                        WriteRedLine($"\n ¡{p1} ya esta en uso!");
                    }
                    
                }

            } while (p2.Length < 1 || p2 == p1 || p2.Length > 16);



             
        }
        static void Simbols()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            WriteRed("     |");
            WriteYellow(" 1: O ");
            WriteRed("|");
            WriteYellow(" 2: X ");
            WriteRed("|");
            WriteYellow(" 3: ♫ ");
            WriteRed("|");
            WriteYellow(" 4: ♠ ");
            WriteRed("|");
            WriteYellow(" 5: ♥ ");
            WriteRedLine("|");
        }
        static void PlayerSymbol()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            
            do
            {
                try
                {
                    if (lang == 1)
                    {
                        WriteCyan($"\n {p1} ");
                        Console.Write("select your symbol :");

                    }
                    else
                    {
                        WriteCyan($"\n {p1} ");
                        Console.Write("selecciona tu símbolo :");
                    }                 
                    
                    symbolP1 = Convert.ToInt32(Console.ReadLine());
                    Console.Beep();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Welcome();
                    Simbols();
                    InstructionFuntion("symbolInstrucction");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid symbol number!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un número valido!");
                    }
                    
                    symbolP1 = 6;
                }

                if (symbolP1 < 1 || symbolP1 > 5)
                {
                    Console.Clear();
                    Welcome();
                    Simbols();
                    InstructionFuntion("symbolInstrucction");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid symbol number!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un número valido!");
                    }
                }

            } while ((symbolP1 < 1) || (symbolP1 > 5));

            Console.Clear();
            Welcome();
            Simbols();
            InstructionFuntion("symbolInstrucction");

            do
            {

                try
                {

                    if (lang == 1)
                    {
                        WriteGreen($"\n {p2} ");
                        Console.Write("select your symbol :");

                    }
                    else
                    {
                        WriteGreen($"\n {p2} ");
                        Console.Write("selecciona tu símbolo :");
                    }

                    
                    symbolP2 = Convert.ToInt32(Console.ReadLine());
                    Console.Beep();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Welcome();
                    Simbols();
                    InstructionFuntion("symbolInstrucction");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid symbol number!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un número valido!");
                    }
                    symbolP2 = 6;

                }
                if (symbolP1 == symbolP2)
                {
                    Console.Clear();
                    Welcome();
                    Simbols();
                    InstructionFuntion("symbolInstrucction");
                    if (lang == 1)
                    {
                        WriteRedLine($"\n The {symbol[symbolP1]} symbol is alredy in use!");
                    }
                    else
                    {
                        WriteRedLine($"\n ¡El símbolo {symbol[symbolP1]} ya está en uso!");
                    }
                   
                }
                if (symbolP2 < 1 || symbolP2 > 5)
                {
                    Console.Clear();
                    Welcome();
                    Simbols();
                    InstructionFuntion("symbolInstrucction");
                    if (lang == 1)
                    {
                        WriteRedLine("\n Enter a valid symbol number!");
                    }
                    else
                    {
                        WriteRedLine("\n ¡Introduzca un número valido!");
                    }
                }

            } while (symbolP2 < 1 || symbolP2 > 5 || symbolP1 == symbolP2);

        }

        static void Load()
        {
            int unity = 6;
            int waittime = 250;
            string textToEnter = "Loading...";

            if (lang == 1)
            {
                textToEnter = "Loading...";

            }
            else
            {
                textToEnter = "Cargando...";
            }

            for (int i = unity; i <= winWidth; i += unity)
            {
                Console.WriteLine("\n\n\n\n\n\n\n\n");
                Console.WriteLine
                (String.Format("{0," 
                + ((Console.WindowWidth / 2) +
                (textToEnter.Length / 2)) + "}", textToEnter));
                Console.WriteLine("\n");

                WriteFullLine("Michi".PadLeft(i, ' '));
                System.Threading.Thread.Sleep(waittime);
                Console.Clear();
            }
           
        }
        
        
        //on game
        static void renderTable()
        {
            Console.WriteLine();
            WriteYellowLine(
                  "\t\t    1   2   3  "
               + "\n\t\t  ┌───┬───┬───┐"
              + $"\n\t\tA │ {symbol[table[0, 0]]} │ {symbol[table[0, 1]]} │ {symbol[table[0, 2]]} │"
              + "\n\t\t  ├───┼───┼───┤"
              + $"\n\t\tB │ {symbol[table[1, 0]]} │ {symbol[table[1, 1]]} │ {symbol[table[1, 2]]} │"
              + "\n\t\t  ├───┼───┼───┤"
              + $"\n\t\tC │ {symbol[table[2, 0]]} │ {symbol[table[2, 1]]} │ {symbol[table[2, 2]]} │"
              + "\n\t\t  └───┴───┴───┘");
        }
        static void EmojiPlayer()
        {
            //Fight
            if (playerT == 1 && end == false)
            {
                AnimaticP1();
            }
            else if (playerT == 2 && end == false)
            {
                AnimaticP2();
            }
            //Win
            else if (playerT == 1 && end == true && checkWin() == true)
            {
                Separator();
                Console.WriteLine();
                WriteCyan("\t    <(^°=ω=°^)>");
                WriteWhiteLine("   <( ˄T ω T˄ )>");

            }
            else if (playerT == 2 && end == true)
            {
                Separator();
                Console.WriteLine();
                WriteWhite("\t    <( ˄T ω T˄ )>");
                WriteGreenLine("   <(^°=ω=°^)>");
            }
            //Tie
            else
            {
                Separator();
                WriteYellowLine("\n\t    <(^°=ω=°^)>   <(^°=ω=°^)>");
            }


        }
        static void TurnPlayer()
        {
            if (playerT == 1)
            {
                string textToEnter1 = $"» {p1}: {symbol[symbolP1]} «";
                string textToEnter2 = $"{p2}: {symbol[symbolP2]}";

                Console.WriteLine();

                WriteCyanLine
                (String.Format("{0,"
                + ((Console.WindowWidth / 2) +
                (textToEnter1.Length / 2)) + "}", textToEnter1));

                WriteGrayLine
                (String.Format("{0,"
                + ((Console.WindowWidth / 2) +
                (textToEnter2.Length / 2)) + "}", textToEnter2));


            }
            else
            {
                string textToEnter1 = $"{p1}: {symbol[symbolP1]}";
                string textToEnter2 = $"» {p2}: {symbol[symbolP2]} «";

                Console.WriteLine();

                WriteGrayLine
                (String.Format("{0,"
                + ((Console.WindowWidth / 2) +
                (textToEnter1.Length / 2)) + "}", textToEnter1));

                WriteGreenLine
                (String.Format("{0,"
                + ((Console.WindowWidth / 2) +
                (textToEnter2.Length / 2)) + "}", textToEnter2));

            }

        }
        static void askCoordinate(int player)
        {
            string coor = "0";
            fase = "inicio";
            valid = true;
            bool error = false;
            notEmpty = false;
            square = "";

            if (player == 1)
            {
                do
                {
                    again:

                    if (lang == 1)
                    {
                        Console.Write("\n Coordinate : ");
                    }
                    else
                    {
                        Console.Write("\n Coordenada : ");
                    }

                    
                    coor = Console.ReadLine();
                    valid = true;
                    error = false;
                    notEmpty = false;
                    Console.Beep();
                

                    //Console.WriteLine(fase);
                    //System.Threading.Thread.Sleep(2000);

                    if (coor.Length < 2 || coor.Length > 2)
                    {
                        fase = " p1 - if - error  min";
                        //Console.WriteLine(fase);
                        //System.Threading.Thread.Sleep(2000);

                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        TurnPlayer();
                        InstructionFuntion("coordinateInstrucction");
                        if (lang == 1)
                        {
                            WriteRedLine("\n Enter a valid coordinate!");
                        }
                        else
                        {
                            WriteRedLine("\n ¡Introduce una coordenada válida!");
                        }
                        

                        goto again;
                    }

                    fase = "p1 - hacia switch";
                    //Console.WriteLine(fase);
                    //System.Threading.Thread.Sleep(2000);

                    coor = coor.ToUpper();
                    switch (coor)
                    { 
  
                        case "A1":
                            {
                                if (table[0, 0] != 0)
                                {
                                    NotEmpty("A1");
                                }
                                else
                                {
                                    table[0, 0] = symbolP1;
                                }
                                break;
                            }
                        case "B1":
                            {
                                if (table[1, 0] != 0)
                                {
                                    NotEmpty("B1");
                                }
                                else
                                {
                                    table[1, 0] = symbolP1;
                                }
                                break;
                            }
                        case "C1":
                            {
                                if (table[2, 0] != 0)
                                {
                                    NotEmpty("C1");
                                }
                                else
                                {
                                    table[2, 0] = symbolP1;
                                }
                            
                                break;
                            }
                        case "A2":
                            {
                                if (table[0, 1] != 0)
                                {
                                    NotEmpty("A2"); ;
                                }
                                else
                                {
                                    table[0, 1] = symbolP1;
                                }
                            
                                break;
                            }
                        case "B2":
                            {
                                if (table[1, 1] != 0)
                                {
                                    NotEmpty("B2");
                                }
                                else
                                {
                                    table[1, 1] = symbolP1;
                                }
                            
                                break;
                            }
                        case "C2":
                            {
                                if (table[2, 1] != 0)
                                {
                                    NotEmpty("C2");
                                }
                                else
                                {
                                    table[2, 1] = symbolP1;
                                }
                            
                                break;
                            }
                        case "A3":
                            {
                                if (table[0, 2] != 0)
                                {
                                    NotEmpty("A3");
                                }
                                else
                                {
                                    table[0, 2] = symbolP1;
                                }
                            
                                break;
                            }
                        case "B3":
                            {
                                if (table[1, 2] != 0)
                                {
                                    NotEmpty("B3");
                                }
                                else
                                {
                                    table[1, 2] = symbolP1;
                                }
                            
                                break;
                            }
                        case "C3":
                            {
                                if (table[2, 2] != 0)
                                {
                                    NotEmpty("C3");
                                }
                                else
                                {
                                    table[2, 2] = symbolP1;
                                }
                           
                                break;
                            }


                        default:
                        {
                                fase = "default p1";
                                error = true;
                                valid = false;
                                //Console.WriteLine(fase);
                                //System.Threading.Thread.Sleep(2000);
                            }
                        break;
                    }

                    if (error == true)
                    {
                        fase = "p1 - if - error";
                        valid = false;
                        //Console.WriteLine(fase);
                        //System.Threading.Thread.Sleep(2000);

                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        TurnPlayer();
                        InstructionFuntion("coordinateInstrucction");

                        if (lang == 1)
                        {
                            WriteRedLine("\n Enter a valid coordinate!");
                        }
                        else
                        {
                            WriteRedLine("\n ¡Introduce una coordenada válida!");
                        }
                    }

                    if (notEmpty == true)
                    {
                        fase = "p1 - if - notempty";
                        valid = false;
                        //Console.WriteLine(fase);
                        //System.Threading.Thread.Sleep(2000);

                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        TurnPlayer();
                        InstructionFuntion("coordinateInstrucction");

                        if (lang == 1)
                        {
                            WriteRedLine($"\n {square} is not empty!");
                        }
                        else
                        {
                            WriteRedLine($"\n ¡{square} ya está lleno!");
                        }

                        
                    }

                } while (coor.Length < 2 || coor.Length > 2 || valid != true);

                fase = "fuera de p1";
                //Console.WriteLine(fase);
                //System.Threading.Thread.Sleep(2000);

            }
            else
            {

                fase = "p2 dentor de else";
                valid = true;
               // Console.WriteLine(fase);
                //System.Threading.Thread.Sleep(2000);

                do
                {
                    again2:

                    if (lang == 1)
                    {
                        Console.Write("\n Coordinate : ");
                    }
                    else
                    {
                        Console.Write("\n Coordenada : ");
                    }

                    
                    coor = Console.ReadLine();
                    valid = true;
                    error = false;
                    notEmpty = false;
                    Console.Beep();


                    //Console.WriteLine(fase);
                    //System.Threading.Thread.Sleep(2000);

                    if (coor.Length < 2 || coor.Length > 2)
                    {
                        fase = " p2 if error  min";
                        // Console.WriteLine(fase);
                        //System.Threading.Thread.Sleep(2000);

                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        TurnPlayer();
                        InstructionFuntion("coordinateInstrucction");

                        if (lang == 1)
                        {
                            WriteRedLine("\n Enter a valid coordinate!");
                        }
                        else
                        {
                            WriteRedLine("\n ¡Introduce una coordenada válida!");
                        }

                        goto again2;
                    }

                    fase = "p2 - hacia switch";
                    //Console.WriteLine(fase);
                    //System.Threading.Thread.Sleep(2000);

                    coor = coor.ToUpper();
                    switch (coor)
                    {

                        case "A1":
                            {
                                if (table[0, 0] != 0)
                                {
                                    NotEmpty("A1");
                                }
                                else
                                {
                                    table[0, 0] = symbolP2;
                                }
                                break;
                            }
                        case "B1":
                            {
                                if (table[1, 0] != 0)
                                {
                                    NotEmpty("B1");
                                }
                                else
                                {
                                    table[1, 0] = symbolP2;
                                }
                                break;
                            }
                        case "C1":
                            {
                                if (table[2, 0] != 0)
                                {
                                    NotEmpty("C1");
                                }
                                else
                                {
                                    table[2, 0] = symbolP2;
                                }

                                break;
                            }
                        case "A2":
                            {
                                if (table[0, 1] != 0)
                                {
                                    NotEmpty("A2");
                                }
                                else
                                {
                                    table[0, 1] = symbolP2;
                                }

                                break;
                            }
                        case "B2":
                            {
                                if (table[1, 1] != 0)
                                {
                                    NotEmpty("B2");
                                }
                                else
                                {
                                    table[1, 1] = symbolP2;
                                }

                                break;
                            }
                        case "C2":
                            {
                                if (table[2, 1] != 0)
                                {
                                    NotEmpty("C2");
                                }
                                else
                                {
                                    table[2, 1] = symbolP2;
                                }

                                break;
                            }
                        case "A3":
                            {
                                if (table[0, 2] != 0)
                                {
                                    NotEmpty("A3");
                                }
                                else
                                {
                                    table[0, 2] = symbolP2;
                                }

                                break;
                            }
                        case "B3":
                            {
                                if (table[1, 2] != 0)
                                {
                                    NotEmpty("B3");
                                }
                                else
                                {
                                    table[1, 2] = symbolP2;
                                }

                                break;
                            }
                        case "C3":
                            {
                                if (table[2, 2] != 0)
                                {
                                    NotEmpty("C3");
                                }
                                else
                                {
                                    table[2, 2] = symbolP2;
                                }

                                break;
                            }


                        default:
                            {
                                fase = "default p2";
                                error = true;
                                valid = false;
                                //Console.WriteLine(fase);
                                //System.Threading.Thread.Sleep(2000);
                            }
                            break;
                    }

                    if (error == true)
                    {
                        fase = "p2 - if - error";
                        valid = false;
                        //Console.WriteLine(fase);
                        //System.Threading.Thread.Sleep(2000);

                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        TurnPlayer();
                        InstructionFuntion("coordinateInstrucction");


                        if (lang == 1)
                        {
                            WriteRedLine("\n Error Enter a valid coordinate!");
                        }
                        else
                        {
                            WriteRedLine("\n Error ¡Introduce una coordenada válida!");
                        }
           
                    }

                    if (notEmpty == true)
                    {
                        fase = "p2 - if - notempty";
                        valid = false;
                        //Console.WriteLine(fase);
                        //System.Threading.Thread.Sleep(2000);

                        Console.Clear();
                        EmojiPlayer();
                        renderTable();
                        TurnPlayer();
                        InstructionFuntion("coordinateInstrucction");

                        if (lang == 1)
                        {
                            WriteRedLine($"\n {square} is not empty!");
                        }
                        else
                        {
                            WriteRedLine($"\n ¡{square} ya está lleno!");
                        }
                      
                      
                    }

                } while (coor.Length < 2 || coor.Length > 2 || valid != true);

                fase = "p2 - fuera de while";
                //Console.WriteLine(fase);
                //System.Threading.Thread.Sleep(2000);
            }

            fase = "fin ciclo";
            //Console.WriteLine(fase);
            //System.Threading.Thread.Sleep(2000);
        }
        static bool checkWin()
        {
            int row = 0, column = 0;
            bool win = false;

            for (row = 0; row < 3; row++)
            {
                if ((table[row, 0] == table[row, 1])
                    && (table[row, 0] == table[row, 2])
                    && (table[row, 0] != 0))
                {
                    win = true;
                }
            }

            for (column = 0; column < 3; column++)
            {
                if ((table[0, column] == table[1, column])
                    && (table[0, column] == table[2, column])
                    && (table[0, column] != 0))
                {
                    win = true;
                }
            }

            if ((table[0, 0] == table[1, 1])
                && (table[0, 0] == table[2, 2])
                && (table[0, 0] != 0))
            {
                win = true;
            }

            if ((table[2, 0] == table[1, 1])
                && (table[2, 0] == table[0, 2])
                && (table[2, 0] != 0))
            {
                win = true;
            }

            return win;
        }
        static bool checkTie()
        {
            int row = 0;
            int column = 0;
            bool tie = true;


            for (row = 0; row < 3; row++)
            {
                for (column = 0; column < 3; column++)
                {
                    if (table[row, column] == 0)
                    {
                        tie = false;
                    }
                }

            }

            return tie;
        }
        static void NotEmpty(string valueSquare)
        {
            fase = $"p1 - if -  lleno: {valueSquare} fase";
            valid = false;
            //Console.WriteLine(fase);
            //System.Threading.Thread.Sleep(2000);
            notEmpty = true;
            square = valueSquare;
        }

        static void Separator()
        {
            WriteDarkRedLine("\n  ###########################################");
        }

        static void AnimaticP1()
        {
            int waitTime = 250;

            Separator();
            Console.WriteLine();
            WriteWhite("\t    <( ˄ºωº˄)>");
            WriteGreen($"  {symbol[symbolP2]}");
            WriteGreenLine("ò=(˄ºωº˄ ò)");
            renderTable();
            TurnPlayer();
            InstructionFuntion("coordinateInstrucction");
            System.Threading.Thread.Sleep(waitTime);
            
             
            Console.Clear();
            Separator();
            Console.WriteLine();
            WriteWhite("\t    <( ˄ºωº˄)>");
            WriteGreen($" {symbol[symbolP2]} ");
            WriteGreenLine("ò=(˄ºωº˄ ò)");
            renderTable();
            TurnPlayer();
            InstructionFuntion("coordinateInstrucction");
            System.Threading.Thread.Sleep(waitTime);
           

            Console.Clear();
            Separator();
            Console.WriteLine();
            WriteWhite("\t    <( ˄ºωº˄)>");
            WriteGreen($"{symbol[symbolP2]}  ");
            WriteGreenLine("ò=(˄ºωº˄ ò)");
            renderTable();
            TurnPlayer();
            InstructionFuntion("coordinateInstrucction");
            System.Threading.Thread.Sleep(waitTime);

            Console.Clear();
            Separator();
            Console.WriteLine();
            WriteCyan("\t    (ó ˄ºωº˄)=ó");
            WriteYellow($"   ");
            WriteWhiteLine("<(˄ºωº˄ )>");




        }
        static void AnimaticP2()
        {
            int waitTime = 250;


            Separator();
            Console.WriteLine();
            WriteCyan("\t    (ó ˄ºωº˄)=ó");
            WriteCyan($"{symbol[symbolP1]}  ");
            WriteWhiteLine("<(˄ºωº˄ )>");
            renderTable();
            TurnPlayer();
            InstructionFuntion("coordinateInstrucction");
            System.Threading.Thread.Sleep(waitTime);
            

            Console.Clear();
            Separator();
            Console.WriteLine();
            WriteCyan("\t    (ó ˄ºωº˄)=ó");
            WriteCyan($" {symbol[symbolP1]} ");
            WriteWhiteLine("<(˄ºωº˄ )>");
            renderTable();
            TurnPlayer();
            InstructionFuntion("coordinateInstrucction");
            System.Threading.Thread.Sleep(waitTime);
           

            Console.Clear();
            Separator();
            Console.WriteLine();
            WriteCyan("\t    (ó ˄ºωº˄)=ó");
            WriteCyan($"  {symbol[symbolP1]}");
            WriteWhiteLine("<(˄ºωº˄ )>");
            renderTable();
            TurnPlayer();
            InstructionFuntion("coordinateInstrucction");
            System.Threading.Thread.Sleep(waitTime);

            Console.Clear();
            Separator();
            Console.WriteLine();
            WriteWhite("\t    <( ˄ºωº˄)>");
            WriteYellow($"   ");
            WriteGreenLine("ò=(˄ºωº˄ ò)");

        }
        static void winMessage()
        {
            if (playerT == 1)
            {
                int waitTime = 250;
                int linesToTop = 12;

                Separator();
                WriteCyanLine($"\n\t\t{symbol[symbolP1]} {p1}\n");
                winText();
                Separator();
                System.Threading.Thread.Sleep(waitTime * 4);

                for (int i = 0; i < linesToTop; i++)
                {
                    Console.WriteLine(" ");
                    System.Threading.Thread.Sleep(waitTime);
                }

            }
            else
            {
                int waitTime = 250;
                int linesToTop = 12;

                Separator();
                WriteGreenLine($"\n\t\t{symbol[symbolP2]} {p2}\n");
                winText();
                Separator();
                System.Threading.Thread.Sleep(waitTime * 4);

                for (int i = 0; i < linesToTop; i++)
                {
                    Console.WriteLine(" ");
                    System.Threading.Thread.Sleep(waitTime);
                }

            }
            
        }
        static void winText()
        {
            WriteRedLine("\t\t##     ## ## ##  ##\n"
                       + "\t\t## ### ## ## ### ##\n"
                       + "\t\t#### #### ## ## ###\n"
                       + "\t\t##    ### ## ##  ##");
        }
        static void tieMessage()
        {
            int waitTime = 250;
            int linesToTop = 10;

            Separator();

            Console.WriteLine("\n\t\tThe game\n");

            WriteYellowLine("\t\t  ##   #####  ######\n"
                          + "\t\t##  ## ##  ## ##    \n"
                          + "\t\t###### #####  ##### \n"
                          + "\t\t##  ## ##  ## ##    \n"
                          + "\t\t##  ## ##  ## ######\n");

            WriteRedLine("\t\t###### ## ###### ###\n"
                       + "\t\t  ##   ## ##     ###\n"
                       + "\t\t  ##   ## #####  ###\n"
                       + "\t\t  ##   ## ##        \n"
                       + "\t\t  ##   ## ###### ### ");
            Separator();
            System.Threading.Thread.Sleep(waitTime * 4);

            for (int i = 0; i < linesToTop; i++)
            {
                Console.WriteLine(" ");
                System.Threading.Thread.Sleep(waitTime);
            }

        }

        //Change colour
        static void WriteFullLine(string value)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(value.PadRight(Console.WindowWidth / 10));
            Console.ResetColor();
        }
        static void WriteYellowLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteYellow(string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value);
            Console.ResetColor();
        }
        static void WriteGreenLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteGreen(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(value);
            Console.ResetColor();
        }
        static void WriteCyanLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteCyan(string value)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(value);
            Console.ResetColor();
        }
        static void WriteMagentaLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteMagenta(string value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(value);
            Console.ResetColor();
        }
        static void WriteGrayLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteGray(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(value);
            Console.ResetColor();
        }
        static void WriteDarkRedLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteDarkRed(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(value);
            Console.ResetColor();
        }
        static void WriteRedLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteRed(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(value);
            Console.ResetColor();
        }
        static void WriteWhiteLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        static void WriteWhite(string value)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(value);
            Console.ResetColor();
        }
    }
}
