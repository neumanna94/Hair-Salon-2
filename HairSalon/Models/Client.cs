using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Client
  {
      private int _id;
      private string _name;
      private int _stylistId;
      // List<Client> allClients = new List<Client>{};

      public Client(string name,int Id = 0, int StylistId = 0)
      {
          _id = Id;
          _stylistId = StylistId;
          _name = name;
      }

      public Client(int Id = 0)
      {
          _id = Id;
          _stylistId = 0;
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
      public int GetStylistId()
      {
          return _stylistId;
      }

      public void Save()
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO clients (name, stylistId) VALUES (@ClientName, @StylistId);";

          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@ClientName";
          name.Value = _name;
          cmd.Parameters.Add(name);

          MySqlParameter stylistIdNum = new MySqlParameter();
          stylistIdNum.ParameterName = "@StylistId";
          stylistIdNum.Value = _stylistId;
          cmd.Parameters.Add(stylistIdNum);

          cmd.ExecuteNonQuery();

          _id = (int) cmd.LastInsertedId;

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
      }

      public static List<Client> GetAll()
      {
          //Opening Database Connection.
          List<Client> allClients = new List<Client> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          //Casting and Executing Commands.
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

          cmd.CommandText = @"SELECT * FROM clients;";

          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          //Contains built in method .Read()
          while(rdr.Read())
          {
            int clientId = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            int stylistId = rdr.GetInt32(2);

            Client newItem = new Client(name, clientId, stylistId);
            allClients.Add(newItem);
          }
          //Close connection
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allClients;
      }

      public static Client Find(int id)
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int clientId = 0;
          string name = "";
          int stylistId = 0;

          while(rdr.Read())
          {
              clientId = rdr.GetInt32(0);
              name = rdr.GetString(1);
              stylistId = rdr.GetInt32(2);
          }
          Client newClient = new Client(name, clientId, stylistId);
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return newClient;
      }

      public static Client FindClientByStylistId(int id)
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = (@searchId);";

          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int clientId = 0;
          string name = "";
          int stylistId = 0;

          while(rdr.Read())
          {
              clientId = rdr.GetInt32(0);
              name = rdr.GetString(1);
              stylistId = rdr.GetInt32(2);
          }
          Client newClient = new Client(name, clientId, stylistId);
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return newClient;
      }

      public static List<Client> FindAllClientByStylistId(int id)
      {
          List<Client> outputList = new List<Client>{};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = (@searchId);";

          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int clientId = 0;
          string name = "";
          int stylistId = 0;

          while(rdr.Read())
          {
              clientId = rdr.GetInt32(0);
              name = rdr.GetString(1);
              stylistId = rdr.GetInt32(2);
              Client newClient = new Client(name, clientId, stylistId);
              outputList.Add(newClient);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return outputList;
      }

      public static void DeleteAll()
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM clients;";
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
          cmd.CommandText = @"DELETE FROM clients WHERE id = @searchId;";

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
      public void Update(string name,int stylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @name, stylistId = @stylistId WHERE id = @clientId;";

            MySqlParameter tempName = new MySqlParameter("@name", name);
            MySqlParameter tempStylistId = new MySqlParameter("@stylistId", stylistId);
            MySqlParameter tempClientId = new MySqlParameter("@clientId", this.GetId());

            cmd.Parameters.Add(tempName);
            cmd.Parameters.Add(tempStylistId);
            cmd.Parameters.Add(tempClientId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
      public override bool Equals(System.Object otherItem)
      {
        if (!(otherItem is Client))
        {
          return false;
        }
        else
        {
           Client newItem = (Client) otherItem;
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
