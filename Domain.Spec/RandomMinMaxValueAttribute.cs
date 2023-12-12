using System.Reflection;
using Xunit.Sdk;

namespace Services.Spec.Infrastructures;

public class RandomMinMaxValueAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var random = new Random();

        decimal randomMin = (decimal)(random.NextDouble() * uint.MaxValue);
        decimal randomMax = randomMin + (decimal)(random.NextDouble() * (uint.MaxValue - (double)randomMin));

        return new[] { new object[] { randomMin, randomMax } };
    }
}