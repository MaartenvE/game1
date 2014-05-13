using UnityEngine;


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

	/// <summary>
	/// The accelerometer implementation to be used by the BumpDetector.
	/// </summary>
	private readonly IAccelerometer accelerometer;

	//---------------- BUMP DETECTION PARAMETERS ----------------//
	// These values can be modified to finetune the forces the   //
	// device must encounter for a bump to be registered.        //
	//-----------------------------------------------------------//
	#region Detection parameters
	/// <summary>
	/// The threshold above which the negative acceleration initiating a bump
	/// should lie.
	/// </summary>
	private const float ACCELERATION_THRESHOLD = -0.2f;
	
	/// <summary>
	/// The threshold above which the positive acceleration progressing the bump
	/// should lie.
	/// </summary>
	private const float DECELERATION_THRESHOLD = 0.2f;
	
	/// <summary>
	/// The threshold below which the total acceleration on all three axes 
	/// combined should lie after completing the bump.
	/// </summary>
	private const float STATIONARY_THRESHOLD = 0.08f;
	
	/// <summary>
	/// The maximum allowed timespan between the acceleration initiating a bump
	/// and the deceleration progressing the bump.
	/// </summary>
	private const float ACCELERATE_DELTATIME = 0.1f;
	
	/// <summary>
	/// The maximum allowed timespan between the deceleration progressing a bump
	/// and the phone being held still completing the bump.
	/// </summary>
	private const float DECELERATE_DELTATIME = 0.05f;
	#endregion

	//---------------- BUMP DETECTION VARIABLES -----------------//
	// These values keep track of the bump detection state, such //
	// as forces and timing.                                     //
	//-----------------------------------------------------------//
	#region Detection variables
	/// <summary>
	/// The acceleration along the Z-axis measured during the previous update.
	/// </summary>
	private float previousAcceleration = 0.0f;
	
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
	#endregion

	/// <summary>
	/// Create a new BumpDetector, with the provided accelerometer 
	/// implementation.
	/// </summary>
	public BumpDetector(IAccelerometer accelerometer)
	{
		this.accelerometer = accelerometer;
	}
	
	/// <summary>
	/// Update accelerometer readings.
	/// </summary>
	public void DetectBump()
	{
		if (IsAccelerating())
		{
			StartBump();
		}
		
		else if (hasBumpStarted && IsDecelerating())
		{
			ProgressBump();
		}
		
		else if (hasBumpProgressed && IsStationary())
		{
			CompleteBump();
		}
		
		previousAcceleration = accelerometer.Acceleration.z;
	}

	/// <summary>
	/// Determines whether the phone is accelerating.
	/// </summary>
	/// <returns><c>true</c> if this instance is accelerating; otherwise, 
	///     <c>false</c>.</returns>
	private bool IsAccelerating()
	{
		return accelerometer.Acceleration.z - previousAcceleration 
			< ACCELERATION_THRESHOLD;
	}
	
	/// <summary>
	/// Determines whether the phone is decelerating.
	/// </summary>
	/// <returns><c>true</c> if this instance is decelerating; otherwise, 
	///     <c>false</c>.</returns>
	private bool IsDecelerating()
	{
		return accelerometer.Acceleration.z - previousAcceleration
			> DECELERATION_THRESHOLD;
	}
	
	/// <summary>
	/// Determines whether the phone is stationary (held still on all three 
	/// axes).
	/// </summary>
	/// <returns><c>true</c> if this instance is stationary; otherwise, 
	/// <c>false</c>.</returns>
	private bool IsStationary()
	{
		// Since the phone's accelerometer measures gravitational forces, 
		// 1g should be subtracted from the total acceleration magnitude to get 
		// user acceleration.
		return Mathf.Abs(accelerometer.Acceleration.magnitude - 1)
			< STATIONARY_THRESHOLD;
	}
	
	/// <summary>
	/// Initiate a new bump detection.
	/// </summary>
	private void StartBump()
	{
		bumpStartTime = Time.time;
		hasBumpStarted = true;
	}
	
	/// <summary>
	/// Progress bump detection state.
	/// </summary>
	private void ProgressBump()
	{
		if (Time.time - bumpStartTime <= ACCELERATE_DELTATIME) {
			hasBumpProgressed = true;
			bumpProgressTime = Time.time;
		} else {
			EndBump();
		}
	}
	
	/// <summary>
	/// Complete a bump and reset bump detection state.
	/// </summary>
	private void CompleteBump()
	{
		if (Time.time - bumpProgressTime <= DECELERATE_DELTATIME) {
			bumpEndTime = Time.time;
		}
		EndBump();

		if (OnBump != null) {
			OnBump(new Bump(bumpStartTime, bumpEndTime));
		}
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
	}
}
