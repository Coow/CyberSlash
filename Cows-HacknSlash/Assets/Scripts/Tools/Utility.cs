using System;

/// <summary>
/// Should contain all methods that can be used across the whole application
/// </summary>
public static class Utility
{
    /// <summary>
    /// Checks if the first enum contains the bits of the second enum with a bitwise operation
    /// </summary>
    /// <param name="e">The enum to check</param>
    /// <param name="flag">The flags to search for</param>
    /// <returns></returns>
    public static bool HasFlag2(this Enum e, Enum flag)
    {
        // Check whether the flag was given
        if (flag == null)
        {
            throw new ArgumentNullException("flag");
        }

        // Compare the types of both enumerations
        if (!e.GetType().IsEquivalentTo(flag.GetType()))
        {
            throw new ArgumentException($"The type of the given flag is not of type {e.GetType()}", "flag");
        }

        // Get the type code of the enumeration
        var typeCode = e.GetTypeCode();

        // If the underlying type of the flag is signed
        if (typeCode == TypeCode.SByte || typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64)
        {
            return (Convert.ToInt64(e) & Convert.ToInt64(flag)) != 0;
        }

        // If the underlying type of the flag is unsigned
        if (typeCode == TypeCode.Byte || typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
        {
            return (Convert.ToUInt64(e) & Convert.ToUInt64(flag)) != 0;
        }

        // Unsupported flag type
        throw new Exception($"The comparison of the type {e.GetType().Name} is not implemented.");
    }
}