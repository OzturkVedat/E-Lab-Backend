namespace E_Lab_Backend.Dto
{
    public class ResultModel
    {
        public bool IsSucess {  get; set; }
        public string Message {  get; set; }
    }

    public class SuccessResult:ResultModel
    {
        public SuccessResult(string message)
        {
            IsSucess = true;
            Message = message;
        }
    }

    public class FailureResult:ResultModel
    {
        public List<string>? Errors { get; set; }   // for server-side val errors

        public FailureResult(string message)
        {
            IsSucess = false;
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
            IsSucess = true;
            Data = data;
            Message = "Istenen veriler basariyla alindi";
        }
    }

}
