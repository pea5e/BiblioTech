using BiblioTech.Pages.Admin.Category;
using System.Data;
using System.Data.SqlClient;

namespace BiblioTech.Pages.Admin.Editor
{
    public class Editeur //: Author.Auteur
    {
        public int id;
        public string name;
        public string email;
        public string telephone;
        public string adresse;
        public string descr;

        public Editeur(int id)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Editeur where IDEdit = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                this.id = r.GetInt32(0);
                this.name = r.GetString(1);
                this.descr = r.GetString(2);
                this.email = r.GetString(3);
                this.telephone = r.GetString(4);
                this.adresse = r.GetString(5);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Editeur(string name, string email, string telephone, string adresse ,string descr, int id = 0)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.telephone = telephone;
            this.adresse = adresse;
            this.descr = descr;
        }

        public Boolean save()
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "insert into Editeur(NomEdit, EmailEdit, TelephoneEdit, AdresseEdit, DescripEdit) values(@nom,@email,@tel,@addr,@descr);";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nom", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@tel", telephone);
                cmd.Parameters.AddWithValue("@addr", adresse);
                cmd.Parameters.AddWithValue("@descr", descr);

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
                //string sql = "update Editeur set NomEdit=@nom , EmailEdit=@email , TelephoneEdit=@tel , AdresseEdit = @addr , DescripEdit = @descr  where IDEdit = @id;";
                string sql = "update Editeur set NomEdit='"+ name + "' , EmailEdit='"+ email + "' , TelephoneEdit='"+ telephone + "' , AdresseEdit = '"+ adresse +"' , DescripEdit = '"+ descr +"'  where IDEdit = "+id+";";

                SqlCommand cmd = new SqlCommand(sql, con);
                /*cmd.Parameters.AddWithValue("@nom", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@tel", telephone);
                cmd.Parameters.AddWithValue("@addr", adresse);
                cmd.Parameters.AddWithValue("@descr", descr);
                cmd.Parameters.AddWithValue("@id", id);*/
                Console.WriteLine(sql);
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
                string sql = "delete from Editeur where IDEdit = @id;";
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

        public static List<Editeur> GetEditeurs()
        {
            List<Editeur> list = new List<Editeur>();
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Editeur";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    Editeur edt = new Editeur(rd.GetString(1), rd.GetString(3), rd.GetString(4), rd.GetString(5), rd.GetString(2), rd.GetInt32(0));

                    list.Add(edt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }

            return list;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return id == ((Editeur)obj).id;
        }
    }
}
