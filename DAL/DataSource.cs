using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Interface.Models;


namespace DAL
{
    public class DataSource : IDataSourse
    {
        private readonly SqlConnection _connection;

        public DataSource(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        #region Offers

        public List<Offer> GetAllOffers()
        {
            List<Offer> offers = new List<Offer>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Model, Year, Image, Type, Description from Offer ";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        offers.Add(new Offer((int) item["Id"], (string) item["Model"],
                            (int) item["Year"], (string)item["Image"],
                            (string) item["Description"], (string)item["Type"]));
                    }
                }

                return offers;
            }

        }

        public Offer GetOfferById(int id)
        {
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Image, Type, Description, Model, Year from Offer " +
                             " where Id = @id";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                    da.SelectCommand.Parameters["@id"].Value = id;
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    return new Offer((int)ds.Tables[0].Rows[0]["Id"], (string)ds.Tables[0].Rows[0]["Model"],
                        (int)ds.Tables[0].Rows[0]["Year"], (string)ds.Tables[0].Rows[0]["Image"], (string)ds.Tables[0].Rows[0]["Description"],
                        (string)ds.Tables[0].Rows[0]["Type"]);
                }
            }
        }

        public int GetOffersCount()
        {
            using (var cmd = new SqlCommand(
                @" select count(Id) from Offer ", _connection))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<Offer> GetAllOffers(Parameters parameters)
        {
            List<Offer> offers = new List<Offer>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Image, Type, Description, Model, Year from Offer " +
                             " order by Id" +
                             " offset @offset ROWS " +
                             " Fetch next @pageSize Rows only";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.SelectCommand.Parameters.Add("@offset", SqlDbType.Int);
                    da.SelectCommand.Parameters["@offset"].Value = parameters.Offset;

                    da.SelectCommand.Parameters.Add("@pageSize", SqlDbType.Int);
                    da.SelectCommand.Parameters["@pageSize"].Value = parameters.PageSize;

                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        offers.Add(new MinivanOffer((int)item["Id"], (string)item["Model"],
                            (int)item["Year"], (string)item["Type"], (string)item["Image"],
                            (string)item["Description"]));
                    }
                }

                return offers;
            }
        }

        #endregion
        //todo

        #region MinivanOffers

        public List<MinivanOffer> GetAllMinivanOffers()
        {
            List<MinivanOffer> minivanOffers = new List<MinivanOffer>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Image, Type, Description, Model, Year from Offer " +
                             " where Type = 'Minivan'";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        minivanOffers.Add(new MinivanOffer((int)item["Id"], (string)item["Model"],
                            (int)item["Year"], (string)item["Type"], (string)item["Image"],
                            (string)item["Description"]));
                    }
                }

                return minivanOffers;
            }
        }

        public int GetMinivansCount()
        {
            using (var cmd = new SqlCommand(
                @" select count(Id) from Offer " + 
                "where Type = 'Minivan'", _connection))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        #endregion 

        #region OutroadCarOffers

        public List<OutroadCarOffer> GetAllOutroadCarOffers()
        {
            List<OutroadCarOffer> outroadCarOffers = new List<OutroadCarOffer>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Image, Type, Description, Model, Year from Offer " +
                             " where Type = 'OutroadCar'";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        outroadCarOffers.Add(new OutroadCarOffer((int)item["Id"], (string)item["Model"],
                            (int)item["Year"], (string)item["Type"], (string)item["Image"],
                            (string)item["Description"]));
                    }
                }

                return outroadCarOffers;
            }
        }

        public int GetOutroadCarsCount()
        {
            using (var cmd = new SqlCommand(
                @" select count(Id) from Offer " +
                "where Type = 'OutroadCar'", _connection))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        #endregion

        #region SportCarOffers

        public List<SportCarOffer> GetAllSportCarOffers()
        {
            List<SportCarOffer> sportCarOffers = new List<SportCarOffer>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Image, Type, Description, Model, Year from Offer " +
                             " where Type = 'SportCar'";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        sportCarOffers.Add(new SportCarOffer((int)item["Id"], (string)item["Model"],
                            (int)item["Year"], (string)item["Type"],(string)item["Image"],
                            (string)item["Description"]));
                    }
                }

                return sportCarOffers;
            }
        }

        public int GetSportCarsCount()
        {
            using (var cmd = new SqlCommand(
                @" select count(Id) from Offer " +
                "where Type = 'SportCar'", _connection))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        #endregion

        #region Customers



        #endregion

        #region Rents

        public int CreateRent(Rent rent)
        {
            string sql = "  Insert into Rent (Id_Offer, Customer_Email, Start_Date, Insurance_Case) " +
                         " Values (@IdOffer, @CustomerEmail, @StartDate, @InsuranceCase) ";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.Add("@IdOffer", SqlDbType.Int);
                cmd.Parameters["@IdOffer"].Value = rent.OfferId;
                cmd.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar);
                cmd.Parameters["@CustomerEmail"].Value = rent.CustomerEmail;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                cmd.Parameters["@StartDate"].Value = rent.StartDate;
                cmd.Parameters.Add("@InsuranceCase", SqlDbType.Bit);
                cmd.Parameters["@InsuranceCase"].Value = Convert.ToInt32(rent.InsuranceCase);
                

                return cmd.ExecuteNonQuery();
            }
        }

        public int CountOfNullEndDateByOfferId(int offerId)
        {
            string sql = " select count(*) from Rent " +
                         " where Rent.Id_Offer = @IdOffer and Rent.End_Date is NULL ";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.Add("@IdOffer", SqlDbType.Int);
                cmd.Parameters["@IdOffer"].Value = offerId;
                
                return (int)cmd.ExecuteScalar();
            }
        }

        public Rent GetRentByOfferId(RentParameters parameters) 
        {
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Id_Offer, Customer_Email, Start_Date, End_Date, Cost, Insurance_Case from Rent " +
                             " where Id_Offer = @IdOffer and Customer_Email = @CustomerEmail and End_Date is NULL";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.SelectCommand.Parameters.Add("@IdOffer", SqlDbType.Int);
                    da.SelectCommand.Parameters["@IdOffer"].Value = parameters.OfferId;
                    da.SelectCommand.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar);
                    da.SelectCommand.Parameters["@CustomerEmail"].Value = parameters.CustomerEmail;
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    return new Rent((int)ds.Tables[0].Rows[0]["Id"], (int)ds.Tables[0].Rows[0]["Id_Offer"],
                        (DateTime)ds.Tables[0].Rows[0]["Start_Date"], (string)ds.Tables[0].Rows[0]["Customer_Email"], (bool)ds.Tables[0].Rows[0]["Insurance_Case"]);
                }
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
