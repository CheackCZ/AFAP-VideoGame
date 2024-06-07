using UnityEngine;

/// <summary>
/// This class is responsible for playing background music in the game.
/// It requires an AudioSource component to be attached to the same GameObject.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// It initializes the AudioSource component and starts playing the background music.
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}