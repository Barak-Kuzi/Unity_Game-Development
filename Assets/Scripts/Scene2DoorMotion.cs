using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene2DoorMotion : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public Text popText;
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
        if (other.gameObject == player.gameObject)
        {
            if(!(animator.GetBool("isOpen")))
            {
                int x = KeyBehaviour.getNumOfKeys();
                int doorNumber;
                try
                {
                    doorNumber = int.Parse(gameObject.name.Substring(4));
                }
                catch (System.Exception ex)
                {               
                    Debug.LogError("Failed to parse door number: " + ex.Message);
                    doorNumber = -1; // Assign a default value or handle the error case as needed
                }
                if (x == doorNumber){
                    animator.SetBool("isOpen", true);
                    sound.Play();
                }
                else
                {
                    popText.text = "The amount of keys needed to open this door is: " + doorNumber;
                    popText.enabled = true; 
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            popText.enabled = false;
        }
        
    }

}
