using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotion : MonoBehaviour
{
    Animator animator;
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //connect to Unity component
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("OpenState", true);
        // sound.PlayDelayed(0.8f);
        sound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("OpenState", false);
        sound.PlayDelayed(0.7f);
    }
}
