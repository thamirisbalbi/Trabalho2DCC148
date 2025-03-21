using UnityEngine;

public class AudioControllerGame : MonoBehaviour
{
     [SerializeField] AudioSource SFXSource;
    public AudioClip hit;
    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
