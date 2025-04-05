using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBehaviour : MonoBehaviour
{
    public GameObject keys;
    public static int numKeys = 0;
    public Text keysText;
    public GameObject player;
    public bool exists = true;
    // Start is called before the first frame update
    void Start()
    {
        if(!exists)
            gameObject.SetActive(false); // checks if key is visible.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            numKeys++;
            keysText.text = "Keys: " + numKeys;
            exists = false;
            //PersistentObjectManager.Instance.setHasCoin(false);
            gameObject.SetActive(false);
            AudioSource sound = keys.GetComponent<AudioSource>();
            sound.Play();
        }
    }

    public static int getNumOfKeys(){
        return numKeys;
    }

    public static void setNumOfKeys(int newNum){
        numKeys = newNum;
    }
}
