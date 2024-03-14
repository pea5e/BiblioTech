using System.Data.SqlClient;

namespace BiblioTech.Pages
{
    public class Article
    {

        public int id;
        public string titre;
        public string para;
        public DateTime date;
        public string img;
        public Utilisateur writer;



        public Article(string titre, string para,string img,string session)
        {
            this.titre = titre;
            this.para = para;
            this.img = img;
            this.writer = Utilisateur.getUsersession(session);
            this.date = DateTime.Now;

        }
        public Article(int id, string titre, string para,DateTime date, string img,int uid)
        {
            this.id = id;
            this.titre = titre;
            this.para = para;
            this.img = img;
            this.writer = Utilisateur.Get_User(uid);
            this.date = date;
        }


        public static List<Article> Get_Articles()
        {
            List<Article> articles = new List<Article>();
            var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
            con.Open();
            string sql = "select TOP(10) * from Blog order by id desc ;";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader rd = cmd.ExecuteReader();
            while(rd.Read())
            {
                articles.Add(new Article(rd.GetInt32(0), rd.GetString(1), rd.GetString(2), rd.GetDateTime(3), rd.GetString(4), rd.GetInt32(5)));
            }


            return articles;
        }

        public bool Save()
        {

            try
            {
                var con = new SqlConnection("Data Source=SALIMALAOUI\\SQLEXPRESS;Initial Catalog=BiblioTech;Integrated Security=True");
                con.Open();
                string sql = "insert into Blog(Titre, Para, dateb,Img,wid) values(@Titre, @Para, @dateb,@Img,@wid);";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Titre", titre);
                cmd.Parameters.AddWithValue("@Para", para);
                cmd.Parameters.AddWithValue("@dateb", date.ToShortDateString());
                cmd.Parameters.AddWithValue("@Img", img);
                cmd.Parameters.AddWithValue("@wid", writer.id);
                string query = cmd.CommandText;

                foreach (SqlParameter p in cmd.Parameters)
                {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }

                Console.WriteLine(query);
                cmd.BeginExecuteNonQuery();


                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}
