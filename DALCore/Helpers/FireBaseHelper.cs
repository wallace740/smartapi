using System;
using System.Collections.Generic;
using System.Net;
using DALCore.Common;
using DALCore.Constants;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DALCore.Helpers
{
    public class FireBaseHelper
    {
        public string TableName { get; set; }

        readonly IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = FireBaseConstants.AuthSecret,
            BasePath = FireBaseConstants.BasePath
        };

        public IFirebaseClient Client;

        public FireBaseHelper(string tableName)
        {
            Client = new FireSharp.FirebaseClient(config);
            TableName = tableName;
        }

        public T GetItemById<T>(string id) where T : BaseModel
        {
            var response = Client.Get(TableName + "/" + id); //products     product.Id
            dynamic output = JsonConvert.DeserializeObject<T>(response.Body);

            return output;
        }

        public List<T> GetAll<T>() where T : BaseModel
        {
            var response = Client.Get(TableName); //"products"
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var itemList = new List<T>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var stud = JsonConvert.DeserializeObject<T>(((JProperty)item).Value.ToString());
                    itemList.Add(stud);
                }
            }

            return itemList;
        }

        public T InsertItemToDB<T>(T item) where T : BaseModel
        {
            var data = item;

            PushResponse resp = Client.Push(TableName + "/", data); //"products/"
            data.Id = resp.Result.name;
            SetResponse setResponse = Client.Set(TableName + "/" + data.Id, data);
            if (setResponse.StatusCode == HttpStatusCode.OK)
            {
                item.SuccessMessage = "Insert item successful.";
            }
            else
            {
                item.ErrorMessage = string.Format("Error occured while inserting! Ex: {0}", setResponse.Body);
            }
            return item;
        }

        public T UpdateItemToDB<T>(T updateItem) where T : BaseModel
        {
            var data = updateItem;
            SetResponse setResponse = Client.Set(TableName + "/" + data.Id, data); //products   product.Id
            if (setResponse.StatusCode == HttpStatusCode.OK)
            {
                data.SuccessMessage = "Update process successful.";
            }
            else
            {
                data.ErrorMessage = string.Format("Error occured while updating! Ex: {0}", setResponse.Body);
            }
            return data;
        }


    }
}
