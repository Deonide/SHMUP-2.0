using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Source ---------")]
    [SerializeField]
    AudioSource m_musicSource;
    [SerializeField]
    AudioSource m_SFXSource;

    [Header("----------- Audio Clip ---------")]
    public AudioClip m_background;
    [CanBeNull]
    public AudioClip m_shoot;

    private void Start()
    {
        //Speelt de audio background af.
        m_musicSource.clip = m_background;
        m_musicSource.Play();
    }

    //Op het moment dat de code aangeroepen word speelt die de aangeroepen audio af.
    public void PlaySFX(AudioClip clip)
    {
        m_SFXSource.PlayOneShot(clip);
    }
}