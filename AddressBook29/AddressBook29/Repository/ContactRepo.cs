using AddressBook27.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBook27.Repository
{
    class ContactRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=addressBook29DB;Integrated Security=True";


        public bool GetBookByName(string bookName)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                ContactModel model = new ContactModel();
                using (connection)
                {
                    string query = @"Select * from book;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            model.bookName = dr.GetString(1);

                            if (model.bookName == bookName)
                            {
                                return true;
                            }
                            /*  else if (model.bookName == bookModel.bookName && model.bookType != bookModel.bookType)
                              {
                                  Console.WriteLine("bookType Mismatch!");
                              }*/
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return false;
        }

        public bool AddBook(string bookName)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_AddBook", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@book_name", bookName);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                connection.Close();
            }
            return false;
        }




        public void AddContact(ContactModel contactModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("sp_AddContactInotBook", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@first_name", contactModel.firstName);
                    command.Parameters.AddWithValue("@last_name", contactModel.lastName);
                    command.Parameters.AddWithValue("@address", contactModel.address);
                    command.Parameters.AddWithValue("@city", contactModel.city);
                    command.Parameters.AddWithValue("@state", contactModel.state);
                    command.Parameters.AddWithValue("@zip", contactModel.zip);
                    command.Parameters.AddWithValue("@phone_number", contactModel.phoneNumber);
                    command.Parameters.AddWithValue("@email", contactModel.email);
                    command.Parameters.AddWithValue("@book_name", contactModel.bookName);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        Console.WriteLine("Data added successfully");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                connection.Close();
            }

           }





        public bool checkDuplicateNameByBook(ContactModel contactModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = @"Select * from contact where book_name = '" + contactModel.bookName + "' and first_name = '" + contactModel.firstName + "';";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string name = dr.GetString(0);
                            string book = dr.GetString(8);

                            if (contactModel.firstName.Equals(name) && contactModel.bookName.Equals(book))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return false;

        }


    }
}
