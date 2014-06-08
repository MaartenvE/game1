using UnityEngine;
using System;

public class BlockBehaviour : MonoBehaviour
{
	void Start ()
    {
        this.tag = "block";
	}
	
	void Update ()
    {
        //Vector3 location = this.transform.position;
        //this.transform.position = new Vector3((float) Math.Round(location.x, 1), (float) Math.Round(location.y, 1), (float) Math.Round(location.z, 1));	
	}

    public void Remove()
    {
        networkView.RPC("RemoveBlock", RPCMode.Server);
    }

    [RPC]
    void SetBlockInfo(string parent, Vector3 color)
    {
        this.transform.parent = GameObject.Find(parent).transform as Transform;
        this.renderer.material.color = new Color(color.x, color.y, color.z);
    }

    [RPC]
    void RemoveBlock() { }
}
