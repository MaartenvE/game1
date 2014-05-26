using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Default magnetometer implementation (using unity's Input.compass).
/// </summary>
public class Magnetometer : IMagnetometer
{
	/// <summary>
	/// The minimum change in microteslas to be considered changing.
	/// </summary>
	private const float CHANGING_THRESHOLD = 3.0f;

	/// <summary>
	/// The number of old magnetometer readings to be kept.
	/// </summary>
	private const int MAGNETOMETER_HISTORY_SIZE = 20;

	private IMagnetometerInput input;

	/// <summary>
	/// A list of previous magnetometer readings.
	/// </summary>
	private Queue<float> magnetometerHistory = new Queue<float>();

	public Magnetometer(IMagnetometerInput magnetometerInput)
	{
		this.input = magnetometerInput;
	}

	public Vector3 Magnetisation { 
		get {
			return input.Magnetisation;
		}
	}

	public void Update()
	{
		if (magnetometerHistory.Count == MAGNETOMETER_HISTORY_SIZE) {
			magnetometerHistory.Dequeue();
		}
		magnetometerHistory.Enqueue(Magnetisation.magnitude);
	}

	public bool IsChanging()
	{
		float average = magnetometerHistory.Average();

		float min = magnetometerHistory.Min();
		float max = magnetometerHistory.Max();

		return (min - average <= -CHANGING_THRESHOLD
		        || max - average >= CHANGING_THRESHOLD);
	}

}

