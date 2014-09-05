using UnityEngine;
using System.Collections;

public class FrameLimiter : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 30;
    }
}
