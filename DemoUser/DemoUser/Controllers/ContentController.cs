using DemoUser.Global;
using DemoUser.Models;
using DemoUser.MyDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUser.Controllers
{
    public class ContentController : Controller
    {

        private IMongoCollection<Content> collection;
        ConnectDB connect = new ConnectDB();
        public ContentController()
        {
            this.collection = connect.getConnect().GetCollection<Content>("Contents");
        }

        
        public IActionResult ViewContent()
        {
            var content = collection.Find(x => x.authorid == GLobalId.global_id).ToList();
           
            return View(content);
        }

        [HttpPost]
        public IActionResult delete(string id)
        {
            ObjectId Idobj = new ObjectId(id);
            var result = collection.DeleteOne<Content>(e => e.Id == Idobj);

            if (result.IsAcknowledged)
            {
                TempData["Message"] = "Content deleted !";
            }
            else
            {
                TempData["Message"] = "Error deleting Content !";
            }
            return RedirectToAction("ViewContent");
        }
    }
}
