namespace E_Lab_Backend.Dto
{
    public class ResultModel
    {
        public bool IsSuccess {  get; set; }
        public string Message {  get; set; }
    }

    public class SuccessResult:ResultModel
    {
        public SuccessResult(string message)
        {
            IsSuccess = true;
            Message = message;
        }
    }

    public class FailureResult:ResultModel
    {
        public List<string>? Errors { get; set; }   // for server-side val errors

        public FailureResult(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public FailureResult(string message, List<string> errors) : this(message)
        {
            Errors = errors;
        }
    }

    public class SuccessDataResult<T> : ResultModel
    {
        public T Data { get; set; }
        public SuccessDataResult(T data)
        {
            IsSuccess = true;
            Data = data;
            Message = "Istenen veriler basariyla alindi";
        }
    }

}
