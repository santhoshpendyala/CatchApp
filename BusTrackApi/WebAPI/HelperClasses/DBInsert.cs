using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization;
using WebAPI.Models;

namespace WebAPI.HelperClasses
{
    public class DBInsert
    {
        //static string connectionstring = "mongodb://localhost:27017";
        //static MongoClient client = new MongoClient(connectionstring);
        //public static IMongoDatabase db = client.GetDatabase("Track");

        static string connectionstring = "mongodb://mahesh:abcd1234@ds058048.mongolab.com:58048/catchapp";
        static MongoClient client = new MongoClient(connectionstring);
        public static IMongoDatabase db = client.GetDatabase("catchapp");

        public static string NextLocation = string.Empty;
        public int NextLocationIndex = 0;

        public DBInsert(string table, DataInsertModel mod)
        {                                    
            var collection = db.GetCollection<DataInsertModel>(table);
            DateTime st = new DateTime();
            TimeSpan ts = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) - DateTime.UtcNow;
            double ms= ts.TotalMilliseconds;
            mod.Time = ms;
            int BID = mod.BID;
            DriverBusFetchModel result = GetDetails(BID);
            double SourceLatitude = result.Source.Latitude;
            double SourceLongitude = result.Source.Longitude;
            mod.Distance=GetCalculatedDistance(SourceLatitude,SourceLongitude,mod.Location.Latitude,mod.Location.Longitude);            

            for (int i = 0; i < result.Boardings.Count; i++)
            {
                if (mod.Distance >= result.Boardings[i].Distance)
                {
                    //Need to validate IsSent Value and take only "N" values.
                    //Need to call Push Notification Method.
                    //Need to update Is_Sent                    
                    break;
                }
            }


            collection.InsertOne(mod);
        }

        public static double GetCalculatedDistance(double sourceLatitude, double sourceLongitude, double destinationLatitude, double destinationLongitude)
        {
            try
            {

                double[] destinationPair = { destinationLatitude, destinationLongitude };
                double sourceLatRadians = sourceLatitude * (Math.PI / 180.0);
                double sourceLongRadians = sourceLongitude * (Math.PI / 180.0);
                double destinationLatRadians = destinationLatitude * (Math.PI / 180.0);
                double destinationLongRadians = destinationLongitude * (Math.PI / 180.0);

                double dLongitude = destinationLongRadians - sourceLongRadians;
                double dLatitude = destinationLatRadians - sourceLatRadians;

                double refDisplacement = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                              Math.Cos(sourceLatRadians) * Math.Cos(destinationLatRadians) *
                              Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

                // Number 3956 is considered as the number of miles around the earth
                double displacement = 3956.0 * 2.0 *
                              Math.Atan2(Math.Sqrt(refDisplacement), Math.Sqrt(1.0 - refDisplacement));



                return displacement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DriverBusFetchModel GetDetails(int BID)
        {
            DriverBusFetchModel busResult = new DriverBusFetchModel();
            try
            {
                var masterCollection = db.GetCollection<DriverBusFetchModel>("tblMaster");
                FilterDefinitionBuilder<DriverBusFetchModel> MasterFetchBuilder = Builders<DriverBusFetchModel>.Filter;
                FilterDefinition<DriverBusFetchModel> Masterfilter = MasterFetchBuilder.Eq("BID", BID);
                var masterResult = masterCollection.Find(Masterfilter).SortByDescending(x => x.Id).FirstOrDefault();
                busResult = GetBusDetails(BID);
            }
            catch (Exception ex)
            {

            }
            return busResult;
        }

        public static DriverBusFetchModel GetBusDetails(int BID)
        {
            DriverBusFetchModel busResult = new DriverBusFetchModel();
            try
            {
                var busCollection = db.GetCollection<DriverBusFetchModel>("tblBus");
                FilterDefinitionBuilder<DriverBusFetchModel> BusFetchBuilder = Builders<DriverBusFetchModel>.Filter;
                FilterDefinition<DriverBusFetchModel> Busfilter = BusFetchBuilder.Eq("BID", BID);
                busResult = busCollection.Find(Busfilter).SingleOrDefault();                
            }
            catch (Exception ex)
            { 

            }
            return busResult;
            //var result = collection.Find(filter).SingleOrDefault();            
        }
    }    
}