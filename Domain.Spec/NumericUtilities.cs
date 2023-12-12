namespace Services.Spec.Infrastructures;

public static class NumericUtilities
{
    public static decimal ConvertToNegative(this decimal number)
    {
        return -Math.Abs(number);
    }
    
    public static decimal ConvertToPositive(this decimal number)
    {
        return Math.Abs(number);
    }
}