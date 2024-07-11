using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControllerBlendTree : MonoBehaviour
{
    Animator animator;

    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;
    private float acceleration = 4f;
    private float deacceleration = 2f;
    private int velocityXHash;
    private int velocityZHash;
    private float maxWalkVelocity = 0.5f;
    private float maxRunVelocity = 2.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        velocityXHash = Animator.StringToHash("velocityX");
        velocityZHash = Animator.StringToHash("velocityZ");
    }

    void ChangeVelocity(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // Update velocityZ based on input
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        else if (backwardPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        else
        {
            // Decelerate velocityZ when neither forward nor backward keys are pressed
            if (velocityZ > 0.0f)
            {
                velocityZ -= Time.deltaTime * deacceleration;
                if (velocityZ < 0.0f)
                {
                    velocityZ = 0.0f;
                }
            }
            else if (velocityZ < 0.0f)
            {
                velocityZ += Time.deltaTime * deacceleration;
                if (velocityZ > 0.0f)
                {
                    velocityZ = 0.0f;
                }
            }
        }

        // Update velocityX based on input
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        else if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        else
        {
            // Decelerate velocityX when neither left nor right keys are pressed
            if (velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deacceleration;
                if (velocityX < 0.0f)
                {
                    velocityX = 0.0f;
                }
            }
            else if (velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * deacceleration;
                if (velocityX > 0.0f)
                {
                    velocityX = 0.0f;
                }
            }
        }
    }

    void LockOrResetVelocity(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // Reset velocityZ for both forward and backward
        if (!forwardPressed && !backwardPressed && Mathf.Abs(velocityZ) < 0.05f)
        {
            velocityZ = 0.0f;
        }

        // Reset velocityX for left and right
        if (!leftPressed && !rightPressed && Mathf.Abs(velocityX) < 0.05f)
        {
            velocityX = 0.0f;
        }

        // Adjust velocityZ if exceeding max velocities
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deacceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < currentMaxVelocity + 0.05f)
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > currentMaxVelocity - 0.05f)
        {
            velocityZ = currentMaxVelocity;
        }

        if (backwardPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if (backwardPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deacceleration;
            if (velocityZ < -currentMaxVelocity && velocityZ > -currentMaxVelocity - 0.05f)
            {
                velocityZ = -currentMaxVelocity;
            }
        }
        else if (backwardPressed && velocityZ > -currentMaxVelocity && velocityZ < -currentMaxVelocity + 0.05f)
        {
            velocityZ = -currentMaxVelocity;
        }

        // Adjust velocityX if exceeding max velocities
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deacceleration;
            if (velocityX < -currentMaxVelocity && velocityX > -currentMaxVelocity - 0.05f)
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < -currentMaxVelocity + 0.05f)
        {
            velocityX = -currentMaxVelocity;
        }

        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deacceleration;
            if (velocityX > currentMaxVelocity && velocityX < currentMaxVelocity + 0.05f)
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > currentMaxVelocity - 0.05f)
        {
            velocityX = currentMaxVelocity;
        }
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        ChangeVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        animator.SetFloat(velocityXHash, velocityX);
        animator.SetFloat(velocityZHash, velocityZ);
    }
}
