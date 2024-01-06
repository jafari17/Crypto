
using ChangePrice.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ChangePrice.Repository
{
    public class PriceJsonFileRepository : IPriceRepository
    {
        private IWebHostEnvironment _environment;

        private readonly IConfiguration _configuration;
        private readonly string _Directory;
        private readonly string _JsonFile;

        private readonly ILogger _logger;

        public PriceJsonFileRepository(IWebHostEnvironment environment, IConfiguration configuration, ILogger<PriceJsonFileRepository> logger)
        {
            _environment = environment;
            _logger = logger;

            _configuration = configuration;
            _Directory = _configuration.GetValue<string>("DataPath:Directory");
            _JsonFile = _configuration.GetValue<string>("DataPath:JsonFile");


        }
        private string GetFilePath()
        {
            string serverPath = _environment.WebRootPath;
            var path = Path.Combine(serverPath, _Directory, _JsonFile);
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

                _logger.LogInformation("GetList successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                _logger.LogError("Exception: " + e.Message);
            }



            //--------------- JsonConvert.DeserializeObject --------------- // 

            List<RegisterPriceModel> registerPriceList = new List<RegisterPriceModel>();
            try
            {
                registerPriceList = JsonConvert.DeserializeObject<List<RegisterPriceModel>>(All);
                return registerPriceList;

                _logger.LogInformation("registerPriceList successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Json Convert Error");
                _logger.LogError("Exception: " + e.Message);
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

                _logger.LogInformation(" Add RegisterPrice successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                _logger.LogError("Exception: " + e.Message);
            }
        }
    }
}
