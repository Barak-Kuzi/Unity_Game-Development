using UnityEngine;
using UnityEngine.UI;

public class CoinBehaviourScript : MonoBehaviour
{
    public GameObject coins;
    public static int numCoins = 0;
    public Text coinText;
    public GameObject player;
    public bool isExists = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!isExists)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            numCoins++;
            coinText.text = "Coins: " + numCoins;
            isExists = false;
            PersistentObjectManager.Instance.setHasCoin(false);
            gameObject.SetActive(false);
            AudioSource sound = coins.GetComponent<AudioSource>();
            sound.Play(); 
        }
    }
}
