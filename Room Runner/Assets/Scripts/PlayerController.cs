using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]

    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float moveSpeed;
    PlayerAccessories accessories;
 
    private Animator animator;
   
    float xRot;
    float yRot;

    Rigidbody RB;

   
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        accessories = GetComponent<PlayerAccessories>();
    }
    public void SetMoveSpeed(float newMoveSpeed) => moveSpeed = newMoveSpeed;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        //for moving straight
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        transform.DORotateQuaternion(Quaternion.Euler(0f, yRot, 0f), 0.15f);
        if (Input.GetMouseButtonDown(0))
        {
            return;
        }

        // handling rotation

        if (Input.GetMouseButton(0))
        {
          
                yRot += Input.GetAxis("Mouse X") * rotationSpeed;
                yRot = Mathf.Clamp(yRot, -20f, 20f);
               
        }
        else
        {
            
                yRot = 0f;
           
        }


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.3f, 2.3f), transform.position.y,
            transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("HeadSet_Gate"))
        {
            other.gameObject.transform.DOScale(Vector2.zero, 0.15f);
            animator.SetTrigger("Walk_Normal");
            accessories.EnableHeadset();
        }
    }

}
