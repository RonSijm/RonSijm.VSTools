// ReSharper disable RedundantUsingDirective
// ReSharper disable RedundantNameQualifier
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using RonSijm.VSTools.Sample.Lib6;
using RonSijm.VSTools.Sample;

namespace RonSijm.VSTools.Sample.Lib5;

public static class OutWriter5
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib5");
        OutWriter6.Write();
        Sample.Lib7.OutWriter7.Write();
    }


    public static async Task<ClassWithInnerClass.InnerClass.Response> DoStuffAsync(Task<RonSijm.VSTools.Sample.Lib5.ClassWithInnerClass.InnerClass.Request> request)
    {
        return null;
    }

    public static ClassWithInnerClass.InnerClass.Response DoStuff(Sample.Lib5.ClassWithInnerClass.InnerClass.Request request)
    {
        return null;
    }

    public static bool ShouldWrite => true;
}