using Microsoft.AspNetCore.Mvc;
using SYN.ApiCore.Model;
using System.Net;

namespace SYN.ApiCore.Common
{
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// 执行成功；没有返回数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ApiResponse<object> Success(int code = 0, string message = null)
        {
            return GetResponse(code == 0 ? (int)HttpStatusCode.OK : code, default(object), message);
        }

        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ApiResponse<T> Success<T>(T data, int code = 0, string message = null)
        {
            return GetResponse(code == 0 ? (int)HttpStatusCode.OK : code, data, message);
        }

        /// <summary>
        /// 执行失败，请求异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ApiResponse<object> Error(int code, string message = null)
        {
            return GetResponse(code == 0 ? (int)HttpStatusCode.BadRequest : code, default(object), message);
        }

        /// <summary>
        /// 执行失败，服务器段异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ApiResponse<object> Failture(int code, string message = null)
        {
            return GetResponse(code == 0 ? (int)HttpStatusCode.InternalServerError : code, default(object), message);
        }

        /// <summary>
        /// 获取执行结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private ApiResponse<T> GetResponse<T>(int code, T data, string message)
        {
            var response = new ApiResponse<T>(code, data, message);
            return response;
        }
    }
}
