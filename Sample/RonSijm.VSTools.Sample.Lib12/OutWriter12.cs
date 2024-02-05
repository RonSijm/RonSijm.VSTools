// ReSharper disable RedundantNameQualifier
// ReSharper disable ArrangeNamespaceBody

using RonSijm.VSTools.Sample.Lib12.Models;

namespace RonSijm.VSTools.Sample.Lib12
{
    public static class OutWriter12
    {
        public static OutWriter12ResponseModelInDifferentProject Write()
        {
            return new OutWriter12ResponseModelInDifferentProject { Response = "Hello from Lib12" };
        }

        public static Task<OutWriter12ResponseModelInDifferentProject> NoNothing1()
        {
            return Task.FromResult(new OutWriter12ResponseModelInDifferentProject());
        }

        public static Task<VSTools.Sample.Lib12.Models.OutWriter12ResponseModelInDifferentProject> NoNothing2()
        {
            return Task.FromResult(new OutWriter12ResponseModelInDifferentProject());
        }

        public static Task<Models.OutWriter12ResponseModelInDifferentProject> NoNothing3(OutWriter12ResponseModelInDifferentProject input)
        {
            return Task.FromResult(new OutWriter12ResponseModelInDifferentProject());
        }

        public static Task<VSTools.Sample.Lib12.Models.OutWriter12ResponseModelInDifferentProject> NoNothing4(Task<OutWriter12ResponseModelInDifferentProject> input)
        {
            return Task.FromResult(new OutWriter12ResponseModelInDifferentProject());
        }

        public static void NoNothing4((OutWriter12ResponseModelInDifferentProject Itemx, Lib12.Models.OutWriter12ResponseModelInDifferentProject ItemY) input)
        {
        }

        public static void NoNothing5((OutWriter12ResponseModelInDifferentProject Itemx, Models.OutWriter12ResponseModelInDifferentProject ItemY) input)
        {
        }
    }
}