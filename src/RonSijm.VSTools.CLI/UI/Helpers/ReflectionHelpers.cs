namespace RonSijm.VSTools.CLI.UI.Helpers;

public static class ReflectionHelpers
{
    public static Type GetGenericType(this object input, Type openGeneric)
    {
        var inputType = input.GetType();
        return GetGenericType(inputType, openGeneric);
    }

    public static Type GetGenericType(this Type type, Type openGeneric)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == openGeneric)
        {
            return type.GetGenericArguments().FirstOrDefault();
        }
        else
        {
            var interfaceTypes = type.GetInterfaces();
            foreach (var interfaceType in interfaceTypes)
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGeneric)
                {
                    return interfaceType.GetGenericArguments().FirstOrDefault();
                }
            }
        }

        return null;
    }
}