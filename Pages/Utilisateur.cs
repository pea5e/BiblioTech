using BiblioTech.Pages.Admin.Books;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace BiblioTech.Pages
{
    public class Utilisateur
    {

        public int id;
        public string name;
        string email;
        string session;
        string password;



        public Utilisateur(int id, string name, string email)
        {
            this.id = id;
            this.name = name;
            this.email = email;
        }
        public Utilisateur(int id, string name, string email, string session)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.session = session;
        }

        public Utilisateur(string name , string email, string password="")
        {
            this.name = name;
            this.email = email;
            this.password = password;
        }

        public static Utilisateur Get_User(int id)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("select Nom,Email,Mdp from Users where id=@id", con);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader rd = cmd.ExecuteReader();

                if(rd.Read())
                {
                    return new Utilisateur(rd.GetString(0), rd.GetString(1), rd.GetString(2));
                }
                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Save()
        {

                try
                {
                    var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                    con.Open();
                    string sql = "insert into Users(Nom, Email, Mdp) values(@nom,@email,@pass);";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@nom", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", password);

                    cmd.BeginExecuteNonQuery();


                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
        }

        public static bool isLogged(string session)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                //SqlCommand cmd = new SqlCommand("select u.id,u.email from Users_session s join Users u on (s.uid=u.id) where Key=@session", con);
                SqlCommand cmd = new SqlCommand("select * from Users_session where skey = @session", con);
                Console.WriteLine(session);
                cmd.Parameters.AddWithValue("@session", session);

                SqlDataReader rd =  cmd.ExecuteReader();

                return rd.Read();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public static Utilisateur getUsersession(string session)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("select u.id,u.email,u.nom from Users_session s join Users u on (s.uid=u.Id) where skey=@session", con);
                Console.WriteLine(session);
                cmd.Parameters.AddWithValue("@session", session);

                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                return new Utilisateur(rd.GetInt32(0), rd.GetString(2), rd.GetString(1));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public string login()
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select id from Users where email=@email and Mdp=@password;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@password", this.password);
                var id = cmd.ExecuteScalar();
                if (id != null)
                {
                    while (true)
                    {
                        try
                        {
                            this.session = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 20)
                                .Select(s => s[new Random().Next(s.Length)]).ToArray());
                            cmd = new SqlCommand("insert into Users_session(skey,uid) values(@key,@id)", con);
                            cmd.Parameters.AddWithValue("@key", session);
                            cmd.Parameters.AddWithValue("@id", id);

                            cmd.BeginExecuteNonQuery();
                            return session;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }

                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public static bool isFindMail(string email)
        {

            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                //SqlCommand cmd = new SqlCommand("select u.id,u.email from Users_session s join Users u on (s.uid=u.id) where Key=@session", con);
                SqlCommand cmd = new SqlCommand("select * from Users where Email=@email", con);

                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader rd = cmd.ExecuteReader();
                return rd.Read();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static void Logout(string session)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                //SqlCommand cmd = new SqlCommand("select u.id,u.email from Users_session s join Users u on (s.uid=u.id) where Key=@session", con);
                SqlCommand cmd = new SqlCommand("delete from Users_session where skey = @session", con);
                Console.WriteLine(session);
                cmd.Parameters.AddWithValue("@session", session);

                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void EncryptPassword(string password)
        {
           
        }
        private static void DecryptPassword(string password)
        {
            
        }
    }
}
