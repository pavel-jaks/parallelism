namespace ThreeDimensionalMatch.Common
{
    public interface IVertex
    {
        int Id { get; }
        abstract int GetHashCode();
    }
}