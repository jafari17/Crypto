
using ChangePrice.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ChangePrice.Repository
{
    public class PriceJsonFileRepository : IPriceRepository
    {
        private IWebHostEnvironment _environment;
        public PriceJsonFileRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        private string GetFilePath()
        {
            string serverPath = _environment.WebRootPath;
            var path = Path.Combine(serverPath, "Data", "TextContext.txt");
            return path;
        }

        public List<RegisterPriceModel> GetList()
        {

            string line = "";
            string All = "";
            try
            {
                string path = GetFilePath();

                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(GetFilePath());
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
                    All = "[]";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }



            //--------------- JsonConvert.DeserializeObject --------------- // 

            List<RegisterPriceModel> registerPriceList = new List<RegisterPriceModel>();
            try
            {
                registerPriceList = JsonConvert.DeserializeObject<List<RegisterPriceModel>>(All);
                return registerPriceList;
            }
            catch
            {
                Console.WriteLine("Json Convert Error");
            }

            return registerPriceList;

        }


        public object Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(List<RegisterPriceModel> item)
        {


            string jsonString = JsonSerializer.Serialize(item);

            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(GetFilePath());
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
