using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using WebAPI.Models;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class FetchBIDController : ApiController
    {
        static string connectionstring = "mongodb://localhost:27017";
        static MongoClient client = new MongoClient(connectionstring);
        public static IMongoDatabase db = client.GetDatabase("Track");

        public FetchBusDetailsModel Get(string GID)
        {
            
            //DataTable dt = new DataTable();                                               
            var collection = db.GetCollection<FetchBusDetailsModel>("tblUser");
            FilterDefinitionBuilder<FetchBusDetailsModel> FetchBuilder = Builders<FetchBusDetailsModel>.Filter;
            FilterDefinition<FetchBusDetailsModel> filter = FetchBuilder.Eq("GID", GID);
            var result = collection.Find(filter).SingleOrDefault();
            int BID = Convert.ToInt16(result.BID);
            FetchBusDetailsModel busDetails =GetDetails(GID,BID);
            return busDetails;
        }

        public static FetchBusDetailsModel GetDetails(string GID, int BID)
        {
            var masterCollection = db.GetCollection<FetchBusDetailsModel>("tblMaster");
            FilterDefinitionBuilder<FetchBusDetailsModel> MasterFetchBuilder = Builders<FetchBusDetailsModel>.Filter;
            FilterDefinition<FetchBusDetailsModel> Masterfilter = MasterFetchBuilder.Eq("BID", BID);
            var masterResult = masterCollection.Find(Masterfilter).SortByDescending(x=>x.Id).FirstOrDefault();
            masterResult.GID = GID;
            FetchBusDetailsModel busResult = GetBusDetails(BID);
            busResult.Time = masterResult.Time;
            busResult.GID = GID;
            busResult.Location = masterResult.Location;
            busResult.Status = masterResult.Status;
            return busResult;

        }
        public static FetchBusDetailsModel GetBusDetails(int BID)
        {            
            var busCollection = db.GetCollection<FetchBusDetailsModel>("tblBus");            
            FilterDefinitionBuilder<FetchBusDetailsModel> BusFetchBuilder = Builders<FetchBusDetailsModel>.Filter;
            FilterDefinition<FetchBusDetailsModel> Busfilter = BusFetchBuilder.Eq("BID", BID);
            FetchBusDetailsModel busResult = busCollection.Find(Busfilter).SingleOrDefault();
            return busResult;
            //var result = collection.Find(filter).SingleOrDefault();            
        }
    }
}
