using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace RPG_Game.Statics
{
    static class FileHandling
    {
        //Pathway to directory that contains save file
        static string pathway = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Saves\\";
        static string file = "playergame.save";
        static string pathwayFull = string.Concat(pathway, file);




        /*----------------------------------------------------------------------
                     CHECK EXISTANCE OF FILES AND FOLDERS AT STARTUP
         ----------------------------------------------------------------------
        
         Method that checks the existance of a folder and file, and if it does not exist, it creates it.
         The file is used the first time this program runs on a computer. The purpose is to check for save file, 
         and else create the folders needed to hold the file.
         */
        public static bool StartCheckExistance()
        {


            if (Directory.Exists(pathway))
            {
                if (File.Exists(pathwayFull))
                    return true;
                else
                {
                    var saveFile = File.Create(pathwayFull);
                    saveFile.Close();
                }
            }
            else
            {
                Directory.CreateDirectory(pathway);
            }

            return false;
        }
        public static int CheckFileFolderExistance()
        {


            bool fileAndFolderExisting = default(bool);
            int errorCounter = 0;

            /*
             If program fails to check, create folder or file 3 times, it will exit. Else it will fall out
            of the loop after checking existance of file and folder, and, if needed, created them.
             */
            do
            {
                fileAndFolderExisting = FileHandling.StartCheckExistance();
                if (!fileAndFolderExisting)
                    errorCounter++;
                if (errorCounter >= 3)
                    Environment.Exit(0);

            } while (!fileAndFolderExisting);


            return errorCounter;
        }
        /*----------------------------------------------------------------------
         [END OF]            CHECK EXISTANCE OF FILES AND FOLDERS AT STARTUP
         ----------------------------------------------------------------------*/







        /*----------------------------------------------------------------------
                          SAVE AND READ PLAYER TO/FROM FILE
         ----------------------------------------------------------------------*/

        public static string SavePlayerToFile(List<Player> listofplayers)
        {
            Console.Clear();
            Print.LogoPrint();
            Print.DragonPrint();
            string textToPrint = "SAVING YOUR GAME";

            for (int i = 0; i < 3; i++)
            {
                Print.SetTopLeftCursorPosToStandard();
                Console.Write(textToPrint);

                for (int j = 0; j < 3; j++)
                {
                    Console.SetCursorPosition(61 + j, 13);

                    Console.Write(".");
                    Thread.Sleep(250);

                }
                Console.SetCursorPosition(61, 13);
                Console.Write(new string(' ', 3));


            }


            BinarySerializer(listofplayers);
            return "Game saved";
        }



        //Method for writing the Player as an object(List with instances of an class) to a file
        public static void BinarySerializer(List<Player> list)
        {
            //Starts a filestream and creates a BinaryFormatter called bd
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();

            fileStream = File.Create(pathwayFull);
            bf.Serialize(fileStream, list);
            fileStream.Close();
        }

        //Method for reading the above file and restoring the previous state for the program.
        public static List<Player> BinaryDeSerializer(List<Player> myList)
        {
            myList.Clear();
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(pathwayFull))
            {
                fileStream = File.OpenRead(pathwayFull);
                myList = (List<Player>)bf.Deserialize(fileStream);
                fileStream.Close();
            }

            return myList;
        }

        /*----------------------------------------------------------------------
         [END OF]           SAVE AND READ PLAYER TO/FROM FILE
         ----------------------------------------------------------------------*/







    }
}
