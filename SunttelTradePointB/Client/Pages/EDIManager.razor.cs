using System.IO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;


namespace SunttelTradePointB.Client.Pages
{
    public partial class EDIManager
    {
        string msgResponse = string.Empty;

        private async Task MigrarClientes()
        {
            //var aaaa = await Http.PostAsJsonAsync("Migrate/customer");
          var resp =   await Http.PostAsJsonAsync<string>("/migrate/customer", null!);

            if (resp.IsSuccessStatusCode)
            {
                msgResponse = await resp.Content.ReadAsStringAsync();
            }
            else
            {
                msgResponse = "Ocurrio un error";
            }

        }
    }
}
