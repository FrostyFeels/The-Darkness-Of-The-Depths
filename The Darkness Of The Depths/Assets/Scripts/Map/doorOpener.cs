using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class doorOpener : MonoBehaviour
{

    public Animator transition;
    public CanvasGroup transitionImage;
    public float transitionTime;

    public string door;
    public int lastlevel;
    public void Start()
    {
        transitionImage.alpha = 1;
        door = gameObject.name.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        StaticManager.lastLevel = lastlevel;
        SceneManager.LoadScene(door);
        
    }
}
