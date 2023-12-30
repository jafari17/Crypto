using ChangePrice.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ChangePrice.Data
{
    public class ReadWriteContext
    {
        public string ReadFile()
        {

            string line = "";
            string All = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\RED\\Documents\\GitHubP\\Crypto\\ChangePrice\\Data\\TextContext.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    //Console.WriteLine(line);
                    All += line;
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();

                if (All == "")
                {
                    return "[]";
                }
        
                return All;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                //Console.ReadKey();
                return ("Exception: " + e.Message);
            }
        }

        public void WriteFile(string jsonString)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("C:\\Users\\RED\\Documents\\GitHubP\\Crypto\\ChangePrice\\Data\\TextContext.txt");
                //Write a line of text
                sw.WriteLine(jsonString);
                //Write a second line of text
                sw.WriteLine("");
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
