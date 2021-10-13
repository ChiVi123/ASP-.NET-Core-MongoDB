using DemoUser.Models;
using DemoUser.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUser.Controllers
{
    public class HomeController : Controller
    {        
        private IMongoCollection<User> collection;
        public HomeController() //
        {
            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("MYDB");
            this.collection = db.GetCollection<User>("Users");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user) //
        {
            var userobj = collection.Find(x => x.Username == user.Username).FirstOrDefault();
            /*khong dang nhap duoc khi
            1. khong ton tai user da nhap
            2. khong dung mat khau*/
            if (userobj == null || user.Password != userobj.Password)
            {
                ViewBag.Message = "Username or Password is incorrect!";
                return View();
            }
            else
            {
                GLobalId.global_id = userobj.Id;
                return RedirectToAction("profile");
            }
        }

        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult insertUser(User user)
        {
            collection.InsertOne(user);
            return RedirectToAction("Index"); //
        }

        public IActionResult profile()
        {
            User user = collection.Find(x => x.Id == GLobalId.global_id).FirstOrDefault(); //
            return View(user);
        }

        public IActionResult editUser()
        {
            User user = collection.Find(x => x.Id == GLobalId.global_id).FirstOrDefault(); //
            return View(user);
        }

        [HttpPost]
        public IActionResult updateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq("Id", user.Id); //
            var updateUser = Builders<User>.Update.Set("FirstName", user.FirstName); //
            updateUser = updateUser.Set("LastName", user.LastName);
            updateUser = updateUser.Set("LastName", user.LastName);
            updateUser = updateUser.Set("Email", user.Email);
            updateUser = updateUser.Set("Username", user.Username);
            updateUser = updateUser.Set("Password", user.Password);
            var result = collection.UpdateOne(filter, updateUser);

            if (result.IsAcknowledged)
            {
                ViewBag.Message = "User updated complete!";
            }
            else
            {
                ViewBag.Message = "Error!";
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult delete(User user)
        {            
            var result = collection.DeleteOne<User>(e => e.Id == user.Id);

            if (result.IsAcknowledged)
            {
                TempData["Message"] = "User deleted !";
            }
            else
            {
                TempData["Message"] = "Error  deleting User !";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
