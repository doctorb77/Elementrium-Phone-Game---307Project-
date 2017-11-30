using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get { return instance; } }
    private static SoundManager instance;

	public AudioClip soundFX1;
    public AudioClip soundFX2;
	public AudioClip soundFX3;
	public AudioClip soundFX4;
	public AudioClip soundFX5;
	public AudioClip soundFX6;
	public AudioClip soundFX7;
	public AudioClip soundFX8;
    public AudioClip soundFX9;
    public AudioClip soundFX10;
    public AudioClip soundFX11;

    public AudioSource musicsrc;
    public AudioSource soundsrc;
    public AudioSource soundsrc2;

    private bool musicIsPlaying;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
        if (TopMenu1.Instance.musicIsOn)
        {
            musicsrc.Play();
            musicIsPlaying = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
		if (!TopMenu1.Instance.musicIsOn)//null ref
		{
            musicsrc.Pause();
            musicIsPlaying = false;
		}
        if (TopMenu1.Instance.musicIsOn) {
            if (!musicIsPlaying) {
				musicsrc.Play();
				musicIsPlaying = true;
            }
        }
	}

    public void PlaySoundOne () {
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX1;
            soundsrc.Play();
        }
    }
	public void PlaySoundTwo()
	{
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX2;
            soundsrc.Play();
        }
	}
	public void PlaySoundThree()
	{
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX3;
            soundsrc.Play();
        }
	}
	public void PlaySoundFour()
	{
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX4;
            soundsrc.Play();
        }
	}
	public void PlaySoundFive()
	{
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX5;
            soundsrc.Play();
        }
	}
	public void PlaySoundSix()
	{
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX6;
            soundsrc.Play();
        }
	}
	public void PlaySoundSeven()
	{
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX7;
            soundsrc.Play();
        }
	}
	public void PlaySoundEight()
	{
        if (TopMenu1.Instance.soundFxIsOn)
        {
            soundsrc.clip = soundFX8;
            soundsrc.Play();
        }
	}
	public void PlaySoundNine()
	{
		if (TopMenu1.Instance.soundFxIsOn)
		{
			soundsrc.clip = soundFX9;
			soundsrc.Play();
		}
	}
	public void PlaySoundTen()
	{
		if (TopMenu1.Instance.soundFxIsOn)
		{
			soundsrc.clip = soundFX10;
			soundsrc.Play();
		}
	}
	public void PlaySoundEleven()
	{
		if (TopMenu1.Instance.soundFxIsOn)
		{
			soundsrc.clip = soundFX11;
			soundsrc.Play();
		}
	}
    public void PlayCombined()
    {
		if (TopMenu1.Instance.soundFxIsOn)
		{
            float pitch = Random.Range(1f, 1.1f);
			soundsrc.clip = soundFX10;
            soundsrc2.clip = soundFX9;
            soundsrc.pitch = pitch;
            soundsrc2.pitch = pitch;
			soundsrc.Play();
            soundsrc2.Play();
		}
    }
}
