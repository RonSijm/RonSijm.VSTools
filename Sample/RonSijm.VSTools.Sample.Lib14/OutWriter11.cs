// ReSharper disable ArrangeNamespaceBody - Justification: Used for testing

namespace RonSijm.VSTools.Sample.Lib14
{
    public static class OutWriter14
    {
        // ReSharper disable once RedundantNameQualifier
        public static void Write()
        {
            var lib14ResponseModel = new Lib14ResponseModel();
            if (lib14ResponseModel.Response != "Hello from Lib14")
            {
                throw new Exception("Lib14 rewired incorrectly");
            }

            var other = new RonSijm.VSTools.Sample.Lib14.NotExtensions.StillNotExtensions.Lib14ResponseModel();
            if (other.Response != "Hello from Lib14 2")
            {
                throw new Exception("Lib14 rewired incorrectly");
            }

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            VSTools.Sample.Lib14.NotExtensions.StillNotExtensions.Lib14ResponseModel other2 = new NotExtensions.StillNotExtensions.Lib14ResponseModel();
            if (other.Response != "Hello from Lib14 2")
            {
                throw new Exception("Lib14 rewired incorrectly");
            }

            Console.WriteLine(lib14ResponseModel.Response);
        }
    }
}