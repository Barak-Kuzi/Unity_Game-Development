using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject playerCamera; // must be connected to a camera in Unity
    CharacterController controller;
    float speed = 0.05f;
    float angularSpeed = 3.0f;
    AudioSource footStepSnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        footStepSnd = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx, dz;
        // Input.GetAxis("Mouse X") is the deviation in pixels from the center to x direction
        float RotationAboutY = angularSpeed* Input.GetAxis("Mouse X");
        float RotationAboutX = -angularSpeed * Input.GetAxis("Mouse Y");

        playerCamera.transform.Rotate(new Vector3(RotationAboutX,0,0));

        transform.Rotate(new Vector3(0, RotationAboutY, 0));

        dx = speed*Input.GetAxis("Horizontal"); // can be -1, 0 or 1
        dz = speed*Input.GetAxis("Vertical"); // can be -1, 0 or 1
        // simple but not adaptive (stupid) motion
        // transform.Translate(0, 0, 0.1f);

        // adaptive motion
        Vector3 motion = new Vector3(dx, -1, dz);
        // trsforms motion to local coordinates
        motion = transform.TransformDirection(motion);
        controller.Move(motion);

        if(dx != 0 || dz != 0)
        {
            if(!footStepSnd.isPlaying)
            {
                footStepSnd.Play();
            }
        }
    }
}
