using Ugly.Mug.Cafe.Domain.Enum;

namespace Ugly.Mug.Cafe.Domain.Response
{
    public class BaseResponse<T>
    {
        public ResultType StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
