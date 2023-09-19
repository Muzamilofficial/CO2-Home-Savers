using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Quiz_Co2HomeServer.Models;

namespace Quiz_Co2HomeServer.Controllers
{
    public class MakeQ4ExtraController : Controller
    {
        db_Co2HomeSaversEntities1 dbContext = new db_Co2HomeSaversEntities1();

        public class RefrigeratorResult
        {
            public int NoOfRefrigilators { get; set; }
        }

        // GET: MakeTips
        public ActionResult Index()
        {
            try
            {
                string username = "1"; // Use a string representation of the username
                var result = dbContext.Database.SqlQuery<RefrigeratorResult>("exec SelectRefrigilatorsByUsernme @Username",
                    new SqlParameter("@Username", username)).FirstOrDefault();
                if (result != null)
                {
                    ViewBag.NoOfRefrigilators = result.NoOfRefrigilators;
                }
                else
                {
                    // Handle the case when the stored procedure didn't return a result
                    ViewBag.NoOfRefrigilators = 0; // Default value
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while generating refrigerator names: " + ex.Message;
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult q4update(List<string> makes)
        {
            //UsernameModel usernameModel = Session["UsernameModelResult"] as UsernameModel;
           // if (usernameModel != null)
           // {
                string username = 1.ToString();
            // Assuming you have a DbContext called "dbContext"
            using (var dbContext = new db_Co2HomeSaversEntities1())
            {
                var refrigerator = dbContext.tbl_Refrigilator.FirstOrDefault(r => r.Usernme == username);

                if (refrigerator != null)
                {
                    int numberOfDropdowns = makes.Count;

                    if (numberOfDropdowns >= 1)
                        refrigerator.Wallt1 = makes[0];
                    // Assuming 'refrigerator.Make1' is a string
                    //Session["Make1"] = refrigerator.Make1;

                    if (numberOfDropdowns >= 2)
                        refrigerator.Wallt2 = makes[1];
                    if (numberOfDropdowns >= 3)
                        refrigerator.Wallt3 = makes[2];

                    dbContext.SaveChanges();

                    dbContext.Database.ExecuteSqlCommand("EXEC UpdateTotalWattsForUser @Username", new SqlParameter("@Username", "1"));

                }
            }
            //}

            return RedirectToAction("Index");
        }
    
    
    }
}
