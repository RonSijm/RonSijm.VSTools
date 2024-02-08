// Global using directives

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

global using System.Collections;
global using System.IO.Abstractions;
global using System.Runtime.CompilerServices;
global using System.Text;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Xml.Linq;
global using JetBrains.Annotations;
global using Microsoft.Win32.SafeHandles;
global using RonSijm.VSTools.Lib.Features.Core.Extensions;
global using RonSijm.VSTools.Lib.Features.Core.Interfaces.CollectionInterfaces;
global using RonSijm.VSTools.Lib.Features.Core.Interfaces.ItemInterfaces;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.Core.Services;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.FolderFixing.Models;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Helpers;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Resharper;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation;
global using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Interfaces;

global using static RonSijm.VSTools.Lib.Features.Core.FileWrapping.SystemIO;