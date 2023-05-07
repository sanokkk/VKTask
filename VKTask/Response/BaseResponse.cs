namespace VKTask.Response
{
    public class BaseResponse<T>
    {
        public T Content { get; set; }

        public bool IsSuccess { get; set; }

        public BaseResponse()
        {
            IsSuccess = true;
        }
    }
}
