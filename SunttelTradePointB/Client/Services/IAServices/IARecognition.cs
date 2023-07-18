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
using Microsoft.AspNetCore.Components.Forms;

namespace SunttelTradePointB.Client.Services.IAServices
{
    public class IARecognition
    {
        private readonly HttpClient _httpClient;
        private string basepath = "";
        private string APIHost = "http://127.0.0.1:5167"; 
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

        private bool ContainCreate(string value)
        {
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

        public async Task<HttpResponseMessage> UploadFileAsync(IBrowserFile file)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                content.Add(new StreamContent(file.OpenReadStream(file.Size)), "file", file.Name);

                return await _httpClient.PostAsync($"{APIHost}/uploadfile/", content);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError                    
                };
            }
        }

        public async Task<HttpResponseMessage> AskQuestion(string question)
        {
            try
            {
                return await _httpClient.GetAsync($"{APIHost}/ask/?question={question}");
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
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
