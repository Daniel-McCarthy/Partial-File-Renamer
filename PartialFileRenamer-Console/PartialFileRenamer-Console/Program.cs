using System;
using System.Collections.Generic;
using PartialFileRenamer;

namespace PartialFileRenamer_Console //ʕ•ᴥ•ʔ
{
    class PartialFileRenamerConsole
    {
        static void Main(string[] args)
        {
            bool exit = false;
            string input = "";

            if (args.Length == 0)
            {

                Console.WriteLine("Welcome to Partial File Renamer ver. 0.0\n");

                Console.WriteLine("Type: \"help\" for List of Commands");
                Console.WriteLine("Type: \"about\" for More Information");
                Console.WriteLine("Type: \"exit\" to Close the Program\n");
            }
            else
            {
                if (args[0] == "help" || args[0] == "-help") { outputCommands(); }
                else if (args[0] == "about" || args[0] == "-about") { outputAbout(); }
                else if (args[0] == "exit" || args[0] == "-exit") { }
                else
                {

                    foreach (string arg in args)
                    {
                        if (input.Length != 0) { input += " "; }
                        input += arg;
                    }
                    Console.WriteLine(input);
                    readInput(input);
                }
            }

            if (args.Length == 0)
            {
                while (!exit)
                {

                    input = Console.ReadLine();

                    if (input == "help" || input == "-help") { outputCommands(); }
                    else if (input == "about" || input == "-about") { outputAbout(); }
                    else if (input == "exit" || input == "-exit") { exit = true; }
                    else { readInput(input); }

                }

            }
        }

        static private void outputCommands()
        {
            Console.WriteLine();
            Console.WriteLine("\thelp\t: Output list of commands.");
            Console.WriteLine("\tabout\t: Output Information about the Program.");
            Console.WriteLine("\texit\t: Close the program.\n");

            Console.WriteLine("\trename \"<folder-path>\" \"<string-to-find>\" \"<string-to-replace-with>\"");
            Console.WriteLine("\t\tExample: rename \"C:\\Users\\Name\\Pictures\" \"Annivrsary \"Anniversary\"\n");

            Console.WriteLine("\tAdditional Arguments:\n");

            Console.WriteLine("\t-f -<extension-name>\t: Rename will only effect files with this extension.");
            Console.WriteLine("\t\tExample: rename \"C:\\Users\\Name\\Videos\" \"Vacation\" \"Hawaii-Vacation\" f \"avi\"\n");

            Console.WriteLine("\t-fo -<extension-name>\t: Rename will ignore files with this extension.");
            Console.WriteLine("\t\tExample: rename \"C:\\Users\\Name\\Documents\" \"Books\" \"Novels\" fo \"pdf\"\n");

            Console.WriteLine("\t-o\t: Rename will overwrite any existing files with the name being changed to");
            Console.WriteLine("\t\tExample: rename \"C:\\Users\\Name\\Desktop\" \"Shortcut\" \"OldShortcut\" o\n");

            Console.WriteLine("\t-s\t: Rename will effect files in all subdirectories of selected folder");
            Console.WriteLine("\t\tExample: rename \"C:\\Users\\Name\" \"MichaelJ\" \"Michael\" s\n");

            //Console.WriteLine("\t-i\t: Rename will also replace any detected matching strings inside the file");
            //Console.WriteLine("\t\tExample: rename \"C:\\Users\\Name\\Project\" \"UnnamedProject\" \"PartialFileRenamer\" i\n");

        }

        static private void outputAbout()
        {
            Console.WriteLine(@"-----------------------------------------");
            Console.WriteLine("\nPartial File Renamer ver 0.0\tby Dan McCarthy (UnknownToaster)");
            Console.WriteLine("Contact: dan.willy.mccarthy@gmail.com");
            Console.WriteLine("Source Code: https://github.com/UnknownToaster/Partial-File-Renamer");

            Console.WriteLine("\nThis program can batch rename files with names containing specific strings.");
            Console.WriteLine("The user can choose the string that is searched for, as well as the string it is replaced by.");

            Console.WriteLine("\nFor example, with these given files:");
            Console.WriteLine("UnnamedProject.exe\nUnnamedProject.dll\nUnnamedProject.pdb");

            Console.WriteLine("\nAnd this comand:\nrename -C:\\Users\\Name\\Files -UnnamedProject -PartialFileRenamer\n");

            Console.WriteLine("Our files become:");
            Console.WriteLine("PartialFileRenamer.exe\nPartialFileRenamer.dll\nPartialFileRenamer.pdb\n");
            Console.WriteLine("-----------------------------------------");

        }


        static private void readInput(string input)
        {

            List<string> arguments = split(input);

            if (arguments[0] == "rename")
            {
                string directory = arguments[1];
                string matchString = arguments[2];
                string replaceString = arguments[3];

                bool overwrite = false;
                bool filter = false;
                bool filterout = false;
                bool subdirectories = false;
                bool internalStrings = false;

                List<string> filterExt = new List<string>();
                List<string> filterOutExt = new List<string>();

                for (int i = 4; i < arguments.Count; i++)
                {
                    switch (arguments[i])
                    {
                        case "o":
                            {
                                overwrite = true;
                                break;
                            }
                        case "f":
                            {
                                filter = true;
                                filterExt.Add(arguments[i + 1]);
                                i++;
                                break;
                            }
                        case "fo":
                            {
                                filterout = true;
                                filterOutExt.Add(arguments[i + 1]);
                                i++;
                                break;
                            }
                        case "s":
                            {
                                subdirectories = true;
                                break;
                            }
                            /*
                        case "i":
                            {
                                internalStrings = true;
                                break;
                            }
                            */

                    }
                }

                FileRenamer renamer = new FileRenamer();

                renamer.rename(directory, matchString, replaceString, overwrite, internalStrings, subdirectories, filter, filterout, filterExt, filterOutExt);

            }
        }


        static List<string> split(string input)
        {
            List<string> split = new List<string>();
            while (input.Length > 0)
            {
                switch (input[0])
                {
                    case ' ':
                        {
                            input = input.TrimStart(' ');
                            break;
                        }
                    case '\"':
                        {
                            input = input.TrimStart('\"');
                            split.Add(input.Substring(0, input.IndexOf('\"')));
                            input = input.Substring(input.IndexOf('\"') + 1);
                            input = input.TrimStart('\"');
                            break;
                        }
                    default:
                        {
                            if (input.Contains(" "))
                            {
                                split.Add(input.Substring(0, input.IndexOf(' ')));
                            }
                            else
                            {
                                split.Add(input);
                                input = "";
                            }

                            input = input.Substring(input.IndexOf(' ') + 1);
                            break;
                        }
                }
            }
            return split;
        }


    }
}