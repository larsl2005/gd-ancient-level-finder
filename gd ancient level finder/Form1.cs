using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gd_ancient_level_finder
{
    
    public partial class Form1 : Form
    {
        public int FFound = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int max = Convert.ToInt32(numericUpDown1.Value);
            int min = Convert.ToInt32(numericUpDown2.Value);
            FFound = 0;
            for(int times = min; times < max + 1; times++) //repeats until max id is reached
            {
                 HttpClient client = new HttpClient();
        var values = new Dictionary<string, string>
            {
            //all arguments it sends to the server
                { "gameVersion", "21" }, //game version, the 21 means 2.1, 20 means 2.0, 22 means 2.2, ect
                { "binaryVersion", "35" },//honestly i have no idea what this means
                { "gdw", "0" }, //i think this means if it is gd world or not (i will double check later)
                { "accountID", "71" }, // rubrub's acc id
                { "type", "0" }, //type, this means if its a search, recent, magic, trending, ect.
                { "str", times.ToString() },// this is the level id
                { "diff", "-" }, // i think this means diffeculty
                { "len", "-" },// idk what this means
                { "page", "0" }, //this means page
                { "total", "0" }, //idk what this means
                { "uncompleted", "0" }, //search filter
                { "onlyCompleted", "0" },//search filter
                { "featured", "0" },//search filter
                { "original", "0" },//search filter
                { "twoPlayer", "0" },//search filter
                { "coins", "0" },//search filter
                { "epic", "0" },//search filter or mod send filter
                { "local", "1" },//idk
                { "secret", "Wmfd2893gb7" }//robtop's way of security: a code thats always the same
            };
                var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://www.boomlings.com/database/getGJLevels21.php", content); // sends everything to the server

            var responseString = await response.Content.ReadAsStringAsync();
                if(times == max + 1)
                {
                    if (times == max)
                    {
                        Console.WriteLine("Done"); // if everything is done, it prints done in the output
                        Console.WriteLine("Program has found " + FFound + " levels."); // writes the amount of levels it found
                    }
                }
                if(responseString == "-1") //checks if the server responded -1, wich is robtops way of saying fuck off
                {
                    Console.WriteLine("Level ID " +times+ " either does not exist , the servers are down or you are IP banned. Server responded: " +responseString+"");
                }
                else // if the level was found, it does this
                {
                    FFound = FFound + 1; // adds to amount of levels it found
                    //string[] eeee = { ":" };
                    //Int32 count = times; // some unused code that doesnt work
                    //String help = responseString.Split(eeee, times);
                    richTextBox1.Text = richTextBox1.Text + " " +
                        "";//idk why i made this line so advanced its literally adding a space
                    richTextBox1.Text = richTextBox1.Text + times.ToString(); // prints it in the textbox
                    Console.WriteLine("Level ID " + times + " found, server responded: " +responseString+ ""); // prints it to the output
                
                }
                
            
        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.Title = "OUTPUT";
        }
    }
}
