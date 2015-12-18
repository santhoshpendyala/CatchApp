using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Models
{
    [BsonIgnoreExtraElements]
    public class DriverBusFetchModel
    {                
            [BsonId]
            public ObjectId Id { get; set; }            
            public int BID { get; set; }
            public Location Location { get; set; }
            public BoardingPoint Source { get; set; }
            public BoardingPoint Destination { get; set; }
            public List<BoardingPoint> Boardings { get; set; }
            public string Status { get; set; }
            public double Time { get; set; }
            public double BusDistance { get; set; }
    }     

        //public class Location
        //{
        //    public double Latitude { get; set; }
        //    public double Longitude { get; set; }
        //    public string AreaName { get; set; }
        //}

        //public class BoardingPoint
        //{
        //    public double Latitude { get; set; }
        //    public double Longitude { get; set; }
        //    public string AreaName { get; set; }
        //    public string Time { get; set; }
        //}
}

