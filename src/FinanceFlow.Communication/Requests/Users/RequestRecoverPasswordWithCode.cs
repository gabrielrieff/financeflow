﻿namespace FinanceFlow.Communication.Requests.Users;

public class RequestRecoverPasswordWithCode
{
    public string Email { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}
