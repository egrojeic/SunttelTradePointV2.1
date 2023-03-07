using MongoDB.Bson;
using SunttelTradePointB.Shared.Common;
using System.Collections;

namespace SunttelTradePointB.Server
{

    /// <summary>
    /// General utilities for the server
    /// </summary>
    public static class GeneralServerUtilities
    {

        /// <summary>
        /// Get the public IP address of the server
        /// </summary>
        /// <returns></returns>
        public static string GetPublicIpAddress()
        {
            string publicIpAddress = "";
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    publicIpAddress = client.DownloadString("http://icanhazip.com");
                }
            }
            catch (Exception)
            {
                publicIpAddress = "";
            }

            return publicIpAddress;
        }

        /// <summary>
        /// Set the id of the list items that have null id to a new ObjectId
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object SetListIds(this object obj)
        {
            if (obj is IList list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] is IDictionary<string, object> dict && dict.ContainsKey("id") && dict["id"] == null)
                    {
                        dict["id"] = ObjectId.GenerateNewId();
                    }
                    else if (list[i] is IList subList)
                    {
                        list[i] = subList.SetListIds();
                    }
                }
            }
            else if (obj is IDictionary<string, object> dict)
            {
                foreach (var keyValuePair in dict.ToList())
                {
                    if (keyValuePair.Value is IDictionary<string, object> subDict)
                    {
                        dict[keyValuePair.Key] = subDict.SetListIds();
                    }
                    else if (keyValuePair.Value is IList subList)
                    {
                        dict[keyValuePair.Key] = subList.SetListIds();
                    }
                }
            }
            return obj;
        }
    }
}
