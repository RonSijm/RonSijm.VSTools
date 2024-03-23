// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace RonSijm.VSTools.CLI.Options.Logging;

internal sealed class LogValuesFormatter
{
    private const string NullValue = "(null)";
    private readonly List<string> _valueNames = new();
#if NET8_0_OR_GREATER
    private readonly CompositeFormat _format;
#else
        private readonly string _format;
#endif

    public LogValuesFormatter(string format)
    {
        OriginalFormat = format;

        var vsb = new ValueStringBuilder(stackalloc char[256]);
        var scanIndex = 0;
        var endIndex = format.Length;

        while (scanIndex < endIndex)
        {
            var openBraceIndex = FindBraceIndex(format, '{', scanIndex, endIndex);
            if (scanIndex == 0 && openBraceIndex == endIndex)
            {
                // No holes found.
                _format =
#if NET8_0_OR_GREATER
                    CompositeFormat.Parse(format);
#else
                        format;
#endif
                return;
            }

            var closeBraceIndex = FindBraceIndex(format, '}', openBraceIndex, endIndex);

            if (closeBraceIndex == endIndex)
            {
                vsb.Append(format.AsSpan(scanIndex, endIndex - scanIndex));
                scanIndex = endIndex;
            }
            else
            {
                // Format item syntax : { index[,alignment][ :formatString] }.
                var formatDelimiterIndex = format.AsSpan(openBraceIndex, closeBraceIndex - openBraceIndex).IndexOfAny(',', ':');
                formatDelimiterIndex = formatDelimiterIndex < 0 ? closeBraceIndex : formatDelimiterIndex + openBraceIndex;

                vsb.Append(format.AsSpan(scanIndex, openBraceIndex - scanIndex + 1));
                vsb.Append(_valueNames.Count.ToString());
                _valueNames.Add(format.Substring(openBraceIndex + 1, formatDelimiterIndex - openBraceIndex - 1));
                vsb.Append(format.AsSpan(formatDelimiterIndex, closeBraceIndex - formatDelimiterIndex + 1));

                scanIndex = closeBraceIndex + 1;
            }
        }

        _format =
#if NET8_0_OR_GREATER
            CompositeFormat.Parse(vsb.ToString());
#else
                vsb.ToString();
#endif
    }

    public string OriginalFormat { get; }
    public List<string> ValueNames => _valueNames;

    private static int FindBraceIndex(string format, char brace, int startIndex, int endIndex)
    {
        // Example: {{prefix{{{Argument}}}suffix}}.
        var braceIndex = endIndex;
        var scanIndex = startIndex;
        var braceOccurrenceCount = 0;

        while (scanIndex < endIndex)
        {
            if (braceOccurrenceCount > 0 && format[scanIndex] != brace)
            {
                if (braceOccurrenceCount % 2 == 0)
                {
                    // Even number of '{' or '}' found. Proceed search with next occurrence of '{' or '}'.
                    braceOccurrenceCount = 0;
                    braceIndex = endIndex;
                }
                else
                {
                    // An unescaped '{' or '}' found.
                    break;
                }
            }
            else if (format[scanIndex] == brace)
            {
                if (brace == '}')
                {
                    if (braceOccurrenceCount == 0)
                    {
                        // For '}' pick the first occurrence.
                        braceIndex = scanIndex;
                    }
                }
                else
                {
                    // For '{' pick the last occurrence.
                    braceIndex = scanIndex;
                }

                braceOccurrenceCount++;
            }

            scanIndex++;
        }

        return braceIndex;
    }

    public string Format(object[] values)
    {
        var formattedValues = values;

        if (values != null)
        {
            for (var i = 0; i < values.Length; i++)
            {
                var formattedValue = FormatArgument(values[i]);
                // If the formatted value is changed, we allocate and copy items to a new array to avoid mutating the array passed in to this method
                if (!ReferenceEquals(formattedValue, values[i]))
                {
                    formattedValues = new object[values.Length];
                    Array.Copy(values, formattedValues, i);
                    formattedValues[i++] = formattedValue;
                    for (; i < values.Length; i++)
                    {
                        formattedValues[i] = FormatArgument(values[i]);
                    }
                    break;
                }
            }
        }

        return string.Format(CultureInfo.InvariantCulture, _format, formattedValues ?? Array.Empty<object>());
    }

    public KeyValuePair<string, object> GetValue(object[] values, int index)
    {
        if (index < 0 || index > _valueNames.Count)
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        if (_valueNames.Count > index)
        {
            return new KeyValuePair<string, object>(_valueNames[index], values[index]);
        }

        return new KeyValuePair<string, object>("{OriginalFormat}", OriginalFormat);
    }

    private static object FormatArgument(object value)
    {
        object stringValue = null;
        return TryFormatArgumentIfNullOrEnumerable(value, ref stringValue) ? stringValue : value!;
    }

    private static bool TryFormatArgumentIfNullOrEnumerable<T>(T value, [NotNullWhen(true)] ref object stringValue)
    {
        if (value == null)
        {
            stringValue = NullValue;
            return true;
        }

        // if the value implements IEnumerable but isn't itself a string, build a comma separated string.
        if (value is not string && value is IEnumerable enumerable)
        {
            var vsb = new ValueStringBuilder(stackalloc char[256]);
            var first = true;
            foreach (var e in enumerable)
            {
                if (!first)
                {
                    vsb.Append(", ");
                }

                vsb.Append(e != null ? e.ToString() : NullValue);
                first = false;
            }
            stringValue = vsb.ToString();
            return true;
        }

        return false;
    }
}