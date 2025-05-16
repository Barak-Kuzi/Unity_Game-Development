using UnityEngine;

public class SmartDoor : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public AudioSource sound;
    public float openDistance = 2f;             // מרחק לפתיחה
    public float closeDistance = 3f;            // מרחק לסגירה אוטומטית
    public float soundDelay = 0.6f;
    public float actionCooldown = 0.25f;
    public KeyCode openKey = KeyCode.E;
    public float raycastDistance = 2f;          // המרחק של ה-Raycast מהשחקן

    private bool isOpen = false;
    private float lastActionTime = -999f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (sound == null)
            sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // שחקן קרוב מספיק
        bool inRange = distance < openDistance;

        // יצירת Raycast מהראש של השחקן (או מאובייקט אחר)
        RaycastHit hit;
        Vector3 raycastOrigin = player.position + Vector3.up * 1.5f; // הראש של השחקן
        Vector3 forwardDirection = player.forward;
        bool hitDoor = Physics.Raycast(raycastOrigin, forwardDirection, out hit, raycastDistance);

        // אם ה-Raycast פוגע בדלת
        bool lookingAtDoor = hitDoor && hit.collider.gameObject == gameObject;

        if (inRange && lookingAtDoor && !isOpen && Time.time - lastActionTime > actionCooldown)
        {
            if (Input.GetKeyDown(openKey))
            {
                OpenDoor();
            }
        }

        // סגירה אוטומטית אם התרחק
        if (isOpen && distance > closeDistance && Time.time - lastActionTime > actionCooldown)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        lastActionTime = Time.time;

        if (animator != null)
            animator.SetBool("OpenState", true);

        if (sound != null)
            sound.PlayDelayed(soundDelay);
    }

    void CloseDoor()
    {
        isOpen = false;
        lastActionTime = Time.time;

        if (animator != null)
            animator.SetBool("OpenState", false);

        if (sound != null)
            sound.PlayDelayed(soundDelay);
    }
}
