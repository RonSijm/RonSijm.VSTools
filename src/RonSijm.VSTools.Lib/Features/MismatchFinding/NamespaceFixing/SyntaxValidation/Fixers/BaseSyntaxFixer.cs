﻿namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public abstract class BaseSyntaxFixer : IFixableItem
{
    // ReSharper disable once EmptyConstructor - Justification - Used for debugging to break when fix is created.
    private protected BaseSyntaxFixer()
    {
    }

    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }
    public FileToFixModel Parent { get; set; }

    public abstract void Fix();
}