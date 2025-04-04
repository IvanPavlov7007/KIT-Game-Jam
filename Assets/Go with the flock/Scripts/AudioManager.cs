using System.Collections;
using UnityEngine;
using Pixelplacement;
using UnityEngine.Audio;
public class AudioManager : Singleton<AudioManager>
{
    public AudioMixerGroup loopGroup;
    public AudioMixerGroup sfxGroup;
    public AudioSource playLoop(AudioClip clip)
    {
        GameObject go = new GameObject("Loop of " + clip.name);
        DontDestroyOnLoad(go);
        var source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = loopGroup;
        source.Play();
        return source;
    }

    public void fadeOut(AudioSource audioSource, float time)
    {
        Tween.Volume(audioSource, 0f, time, 0f);
        Run.After(time + 1f, () => Destroy(audioSource.gameObject));
    }
}