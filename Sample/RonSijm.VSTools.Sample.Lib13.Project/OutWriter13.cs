using RonSijm.VSTools.Sample.Lib13.Project.Models;
using RonSijm.VSTools.Sample.Lib13.Project.Models.Lib13_1;
using RonSijm.VSTools.Sample.Lib13.Project.Models.Lib13_2;

namespace RonSijm.VSTools.Sample.Lib13.Project;

public class OutWriter13
{
    public Lib13ProjectModel ResponseModelInternal { get; set; }
    public OutWriter13ResponseModel1 ResponseModel1 { get; set; }
    public OutWriter13ResponseModel2 ResponseModel2 { get; set; }

    public static void Write()
    {
        // ReSharper disable SuggestVarOrType_SimpleTypes
        var responseModelInternal = new Lib13ProjectModel();

        if (responseModelInternal.OldName != "Lib1Internal")
        {
            throw new Lib13Exception();
        }

        OutWriter13ResponseModel1 response1 = new OutWriter13ResponseModel1();

        if (response1.OldName != "Model1")
        {
            throw new Lib13_1Exception();
        }

        OutWriter13ResponseModel2 response2 = new OutWriter13ResponseModel2();

        if (response2.OldName != "Model2")
        {
            throw new Lib13_2Exception();
        }

        Console.WriteLine("Hello from Lib13");
    }
}