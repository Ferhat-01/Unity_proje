using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource ambientSource;
    public AudioSource sfxSource;
    public AudioSource loopSource;
    public AudioSource heartbeatSource;

    private AudioClip dingClip;
    private AudioClip buzzerClip;
    private AudioClip successClip;

    void Awake()
    {
        Instance = this;
        GenerateClips();
        
        if (ambientSource != null)
        {
            ambientSource.loop = true;
            ambientSource.clip = CreateTone(440, 1f, 0.05f);
            ambientSource.Play();
        }
    }

    void GenerateClips()
    {
        dingClip = CreateTone(880, 0.5f, 0.5f);
        buzzerClip = CreateTone(220, 0.5f, 0.5f);
        successClip = CreateTone(523.25f, 2f, 0.5f);
    }

    public void PlayDing() { if (sfxSource != null) sfxSource.PlayOneShot(dingClip); }
    public void PlayBuzzer() { if (sfxSource != null) sfxSource.PlayOneShot(buzzerClip); }
    public void PlaySuccess() { if (sfxSource != null) sfxSource.PlayOneShot(successClip); }
    
    public void PlayHeartbeat()
    {
        if (heartbeatSource != null && !heartbeatSource.isPlaying)
        {
            heartbeatSource.clip = CreateTone(100, 0.2f, 0.8f);
            heartbeatSource.loop = true;
            heartbeatSource.Play();
        }
    }

    public void PlayEarthquake()
    {
        if (loopSource != null) { loopSource.clip = CreateNoise(2f); loopSource.loop = true; loopSource.Play(); }
    }

    public void PlayWater()
    {
        if (loopSource != null) { loopSource.clip = CreateNoise(1f); loopSource.loop = true; loopSource.Play(); }
    }

    public void PlayFire()
    {
        if (loopSource != null) { loopSource.clip = CreateNoise(0.5f); loopSource.loop = true; loopSource.Play(); }
    }

    public void StopLoop() { if (loopSource != null) loopSource.Stop(); }

    AudioClip CreateTone(float frequency, float duration, float volume)
    {
        int sampleRate = 44100;
        int sampleCount = (int)(sampleRate * duration);
        float[] samples = new float[sampleCount];
        for (int i = 0; i < sampleCount; i++) samples[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate) * volume;
        AudioClip clip = AudioClip.Create("Tone", sampleCount, 1, sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }

    AudioClip CreateNoise(float duration)
    {
        int sampleRate = 44100;
        int sampleCount = (int)(sampleRate * duration);
        float[] samples = new float[sampleCount];
        for (int i = 0; i < sampleCount; i++) samples[i] = Random.Range(-0.5f, 0.5f);
        AudioClip clip = AudioClip.Create("Noise", sampleCount, 1, sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }
}
