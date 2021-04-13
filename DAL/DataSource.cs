using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    class DataSource : IDisposable
    {
        private readonly SqlConnection _connection;

        public DataSource(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        #region Cars

        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select r.Id, c.Id as CarId, Model, Type, Year from Rent r " +
                             " join Car c on r.Id_Car = c.Id";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        cars.Add(new Car((int) item["Id"], (string) item["Model"],
                            (int) item["Year"], (string) item["Type"]));
                    }
                }

                return cars;
            }
        }

        #endregion

        #region Customers

        

        #endregion

        #region Rents

        public int CreateRent(Rent rent)
        {
            string sql = "  Insert into Rent (Id_Car, Id_Customer, Date, Term) " +
                         " Values (@IdCar, @CustomerEmail, @Date, @Term) ";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.Add("@IdCar", SqlDbType.NVarChar);
                cmd.Parameters["@IdCar"].Value = rent.CarId;
                cmd.Parameters.Add("@CustomerEmail", SqlDbType.Float);
                cmd.Parameters["@CustomerEmail"].Value = rent.CustomerEmail;
                cmd.Parameters.Add("@Date", SqlDbType.Float);
                cmd.Parameters["@Date"].Value = rent.Date;
                cmd.Parameters.Add("@Term", SqlDbType.Float);
                cmd.Parameters["@Term"].Value = rent.Term;


                return cmd.ExecuteNonQuery();
            }
        }

        #endregion

        private bool _isDisposed;
        public void Dispose()
        {
            if (_isDisposed) 
                return;
            _connection.Dispose();
            _isDisposed = true;
        }
    }
}
