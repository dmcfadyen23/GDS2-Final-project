using UnityEngine;
using System.Collections;

public class BossFightMusic : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip introTrack;
    [SerializeField] private AudioClip loopTrack;

    private void Start()
    {
        StartCoroutine(PlayBossMusic());
    }

    private IEnumerator PlayBossMusic()
    {
        if (musicSource == null)
        {
            Debug.LogError("BossFightMusic: AudioSource is not assigned!");
            yield break;
        }

        if (introTrack == null || loopTrack == null)
        {
            Debug.LogError("BossFightMusic: Assign both the intro and loop tracks!");
            yield break;
        }

        musicSource.Stop();
        musicSource.clip = introTrack;
        musicSource.loop = false;
        musicSource.Play();

        yield return new WaitWhile(() => musicSource.isPlaying);

        musicSource.clip = loopTrack;
        musicSource.loop = true;
        musicSource.Play();
    }
}
