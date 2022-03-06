using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerLibary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhoneNumber", model.CellPhoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute the stored procedure
                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        // TODO - make the CreatePrize actually save to the database
        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns> The prize information </returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // By writing the 'using' statement that way we prevents memory leaks beacuse the connection get destroyed properly
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute the stored procedure
                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure); 

                model.Id = p.Get<int>("@id");

                return model;
            }             
        }
    }
}
