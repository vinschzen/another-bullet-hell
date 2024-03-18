using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFXManager : MonoBehaviour
{
    [SerializeField] 
    public AudioClip clickAudio;
    public AudioClip confirmAudio;
    public AudioClip cancelAudio;
    public AudioSource source;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void playClick() 
    {
        source.clip = clickAudio;
        source.Play();
    }

    public void playConfirm()
    {
        source.clip = confirmAudio;
        source.Play();
    }

    public void playCancel() 
    {
        source.clip = cancelAudio;
        source.Play();
    }
}
