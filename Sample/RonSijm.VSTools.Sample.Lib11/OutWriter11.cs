// ReSharper disable ArrangeNamespaceBody - Justification: Used for testing
namespace RonSijm.VSTools.Sample.Lib11
{
    public static class OutWriter11
    {
        // ReSharper disable once RedundantNameQualifier
        public static VSTools.Sample.Lib11.OutWriter10ResponseModelInOwnFile Write()
        {
            return new OutWriter10ResponseModelInOwnFile { Response = "Hello from Lib11" };
        }
    }
}