<<<<<<< HEAD
﻿using DemoUser.Global;
using DemoUser.Models;
using DemoUser.MyDB;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
=======
﻿using DemoUser.Models;
using Microsoft.AspNetCore.Mvc;
>>>>>>> fdcab8e9979aae6a79a779b0eb99dad591a2309f
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
<<<<<<< HEAD

        private IMongoCollection<Content> collection;
        ConnectDB connect = new ConnectDB();
        public ContentController()
        {
            this.collection = connect.getConnect().GetCollection<Content>("Contents");
        }

        
        public IActionResult ViewContent()
=======
        private IMongoCollection<Content> collection;
        ConnectDB connect = new ConnectDB();
        public ContentController() {
            this.collection = connect.getConnect().GetCollection<Content>("Content");
        }
        public IActionResult Index()
>>>>>>> fdcab8e9979aae6a79a779b0eb99dad591a2309f
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
