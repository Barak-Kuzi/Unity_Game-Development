using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (player.gameObject == other.gameObject)
        {
            StartCoroutine(SceneTransition());
        }

    }

    IEnumerator SceneTransition()
    {
        //before scene change
        Animator a = fade.GetComponent<Animator>();
        a.SetBool("startFadeIn", true);
        yield return new WaitForSeconds(2);

        //start scene transition
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
