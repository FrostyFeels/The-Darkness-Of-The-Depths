using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiSounds : MonoBehaviour
{
    public AudioClip[] Shot;
    public AudioClip[] Reload;
    public AudioClip[] DoneReload;
    public AudioClip[] Empty;

    public void Start()
    {
        AudioManager.sShot = Shot;
        AudioManager.sReload = Reload;
        AudioManager.sEmpty = Empty;
        AudioManager.sDoneReload = DoneReload;
    }
}
