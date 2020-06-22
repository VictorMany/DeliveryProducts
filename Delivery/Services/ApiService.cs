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

        public async Task<ApiResponse> GetDataAsyncByID<T>(string controller, int id)
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
                var data = JsonConvert.DeserializeObject<T>(result);
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

        public async Task<ApiResponse> GetListDataAsyncByID<T>(string controller, int id)
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
        }

        

        public async Task<ApiResponse> PostDataAsync(string controller, object data)
        {
            try
            {
                var serializeData = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializeData, Encoding.UTF8, "application/json");

                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };

                var response = await client.PostAsync(controller, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Message = result.ToString(),
                        Result = null
                    };
                }

                var created = JsonConvert.DeserializeObject<ApiResponse>(result);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = created
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

        public async Task<ApiResponse> PutDataAsync(string controller, object data, int id)
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializedData, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };
                var response = await client.PutAsync(controller + "/" + id, content);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result.ToString(),
                        Result = "Ok"
                    };
                }
                 // var data = JsonConvert.DeserializeObject<ApiResponse>(result);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Succeded",
                    Result = null
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

        public async Task<ApiResponse> DeleteDataAsync(string controller, int id)
        {
            try
            {

                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl)
                };

                var response = await client.DeleteAsync(controller + "/" + id);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result.ToString(),
                        Result = null
                    };
                }

                return JsonConvert.DeserializeObject<ApiResponse>(result);
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


        public async Task<ApiResponse> DeleteCartDataAsync(string controller, object data)
        {
            try
            {
                var serializeData = JsonConvert.SerializeObject(data);
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(serializeData, Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(ApiUrl + controller)
                };
                var response = await new HttpClient().SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Message = result.ToString(),
                        Result = null
                    };
                }

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = true
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
    }
}
