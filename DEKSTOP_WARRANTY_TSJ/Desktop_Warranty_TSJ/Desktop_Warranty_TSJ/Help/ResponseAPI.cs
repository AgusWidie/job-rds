
using System.Text.Json;

namespace Desktop_Warranty_TSJ.Help
{
    public static class ResponseAPI
    {
        public static GlobalObjectResponse ResponseSuccessAPI(string responseContent, int statusCode)
        {
            GlobalObjectResponse res = JsonSerializer.Deserialize<GlobalObjectResponse>(responseContent,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
            return res;
        }

        public static GlobalObjectResponse ResponseErrorAPI(string responseContent, int statusCode)
        {

            GlobalObjectResponse res = new GlobalObjectResponse();
            if(statusCode == 500)
            {
                if (responseContent != "" && responseContent != null)  {

                    ExceptionResponse exceptionResponse = JsonSerializer.Deserialize<ExceptionResponse>(responseContent,
                    new JsonSerializerOptions
                    {
                       PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                       DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                       PropertyNameCaseInsensitive = true
                    });


                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed + " : " + exceptionResponse.Message;
                    res.Error = true;

                } else {

                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed;
                    res.Error = true;
                }

            } else {

                if (responseContent != null && responseContent != "")  {

                    res = JsonSerializer.Deserialize<GlobalObjectResponse>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    });

                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed + " : " + res.Message;
                    res.Error = true;

                } else  {

                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed + " : " + res.Message;
                    res.Error = true;
                }
                
                
            }
          
            return res;
        }

        public static GlobalObjectListResponse ResponseListSuccessAPI(string responseContent, int statusCode)
        {
            GlobalObjectListResponse res = JsonSerializer.Deserialize<GlobalObjectListResponse>(responseContent,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
            return res;

        }

        public static GlobalObjectListResponse ResponseListErrorAPI(string responseContent, int statusCode)
        {
        
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            if (statusCode == 500)
            {
                if (responseContent != "" && responseContent != null) {

                    ExceptionResponse exceptionResponse = JsonSerializer.Deserialize<ExceptionResponse>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    });

                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed + " : " + exceptionResponse.Message;
                    res.Error = true;

                } else {

                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed;
                    res.Error = true;
                }

            } else {

                if(responseContent != null && responseContent != "")
                {
                    res = JsonSerializer.Deserialize<GlobalObjectListResponse>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    });

                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed + " : " + res.Message;
                    res.Error = true;

                } else {

                    res.Code = statusCode;
                    res.Message = MessageService.MessageFailed + " : " + res.Message;
                    res.Error = true;
                }
              
            }

            return res;
        }
    }
}
