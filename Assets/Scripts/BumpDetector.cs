using UnityEngine;
using System.Collections;

/// <summary>
/// Use the accelerometer to detect phones being bumped together. A bump is
/// detected if a negative acceleration is detected along the phone's Z-axis
/// (moving the phone towards its back), followed by a positive acceleration
/// along the Z-axis, then quickly coming to a halt (no acceleration at all).
/// </summary>
public class BumpDetector : MonoBehaviour
{
	/// <summary>
	/// The threshold above which the negative acceleration initiating a bump
	/// should lie.
	/// </summary>
	private const float AccelerationThreshold = -0.2f;
	
	/// <summary>
	/// The threshold above which the positive acceleration progressing the bump
	/// should lie.
	/// </summary>
	private const float DecelerationThreshold = 0.2f;
	
	/// <summary>
	/// The threshold below which the total acceleration on all three axes 
	/// combined should lie after completing the bump.
	/// </summary>
	private const float StationaryThreshold = 0.08f;
	
	/// <summary>
	/// The maximum allowed timespan between the acceleration initiating a bump
	/// and the deceleration progressing the bump.
	/// </summary>
	private const float AccelerateDeltaTime = 0.1f;
	
	/// <summary>
	/// The maximum allowed timespan between the deceleration progressing a bump
	/// and the phone being held still completing the bump.
	/// </summary>
	private const float DecelerateDeltaTime = 0.05f;
	
	/// <summary>
	/// The acceleration along the Z-axis measured during the previous update.
	/// </summary>
	private float PreviousAcceleration = 0.0f;
	
	/// <summary>
	/// The time the last negative acceleration greater than the threshold was
	/// measured, initiating a possible bump.
	/// </summary>
	private float BumpStartTime = -1.0f;
	
	/// <summary>
	/// The time the last positive acceleration greater than the threshold was 
	/// measured (only if a possible bump was started), progressing the bump.
	/// </summary>
	private float BumpProgressTime = -1.0f;
	
	/// <summary>
	/// The time the last detected bump ended.
	/// </summary>
	private float BumpEndTime = -1.0f;
	
	/// <summary>
	/// Check if the start of a possible bump movement has been detected.
	/// </summary>
	private bool HasBumpStarted    = false;
	
	/// <summary>
	/// Check if a possible bump has progressed to its deceleration phase.
	/// </summary>
	private bool HasBumpProgressed = false;
	
	/// <summary>
	/// Update accelerometer readings.
	/// </summary>
	void Update()
	{
		if (IsAccelerating())
		{
			StartBump();
		}
		
		else if (HasBumpStarted && IsDecelerating())
		{
			ProgressBump();
		}
		
		else if (HasBumpProgressed && IsStationary())
		{
			CompleteBump();
		}
		
		PreviousAcceleration = Input.acceleration.z;
	}
	
	/// <summary>
	/// Determines whether the phone is accelerating.
	/// </summary>
	/// <returns><c>true</c> if this instance is accelerating; otherwise, 
	///     <c>false</c>.</returns>
	private bool IsAccelerating()
	{
		return Input.acceleration.z - PreviousAcceleration 
			< AccelerationThreshold;
	}
	
	/// <summary>
	/// Determines whether the phone is decelerating.
	/// </summary>
	/// <returns><c>true</c> if this instance is decelerating; otherwise, 
	///     <c>false</c>.</returns>
	private bool IsDecelerating()
	{
		return Input.acceleration.z - PreviousAcceleration
			> DecelerationThreshold;
	}
	
	/// <summary>
	/// Determines whether the phone is stationary (held still on all three 
	/// axes).
	/// </summary>
	/// <returns><c>true</c> if this instance is stationary; otherwise, <c>false</c>.</returns>
	private bool IsStationary()
	{
		// Since the phone's accelerometer measures gravitational forces, 
		// 1g should be subtracted from the total acceleration magnitude to get 
		// user acceleration.
		return Mathf.Abs(Input.acceleration.magnitude - 1)
			< StationaryThreshold;
	}
	
	/// <summary>
	/// Initiate a new bump detection.
	/// </summary>
	private void StartBump()
	{
		BumpStartTime = Time.time;
		HasBumpStarted = true;
	}
	
	/// <summary>
	/// Progress bump detection state.
	/// </summary>
	private void ProgressBump()
	{
		if (Time.time - BumpStartTime <= AccelerateDeltaTime) {
			HasBumpProgressed = true;
			BumpProgressTime = Time.time;
		} else {
			EndBump();
		}
	}
	
	/// <summary>
	/// Complete a bump and reset bump detection state.
	/// </summary>
	private void CompleteBump()
	{
		if (Time.time - BumpProgressTime <= DecelerateDeltaTime) {
			BumpEndTime = Time.time;
		}
		EndBump();
	}
	
	/// <summary>
	/// Reset bump detection state.
	/// </summary>
	private void EndBump()
	{
		HasBumpStarted    = false;
		HasBumpProgressed = false;
		BumpStartTime     = -1.0f;
		BumpProgressTime  = -1.0f;
	}
}
