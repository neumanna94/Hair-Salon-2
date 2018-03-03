using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Speciality
    {
        private int _id;
        private string _name;
        private int _stylistId;

        private static string _orderBy = "id";

        public Speciality(string name, int stylistId, int Id = 0)
        {
            _id = Id;
            _stylistId = stylistId;
            _name = name;
        }
        public Speciality(string name, int Id = 0)
        {
            _id = Id;
            _name = name;
        }
        public Speciality(int Id = 0)
        {
            _id = Id;
        }
        public int GetId()
        {
            return _id;
        }
        public int GetStylistId()
        {
            return _stylistId;
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
            cmd.CommandText = @"INSERT INTO specialities (name) VALUES (@SpecialityName);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@SpecialityName";
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
        //Used to save to Join table stylists_specialities
        public void Stylist_Specialities_Save(int tempStylistId,int tempSpecialityId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialities (stylist_id, speciality_id) VALUES (@StylistId, @SpecialityId);";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@StylistId";
            stylistId.Value = tempStylistId;
            cmd.Parameters.Add(stylistId);

            MySqlParameter specialityId = new MySqlParameter();
            specialityId.ParameterName = "@SpecialityId";
            specialityId.Value = tempSpecialityId;
            cmd.Parameters.Add(specialityId);

            cmd.ExecuteNonQuery();

            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Stylist> StylistsWithSpeciality(Speciality inputSpeciality)
        {
            List<Stylist> stylistOutput = new List<Stylist>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialities JOIN stylists_specialities ON (specialities.id = stylists_specialities.speciality_id) JOIN stylists ON (stylists_specialities.stylist_id = stylists.id) WHERE specialities.id = @SpecialityId;";

            MySqlParameter specialityId = new MySqlParameter();
            specialityId.ParameterName = "@SpecialityId";
            specialityId.Value = inputSpeciality.GetId();
            cmd.Parameters.Add(specialityId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int stylistId = (int) rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                Stylist myStylist = new Stylist(stylistName);
                myStylist.SetId(stylistId);
                stylistOutput.Add(myStylist);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylistOutput;
        }

        public List<Speciality> SpecialitiesOfStylist(int tempStylistId)
        {
            List<Speciality> specialityOutput = new List<Speciality>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialities.* FROM stylists JOIN stylists_specialities ON (stylists.id = stylists_specialities.stylist_id) JOIN specialities ON (specialities.id = stylists_specialities.speciality_id) WHERE stylists.id = @StylistsId;";

            MySqlParameter specialityId = new MySqlParameter();
            specialityId.ParameterName = "@StylistsId";
            specialityId.Value = tempStylistId;
            cmd.Parameters.Add(specialityId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int stylistId = (int) rdr.GetInt32(0);
                string specialityName = rdr.GetString(1);
                Speciality mySpeciality = new Speciality(specialityName);
                mySpeciality.SetId(stylistId);
                specialityOutput.Add(mySpeciality);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return specialityOutput;
        }

        public static List<Speciality> GetAll()
        {
            //Opening Database Connection.
            List<Speciality> allSpecialities = new List<Speciality> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            //Casting and Executing Commands.
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM specialities ORDER BY "+ _orderBy + ";";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            //Contains built in method .Read()
            while(rdr.Read())
            {
              int specialityId = rdr.GetInt32(0);
              string name = rdr.GetString(1);

              Speciality newSpeciality = new Speciality(name, specialityId);
              allSpecialities.Add(newSpeciality);
            }
            //Close connection
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialities;
        }

        public static Speciality Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialities WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int specialityId = 0;
            string name = "";

            while(rdr.Read())
            {
                specialityId = rdr.GetInt32(0);
                name = rdr.GetString(1);

            }
            Speciality newSpeciality = new Speciality(name, specialityId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newSpeciality;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialities;";
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
            cmd.CommandText = @"DELETE FROM specialities WHERE id = @searchId;";

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
            cmd.CommandText = @"UPDATE stylists SET name = @name WHERE id = @SpecialityId;";

            MySqlParameter tempName = new MySqlParameter("@name", inputName);
            MySqlParameter tempSpecialityId = new MySqlParameter("@SpecialityId", this.GetId());

            cmd.Parameters.Add(tempName);
            cmd.Parameters.Add(tempSpecialityId);

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
