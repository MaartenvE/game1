using UnityEngine;
using System.Collections;

public class SpecatorGui: MonoBehaviour{

	void OnGUI()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(1);
		}
	}
}
