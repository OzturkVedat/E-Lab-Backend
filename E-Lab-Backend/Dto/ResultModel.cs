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
        public FailureResult(string message)
        {
            IsSucess = false;
            Message = message;
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
