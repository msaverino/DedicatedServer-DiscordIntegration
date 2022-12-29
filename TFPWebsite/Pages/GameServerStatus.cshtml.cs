using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;

namespace TFPWebsite.Pages
{
    public class GameServerStatusModel : PageModel
    {

        string connectionString = CS.ConnectionString;
        
        public void OnGet()
        {

        }


    }
}
