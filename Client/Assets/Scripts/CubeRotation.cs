using UnityEngine;
using System.Collections;

public class CubeRotation : MonoBehaviour
{
	void Start ()
    {
        renderer.material.color = Color.red;
	}
	
    // Rotate block
	void Update ()
    {
        float r = Time.deltaTime * 25.0f;
        transform.Rotate(r, r / 2, r / 3);
    }
}
