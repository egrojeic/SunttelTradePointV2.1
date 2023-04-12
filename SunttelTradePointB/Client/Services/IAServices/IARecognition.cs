using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Communications;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Net.Http.Json;
using SunttelTradePointB.Client.Interfaces.SalesInterfaces;
using SunttelTradePointB.Shared.Sales;
using System.Reflection;
using System.Net.Http;
using SunttelTradePointB.Shared.IA;

namespace SunttelTradePointB.Client.Services.IAServices
{
    public class IARecognition
    {
        private readonly HttpClient _httpClient;
        private string basepath = "";
        public IARecognition(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IASpeechRecognition> GetValueRecognition(string value)
        {
            if (ContainCreate(value))
            {
               return Create.GetCreate(value);
            }
            return null;
        }

        private bool ContainCreate(string value) {
            bool result = false;
            List<string> list = new List<string>();
            list.Add("crear");
            list.Add("nuevo");
            list.Add("agregar");
            foreach (string item in list)
            {
                if (value.ToLower().Contains(item.ToLower().Trim()))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }



    }

   public class Create
    {
        IASpeechRecognition IASpeechRecognition = new IASpeechRecognition();
        static List<string> list = new List<string>();
           
       public static IASpeechRecognition GetCreate(string value)
        {
            if (ContainProduct(value))
            {
                return new IASpeechRecognition
                {
                    Command = "Create",
                    Page = "TransactionalItemCard"
                };
            }
            return null;
        }

       private static bool ContainProduct(string value)
        {
            bool result = false;
            list.Clear();
            list.Add("producto");
            list.Add("productos");
            list.Add("product");
            foreach (string item in list)
            {
                if (value.ToLower().Contains(item.ToLower().Trim()))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }


        
    }

  
    


}
