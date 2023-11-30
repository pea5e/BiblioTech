using BiblioTech.Pages.Admin.Author;
using System.Data.SqlClient;

namespace BiblioTech.Pages.Admin.Category
{
    public class Categorie
    {
        public int id;
        public string name;
        public string descr;

        public Categorie(int id)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Categorie where IDCat = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                this.id = r.GetInt32(0);
                this.name = r.GetString(1);
                this.descr = r.GetString(2);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Categorie(string name, string descr, int id = 0)
        {
            this.id = id;
            this.name = name;
            this.descr = descr;
        }

        public Boolean save()
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "insert into Categorie(NomCat, DescripCat) values(@nom,@descr);";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nom", name);
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
                string sql = "update Categorie set NomCat=@nom , DescripCat=@descr where IDCat = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nom", name);
                cmd.Parameters.AddWithValue("@descr", descr);
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
                string sql = "delete from Categorie where IDCat = @id;";
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

        public static List<Categorie> GetCategories()
        {
            List<Categorie> list = new List<Categorie>();
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Categorie";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Categorie cat = new Categorie(rd.GetString(1), rd.GetString(2), rd.GetInt32(0));

                    list.Add(cat);
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

            return id == ((Categorie)obj).id;
        }
    }
}
