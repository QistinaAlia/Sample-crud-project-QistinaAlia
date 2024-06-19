using CrudSimpleToDo.Pages.To_Do_List;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CrudSimpleToDo.Pages.ToDoList
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration Configuration;
        public ActivityInfo activityInfo = new ActivityInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=ToDoActivities;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM ToDoList WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                activityInfo.id = "" + reader.GetInt32(0);
                                activityInfo.activity = reader.GetString(1);
                                activityInfo.describe = reader.GetString(2);

                            }

                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

            }
        }

        public void OnPost()
        {
            activityInfo.id = Request.Form["id"];
            activityInfo.activity = Request.Form["activity"];
            activityInfo.describe = Request.Form["describe"];

            if (activityInfo.activity.Length == 0 || activityInfo.describe.Length == 0)
            {
                errorMessage = "All fields required";
                return;
            }

            try
            {
                String connectionString = Configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE ToDoList SET activity=@activity, describe=@describe WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@activity", activityInfo.activity);
                        command.Parameters.AddWithValue("@describe", activityInfo.describe);
                        command.Parameters.AddWithValue("@id", activityInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/ToDoList/Index");
        }

        public EditModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
