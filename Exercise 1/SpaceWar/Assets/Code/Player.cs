using UnityEngine;

/// <summary>
/// Adds keyboard steering and shooting to the attached GameObject
/// </summary>
[RequireComponent(typeof(MissileLauncher), typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class Player : Ship
{
    /// <summary>
    /// How fast we accelerate
    /// </summary>
    public float ThrustStrength = 0.01f;
    /// <summary>
    /// How fast we turn
    /// </summary>
    public float Torque = 0.01f;
    /// <summary>
    /// Add audioclip
    /// </summary>
    public AudioClip[] audioClip;
    public AudioSource audioSource;



    /// <summary>
    /// Apply forces and torques based on keyboard input
    /// Called once per physics update
    /// </summary>
	internal void FixedUpdate () {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (Input.GetKey(KeyCode.UpArrow))
            rigidBody.AddRelativeForce(new Vector2(ThrustStrength, 0));
        if (Input.GetKey(KeyCode.DownArrow))
            rigidBody.AddRelativeForce(new Vector2(-ThrustStrength, 0));
        if (Input.GetKey(KeyCode.LeftArrow))
            rigidBody.AddTorque(Torque);
        else if (Input.GetKey(KeyCode.RightArrow))
            rigidBody.AddTorque(-Torque);
    }

    /// <summary>
    /// Fire when told to
    /// Also, check if we need to wrap around the screen
    /// Called once per frame.
    /// </summary>
    internal void Update()
    {
        // Fire missile if necessary
        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<MissileLauncher>().FireMissile();
            // add Fire noise
            audioSource.PlayOneShot(audioClip[0]);
        }
            

        // Check for screen wrap
        var mainCamera = Camera.main;
        if (mainCamera)
        {
            var worldPosition = transform.position;
            var screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
            var screenMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            var screenMin = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));

            if (screenPosition.x < -5)
            {
                worldPosition.x = screenMax.x;
                rigidBody.MovePosition(worldPosition);
            }

            if (screenPosition.x > Screen.width+5)
            {
                worldPosition.x = screenMin.x;
                rigidBody.MovePosition(worldPosition);
            }

            if (screenPosition.y < -5)
            {
                worldPosition.y = screenMax.y;
                rigidBody.MovePosition(worldPosition);
            }

            if (screenPosition.y > Screen.height+5)
            {
                worldPosition.y = screenMin.y;
                rigidBody.MovePosition(worldPosition);
            }
        }
    }
}
