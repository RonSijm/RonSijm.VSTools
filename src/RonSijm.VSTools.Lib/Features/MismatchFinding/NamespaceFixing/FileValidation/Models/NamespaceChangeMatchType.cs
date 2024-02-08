namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

public enum NamespaceChangeMatchType
{
    NoMatch = 0,
    AlreadyMatches = 1,
    Exact = 2,
    StartsWith = 3,
}