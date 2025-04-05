using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BartenderBehaviour : MonoBehaviour
{
    Animator animator;
    public GameObject player; // connect to player in unity
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // connect to property in unity
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 5)
        {
            // rotate the npc towards the player
            Vector3 target_dir = player.transform.position - transform.position;
            target_dir.y = 0; // stay in x-z plane
            Vector3 temp_dir = Vector3.RotateTowards(transform.forward, target_dir, Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(temp_dir);
            animator.SetInteger("State", 1); // start talking

        }
        else
        {
            animator.SetInteger("State", 0); // stop talking
        }
    }
}
