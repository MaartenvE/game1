using UnityEngine;
using System.Collections;

public class BlockAnimationBehaviour : MonoBehaviour {

    public Vector3 startVector;
    public Vector3 endVector;
    public float speed = 4.0F;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        GameObject.Find("GuiOverlay").GetComponent<InGameOverlay>().AnimationDone = false;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startVector, endVector);
    }
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.localPosition = Vector3.Lerp(startVector, endVector, fracJourney);
        if (transform.localPosition.Equals(endVector))
        {
            GameObject.Find("GuiOverlay").GetComponent<InGameOverlay>().AnimationDone = true;
            Destroy(this);
        }
    }

    public void SetUpAnimation(Vector3 start, Vector3 end)
    {
        startVector = start;
        endVector = end;
    }
}
