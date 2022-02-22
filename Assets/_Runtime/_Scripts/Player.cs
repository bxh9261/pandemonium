using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public CharacterController cc;

    private float yVel;

    private void Update()
    {
        Camera cam = Camera.main;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = cam.transform.right * horizontal + cam.transform.forward * vertical;
        movement = Vector3.ProjectOnPlane(movement, Vector3.up);

        if (movement.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            animator.SetBool("walking", true);

            if (Input.GetKey(KeyCode.LeftShift))
                animator.SetBool("running", true);
            else
                animator.SetBool("running", false);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("running", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetFloat("Speed", 1.3f);
        }
        else
        {
            animator.SetFloat("Speed", 1.0f);
        }

        if (cc.isGrounded)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            yVel = 0;
        }

        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z - 10);


        yVel = -4 * Time.deltaTime;

    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = yVel;
        cc.Move(velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
