using System;
using System.Collections.Generic;
using System.Text;

namespace StringSwitchDotNet;

internal class StringComparisonHelper
{
    public static StringComparer FromComparison(StringComparison comparisonType) => comparisonType switch
    {
        StringComparison.CurrentCulture => StringComparer.CurrentCulture,
        StringComparison.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
        StringComparison.InvariantCulture => StringComparer.InvariantCulture,
        StringComparison.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
        StringComparison.Ordinal => StringComparer.Ordinal,
        StringComparison.OrdinalIgnoreCase => StringComparer.OrdinalIgnoreCase,
        _ => throw new ArgumentException(nameof(comparisonType)),
    };
}
