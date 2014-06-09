using UnityEngine;
using System.Collections;

public class TeamBlockTrackerLoader : MonoBehaviour
{
    public GameObject Prefab;
    public TeamBlockTracker Tracker;

	void Start()
    {
        Tracker = new TeamBlockTracker(gameObject, Prefab);
	}
}
