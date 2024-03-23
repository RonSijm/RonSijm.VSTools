// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Collections.Concurrent;
using System.Threading;

namespace RonSijm.VSTools.CLI.Options.Logging;

/// <inheritdoc />
public readonly struct FormattedLogValues : IReadOnlyList<KeyValuePair<string, object>>
{
    internal const int MaxCachedFormatters = 1024;
    private const string NullFormat = "[null]";

    private static int _sCount;
    private static readonly ConcurrentDictionary<string, LogValuesFormatter> SFormatters = new();

    private readonly LogValuesFormatter _formatter;
    private readonly object[] _values;
    private readonly string _originalMessage;

    public FormattedLogValues(string format, params object[] values)
    {
        if (values != null && values.Length != 0 && format != null)
        {
            if (_sCount >= MaxCachedFormatters)
            {
                if (!SFormatters.TryGetValue(format, out _formatter))
                {
                    _formatter = new LogValuesFormatter(format);
                }
            }
            else
            {
                _formatter = SFormatters.GetOrAdd(format, f =>
                {
                    Interlocked.Increment(ref _sCount);
                    return new LogValuesFormatter(f);
                });
            }
        }
        else
        {
            _formatter = null;
        }

        _originalMessage = format ?? NullFormat;
        _values = values;
    }

    public KeyValuePair<string, object> this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            if (index == Count - 1)
            {
                return new KeyValuePair<string, object>("{OriginalFormat}", _originalMessage);
            }

            return _formatter!.GetValue(_values!, index);
        }
    }

    public int Count
    {
        get
        {
            if (_formatter == null)
            {
                return 1;
            }

            return _formatter.ValueNames.Count + 1;
        }
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        for (var i = 0; i < Count; ++i)
        {
            yield return this[i];
        }
    }

    public override string ToString()
    {
        if (_formatter == null)
        {
            return _originalMessage;
        }

        return _formatter.Format(_values);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}