using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolSounds : MonoBehaviour
{
    public AudioClip[] Shot;
    public AudioClip[] Reload;
    public AudioClip[] DoneReload;
    public AudioClip[] Empty;

    public AudioClip[] moving;

    public void Start()
    {
        AudioManager.pShot = Shot;
        AudioManager.pReload = Reload;
        AudioManager.pEmpty = Empty;
        AudioManager.pDoneReload = DoneReload;
        AudioManager.movement = moving;
    }
}
