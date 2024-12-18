﻿namespace CashFlow.Communication.Responses;
public class ResponseError
{
    public List<string> ErrorMessage { get; set; } = [];
    public bool TokenIsExpired { get; set; }

    public ResponseError(string errorMessage)
    {
        ErrorMessage = [errorMessage];
    }

    public ResponseError(List<string> errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
