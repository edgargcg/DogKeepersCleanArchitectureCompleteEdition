using CE = DogKeepers.Shared.CustomEntities;
using DogKeepers.Shared.Metadata;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace DogKeepers.Shared.ApiResponse
{
    public class ApiResponse<T>
    {

        public bool IsDone { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public PaginationMetadata PaginationMetadata{ get; set; }
        public ErrorResponse Error { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(bool isDone, string message, T data, PaginationMetadata paginationMetadata)
        {
            IsDone = isDone;
            Message = message;
            Data = data;
            PaginationMetadata = paginationMetadata;
        }

        public async static Task<ApiResponse<T>> Create(HttpResponseMessage response)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            var jsonData = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var error = JsonConvert.DeserializeObject<CE.Exception>(jsonData);
                apiResponse.Error = error.Errors.FirstOrDefault();
            }
            else
            {
                apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonData);

            }

            return apiResponse;
        }

    }
}
