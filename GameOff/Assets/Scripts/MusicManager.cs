using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip battleTheme;
    public AudioClip waitForBattleTheme;
    public AudioClip[] birdsSFXs;
    public AudioClip HitSFX;

    private AudioSource _audioSource;

    public static MusicManager instance { get; private set; }

    private DoubleAudioSource _doubleAudioSource;
    public AudioSource SFXAudioSource;

    public AudioClip HornSFX;
    public AudioClip BuySFX;
    public AudioClip CantBuySFX;

    void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
        _doubleAudioSource = GetComponent<DoubleAudioSource>();
    }

    public void PlayBattleTheme()
    {
        if (!_audioSource.clip || _audioSource.clip != battleTheme)
        {
            _doubleAudioSource.CrossFade(battleTheme, 0.3f, 7f);
            SFXAudioSource.PlayOneShot(HornSFX);
        }
    }
    public void PlayWaitForBattleTheme()
    {
        // _audioSource.clip = waitForBattleTheme;
        _doubleAudioSource.CrossFade(waitForBattleTheme, 0.4f, 4f);
        // _audioSource.Play();
    }

    public void Buy()
    {
        SFXAudioSource.PlayOneShot(BuySFX);
    }

    public void CantBuy()
    {
        SFXAudioSource.PlayOneShot(CantBuySFX);
    }


}
