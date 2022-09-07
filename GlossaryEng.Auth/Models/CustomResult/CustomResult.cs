namespace GlossaryEng.Auth.Models.CustomResult;

public class CustomResult
{
    public bool IsSuccess { get; }
    public string Error { get; }

    public CustomResult(bool isSuccess, string error = "")
    {
        IsSuccess = isSuccess;
        Error = error;
    }
}