using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private PlayerCamera camRef;
	private Animator anim;
	private CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;
	
	[SerializeField]
	private float turnSpeed = 0.6f, speed = 7f, jumpHeight = 6.5f;
	private float gravity = 9.8f;

	void Start()
	{
		camRef = GetComponent<PlayerCamera>();
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (Input.GetKey("w") || Input.GetKey("s"))		
			anim.SetBool("Move", true);
		
		else
			anim.SetBool("Move", false);

		//rotate
		if (!camRef.IsFirstPerson())		
			transform.Rotate(new Vector3(0, turnSpeed * Input.GetAxis("Horizontal"), 0));
		
		//movement
		if (controller.isGrounded)
		{
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			if (Input.GetKey(KeyCode.Space))
			{
				anim.SetTrigger("Jump");
				moveDirection.y = jumpHeight;
			}
			if (camRef.IsFirstPerson())
				moveDirection += transform.right * Input.GetAxis("Horizontal") * speed;
		}
		else
		{
			if (moveDirection.y > 0)
				moveDirection.y -= gravity * Time.deltaTime;
			else			
				moveDirection.y -= Mathf.Pow(gravity, 1.5f) * Time.deltaTime;
		}
		controller.Move(moveDirection * Time.deltaTime);
	}

	public void PlayJumpSFX()
    {
		GetComponent<AudioSource>().Play();
	}
}
