using AutoMapper;

namespace TildeTestAssignment.Application.Common.Mapping.Interfaces
{
    /// <summary>
    /// Creates default mapping from <typeparamref name="TSource"/> without any configuration
    /// </summary>
    public interface IMappedFrom<TSource>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(TSource), GetType());
        }
    }
}