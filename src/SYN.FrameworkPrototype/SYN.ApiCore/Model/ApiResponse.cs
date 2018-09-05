namespace SYN.ApiCore.Model
{
    public class ApiResponse<T>
    {
        public ApiResponse(int code, T data, string message = null)
        {
            Status = GetStatus(code);
            Code = code;
            Data = data;
            Message = message;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { set; get; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { set; get; }

        /// <summary>
        /// 获取状态描述
        /// </summary>
        /// <returns></returns>
        private string GetStatus(int code)
        {
            if (code < 400)
            {
                //1XX、2XX、3XX
                return "success";
            }

            if (code >= 400 && code < 500)
            {
                return "error";
            }
                
            return "fail";
        }
    }
}
