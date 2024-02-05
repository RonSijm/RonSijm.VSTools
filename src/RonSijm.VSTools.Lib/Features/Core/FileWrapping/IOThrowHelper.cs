namespace RonSijm.VSTools.Lib.Features.Core.FileWrapping;

public static class IOThrowHelper
{
    public static bool ReadEnabled { get; set; } = true;

    public static bool ThrowRead { get; set; } = false;
    public static bool ThrowWrite { get; set; } = false;
    public static bool WriteEnabled { get; set; } = true;

    public static T ReturnOrThrowRead<T>()
    {
        if (ThrowRead)
        {
            throw new AccessViolationException();
        }

        return default;
    }

    public static void IgnoreOrThrowWrite()
    {
        if (ThrowWrite)
        {
            throw new AccessViolationException();
        }
    }
}