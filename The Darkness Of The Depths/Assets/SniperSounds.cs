using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSounds : MonoBehaviour
{
    public AudioClip[] Shot;
    public AudioClip[] Reload;
    public AudioClip[] Empty;

    public void Start()
    {
        AudioManager.sssShot = Shot;
        AudioManager.sssReload = Reload;
        AudioManager.sssEmpty = Empty;
    }
}
