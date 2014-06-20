using UnityEngine;

namespace BuildingBlocks
{
    public interface IRenderer
    {
        IMaterial material { get; }
        bool enabled { get; set; }
    }
}
