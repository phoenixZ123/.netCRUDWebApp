using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static MyStore.Pages.Clients.IndexModel;

namespace MyStore.Pages.Clients
{
    public class EditcshtmlModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        
        
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                using(SqlConnection conn=new SqlConnection(@"Data Source=localhost;Initial Catalog=master;User ID=sa;Password=ComplexPassword123!;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();
                    string sql = "select * from clients where id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                               
                            }
                        }
                        cmd.ExecuteNonQuery();
                    }
                }

            }catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                 clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "some fields are required";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=master;User ID=sa;Password=ComplexPassword123!;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();
                    String update = "UPDATE clients SET name=@name,email=@email,phone=@phone,address=@address WHERE id=@id;";
                    using (SqlCommand cmd = new SqlCommand(update, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);

                        cmd.Parameters.AddWithValue("@id", clientInfo.id);

                        cmd.ExecuteNonQuery();
                        
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Clients/Index");

        }
    }
}
