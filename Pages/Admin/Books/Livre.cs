using BiblioTech.Pages.Admin.Author;
using BiblioTech.Pages.Admin.Category;
using BiblioTech.Pages.Admin.Editor;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace BiblioTech.Pages.Admin.Books
{
    public class Livre
    {
        public int id;
        public string titre;
        public string descr;
        public int annee;
        public Editeur edit;
        public Categorie cat;
        public Auteur aut;

        public Livre(int id)
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Livre where IDLivre = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();
                this.id = r.GetInt32(0);
                this.titre = r.GetString(1);
                this.descr = r.GetString(2);
                if (r.GetValue(3).ToString() == "")
                    this.annee = 0;
                else
                {
                    try
                    {
                        annee = r.GetInt32(3);
                    }
                    catch
                    {
                        annee = Int32.Parse(r.GetString(3));
                    }
                }
                this.edit = new Editeur(r.GetInt32(4));
                this.cat = new Categorie(r.GetInt32(5));
                this.aut = new Auteur(r.GetInt32(6));

            }
            catch (Exception e)
            {
                Console.WriteLine("constructor" +e.Message);
            }
        }
        public Livre(string titre, string descr, int annee, int IDE, int IDC , int IDA ,  int id = 0)
        {
            this.id = id;
            this.titre = titre;
            this.descr = descr;
            this.annee = annee;
            this.edit = new Editeur(IDE);
            this.cat = new Categorie(IDC);
            this.aut = new Auteur(IDA);
        }

        public Boolean save()
        {
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "insert into Livre(titre, DescripLivre, AnneeEdition, IDEditeur ,IDCategorie ,IDAuteur) values(@titre,@descr,@annee,@ide,@idc,@ida);";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@titre", titre);
                cmd.Parameters.AddWithValue("@descr", descr);
                if(annee > 0)
                    cmd.Parameters.AddWithValue("@annee", annee);
                else
                    cmd.Parameters.AddWithValue("@annee", DBNull.Value);
                cmd.Parameters.AddWithValue("@ide", edit.id);
                cmd.Parameters.AddWithValue("@idc", cat.id);
                cmd.Parameters.AddWithValue("@ida", aut.id);


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
                string sql = "update Livre set titre=@titre , DescripLivre=@descr , AnneeEdition=@annee , IDEditeur = @ide , IDCategorie = @idc , IDAuteur = @ida where IDlivre = @id;";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@titre", titre);
                cmd.Parameters.AddWithValue("@descr", descr);
                if (annee > 0)
                    cmd.Parameters.AddWithValue("@annee", annee);
                else
                    cmd.Parameters.AddWithValue("@annee", DBNull.Value);
                cmd.Parameters.AddWithValue("@ide", edit.id);
                cmd.Parameters.AddWithValue("@idc", cat.id);
                cmd.Parameters.AddWithValue("@ida", aut.id);
                cmd.Parameters.AddWithValue("@id", id);
                /*string query = cmd.CommandText;

                foreach (SqlParameter p in cmd.Parameters)
                {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }
                IAsyncResult r =  cmd.BeginExecuteNonQuery();
                Console.WriteLine(cmd.EndExecuteNonQuery(r));
                Console.WriteLine(query);*/
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
                string sql = "delete from Livre where IDlivre = @id;";
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

        public static List<Livre> GetLivres()
        {
            List<Livre> list = new List<Livre>();
            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "select * from Livre";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                int annee;
                while (rd.Read())
                {
                    if (rd.GetValue(3).ToString() == "")
                        annee = 0;
                    else
                    {
                        try
                        {
                            annee = rd.GetInt32(3);
                        }
                        catch
                        {
                            annee = Int32.Parse(rd.GetString(3));
                        }
                    }
                    Livre liv = new Livre(rd.GetString(1), rd.GetString(2), annee, rd.GetInt32(4), rd.GetInt32(5), rd.GetInt32(6), rd.GetInt32(0));

                    list.Add(liv);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }

            return list;
        }
    }
}
