using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");


        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
            Debug.Log("Start Walking");
        }
        if(isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
            Debug.Log("Stop Walking");

        }
        if(!isRunning && (runPressed && forwardPressed))
        {
            animator.SetBool(isRunningHash, true);
            Debug.Log("Start Running");
        }
        if (isRunning && (!runPressed || !forwardPressed))
        {
            animator.SetBool(isRunningHash, false);
            Debug.Log("Stop Running");
        }
    }
}
