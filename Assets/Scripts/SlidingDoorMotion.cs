using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorMotion : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //connect to Unity component
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("OpenState", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("OpenState", false);
    }
}
