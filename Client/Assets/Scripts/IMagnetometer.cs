using UnityEngine;

public interface IMagnetometer
{
	Vector3 Magnetisation { get; }
	void Update();
	bool IsChanging();
}
