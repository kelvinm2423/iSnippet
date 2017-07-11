using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace iSnippet
{
    public class CRUDhw
    {
        private string connString;

        public CRUDhw(string server,string database,string uid,string password)
        {
            connString = string.Format("server={0};database={1};user={2};password={3}", 
                server, database, uid, password);
        }

        public void TestConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");
                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }
        public void CreateData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "insert into sport_player(name,touchdowns,receptions) values(@name,@touchdowns,@receptions)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@name", MySqlDbType.VarChar, 45);
                cmd.Parameters.Add("@touchdowns", MySqlDbType.Float);
                cmd.Parameters.Add("@receptions", MySqlDbType.DateTime);

                Console.Write("Inserting data....");

                Console.WriteLine("Done");

                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public void BulkData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                MySqlBulkLoader bulk = new MySqlBulkLoader(conn);
                bulk.TableName = "sport_player";
                bulk.FieldTerminator = "\t";
                bulk.LineTerminator = "\n";
                bulk.FileName = "D:/sport_player.txt"; // change with your file
                bulk.NumberOfLinesToSkip = 0;
                bulk.Columns.Add("name");
                bulk.Columns.Add("touchdowns");
                bulk.Columns.Add("receptions");
                
                Console.Write("Inserting bulk data....");
                int count = bulk.Load();
                Console.WriteLine("Done-" + count.ToString());

                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void ReadData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "select idplayer,name,touchdowns,receptions";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    Console.WriteLine("Id: " + rd["idplayer"].ToString());
                    Console.WriteLine("Name: " + rd["name"].ToString());
                    Console.WriteLine("Touchdowns: " + rd["touchdowns"].ToString());
                    Console.WriteLine("Receptions: " + rd["receptions"].ToString());
                }
                rd.Close();

                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void UpdateData(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "update sport_player set name=@name,touchdowns=@touchdowns where idproduct=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@name", MySqlDbType.VarChar, 45);
                cmd.Parameters.Add("@touchdowns", MySqlDbType.Float);
                cmd.Parameters.Add("@id", MySqlDbType.Int32);

                Console.Write("Updating data....");
                cmd.Parameters[0].Value = "player-update";
                cmd.Parameters[1].Value = 0.75;
                cmd.Parameters[2].Value = id;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Done");


                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void DeleteData(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "delete from sport_player where idplayer=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@id", MySqlDbType.Int32);

                Console.Write("Deleting data....");
                cmd.Parameters[0].Value = id;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Done");


                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}