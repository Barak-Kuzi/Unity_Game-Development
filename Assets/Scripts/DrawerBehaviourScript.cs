using UnityEngine;
using UnityEngine.UI;


public class DrawerBehaviourScript : MonoBehaviour
{
    public GameObject PlayerEye;
    public GameObject CrossHair;
    public GameObject CrossHairTouch;
    public Text openText;
    public Text closeText;
    public Text pickUpText;
    private bool isChestOpened = false;
    public GameObject keyObject;
    public static bool hasCollectedKey = false;
    public Text portalKeyText;
    public GameObject PortalGate;
    public GameObject PortalKeyBoxes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(PlayerEye.transform.position, PlayerEye.transform.forward, out hit))
        {
            GameObject target = hit.transform.gameObject;
            bool showInteraction = ShouldShowInteraction(target);
            bool canActivate = CanActivateByTag(target.tag);


            if (showInteraction && canActivate && !isChestOpened && !hasCollectedKey)
            {
                CrossHair.gameObject.SetActive(false);
                CrossHairTouch.gameObject.SetActive(true);
                openText.gameObject.SetActive(true);
                HandleKeyPress(target);
            }
            // Handle pick up Key
            else if (isChestOpened && target.CompareTag("Key") && !hasCollectedKey)
            {
                CrossHair.gameObject.SetActive(false);
                CrossHairTouch.gameObject.SetActive(true);
                openText.gameObject.SetActive(false);
                pickUpText.gameObject.SetActive(true);

                if (keyObject != null && Input.GetKeyDown(KeyCode.P))
                {
                    AudioSource audio = PortalKeyBoxes.GetComponent<AudioSource>();
                    audio.Play();
                    keyObject.SetActive(false);
                    hasCollectedKey = true;
                    PortalGate.SetActive(true);
                    pickUpText.gameObject.SetActive(false);
                    portalKeyText.text = "Portal Key: 1 / 1";
                    Debug.Log("Key collected!");
                }
            }
            else
            {
                CrossHair.gameObject.SetActive(true);
                CrossHairTouch.gameObject.SetActive(false);
                openText.gameObject.SetActive(false);
                pickUpText.gameObject.SetActive(false);
            }
        } 
    }

    private bool ShouldShowInteraction(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Chest":
                return true;

            case "Untagged":
            default:
                return false;
        }
    }

    private bool CanActivateByTag(string tag)
    {
        switch (tag)
        {
            case "Chest":
                return CoinBehaviourScript.numCoins >= 5;
            default:
                return false;
        }
    }

    private void HandleKeyPress(GameObject target)
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Animator animator = target.GetComponent<Animator>();
            AudioSource audio = target.GetComponent<AudioSource>();

            if (animator != null)
            {
                switch (target.tag)
                {
                    case "Chest":
                        animator.SetBool("OpenChest", true);
                        Collider chestCollider = target.GetComponent<Collider>();
                        if (chestCollider != null)
                            chestCollider.enabled = false;
                        isChestOpened = true;
                        if (audio != null && !audio.isPlaying)
                            audio.PlayDelayed(0.6f);
                        break;

                    default:
                        Debug.Log("No animation configured for tag: " + target.tag);
                        break;
                }
            }
            else
            {
                Debug.LogWarning("Animator missing on object with tag: " + target.tag);
            }
        }
    }
}
