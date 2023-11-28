using System.Reflection;
using Xunit.Sdk;

namespace DomainTests;

public class PositiveRandomRangeAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var random = new Random();

        decimal randomMin = (decimal)Math.Round(random.NextDouble() * uint.MaxValue, 1);
        decimal randomMax = (decimal)Math.Round(
            random.NextDouble() * (uint.MaxValue - (double)randomMin) + (double)randomMin, 1);

        return new[] { new object[] { randomMin, randomMax } };
    }
}