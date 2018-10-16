namespace Starter.Common.Extensions
{
    public interface ITypeTransformer
    {
        string Transform(string name);
        string Transform<T>();
        string TransformToTypeName(string name);
    }
}