﻿using FluentValidation;
using FluentValidation.Results;
using UserApp.Service.Exceptions;

namespace EduTrack.Service.Extensions;

public static class ValidationExtensions
{
    public static async Task<ValidationResult> EnsureValidatedAsync<TValidator, TObject>(this TValidator validator,
        TObject @object)
        where TObject : class
        where TValidator : AbstractValidator<TObject>
    {
        var validationResult = await validator.ValidateAsync(@object);
        if (validationResult.Errors.Any())
            throw new ArgumentIsNotValidException(validationResult.Errors.First().ErrorMessage);

        return validationResult;
    }
}