using System;
using System.Collections.Generic;
using System.Data.Odbc;

using IQXdotNet5API.Db;

using Microsoft.AspNetCore.Mvc;

namespace IQXdotNet5API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IQXController : ControllerBase
    {
        public readonly IIQXDbClient _client;

        public IQXController(IIQXDbClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Person> people = new List<Person>();

                string queryString = "Select top 10 EmployeeID, GivenName from Employees";
                OdbcCommand command = new OdbcCommand(queryString);

                using (OdbcConnection connection = _client.GetConnection())
                {
                    command.Connection = connection;
                    connection.Open();

                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Person p = new Person() { EmployeeID = reader.GetString(0), GivenName = reader.GetString(1) };
                            people.Add(p);
                        }
                    }
                }

                return Ok(people);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
