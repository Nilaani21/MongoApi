using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMongo.Models
{
    public class Employee
    {


        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public ObjectId InternalId;
        //[BsonElement("EMP_ID")]
        //[BsonElement("Name")]

        public int Emp_ID { get; set; }
        public string First_Name { get; set; }


        public string Last_Name { get; set; }
        public string Gender { get; set; }


        public string Dept { get; set; }
      //  public List<Employee> Employees { get; set; }


    }
}
