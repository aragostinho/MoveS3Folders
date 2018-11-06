 using MoveS3Folders.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveS3Folders
{
    public class Program
    {

        static void Main(string[] args)
        {

            try
            {
                args = SetDefaultArgs(args);
                Console.WriteLine("*****************************************************");
                Console.WriteLine("Starting MoveS3Folders");
                Console.WriteLine("*****************************************************");
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Console.WriteLine("Please wait.....\n");
                OperationResponse result = MoveToS3.CopyOrMoveObjects(args[0], args[1], args[2], args[3], args[4], args[5]);
                stopWatch.Stop();

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(result.Message);

                TimeSpan pElapsedTime = stopWatch.Elapsed;
                Console.WriteLine(string.Format("Process accomplished --- Elapsed time: {0}h-{1}m-{2}s-{3}ms",
                    pElapsedTime.Hours,
                    pElapsedTime.Minutes,
                    pElapsedTime.Seconds,
                    pElapsedTime.Milliseconds)
               );

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    Console.WriteLine("Folder moved/copied");


                string pressToFinish = Console.ReadLine();

            }
            catch (Exception oException)
            {
                Console.WriteLine("Error: {0}", oException.Message);
                Console.ReadKey();
            }


        }

        private static string[] SetDefaultArgs(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[7];
                args[0] = ConfigurationManager.AppSettings["BucketOrigin"];
                args[1] = ConfigurationManager.AppSettings["BucketDestination"];
                args[2] = ConfigurationManager.AppSettings["KeyNameOrigin"];
                args[3] = ConfigurationManager.AppSettings["KeyNameDestination"];
                args[4] = ConfigurationManager.AppSettings["MoveFolder"];
                args[5] = ConfigurationManager.AppSettings["OverwriteDestinationFolder"]; 
            } 

            return args;
        }
    }
}
