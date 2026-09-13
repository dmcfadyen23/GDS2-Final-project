using UnityEngine;

public class AttackAudioManager : MonoBehaviour
{
    public static AttackAudioManager Instance;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
            return;

        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource");
            return;
        }

        audioSource.PlayOneShot(clip);
    }
}
