using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControllerBlendTree : MonoBehaviour
{

    Animator animator;

    private float velocity = 0.0f;
    public float acceleration = 0.1f;
    int velocityHash;

    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("velocity");
    }

    // Update is called once per frame
    void Update()
    {
    
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed)
        {

            velocity += Time.deltaTime * acceleration;
            Debug.Log("speed increase");
        }
        
        animator.SetFloat(velocityHash, velocity);
    }
}
