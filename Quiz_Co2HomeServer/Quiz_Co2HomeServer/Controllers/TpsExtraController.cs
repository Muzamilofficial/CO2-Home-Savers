using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using Quiz_Co2HomeServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quiz_Co2HomeServer.Controllers
{
    public class TpsExtraController : Controller
    {
        // GET: TpsExtra
        public async Task<ActionResult> Index()
        {
            
            var openAIApiKey = "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";

          
            var openAI = new OpenAIAPI(openAIApiKey);

       
            var completionRequest1 = new CompletionRequest
            {
                Prompt = "Generate Energy Saving World Number 1 Tip for Refregerator In One Sentence",
                Model = Model.DavinciText,
                MaxTokens = 100,
                Temperature = 0.7, 
                TopP = 1.0 
            };       
            var completions1 = await openAI.Completions.CreateCompletionAsync(completionRequest1);
            var tip1 = completions1.Completions[0].Text.Trim();            
            TempData["Tip1"] = tip1;
            
            var completionRequest2 = new CompletionRequest
            {
                Prompt = "Generate Energy Saving World Number 2nd best Tip for Refregerator In One Sentence",
                Model = Model.DavinciText,
                MaxTokens = 100, 
                Temperature = 0.7, 
                TopP = 1.0 
            };         
            var completions2 = await openAI.Completions.CreateCompletionAsync(completionRequest2);
            var tip2 = completions2.Completions[0].Text.Trim();
            TempData["Tip2"] = tip2;

            var completionRequest3 = new CompletionRequest
            {
                Prompt = "Generate Energy Saving World Number 3rd best Tip for Refregerator In One Sentence",
                Model = Model.DavinciText,
                MaxTokens = 100, 
                Temperature = 0.7, 
                TopP = 1.0 
            };            
            var completions3 = await openAI.Completions.CreateCompletionAsync(completionRequest3);
            var tip3 = completions3.Completions[0].Text.Trim();
            TempData["Tip3"] = tip3;


            return View();
        }

        [HttpPost]
        public ActionResult SaveTips(tbl_Refrigilator model)
        {
            // Validate the model here if needed
            if (ModelState.IsValid)
            {
                // Assuming you have a database context called ApplicationDbContext
                using (var db = new db_Co2HomeSaversEntities1())
                {

                    var refrigeratortip = new tbl_Refrigilator

                    {
                       Tip1 = TempData["Tip1"].ToString(), 
                       Tip2 = TempData["Tip2"].ToString(),
                       Tip3 = TempData["Tip2"].ToString(),


                       
                    };

                    //model.Tip1 = TempData["Tip1"].ToString();

                    // Add the new tip to the database
                    db.tbl_Refrigilator.Add(model);

                    // Save changes to the database
                    db.SaveChanges();
                }

                // Redirect to a success page or wherever you need to go
                return RedirectToAction("Result");
            }

            // If the model is not valid, return to the same view with errors
            return View("Index", model);

        }


    }
}