using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Domain.Utility;

/// <summary>
/// Provides utility methods for validating objects and properties against their validation attributes.
/// </summary>
public static class ValidationExtension
{
    /// <summary>
    /// Validates an object's properties against the defined validation attributes.
    /// </summary>
    /// <param name="obj">The object to validate.</param>
    /// <exception cref="ValidationException">Thrown when the object fails to satisfy its validation attributes.</exception>
    /// <remarks>
    /// This method checks all properties of the provided object that are decorated with validation attributes
    /// and aggregates all validation errors. If any validation errors are found, a ValidationException is thrown
    /// with a detailed error message.
    /// </remarks>
    public static void ValidateObjectOrThrow(this IEntity obj)
    {
        var context = new ValidationContext(obj);
        var results = new List<ValidationResult>();
        if (Validator.TryValidateObject(obj, context, results, true)) return;

        var errors = results.SelectMany(vr => vr.MemberNames.Select(mn => $"{mn}: {vr.ErrorMessage}"));
        var message = string.Join("\n", errors);
        throw new ValidationException($"Validation failed: \n{message}");
    }

    /// <summary>
    /// Validates a single property of an object against its validation attributes.
    /// </summary>
    /// <param name="propertyValue">The value of the property to validate.</param>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="validatingObject">The object that contains the property to validate.</param>
    /// <typeparam name="T">The type of the property being validated.</typeparam>
    /// <exception cref="ValidationException">Thrown when the property fails to satisfy its validation attributes.</exception>
    /// <remarks>
    /// This method checks the specified property against its validation attributes. If the validation fails,
    /// a ValidationException is thrown with a detailed error message specifying the property name and the validation errors.
    /// </remarks>
    public static void ValidatePropertyOrThrow<T>(this IEntity validatingObject, T propertyValue, string propertyName)
    {
        var context = new ValidationContext(validatingObject) { MemberName = propertyName };
        var results = new List<ValidationResult>();
        if (Validator.TryValidateProperty(propertyValue, context, results)) return;

        var errors = results.Select(vr => vr.ErrorMessage);
        var message = string.Join("\n", errors);
        throw new ValidationException($"Validation failed for {propertyName}: \n{message}");
    }
}