using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using Quiz_Co2HomeServer.Models;

namespace Quiz_Co2HomeServer.Controllers
{
    public class MakeController : Controller
    {
        db_Co2HomeSaversEntities1 dbContext = new db_Co2HomeSaversEntities1();
        public class RefrigeratorResultforquestion3
        {
            public int NoOfRefrigilators { get; set; }
        }


        public async Task<List<string>> GetRefrigeratorNamesAsync()
        {
            string username = 1.ToString();
            var openAIApiKey = "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";

            var openAI = new OpenAIAPI(openAIApiKey);

            var make1 = dbContext.tbl_Refrigilator.Where(r => r.Usernme == username).Select(r => r.Make1).FirstOrDefault();

            var completionRequest = new CompletionRequest
            {
                Prompt = "Generate 10 Model Of" + make1 + "Refrigilators ",
                Model = Model.DavinciText,
                MaxTokens = 200,
            };

            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);

            var refrigeratorNames = new List<string>();
            foreach (var completion in completions.Completions)
            {
                var r = completion.Text.Split('\n');
                foreach (var item in r.Where(x => !string.IsNullOrEmpty(x)))
                {
                    string Val = item.Split('.')[1];
                    if (Val.Contains("."))
                    {
                        Val = Val.Split('.')[1];
                    }
                    refrigeratorNames.Add(Val);
                }
            }

            return refrigeratorNames;
        }

        public async Task<List<string>> GetRefrigeratorNamesAsync2()
        {
            string username = 1.ToString();
            var openAIApiKey = "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";

            var openAI = new OpenAIAPI(openAIApiKey);

            var make2 = dbContext.tbl_Refrigilator.Where(r => r.Usernme == username).Select(r => r.Make2).FirstOrDefault();

            var completionRequest = new CompletionRequest
            {
                Prompt = "Generate 10 Model Of" + make2 + "Refrigilators ",
                Model = Model.DavinciText,
                MaxTokens = 200,
            };

            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);

            var refrigeratorNames2 = new List<string>();
            foreach (var completion in completions.Completions)
            {
                var r = completion.Text.Split('\n');
                foreach (var item in r.Where(x => !string.IsNullOrEmpty(x)))
                {
                    string Val = item.Split('.')[1];
                    if (Val.Contains("."))
                    {
                        Val = Val.Split('.')[1];
                    }
                    refrigeratorNames2.Add(Val);
                }
            }

            return refrigeratorNames2;
        }
        public async Task<List<string>> GetRefrigeratorNamesAsync3()
        {
            string username = 1.ToString();
            var openAIApiKey = "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";

            var openAI = new OpenAIAPI(openAIApiKey);

            var make3 = dbContext.tbl_Refrigilator.Where(r => r.Usernme == username).Select(r => r.Make3).FirstOrDefault();

            var completionRequest = new CompletionRequest
            {
                Prompt = "Generate 10 Model Of" + make3 + "Refrigilators ",
                Model = Model.DavinciText,
                MaxTokens = 200,
            };

            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);

            var refrigeratorNames2 = new List<string>();
            foreach (var completion in completions.Completions)
            {
                var r = completion.Text.Split('\n');
                foreach (var item in r.Where(x => !string.IsNullOrEmpty(x)))
                {
                    string Val = item.Split('.')[1];
                    if (Val.Contains("."))
                    {
                        Val = Val.Split('.')[1];
                    }
                    refrigeratorNames2.Add(Val);
                }
            }

            return refrigeratorNames2;
        }
        public class RefrigeratorResult
        {
            public int NoOfRefrigilators { get; set; }
        }

        // GET: Make
        public async Task<ActionResult> Index()
        {
            db_Co2HomeSaversEntities1 dbContext = new db_Co2HomeSaversEntities1();
            try
            {
                var refrigeratorNames = await GetRefrigeratorNamesAsync();
                ViewBag.RefrigeratorNames = refrigeratorNames;

                var refrigeratorNames2 = await GetRefrigeratorNamesAsync2();
                ViewBag.RefrigeratorNames2 = refrigeratorNames2;

                var refrigeratorNames3 = await GetRefrigeratorNamesAsync3();
                ViewBag.RefrigeratorNames3 = refrigeratorNames3;

                string username = 1.ToString();
                var result = dbContext.Database.SqlQuery<RefrigeratorResult>("exec SelectRefrigilatorsByUsernme @Username",
                    new SqlParameter("@Username", username)).FirstOrDefault();
                if (result != null)
                {
                    ViewBag.NoOfRefrigilators = result.NoOfRefrigilators;
                    if (result.NoOfRefrigilators == 2)
                    {
                        var newOptions = new List<string> { "Option X", "Option Y", "Option Z" }; // Replace with your actual new options
                        ViewBag.RefrigeratorNames2 = refrigeratorNames2; // Set the new options in ViewBag
                    }
                    if (result.NoOfRefrigilators == 3)
                    {
                        var newOptions = new List<string> { "Option X", "Option Y", "Option Z" }; // Replace with your actual new options
                        ViewBag.NewOptionsForThirdDropdown = refrigeratorNames3; // Set the new options in ViewBag
                    }
                }
                else
                {
                    ViewBag.NoOfRefrigilators = 0; // Default value
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while generating refrigerator names: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult QuestionUpdate(List<string> makes)
        {
            //UsernameModel usernameModel = Session["UsernameModelResult"] as UsernameModel;
            //if (usernameModel != null)
            // {
            //   string username = usernameModel.Username;
            string username = 1.ToString();
            // Assuming you have a DbContext called "dbContext"
            using (var dbContext = new db_Co2HomeSaversEntities1())
            {
                var refrigerator = dbContext.tbl_Refrigilator.FirstOrDefault(r => r.Usernme == username);

                if (refrigerator != null)
                {
                    try {
                        int numberOfDropdowns = makes.Count;

                        if (numberOfDropdowns >= 1)
                            refrigerator.Model1 = makes[0];
                        if (numberOfDropdowns >= 2)
                            refrigerator.Model2 = makes[1];
                        if (numberOfDropdowns >= 3)
                            refrigerator.Model3 = makes[2];

                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "An error occurred while generating refrigerator names: " + ex.Message;
                        return View();
                    }
                }

            }
            // }

            return RedirectToAction("Index");
        }

    }
}