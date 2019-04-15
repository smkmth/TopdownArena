using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum playerFacing
{
    Forward = 1,
    FR = 2,
    Right = 3,
    BR = 4,
    Back = 5,
    BL = 6,
    Left = 7,
    FL = 8,
}
public class PlayerMovement : MonoBehaviour {

    private PlayerFacing playerDir;
    private Animator animator;
    private SpriteRenderer sp;


    public float moveSpeed;
    public playerFacing currentPlayerFacing;
    public bool canMove;
    public float attackTime;

    // Use this for initialization
    void Start () {

        playerDir = GetComponentInChildren<PlayerFacing>();;
        animator = GetComponentInChildren<Animator>();
        sp = GetComponentInChildren<SpriteRenderer>();
        canMove = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        //only do movement stuff if we can move
        if (canMove)
        {
            float forwardMovement = Input.GetAxis("Vertical") * moveSpeed;
            float rightMovement = Input.GetAxis("Horizontal") * moveSpeed;

            transform.position += (Vector3.up * forwardMovement * Time.deltaTime);
            transform.position += (Vector3.right * rightMovement * Time.deltaTime);

            if (forwardMovement > 0)
            {
                animator.SetBool("Walking", true);
                currentPlayerFacing = playerFacing.Back;
            }
            else if (forwardMovement < 0)
            {
                animator.SetBool("Walking", true);
                currentPlayerFacing = playerFacing.Forward;
            }
            else if (rightMovement < 0)
            {
                animator.SetBool("Walking", true);
                currentPlayerFacing = playerFacing.Left;
            }
            else if (rightMovement > 0)
            {
                animator.SetBool("Walking", true);
                currentPlayerFacing = playerFacing.Right;
            }
            if (rightMovement == 0 && forwardMovement ==0)
            {
                animator.SetBool("Walking", false);

            }

            //pass enum to animation to tell it where we are facing
            animator.SetInteger("PlayerFacing", (int)currentPlayerFacing);

            sp.flipX = false;
            switch (currentPlayerFacing)
            {
                case (playerFacing.Left):

                    sp.flipX = true;
                    break;

            }

            if (Input.GetButtonDown("Attack"))
            {
          
                StopCoroutine(Attack());
                StartCoroutine(Attack());

            
            }
        }


     
    }

    IEnumerator Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
        canMove = false;
        yield return new WaitForSeconds(attackTime);
        canMove = true;


    }
}
