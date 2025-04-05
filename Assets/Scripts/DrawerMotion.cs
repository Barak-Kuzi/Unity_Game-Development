using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerMotion : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public Text popText;
    AudioSource sound;
    bool inCollider = false;
    public bool hasKey;
    public GameObject keys;
    public Text keysText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            inCollider = true;
            StartCoroutine(WaitForPlayerInput());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            inCollider = false;
            popText.enabled = false;
        }
    }

    private IEnumerator WaitForPlayerInput()
    {
        while (inCollider)
        {
            if (!animator.GetBool("isOpen"))
            {
                popText.text = "Press 'O' to open the Drawer";
                popText.enabled = true;

                if (Input.GetKeyDown(KeyCode.O))
                {
                    animator.SetBool("isOpen", true);
                    if(hasKey)
                    {
                        popText.text = "A key has found!";
                        KeyBehaviour.setNumOfKeys(KeyBehaviour.getNumOfKeys() + 1);
                        sound = keys.GetComponent<AudioSource>();
                        keysText.text = "Keys: " + KeyBehaviour.getNumOfKeys();
                        hasKey = false;
                    }
                    sound.PlayDelayed(0.5f);
                    break;
                }
            }
            else
            {
                popText.text = "Press 'C' to close the Drawer";
                popText.enabled = true;

                if (Input.GetKeyDown(KeyCode.C))
                {
                    animator.SetBool("isOpen", false);
                    sound.PlayDelayed(0.5f);
                    break;
                }
            }

            yield return null;
        }

        popText.enabled = false;
}
}
