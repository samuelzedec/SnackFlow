﻿namespace SnackFlow.Application.Common;

public sealed record ValidationError(string PropertyName, string ErrorMessage);