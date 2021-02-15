using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MongoDB.Bson;
using BazeApoteka.Entiteti;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
namespace BazeApoteka.Pages
{
    public class ObrisiFarmaceutaModel : PageModel
    {

        [BindProperty]
        public String Prosledjeno { get; set; }
        [BindProperty]
        public bool ok { get; set; }
        public IMongoCollection<Farmaceut> collection { get; set; }
        [BindProperty]
        public List<Farmaceut> farmaceuti { get; set; }
        [BindProperty]
        public Farmaceut Farmaceut { get; set; }
        public void OnGet()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");

            farmaceuti = collection.Find(FilterDefinition<Farmaceut>.Empty).ToList();
        }
        public IActionResult OnPost()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");


            collection = database.GetCollection<Farmaceut>("farmaceuti");
            farmaceuti = collection.Find(FilterDefinition<Farmaceut>.Empty).ToList();
            return Page();
        }
        public IActionResult OnPostObrisi()
        {

            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");
            collection = database.GetCollection<Farmaceut>("farmaceuti");
            ObjectId idF = ObjectId.Parse(Prosledjeno);
           // farmaceuti = collection.Find(x => x.Id == idF).ToList();
          
           collection.DeleteOne(x => x.Id == idF);
           ok=true;
            farmaceuti = collection.Find(FilterDefinition<Farmaceut>.Empty).ToList();
            return Page();
        }
    }
}
