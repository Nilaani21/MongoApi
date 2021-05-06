using ApiMongo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using System.Net.Http;
//using System.Web.Http;

namespace ApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {

        [HttpGet("get")]
        public IEnumerable<Employee> GetEmployee()
        {
            var constr = "mongodb://localhost:27017/";
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("POC");
            var collection = DB.GetCollection<Employee>("Records").Find(new BsonDocument()).ToList();
            return collection;
        }
        [HttpGet("names/{id}")]
        public Employee GetEmployeeById(int id)
        {
            Employee emp = new Employee();

            var constr = "mongodb://localhost:27017/";
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("POC");
            var collection = DB.GetCollection<Employee>("Records");
            var plant = collection.Find(Builders<Employee>.Filter.Where(s => s.Emp_ID == id)).FirstOrDefault();
            emp.Emp_ID = plant.Emp_ID;
            emp.Dept = plant.Dept;
            return emp;
           
        }
        [HttpPost("EmpidList")]
        public async Task<IEnumerable<Employee>> GetEmployeeByIdsAsync([FromBody]int[] EmpidList)
        {
            List<Employee> empList = new List<Employee>();



            var constr = "mongodb://localhost:27017/";
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("POC");
            var collection = DB.GetCollection<Employee>("Records");
            var plant = Builders<Employee>.Filter;

     
            var filter = Builders<Employee>.Filter.In(a => a.Emp_ID, EmpidList);
            var res = await collection.Find(filter).ToListAsync();
            //  foreach ()
            //  {
            //      //   {
            //      Employee e2 = new Employee();


            //  e1.Emp_ID = plant.Emp_ID;
            //e1.Dept = plant.Dept;

            //empList.Add(e1);
            //}

            return res;
        }
        //[HttpGet("id/{EmpidList}")]
        //public async Task<IEnumerable<Employee>> GetEmployeeByIdsAsync([FromBody] int[] EmpidList)
        //{
        //    List<Employee> empList = new List<Employee>();

        //    var constr = "mongodb://localhost:27017/";
        //    var Client = new MongoClient(constr);
        //    var DB = Client.GetDatabase("POC");
        //    var collection = DB.GetCollection<Employee>("Records");
        //    var plant = Builders<Employee>.Filter;

        //    var filter = Builders<Employee>.Filter.In(s => s.Emp_ID, EmpidList);
        //    var res = await collection.Find(filter).ToListAsync();


        //    return empList;
        //}

    }
}
    


