using UnityEngine;

public class Bump
{
	/// <summary>
	/// The time the last negative acceleration greater than the threshold was
	/// measured, initiating a possible bump.
	/// </summary>
	public readonly float StartTime;
	
	/// <summary>
	/// The time the last detected bump ended.
	/// </summary>
	public readonly float EndTime;

	/// <summary>
	/// The measured acceleration during the deceleration phase of the bump.
	/// </summary>
	public readonly float Force;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Bump"/> class.
	/// </summary>
	/// <param name="startTime">Start time.</param>
	/// <param name="endTime">End time.</param>
	public Bump(float startTime, float endTime, float force)
	{
		StartTime = startTime;
		EndTime   = endTime;
		Force     = force;
	}
}
