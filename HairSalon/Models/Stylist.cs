using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private static string _orderBy = "id";

        public Stylist(string name,int Id = 0)
        {
            _id = Id;
            _name = name;
        }
        public Stylist(int Id = 0)
        {
            _id = Id;
            _name = "";
        }
        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public static void SetOrderBy(string orderBy)
        {
            _orderBy = orderBy;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@StylistName);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@StylistName";
            name.Value = _name;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();

            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> GetAll()
        {
            //Opening Database Connection.
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            //Casting and Executing Commands.
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM stylists ORDER BY "+ _orderBy + ";";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            //Contains built in method .Read()
            while(rdr.Read())
            {
              int stylistId = rdr.GetInt32(0);
              string name = rdr.GetString(1);

              Stylist newStylist = new Stylist(name, stylistId);
              allStylists.Add(newStylist);
            }
            //Close connection
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int stylistId = 0;
            string name = "";

            while(rdr.Read())
            {
                stylistId = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            Stylist newStylist = new Stylist(name, stylistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStylist;
        }

        public string FindClientsToString(List<Client> inputList)
        {
            string outputString = "";
            for(int i = 0; i < inputList.Count; i ++)
            {
                outputString = outputString + inputList[i].GetName() + ", ";
            }

            if(outputString.Length > 0)
            {
                outputString = outputString.Remove(outputString.Length-2, 1);
            } else {
                outputString = "Currently no clients for this stylist!";
            }
            return outputString;
        }

        public List<Client> FindClients()
        {
            //Opening Database Connection.
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            //Casting and Executing Commands.
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM clients WHERE stylistID = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            //Contains built in method .Read()
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                int stylistId = rdr.GetInt32(2);

                Client newClient = new Client(name, clientId, stylistId);
                allClients.Add(newClient);
            }
            //Close connection
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static void DeleteRow(int idDelete)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = idDelete;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void UpdateName(string inputName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @name WHERE id = @userId;";

            MySqlParameter tempName = new MySqlParameter("@name", inputName);
            MySqlParameter tempUserId = new MySqlParameter("@userId", this.GetId());

            cmd.Parameters.Add(tempName);
            cmd.Parameters.Add(tempUserId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherItem)
        {
          if (!(otherItem is Stylist))
          {
            return false;
          }
          else
          {
             Stylist newItem = (Stylist) otherItem;
             bool idEquality = this.GetId() == newItem.GetId();
             bool nameEquality = this.GetName() == newItem.GetName();
             return (idEquality && nameEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetName().GetHashCode();
        }

    }
}
