using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] pShot;
    public AudioClip[] pReload;
    public AudioClip[] pDoneReload;
    public AudioClip[] pEmpty;

    public AudioClip[] aShot;
    public AudioClip[] aReload;
    public AudioClip[] aDoneReload;
    public AudioClip[] aEmpty;

    public AudioClip[] sShot;
    public AudioClip[] sReload;
    public AudioClip[] sDoneReload;
    public AudioClip[] sEmpty;

    public AudioClip[] shShot;
    public AudioClip[] shReload;
    public AudioClip[] shEmpty;

    public AudioClip[] snShot;
    public AudioClip[] snReload;
    public AudioClip[] snEmpty;

    public AudioClip[] moving;

    public AudioClip backGround;

    public AudioClip impactBody;
    public AudioClip impactWall;

    private AudioSource audioCenter;
    public void Awake()
    {
        audioCenter = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audioCenter.loop = true;
        audioCenter.Play();

        AudioManager.enemyHit = impactBody;
        AudioManager.wallHit = impactWall;

        AudioManager.pShot = pShot;
        AudioManager.pReload = pReload;
        AudioManager.pEmpty = pEmpty;
        AudioManager.pDoneReload = pDoneReload;

        AudioManager.aShot = aShot;
        AudioManager.aReload = aReload;
        AudioManager.aEmpty = aEmpty;
        AudioManager.aDoneReload = aDoneReload;

        AudioManager.sShot = sShot;
        AudioManager.sReload = sReload;
        AudioManager.sEmpty = sEmpty;
        AudioManager.sDoneReload = sDoneReload;

        AudioManager.shShot = shShot;
        AudioManager.shReload = shReload;
        AudioManager.shEmpty = shEmpty;

        AudioManager.snShot = snShot;
        AudioManager.snReload = snReload;
        AudioManager.snEmpty = snEmpty;

        AudioManager.movement = moving;
    }
}
