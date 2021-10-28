using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioClipIndex : int
{
    SourceA = 1,
    SourceB = 2,
}
public class AudioManager : MonoBehaviour
{
    public AudioClip[] Playlist;
    public AudioSource AudioSourceA;
   // public AudioSource AudioSourceB;
    public AudioMixer AudioMixer;
   
    private static AudioManager instance = null;
    public static AudioManager Instance { get { return instance; } private set {} }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        if (Playlist.Length > 0)
        {
            SetAudio(Playlist[0]);
            PlayAudio();
        }
        

        DontDestroyOnLoad(this);
    }

    public void PlayAudio()
    {
        if (!AudioSourceA.isPlaying)
        {
            AudioSourceA.Play();
        }
    }

    public void StopAudio()
    {
        if (AudioSourceA.isPlaying)
        {
            AudioSourceA.Stop();
        }
    }
    /*public void PlayAudio(AudioClipIndex sIndex = AudioClipIndex.SourceA)
    {
        AudioSource tAudioSource = AudioSourceA;
        if (sIndex == AudioClipIndex.SourceB)
        {
            tAudioSource = AudioSourceB;
        }

        tAudioSource.Play();
        StartCoroutine(StartFade(tAudioSource, 3f, 1, 0));
        Debug.Log("Playing Audio");
    }
    public void StopAudio(AudioClipIndex sIndex = AudioClipIndex.SourceA)
    {
        AudioSource tAudioSource = AudioSourceA;
        if (sIndex == AudioClipIndex.SourceB)
        {
            tAudioSource = AudioSourceB;
        }

        StartCoroutine(StartFade(tAudioSource, 3f, 0, 1));
    }*/
    public void SetAudio(AudioClip sClip, AudioClipIndex sIndex = AudioClipIndex.SourceA)
    {
        AudioSource tAudioSource = AudioSourceA;
       /* if (sIndex == AudioClipIndex.SourceB)
        {
            tAudioSource = AudioSourceB;
        }*/

        tAudioSource.clip = sClip;
    }
    public void SetAudio(int sPlayListIndex, AudioClipIndex sIndex = AudioClipIndex.SourceA)
    {
        if (Playlist.Length > 0 && Playlist.Length - 1 <= sPlayListIndex)
        {
            SetAudio(Playlist[sPlayListIndex], sIndex);
        }
    }
    public static IEnumerator StartFade(AudioSource sAudioSource, float sDuration, float sFinalVolume, float sStartVolume)
    {
        float tCurrentTime = 0;
        sAudioSource.volume = sStartVolume;

        while (tCurrentTime < sDuration)
        {
            tCurrentTime += Time.deltaTime;
            sAudioSource.volume = Mathf.Lerp(sStartVolume, sFinalVolume, tCurrentTime / sDuration);
            if (sAudioSource.volume == 0f)
            {
                sAudioSource.Stop();
            }
            yield return null;
        }
    }
    public void SetVolume(float sliderValue)
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Volume", sliderValue);
    }
}

