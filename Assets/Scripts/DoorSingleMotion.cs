using UnityEngine;

public class DoorSingleMotion : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false; // מעקב אחרי מצב הדלת

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("OpenState", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            animator.SetBool("OpenState", true);
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            animator.SetBool("OpenState", false);
            isOpen = false;
        }
    }
}
