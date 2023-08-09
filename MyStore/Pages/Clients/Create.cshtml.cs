using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static MyStore.Pages.Clients.IndexModel;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if(clientInfo.name.Length==0 || clientInfo.email.Length==0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "some fields are required";
                return;
            }

            //save the new client into the database
            try
            {
                using(SqlConnection conn=new SqlConnection(@"Data Source=localhost;Initial Catalog=master;User ID=sa;Password=ComplexPassword123!;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();
                    string insert = "insert into clients(name,email,phone,address)" +
                        " values(@name,@email,@phone,@address);";
                    using(SqlCommand cmd=new SqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);
                       


                        cmd.ExecuteNonQuery();
                    }
                }

            }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            clientInfo.name = "";
            clientInfo.email="";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMessage = "Successful your new client adding.";

            Response.Redirect("/Clients/Index");

        }
    }
}
