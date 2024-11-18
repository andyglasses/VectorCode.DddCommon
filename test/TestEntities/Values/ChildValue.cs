using System.Diagnostics.CodeAnalysis;

namespace VectorCode.DddCommon.Test.TestEntities.Values;

[ExcludeFromCodeCoverage]
public partial record ChildValue(string Value)
{
    public Dto ToDto()
    {
        return new Dto(Value);
    } 
}
