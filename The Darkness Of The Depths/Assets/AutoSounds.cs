using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSounds : MonoBehaviour
{
    public AudioClip[] Shot;
    public AudioClip[] Reload;
    public AudioClip[] DoneReload;
    public AudioClip[] Empty;

    public void Start()
    {
        AudioManager.aShot = Shot;
        AudioManager.aReload = Reload;
        AudioManager.aEmpty = Empty;
        AudioManager.aDoneReload = DoneReload;
    }
}
