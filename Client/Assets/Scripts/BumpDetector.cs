using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Use the accelerometer to detect phones being bumped together. A bump is
/// detected if a negative acceleration is detected along the phone's Z-axis
/// (moving the phone towards its back), followed by a positive acceleration
/// along the Z-axis, then quickly coming to a halt (no acceleration at all).
/// </summary>
public class BumpDetector
{
	/// <summary>
	/// Callbacks to be called when a bump is detected can be provided to the 
	/// BumpDetector. These callbacks must be of this type.
	/// </summary>
	public delegate void BumpEventHandler(Bump bump);
	
	/// <summary>
	/// EventHandler to be called when a bump is detected.
	/// </summary>
	public event BumpEventHandler OnBump;

	private readonly IAccelerometer accelerometer;
	private readonly IMagnetometer  magnetometer;
	
	//---------------- BUMP DETECTION PARAMETERS ----------------//
	// These values can be modified to finetune the forces the   //
	// device must encounter for a bump to be registered.        //
	//-----------------------------------------------------------//
	#region Detection parameters

	/// <summary>
	/// The maximum allowed timespan in millisecondsbetween the acceleration 
    /// initiating a bump and the deceleration progressing the bump.
	/// </summary>
	private const long ACCELERATE_DELTATIME = 100;
	
	/// <summary>
	/// The maximum allowed timespan in milliseconds between the deceleration 
    /// progressing a bump and the phone being held still completing the bump.
	/// </summary>
	private const long DECELERATE_DELTATIME = 100;

	#endregion
	
	//---------------- BUMP DETECTION VARIABLES -----------------//
	// These values keep track of the bump detection state, such //
	// as forces and timing.                                     //
	//-----------------------------------------------------------//
	#region Detection variables	
	/// <summary>
	/// The time the last negative acceleration greater than the threshold was
	/// measured, initiating a possible bump.
	/// </summary>
	private float bumpStartTime = -1.0f;
	
	/// <summary>
	/// The time the last positive acceleration greater than the threshold was 
	/// measured (only if a possible bump was started), progressing the bump.
	/// </summary>
	private float bumpProgressTime = -1.0f;
	
	/// <summary>
	/// The time the last detected bump ended.
	/// </summary>
	private float bumpEndTime = -1.0f;
	
	/// <summary>
	/// Check if the start of a possible bump movement has been detected.
	/// </summary>
	private bool hasBumpStarted = false;
	
	/// <summary>
	/// Check if a possible bump has progressed to its deceleration phase.
	/// </summary>
	private bool hasBumpProgressed = false;

    private Stopwatch accelerationStopwatch = new Stopwatch();
    private Stopwatch decelerationStopwatch = new Stopwatch();

	/// <summary>
	/// The acceleration measured during the decelerating phase of the bump.
	/// </summary>
	private Vector3 bumpAcceleration = Vector3.zero;
	#endregion
	
	/// <summary>
	/// Create a new BumpDetector, with the provided accelerometer and 
	/// magnetometer implementations.
	/// </summary>
	public BumpDetector(IAccelerometer accelerometer, IMagnetometer magnetometer)
	{
		this.accelerometer = accelerometer;
		this.magnetometer  = magnetometer;
	}
	
	/// <summary>
	/// Update accelerometer and magnetometer readings.
	/// </summary>
	public void DetectBump()
	{
		if (accelerometer.IsAccelerating())
		{
			StartBump();
		}
		
		else if (hasBumpStarted && accelerometer.IsDecelerating() 
		         && magnetometer.IsChanging())
		{
			ProgressBump();
		}
		
		else if (hasBumpProgressed && accelerometer.IsStationary() 
		         && accelerometer.IsUpright())
		{
			CompleteBump();
		}

		accelerometer.Update();
		magnetometer.Update();
	}

	/// <summary>
	/// Initiate a new bump detection.
	/// </summary>
	private void StartBump()
	{
		bumpStartTime = Time.time;
		hasBumpStarted = true;
        accelerationStopwatch.Reset();
        accelerationStopwatch.Start();
	}
	
	/// <summary>
	/// Progress bump detection state.
	/// </summary>
	private void ProgressBump()
	{
        if (accelerationStopwatch.ElapsedMilliseconds <= ACCELERATE_DELTATIME)
        {
            accelerationStopwatch.Reset();
			hasBumpProgressed = true;
			bumpProgressTime = Time.time;
			bumpAcceleration = accelerometer.Acceleration;
            decelerationStopwatch.Reset();
            decelerationStopwatch.Start();
		} 
        else
        {
			EndBump();
		}
	}
	
	/// <summary>
	/// Complete a bump and reset bump detection state.
	/// </summary>
	private void CompleteBump()
	{
		if (decelerationStopwatch.ElapsedMilliseconds <= DECELERATE_DELTATIME) {
			bumpEndTime = Time.time;

            if (OnBump != null)
            {
                OnBump(new Bump(bumpStartTime, bumpEndTime, bumpAcceleration.magnitude));
            }
		}
		EndBump();
	}
	
	/// <summary>
	/// Reset bump detection state.
	/// </summary>
	private void EndBump()
	{
		hasBumpStarted    = false;
		hasBumpProgressed = false;
		bumpStartTime     = -1.0f;
		bumpProgressTime  = -1.0f;
        accelerationStopwatch.Reset();
        decelerationStopwatch.Reset();
	}
}
