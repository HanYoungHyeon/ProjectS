using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxControll : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip audioClip;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
