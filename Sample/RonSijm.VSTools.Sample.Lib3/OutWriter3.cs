using RonSijm.VSTools.Sample.Lib3.Enums;
using RonSijm.VSTools.Sample.Lib3.OtherStuff.Subfolder;
using RonSijm.VSTools.Sample.Lib3.OtherStuff.Subfolder2;
using RonSijm.VSTools.Sample.Lib3.Records;
using RonSijm.VSTools.Sample.Lib3.Records.Subfolder;

namespace RonSijm.VSTools.Sample.Lib3;

#pragma warning disable CS0168 // Variable is declared but never used
// ReSharper disable RedundantNameQualifier
// ReSharper disable InconsistentNaming
// ReSharper disable once ConditionIsAlwaysTrueOrFalse


public static class OutWriter3
{
    public static ShouldWrite ShouldWrite1 { get; set; }
    public static RonSijm.VSTools.Sample.Lib3.Enums.ShouldWrite ShouldWrite2 { get; set; }
    public static Sample.Lib3.Enums.ShouldWrite ShouldWrite3 { get; set; }


    public static WriteRecord WriteRecord1 { get; set; }
    public static RonSijm.VSTools.Sample.Lib3.Records.WriteRecord WriteRecord2 { get; set; }
    public static Lib3.Records.WriteRecord WriteRecord3 { get; set; }


    public static WriteRecordInSubfolder WriteRecordInSubfolder1 { get; set; }
    public static RonSijm.VSTools.Sample.Lib3.Records.Subfolder.WriteRecordInSubfolder WriteRecordInSubfolder2 { get; set; }
    public static Lib3.Records.Subfolder.WriteRecordInSubfolder WriteRecordInSubfolder3 { get; set; }


    public static IWriterDummy IWriterDummy { get; set; }
    public static RonSijm.VSTools.Sample.Lib3.OtherStuff.Subfolder2.IWriterDummy IWriterDummy1 { get; set; }
    public static OtherStuff.Subfolder2.IWriterDummy IWriterDummy2 { get; set; }


    public static void Write()
    {
        var something = ShouldWrite.Option1;

        if (something == VSTools.Sample.Lib3.Enums.ShouldWrite.Option1)
        {
            Console.WriteLine("Hello from Lib3");
        }
    }

    public static void DummyMethod(Task<(VSTools.Sample.Lib3.Enums.ShouldWrite shouldWrite, (VSTools.Sample.Lib3.Enums.ShouldWrite, WriteRecordInSubfolder))> input)
    {
        var writeDelegate = new WriteDelegate(() => { });

        RonSijm.VSTools.Sample.Lib3.OtherStuff.Subfolder.WriteDelegate writeDelegate2;
    }
}