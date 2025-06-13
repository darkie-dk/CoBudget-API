﻿using System.Net;

namespace CoBudget.Exception.ExceptionsBase;

public class NotFoundException : CoBudgetException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
