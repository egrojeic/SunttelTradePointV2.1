using static System.Net.WebRequestMethods;
using SunttelTradePointB.Shared;
using System.Net.Http.Json;

namespace MigrationProcess.Client.Pages
{
    public partial class Index
    {



        private async Task MigrarClientes()
        {
            var aaaa = await Http.GetFromJsonAsync<string>("Migrate/customer");
        }

    }
}
