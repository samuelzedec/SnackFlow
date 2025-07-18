﻿using System.Net;
using SnackFlow.Application.Abstractions;
using SnackFlow.Application.Common;

namespace SnackFlow.Application.Exceptions;

public class ValidationException(IEnumerable<ValidationError> errors) 
    : ApplicationException("One or more validation errors occurred", (int)HttpStatusCode.BadRequest)
{
    public IEnumerable<ValidationError> Errors => errors;
}
