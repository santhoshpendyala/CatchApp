using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;
using WebAPI.HelperClasses;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class DataInsertController : ApiController
    {                
        [HttpPost]
        public string Post( DataInsertModel DataInsert)
        {
            DBInsert Insert = new DBInsert("tblMaster", DataInsert);
            return "Inserted Successfully";
        }
    }
}
