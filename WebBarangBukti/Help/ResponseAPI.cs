using Newtonsoft.Json;

namespace WebBarangBukti.Help
{
    public static class ResponseAPI
    {
        public static GlobalObjectResponse ResponseSuccessAPI(string responseContent, int statusCode)
        {

            GlobalObjectResponse res = JsonConvert.DeserializeObject<GlobalObjectResponse>(responseContent);
            return res;
        }

        public static GlobalObjectResponse ResponseErrorAPI(string responseContent, int statusCode)
        {

            GlobalObjectResponse res = new GlobalObjectResponse();
            if (responseContent != "" && responseContent != null)  {

                ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
                res.Code = statusCode;
                res.Message = MessageService.MessageFailed + " : " + exceptionResponse.Message;
                res.Error = true;

            } else {

                res.Code = statusCode;
                res.Message = MessageService.MessageFailed;
                res.Error = true;
            }

            return res;
        }

        public static GlobalObjectListResponse ResponseListSuccessAPI(string responseContent, int statusCode)
        {

            GlobalObjectListResponse res = JsonConvert.DeserializeObject<GlobalObjectListResponse>(responseContent);
            return res;
        }

        public static GlobalObjectListResponse ResponseListErrorAPI(string responseContent, int statusCode)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            if (responseContent != "" && responseContent != null)
            {
                ExceptionResponse exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseContent);
                res.Code = statusCode;
                res.Message = MessageService.MessageFailed + " : " + exceptionResponse.Message;
                res.Error = true;

            } else {

                res.Code = statusCode;
                res.Message = MessageService.MessageFailed;
                res.Error = true;
            }
          
            return res;
        }
    }
}
