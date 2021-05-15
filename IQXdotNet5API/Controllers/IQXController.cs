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

                //string connectionString = "DRIVER={SQL Anywhere 17};server=iqxbuild;Database=iqxbuild;Uid=match;pwd=hctam;LINKs=tcpip(host=h-iqxtest-01,4096);CPOOL=YES(MaxCached=100);IDLE=1;";
                string queryString = "Select top 10 PersonId, Name from Person";
                OdbcCommand command = new OdbcCommand(queryString);

                //using (OdbcConnection connection = new OdbcConnection(connectionString))
                using (OdbcConnection connection = _client.GetConnection())
                {
                    command.Connection = connection;
                    connection.Open();

                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Person p = new Person() { PersonId = reader.GetString(0), Name = reader.GetString(1) };
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
