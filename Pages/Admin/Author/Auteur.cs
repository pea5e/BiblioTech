using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System.Data.SqlClient;


namespace BiblioTech.Pages.Admin.Author
{
    public class Auteur
    {
        public int id;
        public string name;
        public string email;
        public string telephone;
        public string adresse;

        public Auteur(int id)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Auteur where IDAut = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                this.id = r.GetInt32(0);
                this.name = r.GetString(1);
                this.email = r.GetString(2);
                this.telephone = r.GetString(3);
                this.adresse = r.GetString(4);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Auteur(string name, string email, string telephone, string adresse, int id = 0)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.telephone = telephone;
            this.adresse = adresse;
        }

        public Boolean save()
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "insert into Auteur(NomAut, EmailAut, TelephoneAut, AdresseAut) values(@nom,@email,@tel,@addr);";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nom", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@tel", telephone);
                cmd.Parameters.AddWithValue("@addr", adresse);

                cmd.BeginExecuteNonQuery();


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Boolean update()
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "update Auteur set NomAut=@nom , EmailAut=@email , TelephoneAut=@tel , AdresseAut = @addr where IDAut = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nom", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@tel", telephone);
                cmd.Parameters.AddWithValue("@addr", adresse);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.BeginExecuteNonQuery();


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }


        }

        public Boolean remove()
        {
            //DBCC CHECKIDENT ('Auteur', RESEED, 0);
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "delete from Auteur where IDAut = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.BeginExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static List<Auteur> GetAuteurs(){

            List<Auteur> l = new List<Auteur>();
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Auteur";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Auteur aut = new Auteur(rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4), rd.GetInt32(0));

                    l.Add(aut);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }

            return l;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return id == ((Auteur)obj).id;
        }
    }
}
