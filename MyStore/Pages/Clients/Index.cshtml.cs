using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        //to create client list
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
              
                using (SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=master;User ID=sa;Password=ComplexPassword123!;Connect Timeout=30;Encrypt=False;"))
                {
                    conn.Open();
                    string sql = "SELECT * from clients";
                    SqlCommand sqlCommand = new SqlCommand(sql, conn);



                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ClientInfo clientinfo = new ClientInfo();
                        clientinfo.id = "" + reader.GetInt32(0);
                        clientinfo.name = reader.GetString(1);
                        clientinfo.email = reader.GetString(2);
                        clientinfo.phone = reader.GetString(3);
                        clientinfo.address = reader.GetString(4);
                        clientinfo.created_at = reader.GetDateTime(5).ToString();

                        listClients.Add(clientinfo);





                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceltion occur " + ex.ToString());
            }
        }
        public class ClientInfo
        {
            public String id;
            public String name;
            public String email;
            public String phone;       
            public String address;
            public String created_at;



        }
    }
}
