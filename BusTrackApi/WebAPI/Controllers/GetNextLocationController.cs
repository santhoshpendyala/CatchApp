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
    public class GetNextLocationController : ApiController
    {
        DriverBusFetchModel busResult = new DriverBusFetchModel();
        static string connectionstring = "mongodb://localhost:27017";
        //static string connectionstring = "mongodb://mahesh:abcd1234@ds058048.mongolab.com:58048/catchapp";
        static MongoClient client = new MongoClient(connectionstring);
        //public static IMongoDatabase db = client.GetDatabase("catchapp");
        public static IMongoDatabase db = client.GetDatabase("Track");
        
        public DriverBusFetchModel Get(int BID)
        {
            try
            {
                var masterCollection = db.GetCollection<DriverBusFetchModel>("tblMaster");
                FilterDefinitionBuilder<DriverBusFetchModel> MasterFetchBuilder = Builders<DriverBusFetchModel>.Filter;
                FilterDefinition<DriverBusFetchModel> Masterfilter = MasterFetchBuilder.Eq("BID", BID);
                var masterResult = masterCollection.Find(Masterfilter).SortByDescending(x => x.Id).FirstOrDefault();

                busResult = GetBusDetails(BID);
                busResult.Time = masterResult.Time;

                busResult.Location = masterResult.Location;
                busResult.Status = masterResult.Status;
            }
            catch (Exception ex)
            {

            }
            return busResult;
        }

        public static DriverBusFetchModel GetBusDetails(int BID)
        {
            var busCollection = db.GetCollection<DriverBusFetchModel>("tblBus");
            FilterDefinitionBuilder<DriverBusFetchModel> BusFetchBuilder = Builders<DriverBusFetchModel>.Filter;
            FilterDefinition<DriverBusFetchModel> Busfilter = BusFetchBuilder.Eq("BID", BID);
            DriverBusFetchModel busResult = busCollection.Find(Busfilter).SingleOrDefault();
            return busResult;
            //var result = collection.Find(filter).SingleOrDefault();            
        }

        //public static DriverBusFetchModel GetNextBoarding()
        //{ 

        //}
    }
}
