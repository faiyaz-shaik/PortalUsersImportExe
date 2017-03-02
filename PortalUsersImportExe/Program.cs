using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using PortalUsersImportExe.Model;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace PortalUsersImportExe
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = args[0];
            string givenName = args[1];
            string surName = args[2];
            string primaryEmailAddress = args[3];

            try
            {
                 User user = new User
                {
                    Title = title,
                    GivenName = givenName,
                    Surname = surName,
                    PrimaryEmailAddress = primaryEmailAddress,
                    Password = "E-library2"
                };
                Log("INFO: User Details: " + user.Title + " " + user.GivenName + " " + user.Surname + " " + user.PrimaryEmailAddress);
                Task<User> newUser = CreateUser(user);
                Console.WriteLine(newUser.Result.Id);
                Log("SUCCESS: Account Successfully created " + primaryEmailAddress + " : Id " + newUser.Result.Id);
            }
            catch (Exception exp)
            {
                Log("ERROR: " + primaryEmailAddress + " " + exp.Message);
                Console.WriteLine("-100");
            }

            
        }

        private static async Task<User> CreateUser(User user)
        {

            var appSettings = ConfigurationManager.AppSettings;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(appSettings["Environment"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (appSettings["Authorization"] != null) { 
                client.DefaultRequestHeaders.Add("Authorization", appSettings["Authorization"]);
            }

            //System.Net.ServicePointManager.Expect100Continue = false;

            Log("SUCCESS: Calling PostAsJsonAsync " + user.PrimaryEmailAddress);
            HttpResponseMessage response = new HttpResponseMessage();
             try
            {
                string json = JsonConvert.SerializeObject(user);

                response = await client.PostAsJsonAsync("api/user/create", user);
            }
            catch(Exception exp)
            {
                Log("ERROR: " + exp.Message  + " " + user.PrimaryEmailAddress);
            }

            Log("INFO: Status Code " + response.StatusCode);

            // Return the URI of the created resource.
            return await response.Content.ReadAsAsync<User>();
            //return Task<res>;

        }

        public static void Log(string logMessage)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                try
                {
                    w.WriteLine("{0} {1} {2} {3} {4}", "[",DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(),"]",
                             logMessage);
                }
                catch
                {

                }
            }
          
        }

    }
}
