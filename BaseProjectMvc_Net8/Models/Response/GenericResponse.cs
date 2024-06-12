
namespace BaseProjectMvc_Net8.Models.Response
{
    public class GenericResponse<TResult>
    {
        public GenericResponse() { }


        public GenericResponse(string message, object error, TResult result)
        {
            Message = message;
            Error = error;
            Result = result;
        }

        public string Message { get; set; } = string.Empty;
        public object Error { get; set; }
        public TResult Result { get; set; }

        public static implicit operator GenericResponse<TResult>(GenericResponse<List<ItemModel>> v)
        {
            throw new NotImplementedException();
        }
    }
}
