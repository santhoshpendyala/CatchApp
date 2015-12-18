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
    public class DataInsertModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        //public string GID { get; set; }
        public int BID { get; set; }
        public Location Location { get; set; }
        //public SourcePoint Source { get; set; }
        //public DestinationPoint Destination { get; set; }
        //public BoardingPoint Boardings { get; set; } 
        public double Distance { get; set; }
        public string Status { get; set; }
        public double Time { get; set; }
    }

    //public class Location
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //    public string AreaName { get; set; }
    //}

    //public class SourcePoint
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //    public string AreaName { get; set; }
    //    public double Time { get; set; }
    //}

    //public class DestinationPoint
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //    public string AreaName { get; set; }
    //    public double Time { get; set; }
    //}

    //public class BoardingPoint
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }       
    //    public double Time { get; set; }
    //}
}