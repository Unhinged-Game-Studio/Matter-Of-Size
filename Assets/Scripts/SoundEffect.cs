using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    public AudioClip footStep;
    private float minPitch = 0.5f;
    public float maxPitch = 1.5f;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.pitch = Random.Range(minPitch, maxPitch);
        SFXSource.PlayOneShot(clip);
    }
}
