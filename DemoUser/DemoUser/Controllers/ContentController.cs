using DemoUser.Global;
using DemoUser.Models;
using DemoUser.MyDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
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
        public IActionResult addContent(Content content, IFormFile image)
        {
           
            content.authorid = GLobalId.global_id;
            content.createdate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            MemoryStream memoryStream = new MemoryStream();
            image.OpenReadStream().CopyTo(memoryStream);
            content.image = Convert.ToBase64String(memoryStream.ToArray());
            collection.InsertOne(content);
            return RedirectToAction("viewContent"); 
        }

        public IActionResult addContent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult getContent(string contentid)
        {
            ObjectId idcontent = new ObjectId(contentid);
           Content content = collection.Find(x => x.Id == idcontent).FirstOrDefault();
            return View(content);
        }
      
        public IActionResult editContent(string id)
        {
            ObjectId Idobj = new ObjectId(id);
            Content content = collection.Find(x => x.Id == Idobj).FirstOrDefault();
            return View(content);
        }

        [HttpPost]
        public IActionResult editContent(string id, Content content, IFormFile image)
        {

            content.Id = new ObjectId(id);
            var filter = Builders<Content>.Filter.Eq("Id", content.Id);
            var updateContent = Builders<Content>.Update.Set("title", content.title);
            updateContent = updateContent.Set("brief", content.brief);
            updateContent = updateContent.Set("note", content.note);
            updateContent = updateContent.Set("createdate", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            MemoryStream memoryStream = new MemoryStream();
            image.OpenReadStream().CopyTo(memoryStream);
            content.image = Convert.ToBase64String(memoryStream.ToArray());
            updateContent = updateContent.Set("image", content.image);
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
