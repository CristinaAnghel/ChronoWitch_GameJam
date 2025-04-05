using UnityEngine;

public class BGMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip musicClip;
    void Start()
    {
        audioSource.PlayOneShot(musicClip);
    }

}
