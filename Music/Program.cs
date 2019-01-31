using System;
using System.Data.SqlClient;

namespace Music
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString="Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog = Music;Integrated Security=true;MultipleActiveResultSets = true";
            using (var connection = new SqlConnection(connectionString))
            {

            }
        }
    }
}
