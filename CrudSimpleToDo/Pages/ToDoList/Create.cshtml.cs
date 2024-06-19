using CrudSimpleToDo.Pages.To_Do_List;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Runtime;
using Microsoft.Extensions.Configuration;


namespace CrudSimpleToDo.Pages.ToDoList
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration Configuration;
        public ActivityInfo activityInfo = new ActivityInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            activityInfo.activity = Request.Form["activity"];
            activityInfo.describe = Request.Form["describe"];

            if (activityInfo.activity.Length == 0 || activityInfo.describe.Length == 0)
            {
                errorMessage = "All fields required";
                return;
            }

            //save new task to database

            try
            {
                String connectionString = Configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO ToDoList" + "(activity,describe) VALUES" + "(@activity, @describe);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@activity", activityInfo.activity);
                        command.Parameters.AddWithValue("@describe", activityInfo.describe);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            activityInfo.activity = ""; activityInfo.describe = "";
            successMessage = "New task added!";

            Response.Redirect("/ToDoList/Index");
        }

        public CreateModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
