using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunPickUp : MonoBehaviour
{
    public GameObject player;
    public static string currentWeaponName = "";

    // Pistol
    public GameObject PistolBoxes;
    public GameObject PistolOnTheFloor;
    public GameObject PistolInPlayerHands;
    public static int pistolPickUped = 0;
    public bool pistolIsExists = true;
    public bool hasPistolOnFloor = true;
    public Text pistolText;

    // M4
    public GameObject M4Boxes;
    public GameObject M4OnTheFloor;
    public GameObject M4InPlayerHands;
    public static int m4PickUped = 0;
    public bool m4IsExists = true;
    public bool hasM4OnFloor = true;
    public Text m4Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!hasPistolOnFloor)
        {
            PistolOnTheFloor.SetActive(false);
        }

        if (!hasM4OnFloor)
        {
            M4OnTheFloor.SetActive(false);
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
            if (gameObject.CompareTag("Pistol"))
            {
                AudioSource sound = PistolBoxes.GetComponent<AudioSource>();
                sound.Play();
                gameObject.SetActive(false);
                PistolOnTheFloor.SetActive(false);
                SwitchWeapon("Pistol");
                pistolPickUped = 1;
                pistolText.text = "Pistol: " + pistolPickUped + " / 1";
                PersistentObjectManager.Instance.SetHasGunInHands(true);
                pistolIsExists = false;

                hasPistolOnFloor = false;
                PersistentObjectManager.Instance.SetPistolOnFloor(false);
            }
            else if (gameObject.CompareTag("M4"))
            {
                AudioSource sound = M4Boxes.GetComponent<AudioSource>();
                sound.Play();
                gameObject.SetActive(false);
                M4OnTheFloor.SetActive(false);
                SwitchWeapon("M4");
                m4PickUped = 1;
                m4Text.text = "M4: " + m4PickUped + " / 1";
                PersistentObjectManager.Instance.SetHasGunInHands(true);
                m4IsExists = false;

                hasM4OnFloor = false;
                PersistentObjectManager.Instance.SetM4OnFloor(false);
            }
        }
    }

    private void SwitchWeapon(string newWeaponName)
    {
        // Turn off the current weapon in player hands
        switch (currentWeaponName)
        {
            case "Pistol":
                Debug.Log("Switching from Pistol to " + newWeaponName);
                PistolInPlayerHands.SetActive(false);
                break;
            case "M4":
                Debug.Log("Switching from M4 to " + newWeaponName);
                M4InPlayerHands.SetActive(false);
                break;
        }

        // Turn on the new weapon in player hands
        switch (newWeaponName)
        {
            case "Pistol":
                PistolInPlayerHands.SetActive(true);
                break;
            case "M4":
                M4InPlayerHands.SetActive(true);
                break;
        }

        // Update the current weapon name
        currentWeaponName = newWeaponName;
    }
}
