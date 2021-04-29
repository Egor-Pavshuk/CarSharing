﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;


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
            return null; //todo
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

        #endregion

        #region Customers



        #endregion

        #region Rents

        public int CreateRent(Rent rent)
        {
            string sql = "  Insert into Rent (Id_Offer, Customer_Email, Start_Date, End_Date, Insurance_Case) " +
                         " Values (@IdOffer, @CustomerEmail, @StartDate, @EndDate, @InsuranceCase) ";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.Add("@IdOffer", SqlDbType.Int);
                cmd.Parameters["@IdOffer"].Value = rent.OfferId;
                cmd.Parameters.Add("@CustomerEmail", SqlDbType.NVarChar);
                cmd.Parameters["@CustomerEmail"].Value = rent.CustomerEmail;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime);
                cmd.Parameters["@StartDate"].Value = rent.StartDate;
                cmd.Parameters.Add("@Term", SqlDbType.DateTime);
                cmd.Parameters["@EndDate"].Value = rent.EndDate;
                cmd.Parameters.Add("@InsuranceCase", SqlDbType.Bit);
                cmd.Parameters["@InsuranceCase"].Value = Convert.ToInt32(rent.InsuranceCase);
                

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
