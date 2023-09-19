using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using System.Linq;
using System.Threading.Tasks;
using Quiz_Co2HomeServer.Models;
using OpenAI_API.Models;

namespace Quiz_Co2HomeServer.Controllers
{
    public class ChatGPTController : Controller
    {
        // GET: ChatGPT
        public async Task<ActionResult> Index()
        {
            try
            {
                // Replace with your actual API key
                var openAIApiKey = "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";

                // Create an instance of the OpenAI API client
                var openAI = new OpenAIAPI(openAIApiKey);

                // Configure the completion request
                var completionRequest = new CompletionRequest
                {
                    Prompt = "Top 10 refrigerator models",
                    Model = Model.DavinciText,
                    MaxTokens = 200,// Adjust the number of tokens as needed
                };

                // Make an asynchronous API call to get completions
                var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);

                // Extract the refrigerator names from completions
                var refrigeratorNames = new List<string>();
                foreach (var completion in completions.Completions)
                {
                    refrigeratorNames.Add(completion.Text);
                }

                // Pass the generated names to the view
                ViewBag.RefrigeratorNames = refrigeratorNames;

                return View();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, display an error message)
                ViewBag.ErrorMessage = "An error occurred while generating refrigerator names: " + ex.Message;
                return View();
            }
        }

    }
}
