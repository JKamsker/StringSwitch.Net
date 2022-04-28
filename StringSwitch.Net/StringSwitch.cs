
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StringSwitchDotNet;

public class StringSwitch : IEnumerable<KeyValuePair<string, Action>>
{
    public static StringSwitch New => new();

    public StringSwitch IgnoreCase => new(StringComparison.OrdinalIgnoreCase, this);

    private readonly Dictionary<string, Action> _switches = new();
    //private readonly StringComparison _comparison;

    public StringSwitch(StringComparison comparison = StringComparison.Ordinal, StringSwitch? stringSwitch = null)
    {
        _switches = new(StringComparisonHelper.FromComparison(comparison));
        //_comparison = comparison;

        if (stringSwitch?._switches is null)
        {
            return;
        }

        foreach (var item in stringSwitch._switches)
        {
            _switches[item.Key] = item.Value;
        }
    }

    public StringSwitch Case(string key, Action action)
    {
        _switches[key] = action;
        return this;
    }

    public StringSwitch<T> Case<T>(string key, T value)
        => Case(key, () => value);

    public StringSwitch<T> Case<T>(string key, Func<T> action)
        => new StringSwitch<T>(_switches.Comparer, this).Case(key, action);

    public void Execute(string value)
    {
        if (_switches.TryGetValue(value, out Action action))
        {
            action();
        }
    }

    public bool TryExecute(string value)
    {
        if (_switches.TryGetValue(value, out Action action))
        {
            action();
            return true;
        }

        return false;
    }

    public IEnumerator<KeyValuePair<string, Action>> GetEnumerator() => ((IEnumerable<KeyValuePair<string, Action>>)_switches).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_switches).GetEnumerator();
}