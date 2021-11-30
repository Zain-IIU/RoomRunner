using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
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


    int counterforPickUps;

  
    [SerializeField]
    CinemachineVirtualCamera EndCam;
    [SerializeField]
    CinemachineVirtualCamera winCam;

    public bool playerIsRotating;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        accessories = GetComponent<PlayerAccessories>();
        counterforPickUps = 0;
        GameManager.instance.onGameStarted += StarttheGame;
    }

    private void StarttheGame()
    {
        animator.SetTrigger("Play");
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
        if (!GameManager.instance.hasStarted) return;
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

    void SetAnimations(int counter)
    {
 

        if(counter>=2 && counter <4)
        {
            animator.SetTrigger("Walk_Normal");
            animator.SetTrigger("Happy");
            PlayerParticle.instance.PlaylevelUpVFX();
        }
        else if(counter>=4 && counter <10)
        {
            animator.SetTrigger("Walk_Happy");
            PlayerParticle.instance.PlaylevelUpVFX();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("HeadSet_Gate"))
        {
            if(other.gameObject.GetComponent<Gate>().isActive)
            {

                CashPickUp.instance.DecrementCash(other.gameObject.GetComponent<Gate>().getPrice());
                accessories.EnableHeadset();
                accessories.PickUpItemByFollowers(other.transform.GetChild(0).transform);
                counterforPickUps++;
                SetAnimations(counterforPickUps);
                PlayerParticle.instance.PlayPickUpVFX();
                UIManager.instance.IncreaseFollowersAmount();
                

            }
            else
            {
                LosePlayer();
                accessories.HideFollowers();
               
            }
            other.gameObject.transform.DOScale(Vector2.zero, 0.15f);

        }
        if(other.gameObject.CompareTag("EndLine"))
        {
            animator.SetTrigger("Idle");
            UIManager.instance.HideFollowersMeter();    
            moveSpeed = 0f;
            EndCam.m_Priority = 12;
            accessories.RearrangeItems(EndCam);
        }

       
    }

    public void LosePlayer()
    {
        moveSpeed = 0;
        animator.SetTrigger("Defeated");
        winCam.m_Priority = 15;
     
        UIManager.instance.ShowLosePanel();
    }

}
