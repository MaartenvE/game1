using UnityEngine;

namespace BuildingBlocks.BumpDetection
{
    public delegate void BumpMatchHandler(Bump bump1, Bump bump2);

    public interface IBumpMatcher
    {
        event BumpMatchHandler OnBumpMatch;

        void Add(Bump bump);
    }
}
