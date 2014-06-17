using UnityEngine;

public interface IRenderer
{
    IMaterial material { get; }
    bool enabled { get; set; }
}
