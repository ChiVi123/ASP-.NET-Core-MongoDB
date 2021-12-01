using DemoUser.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoUser.MyDB;
using MongoDB.Driver;
using DemoUser.Global;
using MongoDB.Bson;

namespace DemoUser.Controllers
{
    public class ContentController : Controller
    {
        private IMongoCollection<Content> collection;
        ConnectDB connect = new ConnectDB();
        public ContentController() {
            this.collection = connect.getConnect().GetCollection<Content>("Content");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addContent(Content content)
        {
            collection.InsertOne(content);
            return RedirectToAction("viewContent"); 
        }
        public IActionResult addContent()
        {
            return View();
        }
        public IActionResult viewContent()
        {
            return View();
        }
        public IActionResult editContent()
        {
            Content content = collection.Find(x => x.Id == GLobalId.global_id).FirstOrDefault();
            return View(content);
        }
        [HttpPost]
        public IActionResult editContent(string id, Content content)
        {

            content.Id = new ObjectId(id);
            var filter = Builders<Content>.Filter.Eq("Id", content.Id);
            var updateContent = Builders<Content>.Update.Set("title", content.title);
            updateContent = updateContent.Set("brief", content.brief);
            updateContent = updateContent.Set("note", content.note);
       
            var result = collection.UpdateOne(filter, updateContent);

            if (result.IsAcknowledged)
            {
                ViewBag.Message = "Content updated complete!";
            }
            else
            {
                ViewBag.Message = "Error!";
            }
            return RedirectToAction("viewContent");
        
        }

    }
}
