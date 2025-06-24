namespace LusiUtilsLibrary.Initialization;

public static class IntegrityCheck
{
    /// <summary>
    /// Check if the passed variable is null, if it is, throw ArgumentNullException with passed message.
    /// </summary>
    /// <param name="variable">Variable checked to not be null.</param>
    /// <param name="message">Exception message in case variable is null</param>
    public static void RequiresNotNull(object variable, string message)
    {
        if (variable is null)
            ArgumentNullException.ThrowIfNull(variable, message);
    }

    /// <summary>
    /// Check if the passed variable is null, if it is, throw ArgumentNullException with default message.
    /// </summary>
    /// <param name="variable">Variable checked to not be null.</param>
    public static void RequiresNotNull(object variable)
    {
        if (variable is null)
        {
            string message = $"Variable <{variable}> cannot be null.";
            ArgumentNullException.ThrowIfNull(variable, message);
        }
    }

    /// <summary>
    /// Verify that the required check is true, if not, throw ArgumentNullException with passed message.
    /// </summary>
    /// <param name="check">Check to be true</param>
    /// <param name="message">Exception message in case check is false</param>
    public static void Requires(bool check, string message)
    {
        if (!check)
            ArgumentNullException.ThrowIfNull(check, message);
    }
}
