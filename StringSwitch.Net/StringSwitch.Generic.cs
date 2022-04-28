
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace StringSwitchDotNet;

public class StringSwitch<T> : IEnumerable<KeyValuePair<string, Func<T>>>
{
    public static StringSwitch<T> New => new();

    public StringSwitch<T> IgnoreCase => new(StringComparison.OrdinalIgnoreCase, this);

    private readonly Dictionary<string, Func<T>> _switches = new();

    public StringSwitch() : this(StringComparison.Ordinal, default(StringSwitch<T>))
    {
    }

    public StringSwitch(StringComparison comparison = StringComparison.Ordinal, StringSwitch? stringSwitch = null)
        : this(StringComparisonHelper.FromComparison(comparison), stringSwitch)
    {

    }
    

    public StringSwitch(StringComparison comparison = StringComparison.Ordinal, StringSwitch<T>? stringSwitch = null)
        : this(StringComparisonHelper.FromComparison(comparison), stringSwitch)
    {
    }
    public StringSwitch(IEqualityComparer<string> comparer, StringSwitch? stringSwitch = null)
    {
        _switches = new(comparer);
        if (stringSwitch is null)
        {
            return;
        }

        foreach (var item in stringSwitch)
        {
            _switches[item.Key] = () =>
            {
                item.Value();
                return default;
            };
        }
    }

    public StringSwitch(IEqualityComparer<string> comparer, StringSwitch<T>? stringSwitch = null)
    {
        _switches = new(comparer);
        if (stringSwitch?._switches is null)
        {
            return;
        }

        foreach (var item in stringSwitch._switches)
        {
            _switches[item.Key] = item.Value;
        }
    }

    public StringSwitch<T> Case(string key, T value)
            => Case(key, () => value);

    public StringSwitch<T> Case(string key, Func<T> action)
    {
        _switches[key] = action;
        return this;
    }

    public T Execute(string value)
    {
        return TryExecute(value, out var result) ? result : default;
    }

    public bool TryExecute(string value, out T result)
    {
        if (_switches.TryGetValue(value, out var func))
        {
            result = func();
            return true;
        }

        result = default;
        return false;
    }

    public void Execute(string value, Action<T> onSuccess)
    {
        if (!_switches.TryGetValue(value, out var func))
        {
            return;
        }

        var result = func();
        onSuccess?.Invoke(result);
    }

    public TX Execute<TX>(string value, Func<T, TX> onSuccess)
    {
        if (!_switches.TryGetValue(value, out var func))
        {
            return default;
        }

        var result = func();
        if (onSuccess == default)
        {
            return default;
        }

        return onSuccess(result);
    }

    public IEnumerator<KeyValuePair<string, Func<T>>> GetEnumerator() 
        => ((IEnumerable<KeyValuePair<string, Func<T>>>)_switches).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_switches).GetEnumerator();
}
