using AutoMapper;

namespace TildeTestAssignment.Application.Common.Mapping.Interfaces
{
    /// <summary>
    /// Allows to configure mappings inside implementing class
    /// </summary>
    public interface IMapped
    {
        void Mapping(Profile profile);
    }
}