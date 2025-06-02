using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnPoint;
    public GameObject fade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            // Multi Threads
            StartCoroutine(SceneTransition());
        }
    }

    IEnumerator SceneTransition()
    {
        // Before the scene changes, execute an update to the amount of gold
        PersistentObjectManager.Instance.setGold(CoinBehaviourScript.numCoins);

        Animator animator = fade.GetComponent<Animator>();
        animator.SetBool("startFadeIn", true);

        // Delay for 3 seconds
        yield return new WaitForSeconds(3);

        if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                PersistentObjectManager.Instance.SetSpawnPointName("SpawnPointToScene2");
                SceneManager.LoadScene(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (CoinBehaviourScript.numCoins == 5)
                {           
                    PersistentObjectManager.Instance.SetSpawnPointName("SpawnPointToScene1");
                    SceneManager.LoadScene(0);
                }
            }
    }
}
