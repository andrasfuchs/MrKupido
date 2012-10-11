using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Model;
using MrKupido.DataAccess;
using System.Data.Entity;
using System.IO;
using MrKupido.Utils;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace MrKupido.RecipeUpload.MindMegette
{
    class Program
    {
        private static MrKupidoContext context = new MrKupidoContext();
        private static ManualResetEvent doneEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MrKupidoContext>());
            //string[] files = new string[20];
            string[] files = Directory.GetFiles(args[0]);
            //Array.Copy(files2, files, 20);

            WorkItem.NumBusy = files.Length;
            WorkItem[] workItems = new WorkItem[files.Length];

            ThreadPool.SetMaxThreads(16, 4);
            for (int i = 0; i < files.Length; i++)
            {
                WorkItem wi = new WorkItem(files[i], doneEvent);
                workItems[i] = wi;
                ThreadPool.QueueUserWorkItem(wi.ThreadPoolCallback, i);
            }

            Console.WriteLine(DateTime.Now.ToLongTimeString());
            while (WorkItem.NumBusy > 0)
            {
                Thread.Sleep(3000);
                Console.Write("{0} ", (1.0 - ((float)WorkItem.NumBusy / files.Length)).ToString("0.00%"));
            }

            // Wait for all threads in pool to calculation...
            doneEvent.WaitOne();
            Console.WriteLine(DateTime.Now.ToLongTimeString());

            Console.WriteLine("Saving...");
            for (int i = 0; i < files.Length; i++)
            {
                ImportedRecipe cr = workItems[i].Recipe;
                ImportedRecipe ir = context.ImportedRecipes.FirstOrDefault(r => (r.UniqueName == cr.UniqueName) && (r.Language == "hun"));
                if (ir == null) ir = context.ImportedRecipes.FirstOrDefault(r => r.UniqueName == (cr.UniqueName + "-hun") && (r.Language == "hun"));

                if (ir == null)
                {
                    //context.ImportedRecipes.Add(workItems[i].Recipe);
                }
                else
                {
                    if (ir.OriginalDirections != workItems[i].Recipe.OriginalDirections)
                    {
                        ir.OriginalDirections = workItems[i].Recipe.OriginalDirections;
                    }
                }

                if (i % 250 == 0)
                {
                    context.SaveChanges();
                    Console.Write("{0} ", ((float)i / files.Length).ToString("0.00%"));
                }
            }
            context.SaveChanges();
            Console.WriteLine(DateTime.Now.ToLongTimeString());
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
