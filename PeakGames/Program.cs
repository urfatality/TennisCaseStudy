using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeakGames
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Creating a Form for easy file selection.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Begin checkpoint is used when any problems regarding the JSON file occurs.
            begin:
            var myForm = new Form1();           
            Application.Run(myForm);
            
            
            JsonUtilities util = new JsonUtilities();
            string path = "";
            //File path is received from the Form
            path = myForm.FilePath;
            Console.WriteLine($"Path {path}");
            if(path == null)
            {
                Console.WriteLine("Failed to find the file or the file is in incorrect format, please select a new file");
                Console.ReadKey();
                goto begin;
            }
            if (path.Contains(".json")){ }
            else
            {
                //If the user enters the filename without the extension .json , it is concatenated to the path here.
                path = String.Concat(path, ".json");
            }
            List<Player> playerList = util.PlayerFromJson(path);
            if(playerList == null)
            {
                //If the file is empty, the user is prompted to select a new file
                Console.WriteLine("No player information is found in the file, please select a new file");
                Console.ReadKey();
                goto begin;
                
            }
            List<Tournament> tournamentList = util.TournamentFromJson(path);
            if (playerList == null)
            {
                //If the file is empty, the user is prompted to select a new file
                Console.WriteLine("No tournament information is found in the file, please select a new file");
                Console.ReadKey();
                goto begin;

            }
            foreach(Tournament tournament in tournamentList)
            {
                tournament.SetMatches(playerList);
            }
            
         
            path = myForm.InputPath;
            //Output file is created in the same directory as the input file. The default name is output.json 
            string outputPath;
            if (myForm.OutputPath == "" || myForm.OutputPath == "Output File Name" || myForm.OutputPath == null)
                outputPath = "output";
            else
                outputPath = myForm.OutputPath;

            path = String.Concat(path, String.Concat(@"\", outputPath));
            if (path.Contains(".json")) { }
            else
            {
                path = String.Concat(path, ".json");
            }
            util.WriteResults(playerList, path);

            Console.WriteLine("Process complete, please press any key to exit");  
            Console.ReadKey();
            
        }
        
    }
}
