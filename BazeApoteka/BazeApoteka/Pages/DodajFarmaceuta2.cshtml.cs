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
using System.Security.Policy;
using MongoDB.Thin;
using MongoDB.Driver.Builders;

namespace BazeApoteka.Pages
{
    public class DodajFarmaceuta2Model : PageModel
    {
        [BindProperty]
        public Farmaceut Farmaceut { get; set; }
        [BindProperty]
        public Apoteka Apoteka { get; set; }
        [BindProperty]
        public Apoteka Apoteka2 { get; set; }
        public List<MongoDBRef> farmaceuti { get; set; }
        public IMongoCollection<Farmaceut> collectionF { get; set; }
        public IMongoCollection<Apoteka> collectionA { get; set; }
        [BindProperty]
        public bool ok { get; set; }

        public IActionResult OnGet([FromRoute] String id)
        {
            
            return Page();
        }
        public IActionResult OnPost([FromRoute] String id)
        {
            
            return Page();
        }

        public IActionResult OnPostDodaj([FromRoute] String pom)
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Apoteka3");

            collectionF = database.GetCollection<Farmaceut>("farmaceuti");
            collectionA = database.GetCollection<Apoteka>("apoteke");

            Apoteka2 = collectionA.Find(x => x.RegistarskiBroj == Apoteka.RegistarskiBroj).FirstOrDefault();
            Farmaceut.MojaApoteka = new MongoDBRef("apoteke", Apoteka2.Id);
            collectionF.InsertOne(Farmaceut);

            farmaceuti = new List<MongoDBRef>();
            farmaceuti = Apoteka2.Farmaceuti;
            farmaceuti.Add(new MongoDBRef("farmaceuti", Farmaceut.Id));
            var res = Builders<Apoteka>.Filter.Eq(pd => pd.Id, Apoteka2.Id);
            var operation = Builders<Apoteka>.Update.Set(u => u.Farmaceuti, farmaceuti);
            database.GetCollection<Apoteka>("apoteke").UpdateOne(res, operation);
            ok = true;
            return Page();
        }
    }
}
