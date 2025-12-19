using System.Text.RegularExpressions;

namespace YugiohLifeCounter.Core.Calculator;

public sealed class LifePointCalculator
{
    public int Apply(string input, int current)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Empty input.", nameof(input));
        }

        string expr = input.Trim().ToLowerInvariant();

        // 8k -> 8000
        expr = Regex.Replace(expr, @"\b(\d+)k\b", m =>
        {
            int v = int.Parse(m.Groups[1].Value);
            return checked(v * 1000).ToString();
        });

        if (Regex.IsMatch(expr, @"^\d+$"))
        {
            return ClampNonNegative(int.Parse(expr));
        }

        if (Regex.IsMatch(expr, @"^[+\-]\d+$"))
        {
            return ClampNonNegative(checked(current + int.Parse(expr)));
        }

        if (Regex.IsMatch(expr, @"^\*\d+$"))
        {
            int factor = int.Parse(expr[1..]);
            return ClampNonNegative(checked(current * factor));
        }

        if (Regex.IsMatch(expr, @"^/\d+$"))
        {
            int divisor = int.Parse(expr[1..]);
            if (divisor == 0) throw new ArgumentException("Division by zero.", nameof(input));
            return ClampNonNegative(current / divisor);
        }

        if (expr == "%2")
        {
            return ClampNonNegative(current / 2);
        }

        throw new ArgumentException("Invalid expression.", nameof(input));
    }

    private static int ClampNonNegative(int value) => value < 0 ? 0 : value;
}