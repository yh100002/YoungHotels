using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

using Young.Core.Cqrs;
using Young.App.CQRS.Aggregate;

namespace Young.Web.Hotel
{
    public class DBInitializer
    {
        public static void DataInitDump()
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString;
                string dbName = ConfigurationManager.AppSettings["DB"];
                string collectionName = ConfigurationManager.AppSettings["COLLECTION"];

                string filePath = HttpContext.Current.Server.MapPath("~/App_Data/hotelsrates.json");

                var client = new MongoClient(cs);

                var server = client.GetServer();

                var database = server.GetDatabase(dbName);

                database.DropCollection(collectionName);

                string text = System.IO.File.ReadAllText(filePath);

                var jsonDto = JsonConvert.DeserializeObject<Hotels[]>(text);

                var collection = database.GetCollection<Hotels>(collectionName);

                foreach (var obj in jsonDto)
                {
                    collection.Insert(obj);

                }
            }
            catch(Exception)
            {

            }
        }
    }
}