﻿namespace SnackFlow.Domain.Exceptions;

public class UnsupportedCompanyTypeException(string message) 
    : DomainException(message);