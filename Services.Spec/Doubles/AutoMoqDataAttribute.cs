using AutoFixture;
using AutoFixture.AutoMoq;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(new Fixture().Customize(new AutoMoqCustomization()))
    { }
}