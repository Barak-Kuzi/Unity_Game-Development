using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

// Based on Singleton
public class PersistentObjectManager : MonoBehaviour
{
    public static PersistentObjectManager Instance = null;
    private static int gold = 0;
    private static bool hasCoin = true;
    public Text coinsText;

    // Coins
    public CoinBehaviourScript Coin_1;
    public CoinBehaviourScript Coin_2;
    public CoinBehaviourScript Coin_3;
    public CoinBehaviourScript Coin_4;
    public CoinBehaviourScript Coin_5;

    private Vector3 SpawnPoint;
    public GameObject player;
    private static bool shouldTeleport = false;
    private static bool hasGunInHands = true;
    private static bool hasPistolOnFloor = true;
    private static bool hasM4OnFloor = true;

    public GameObject pistol;
    public Text pistolText;
    public GunPickUp PistolExsists;
    public GameObject m4;
    public Text m4Text;
    public GunPickUp M4Exsists;
    public Text portalKeyText;

    private static string spawnPointName;
    public GunPickUp PistolIsOnTheFloor;

    public GunPickUp M4IsOnTheFloor;
    public GameObject portalGate;
    public GameObject GoldChest;
    public GameObject PortalKeyBoxes;

    // Runs before state method
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                player.transform.position = SpawnPoint;
                player.transform.Rotate(new Vector3(0, 90, 0));
            }
        }


        SetTexts();
        SetCoins();
        SetGuns();
        SetPortalGate();
        SetStateGoldChest();
        DontDestroyOnLoad(gameObject);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!shouldTeleport) return;

        player = GameObject.FindWithTag("Player");
        if (player != null && !string.IsNullOrEmpty(spawnPointName))
        {
            GameObject targetSpawn = GameObject.Find(spawnPointName);
            if (targetSpawn != null)
            {
                player.transform.position = targetSpawn.transform.position;

                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            else
            {
                Debug.LogWarning("SpawnPoint with name " + spawnPointName + " not found in scene " + scene.name);
            }
        }

        shouldTeleport = false;
    }


    public void setGold(int num)
    {
        gold = num;
    }

    public void setHasCoin(bool value)
    {
        hasCoin = value;
    }

    public void setSpawnPoint(Vector3 sp)
    {
        SpawnPoint = sp;
    }

    public void SetSpawnPointName(string name)
    {
        spawnPointName = name;
        shouldTeleport = true;
    }


    public void SetShouldTeleport(bool value)
    {
        shouldTeleport = value;
    }

    public void SetHasGunInHands(bool value)
    {
        hasGunInHands = value;
    }

    public void SetPistolOnFloor(bool value)
    {
        hasPistolOnFloor = value;
    }

    public void SetM4OnFloor(bool value)
    {
        hasM4OnFloor = value;
    }

    private void SetTexts()
    {
        coinsText.text = "Coins: " + gold;
        pistolText.text = "Pistol: " + GunPickUp.pistolPickUped + " / 1";
        m4Text.text = "M4: " + GunPickUp.m4PickUped + " / 1";
        if (DrawerBehaviourScript.hasCollectedKey)
        {
            portalKeyText.text = "Portal Key: 1 / 1";
        }
        else
        {
            portalKeyText.text = "Portal Key: 0 / 1";
        }
    }

    private void SetCoins()
    {
        Coin_1.isExists = hasCoin;
        Coin_2.isExists = hasCoin;
        Coin_3.isExists = hasCoin;
        Coin_4.isExists = hasCoin;
        Coin_5.isExists = hasCoin;
    }

    private void SetGuns()
    {
        PistolExsists.pistolIsExists = hasGunInHands;
        M4Exsists.m4IsExists = hasGunInHands;

        if (GunPickUp.currentWeaponName == "Pistol")
        {
            pistol.SetActive(hasGunInHands);
        }
        else if (GunPickUp.currentWeaponName == "M4")
        {
            m4.SetActive(hasGunInHands);
        }

        PistolIsOnTheFloor.hasPistolOnFloor = hasPistolOnFloor;
        M4IsOnTheFloor.hasM4OnFloor = hasM4OnFloor;
    }

    private void SetPortalGate()
    {
        if (DrawerBehaviourScript.hasCollectedKey && SceneManager.GetActiveScene().buildIndex == 1)
        {
            portalGate.SetActive(true);
        }
        else
        {
            portalGate.SetActive(false);
        }
    }

    private void SetStateGoldChest()
    {
        Animator animator = GoldChest.GetComponent<Animator>();
        if (DrawerBehaviourScript.hasCollectedKey)
        {
            animator.SetBool("OpenChest", true);
            PortalKeyBoxes.SetActive(false);
        }
        
    }
}
