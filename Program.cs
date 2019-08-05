using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;

namespace UptimeCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hard code some URLs for now.
            var urls = new List<string>();
            urls.Add("https://google.com");
            urls.Add("https://bbc.com");

            //Ask if we want to check 1 URL or multiple.
            Console.Write("Would you like to check One or More URLs (One|More): ");
            string oneOrMore = Console.ReadLine();

            if(oneOrMore == "One")
            {            
                Console.Write("What URL would you like to check: https://");
                string url = $"https://{Console.ReadLine()}";
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "HEAD";

                    var response = (HttpWebResponse)request.GetResponse();

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Looks good, Captain! - Status is 200");
                    }
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Whoops! You have a 404");

                    //Send an email, just to test
                    //MailMessage mail = new MailMessage();
                    //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    //mail.From = new MailAddress("your_email_address@gmail.com");
                    //mail.To.Add("graham@whole.school");
                    //mail.Subject = $"the URL {url} is getting a 404 message";
                    //mail.Body = $"the URL {url} is getting a 404 message";

                    //SmtpServer.Port = 587;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                    //SmtpServer.EnableSsl = true;
                }
            }
            else if(oneOrMore == "More")
            {
                foreach(string url in urls)
                {
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "HEAD";

                    var response = (HttpWebResponse)request.GetResponse();

                    //Console.WriteLine($"Response is: {response.StatusCode}");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine($"Looks good, Captain! - The URL: {url} is showing a Status of 200");
                        Console.WriteLine("");
                    }
                }
            }
            
            Console.ReadLine();
        }
    }
}
