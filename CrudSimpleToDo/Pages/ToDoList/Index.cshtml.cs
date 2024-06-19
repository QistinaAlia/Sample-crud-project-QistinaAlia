using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CrudSimpleToDo.Pages.To_Do_List
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration Configuration;


        public List<ActivityInfo> listInfo = new List<ActivityInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = Configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM ToDoList";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActivityInfo activityInfo = new ActivityInfo();

                                activityInfo.id = "" + reader.GetInt32(0);
                                activityInfo.activity = reader.GetString(1);
                                activityInfo.describe = reader.GetString(2);
                                activityInfo.created_at = reader.GetDateTime(3).ToString();

                                listInfo.Add(activityInfo);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }

    public class ActivityInfo
    {
        public String id;
        public String activity;
        public String describe;
        public String created_at;
    }


}
