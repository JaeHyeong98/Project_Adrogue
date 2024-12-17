using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("# BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmHighPassFilter;

    [Header("# SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIdx;

    public enum SFX 
    { 
        Dead,
        Hit,
        LevelUp = 3,
        Lose,
        Melee,
        Range = 7,
        Select,
        Win
    }

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmHighPassFilter = Camera.main.GetComponent<AudioHighPassFilter>();

        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < channels; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].bypassListenerEffects = true;
        }
    }

    public void PlayBGM(bool isPlay)
    {
        if (isPlay)
            bgmPlayer.Play();
        else 
            bgmPlayer.Stop();
    }

    public void EffBGM(bool isPlay)
    {
        bgmHighPassFilter.enabled = isPlay;
    }

    public void PlaySFX(SFX sfx)
    {
        for (int i = 0; i < channels; i++)
        {
            int loopIdx = (i + channelIdx) % channels;

            if (sfxPlayers[loopIdx].isPlaying)
                continue;

            int ranIdx = 0;
            if(sfx == SFX.Hit || sfx == SFX.Melee)
            {
                ranIdx = Random.Range(0, 2);
            }

            channelIdx = loopIdx;
            sfxPlayers[loopIdx].clip = sfxClips[(int)sfx + ranIdx];
            sfxPlayers[loopIdx].Play();
            break;
        }
    }
}
