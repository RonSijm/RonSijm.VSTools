// Global using directives
global using static RonSijm.VSTools.Lib.Features.Core.FileWrapping.SystemIO;

global using System;
global using System.Collections.Generic;
global using System.Linq;

global using System.Diagnostics;
global using Microsoft.Build.Construction;
global using Microsoft.Extensions.Logging;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using OneOf;

global using Microsoft.CodeAnalysis;
global using Microsoft.CodeAnalysis.CSharp;
global using Microsoft.CodeAnalysis.CSharp.Syntax;
global using RonSijm.VSTools.Lib.Features.Core;
global using RonSijm.VSTools.Lib.Features.Core.Interfaces;
global using RonSijm.VSTools.Lib.Features.Core.Models;
global using RonSijm.VSTools.Lib.Features.Core.Options.Models;
global using RonSijm.VSTools.Lib.Features.CreateReferences.Interfaces;
global using RonSijm.VSTools.Lib.Features.CreateReferences.Models;
global using RonSijm.VSTools.Lib.Features.DebugHelper;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.Core;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core.Models;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Helpers;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Extensions;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;