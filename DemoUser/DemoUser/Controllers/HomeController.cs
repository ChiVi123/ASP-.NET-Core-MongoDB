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
using DemoUser.MyDB;

namespace DemoUser.Controllers
{
    public class HomeController : Controller
    {
        private IMongoCollection<User> collection;
        ConnectDB connect = new ConnectDB();
        public HomeController()
        {
            this.collection = connect.getConnect().GetCollection<User>("Users");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
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
            return RedirectToAction("Index"); //chuyen huong den Index
        }

        public IActionResult profile()
        {
            //kiem tra id co trong collection khong
            //tra ve la mot user hoac null
            User user = collection.Find(x => x.Id == GLobalId.global_id).FirstOrDefault();
            return View(user);
        }

        public IActionResult editUser()
        {
            User user = collection.Find(x => x.Id == GLobalId.global_id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult editUser(string id, User user)
        {
            user.Id = new ObjectId(id);
            var filter = Builders<User>.Filter.Eq("Id", user.Id);
            var updateUser = Builders<User>.Update.Set("FirstName", user.FirstName);
            updateUser = updateUser.Set("LastName", user.LastName);
            updateUser = updateUser.Set("Username", user.Username);
            updateUser = updateUser.Set("Email", user.Email);
            var result = collection.UpdateOne(filter, updateUser);

            if (result.IsAcknowledged)
            {
                ViewBag.Message = "User updated complete!";
            }
            else
            {
                ViewBag.Message = "Error!";
            }
            return RedirectToAction("profile");
            //return View(result); //khong the tra ve trang edit dan den bao loi
        }

        public IActionResult delete()
        {
            User user = collection.Find(x => x.Id == GLobalId.global_id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult delete(string id)
        {
            ObjectId Idobj = new ObjectId(id);
            var result = collection.DeleteOne<User>(e => e.Id == Idobj);

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
