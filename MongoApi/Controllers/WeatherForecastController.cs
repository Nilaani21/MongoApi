using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoApi.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Net.Http;
//using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace MongoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
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
           // var ids = new int[] { 1, 2, 3, 4, 5 };
            //var query = query.In("name", BsonArray.Create(emp));
            //var items = collection.Find(query);
        }
        [HttpGet("{EmpidList}")]
        public async Task<IEnumerable<Employee>> GetEmployeeByIdsAsync([FromBody]int[] EmpidList)
        {
            List<Employee> empList = new List<Employee>();
            


            var constr = "mongodb://localhost:27017/";
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("POC");
            var collection = DB.GetCollection<Employee>("Records");
            var plant = Builders<Employee>.Filter;
           
            //var filter = plant.AnyIn(x => x.Emp_ID, EmpidList.FirstOrDefault());
            //var EmpidListr = plant.In(x => x.Emp_ID, EmpidList);
            //  var plant = collection.Find(Builders<Employee>.Filter.Where(s => s.Emp_ID == id)).FirstOrDefault();
            var filter = Builders<Employee>.Filter.In( a => a.Emp_ID,EmpidList);   
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
    }
}
