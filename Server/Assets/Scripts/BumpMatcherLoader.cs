using UnityEngine;

public class BumpMatcherLoader : MonoBehaviour
{
    private IBumpMatcher bumpMatcher;
    public IBumpMatcher BumpMatcher
    {
        get
        {
            return bumpMatcher;
        }
    }

	void Start()
    {
        bumpMatcher = new BumpMatcher();
        bumpMatcher.OnBumpMatch += (Bump bump1, Bump bump2) => Debug.Log("Detected bump between players " + bump1.Sender + " and " + bump2.Sender);
	}

    [RPC]
    void Tap(float force, NetworkMessageInfo info)
    {
        bumpMatcher.Add(new Bump(info.timestamp, force, new NetworkPlayerWrapper(info.sender)));
    }
}
