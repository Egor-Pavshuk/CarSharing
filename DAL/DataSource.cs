﻿using System;
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

        public List<Offer> GetTakenOffers()
        {
            List<Offer> offers = new List<Offer>();

            using (DataSet ds = new DataSet())
            {
                string sql = " select Offer.Id, Image, Type, Description, Model, Year from Offer, Rent " +
                             " where Rent.Id_Offer = Offer.Id and Rent.End_Date is NULL ";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        offers.Add(new Offer((int)item["Id"], (string)item["Model"],
                            (int)item["Year"], (string)item["Image"], (string)item["Description"],
                            (string)item["Type"]));
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

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, First_Name, Surname, [E-mail] from Customer ";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.Fill(ds);
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        customers.Add(new Customer
                        {
                            Id = (int)item["Id"],
                            FirstName = (string)item["First_Name"],
                            Surname = (string)item["Surname"],
                            Email = (string)item["E-mail"]
                        });
                    }
                }

                return customers;
            }
        }

        public int AddNewCustomer(Customer customer)
        {
            string sql = "  Insert into Customer (First_Name, Surname, [E-mail]) " +
                         " Values (@FirstName, @Surname, @Email) ";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
                cmd.Parameters["@FirstName"].Value = customer.FirstName;
                cmd.Parameters.Add("@Surname", SqlDbType.NVarChar);
                cmd.Parameters["@Surname"].Value = customer.Surname;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = customer.Email;

                return cmd.ExecuteNonQuery();
            }
        }

        public int IsCustomerExist(Customer customer)
        {
            string sql = " select count(*) from Customer " +
                         " where Customer.[E-mail] = @Email ";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = customer.Email;

                return (int)cmd.ExecuteScalar();
            }
        }

        public Customer GetCustomerById(int customerIndex)
        {

            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, First_name, Surname, [E-mail] from Customer " +
                             " where Customer.Id = @customerId ";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.SelectCommand.Parameters.Add("@customerId", SqlDbType.Int);
                    da.SelectCommand.Parameters["@customerId"].Value = customerIndex;
                    da.Fill(ds);

                    DataRow dataRow = ds.Tables[0].Rows[0];

                    return new Customer
                    {
                        Id = (int) dataRow["Id"],
                        FirstName = (string) dataRow["First_name"],
                        Surname = (string) dataRow["Surname"],
                        Email = (string) dataRow["E-mail"]
                    };
                }
            }
        }

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

        public Rent GetOpenRentByOfferId(RentParameters parameters) 
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

        public int CloseRent(Rent rent)
        {
            string sql = "  update Rent set End_Date = @EndDate, Cost = @Cost " +
                         " where Id = @IdRent ";

            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.Add("@IdRent", SqlDbType.Int);
                cmd.Parameters["@IdRent"].Value = rent.Id;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
                cmd.Parameters["@EndDate"].Value = rent.EndDate;
                cmd.Parameters.Add("@Cost", SqlDbType.Int);
                cmd.Parameters["@Cost"].Value = rent.Cost;


                return cmd.ExecuteNonQuery();
            }
        }

        public List<Rent> GetRentsByEmail(string email)
        {
            List<Rent> rents = new List<Rent>();
            using (DataSet ds = new DataSet())
            {
                string sql = " select Id, Id_Offer, Customer_Email, Start_Date, End_Date, Cost, Insurance_Case from Rent " +
                             " where Customer_Email = @customerEmail ";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, _connection))
                {
                    da.SelectCommand.Parameters.Add("@customerEmail", SqlDbType.NVarChar);
                    da.SelectCommand.Parameters["@customerEmail"].Value = email;
                    
                    da.Fill(ds);

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        rents.Add(new Rent((int)item["Id"], (int)item["Id_Offer"],
                            (DateTime)item["Start_Date"], (string)item["Customer_Email"], (bool)item["Insurance_Case"]));
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            rents[i].EndDate = (DateTime) ds.Tables[0].Rows[i]["End_Date"];
                            rents[i].Cost = float.Parse(ds.Tables[0].Rows[i]["Cost"].ToString());
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    return rents;
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
