using Microsoft.CodeAnalysis.CSharp;
using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation;

namespace RonSijm.VSTools.Tests;

public class UnitTest1 : BaseTestFor<RoslynSyntaxFixingFacade>
{
    [Fact]
    public async Task Test1()
    {
        var fileModel = new FileToFixModel();

        var file = await File.ReadAllTextAsync("C:\\Dev\\Personal\\RonSijm.VSTools\\Sample\\RonSijm.VSTools.Sample.CLIClient\\Program.cs");

        fileModel.SyntaxTree = CSharpSyntaxTree.ParseText(file);
        fileModel.Root = fileModel.SyntaxTree.GetCompilationUnitRoot();
        fileModel.DescendantNodes = fileModel.Root.DescendantNodes().ToList();

        var result = new NamespaceChangedCollectionModel
        {
            ("RonSijm.VSTools.Sample.Lib10", "Lib10Renamed"),
            ("Lib10.OutWriter10", "Lib10Renamed.OutWriter10"),
            ("Sample.Lib10.OutWriter10", "Lib10Renamed.OutWriter10"),
            ("VSTools.Sample.Lib10.OutWriter10", "Lib10Renamed.OutWriter10"),
            ("RonSijm.VSTools.Sample.Lib10.OutWriter10", "Lib10Renamed.OutWriter10"),
            ("Lib10.OutWriter10ResponseModelInFile", "Lib10Renamed.OutWriter10ResponseModelInFile"),
            ("Sample.Lib10.OutWriter10ResponseModelInFile", "Lib10Renamed.OutWriter10ResponseModelInFile"),
            ("VSTools.Sample.Lib10.OutWriter10ResponseModelInFile", "Lib10Renamed.OutWriter10ResponseModelInFile"),
            ("RonSijm.VSTools.Sample.Lib10.OutWriter10ResponseModelInFile", "Lib10Renamed.OutWriter10ResponseModelInFile"),
            ("RonSijm.VSTools.Sample.Lib11", "Lib11Renamed"),
            ("Lib11.OutWriter10ResponseModelInOwnFile", "Lib11Renamed.OutWriter10ResponseModelInOwnFile"),
            ("Sample.Lib11.OutWriter10ResponseModelInOwnFile", "Lib11Renamed.OutWriter10ResponseModelInOwnFile"),
            ("VSTools.Sample.Lib11.OutWriter10ResponseModelInOwnFile", "Lib11Renamed.OutWriter10ResponseModelInOwnFile"),
            ("RonSijm.VSTools.Sample.Lib11.OutWriter10ResponseModelInOwnFile", "Lib11Renamed.OutWriter10ResponseModelInOwnFile"),
            ("Lib11.OutWriter11", "Lib11Renamed.OutWriter11"),
            ("Sample.Lib11.OutWriter11", "Lib11Renamed.OutWriter11"),
            ("VSTools.Sample.Lib11.OutWriter11", "Lib11Renamed.OutWriter11"),
            ("RonSijm.VSTools.Sample.Lib11.OutWriter11", "Lib11Renamed.OutWriter11"),
            ("RonSijm.VSTools.Sample.Lib12", "Lib12Renamed"),
            ("Lib12.OutWriter12", "Lib12Renamed.OutWriter12"),
            ("Sample.Lib12.OutWriter12", "Lib12Renamed.OutWriter12"),
            ("VSTools.Sample.Lib12.OutWriter12", "Lib12Renamed.OutWriter12"),
            ("RonSijm.VSTools.Sample.Lib12.OutWriter12", "Lib12Renamed.OutWriter12"),
            ("RonSijm.VSTools.Sample.Lib12.Models", "Lib12RenamedMods"),
            ("Models.OutWriter12ResponseModelInDifferentProject", "Lib12RenamedMods.OutWriter12ResponseModelInDifferentProject"),
            ("Lib12.Models.OutWriter12ResponseModelInDifferentProject", "Lib12RenamedMods.OutWriter12ResponseModelInDifferentProject"),
            ("Sample.Lib12.Models.OutWriter12ResponseModelInDifferentProject", "Lib12RenamedMods.OutWriter12ResponseModelInDifferentProject"),
            ("VSTools.Sample.Lib12.Models.OutWriter12ResponseModelInDifferentProject", "Lib12RenamedMods.OutWriter12ResponseModelInDifferentProject"),
            ("RonSijm.VSTools.Sample.Lib12.Models.OutWriter12ResponseModelInDifferentProject", "Lib12RenamedMods.OutWriter12ResponseModelInDifferentProject"),
            ("RonSijm.VSTools.Sample.Lib2", "Lib2FolderRenamed"),
            ("Lib2.OutWriter2", "Lib2FolderRenamed.OutWriter2"),
            ("Sample.Lib2.OutWriter2", "Lib2FolderRenamed.OutWriter2"),
            ("VSTools.Sample.Lib2.OutWriter2", "Lib2FolderRenamed.OutWriter2"),
            ("RonSijm.VSTools.Sample.Lib2.OutWriter2", "Lib2FolderRenamed.OutWriter2"),
            ("RonSijm.VSTools.Sample.ConsoleApp", "RonSijm.VSTools.Sample.CLIClient"),
            ("ConsoleApp.Program", "RonSijm.VSTools.Sample.CLIClient.Program"),
            ("Sample.ConsoleApp.Program", "RonSijm.VSTools.Sample.CLIClient.Program"),
            ("VSTools.Sample.ConsoleApp.Program", "RonSijm.VSTools.Sample.CLIClient.Program"),
            ("RonSijm.VSTools.Sample.ConsoleApp.Program", "RonSijm.VSTools.Sample.CLIClient.Program"),
            ("RonSijm.VSTools.Sample.Lib9", "Lib9Renamed"),
            ("Lib9.OutWriter9", "Lib9Renamed.OutWriter9"),
            ("Sample.Lib9.OutWriter9", "Lib9Renamed.OutWriter9"),
            ("VSTools.Sample.Lib9.OutWriter9", "Lib9Renamed.OutWriter9"),
            ("RonSijm.VSTools.Sample.Lib9.OutWriter9", "Lib9Renamed.OutWriter9"),
            ("RonSijm.VSTools.Sample.Lib5", "Lib5Renamed"),
            ("Lib5.OutWriter5", "Lib5Renamed.OutWriter5"),
            ("Sample.Lib5.OutWriter5", "Lib5Renamed.OutWriter5"),
            ("VSTools.Sample.Lib5.OutWriter5", "Lib5Renamed.OutWriter5"),
            ("RonSijm.VSTools.Sample.Lib5.OutWriter5", "Lib5Renamed.OutWriter5"),
            ("RonSijm.VSTools.Sample.Lib6", "Lib6Renamed"),
            ("Lib6.OutWriter6", "Lib6Renamed.OutWriter6"),
            ("Sample.Lib6.OutWriter6", "Lib6Renamed.OutWriter6"),
            ("VSTools.Sample.Lib6.OutWriter6", "Lib6Renamed.OutWriter6"),
            ("RonSijm.VSTools.Sample.Lib6.OutWriter6", "Lib6Renamed.OutWriter6"),
            ("RonSijm.VSTools.Sample.Lib8", "Lib8Renamed"),
            ("Lib8.OutWriter8", "Lib8Renamed.OutWriter8"),
            ("Sample.Lib8.OutWriter8", "Lib8Renamed.OutWriter8"),
            ("VSTools.Sample.Lib8.OutWriter8", "Lib8Renamed.OutWriter8"),
            ("RonSijm.VSTools.Sample.Lib8.OutWriter8", "Lib8Renamed.OutWriter8"),
            ("RonSijm.VSTools.Sample.Lib7", "Lib7Renamed"),
            ("Lib7.OutWriter7", "Lib7Renamed.OutWriter7"),
            ("Sample.Lib7.OutWriter7", "Lib7Renamed.OutWriter7"),
            ("VSTools.Sample.Lib7.OutWriter7", "Lib7Renamed.OutWriter7"),
            ("RonSijm.VSTools.Sample.Lib7.OutWriter7", "Lib7Renamed.OutWriter7"),
        };

        SUT.FixFile(fileModel, result);
    }
}