
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StringSwitch.Net;

public class StringSwitch<T> : IEnumerable<KeyValuePair<string, Func<T>>>
{
    public static StringSwitch<T> New => new StringSwitch<T>();


    public StringSwitch<T> IgnoreCase => new(StringComparison.OrdinalIgnoreCase, this);


    private readonly Dictionary<string, Func<T>> switches = new();


    public StringSwitch() : this(StringComparison.Ordinal, default(StringSwitch<T>))
    {

    }

    public StringSwitch(StringComparison comparison = StringComparison.Ordinal, StringSwitch stringSwitch = null)
    {
        switches = new(StringComparisonHelper.FromComparison(comparison));
        if (stringSwitch is not null)
        {
            foreach (var item in stringSwitch)
            {
                switches[item.Key] = () =>
                {
                    item.Value();
                    return default;
                };
            }
        }
    }

    public StringSwitch(StringComparison comparison = StringComparison.Ordinal, StringSwitch<T> stringSwitch = null)
    {
        switches = new(StringComparisonHelper.FromComparison(comparison));
        if (stringSwitch?.switches is not null)
        {
            foreach (var item in stringSwitch?.switches)
            {
                switches[item.Key] = item.Value;
            }
        }
    }

    public StringSwitch<T> Case(string key, T value)
            => Case(key, () => value);

    public StringSwitch<T> Case(string key, Func<T> action)
    {
        switches[key] = action;
        return this;
    }

    public T Execute(string value)
    {
        if (switches.TryGetValue(value, out var func))
        {
            return func();
        }

        return default;
    }

    public void Execute(string value, Action<T> onSuccess)
    {
        if (!switches.TryGetValue(value, out var func))
        {
            return;
        }

        var result = func();
        onSuccess?.Invoke(result);
    }

    public TX Execute<TX>(string value, Func<T, TX> onSuccess)
    {
        if (!switches.TryGetValue(value, out var func))
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

    public IEnumerator<KeyValuePair<string, Func<T>>> GetEnumerator() => ((IEnumerable<KeyValuePair<string, Func<T>>>)switches).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)switches).GetEnumerator();
}


public class StringSwitch : IEnumerable<KeyValuePair<string, Action>>
{
    public static StringSwitch New => new StringSwitch();

    public StringSwitch IgnoreCase => new StringSwitch(StringComparison.OrdinalIgnoreCase, this);

    private readonly Dictionary<string, Action> switches = new();
    private readonly StringComparison comparison;

    public StringSwitch(StringComparison comparison = StringComparison.Ordinal, StringSwitch stringSwitch = null)
    {
        switches = new(StringComparisonHelper.FromComparison(comparison));

        if (stringSwitch?.switches is not null)
        {
            foreach (var item in stringSwitch?.switches)
            {
                switches[item.Key] = item.Value;
            }
        }

        this.comparison = comparison;
    }

    public StringSwitch Case(string key, Action action)
    {
        switches[key] = action;
        return this;
    }

    public StringSwitch<T> Case<T>(string key, T value)
        => Case(key, () => value);

    public StringSwitch<T> Case<T>(string key, Func<T> action)
        => new StringSwitch<T>(comparison, this).Case(key, action);

    public void Execute(string value)
    {
        if (switches.TryGetValue(value, out Action action))
        {
            action();
        }
    }

    public IEnumerator<KeyValuePair<string, Action>> GetEnumerator() => ((IEnumerable<KeyValuePair<string, Action>>)switches).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)switches).GetEnumerator();
}
