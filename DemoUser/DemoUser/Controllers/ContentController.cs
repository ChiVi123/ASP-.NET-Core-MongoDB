using DemoUser.Global;
using DemoUser.Models;
using DemoUser.MyDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

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

        
        public IActionResult viewContent()
        {
            var content = collection.Find(x => x.authorid == GLobalId.global_id).ToList();           
            return View(content);
        }

        [HttpPost]
        public IActionResult deleteContent(string id)
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

        [HttpPost]
        public IActionResult addContent(Content content)
        {
            content.authorid = GLobalId.global_id;
            content.createdate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            collection.InsertOne(content);
            return RedirectToAction("viewContent"); 
        }

        public IActionResult addContent()
        {
            return View();
        }

        public IActionResult editContent(string id)
        {
            ObjectId Idobj = new ObjectId(id);
            Content content = collection.Find(x => x.Id == Idobj).FirstOrDefault();
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
            updateContent = updateContent.Set("createdate", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

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
