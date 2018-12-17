using UnityEngine;


/// <summary>
/// Causes the attached GameObject to die and respawn when it hits things
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class DieOnCollision : MonoBehaviour
{
    /// <summary>
    /// True if player gets a point when this is hit with a missile.
    /// </summary>
    public bool IsPlayerTarget;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    /// <summary>
    /// Called each time the attached GameObject's Collider(s) initiate contact with another Collider
    /// </summary>
    /// <param name="obstacle">Information about what was collided with</param>
    internal void OnCollisionEnter2D(Collision2D obstacle)
    {
        audioSource = GetComponent<AudioSource>();
        if (IsPlayerTarget && obstacle.collider.tag == "Missile"){
            FindObjectOfType<ScoreKeeper>().ScoreKill();
            audioSource.PlayOneShot(audioClip[1]);
        }

        if (!Respawner.TryRespawn(gameObject))
        {
            Destroy(gameObject);
        }
        else {
            audioSource.PlayOneShot(audioClip[0]);
        }



    }
}
