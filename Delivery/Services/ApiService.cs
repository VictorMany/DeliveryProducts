using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services
{
    public class ApiService
    {
        private string ApiUrl = "https://azuresqlapidelivery.azurewebsites.net/";

        public async Task<ApiResponse> GetDataAsync<T>(string controller)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var data = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = data
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        /*public async Task<ApiResponse> GetDataAsyncByID<T>(string controller, int id)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };
                var response = await client.GetAsync(controller + "/" + id);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var data = JsonConvert.DeserializeObject<ObservableCollection<T>>(result);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = data
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }*/
    }
}
