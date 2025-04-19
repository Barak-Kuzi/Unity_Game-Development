using UnityEngine;

public class DoubleDoorMotion : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //connect to Unity component
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen_Obj_1", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpen_Obj_1", false);
    }
}
