namespace LusiUtilsLibrary.Backend.APIs_REST;

public class InitializeChecks
{
    public static void InitialCheck(object variable, string message)
    {
        if (variable is null)
        {
            ArgumentNullException.ThrowIfNull(variable, message);
        }
    }
}
