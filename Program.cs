using System;
using CaptchaSharp;
using CaptchaSharp.Services;
using CaptchaSharp.Services.More;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Captcha
{
    internal class Program
    {
        public static string CaptchaType = "TwoCaptcha";
        public static string CaptchaCred = "Credentials";

        public static decimal Balance = 0;
        public static int Timeout = 120;

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.Write(" Enter Captcha Type (TwoCaptcha, Custom TwoCaptcha, Anti Captcha): ");
            Program.CaptchaType = Console.ReadLine();

            Console.WriteLine();
            Console.Write(" Enter Captcha Credentials: ");
            Program.CaptchaCred = Console.ReadLine();

            Console.WriteLine();
            Console.Write(" Getting Balance, Please Wait: ");

            try
            {
                Balance = GetService().GetBalanceAsync().Result;
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.ToString());
                System.Threading.Thread.Sleep(-1);
            } 

            Console.WriteLine($"Current Balance: {(decimal)Balance}");
            Console.WriteLine();

            Console.WriteLine(" Press Any Key To Exit.");
            Console.ReadKey();
        }

        public static CaptchaService GetService()
        {
            CaptchaService Service;

            switch (Program.CaptchaType)
            {
                case "TwoCaptcha": Service = new TwoCaptchaService(CaptchaCred); break;

                case "Custom TwoCaptcha": var credentials = CaptchaCred.Split(new char[] { ':' }, StringSplitOptions.None); Service = new CustomTwoCaptchaService(credentials[0], new Uri($"http://{credentials[1]}:{credentials[2]}")); break;

                case "Anti Captcha": Service = new AntiCaptchaService(CaptchaCred); break;

                default: throw new NotSupportedException();
            }

            Service.Timeout = TimeSpan.FromSeconds(Timeout);    

            return Service;
        }
    }
}
