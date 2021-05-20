using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using NUnit.Framework;

namespace IQXdotNet5API.Testing
{
    //[Ignore("Need to figure out how to install the ODBC driver for tests")]
    public class Tests : TestBase
    {
        [Test]
        public void can_connect()
        {
            using var connection = Db.GetConnection();
            connection.Open();
            Assert.That(connection.State, Is.EqualTo(ConnectionState.Open));
        }

        [Test]
        public void can_get_list_of_person_records()
        {
            List<Person> people = new List<Person>();
            OdbcCommand command = new OdbcCommand("Select top 10 EmployeeID, GivenName from Employees");

            using (var connection = Db.GetConnection())
            {
                command.Connection = connection;
                connection.Open();

                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person p = new Person { EmployeeID = reader.GetString(0), GivenName = reader.GetString(1) };
                        people.Add(p);
                    }
                }
            }

            Assert.That(people.Count == 10);
        }
    }
}
