
using System.Collections;
using UnityEngine;


public class CharaterMovement : MonoBehaviour
{
    public CharacterController cc;
    public Animator anim;
    public Transform groundCheck;
    public float speed;
    public float iniSpd;
    public float gravity = -9.81f;
    public float jumpHeight;
    public float groundDistance = 0.4f;
    private bool isGrounded;
    private Vector3 velocity;
    private float combatSpeed = 6f;
    public LayerMask groundMask;
   

    public bool notStatic ;
    public bool drawS = false;
    public bool att = false;
    

    // Update is called once per frame
    private void Start()
    {
      
        anim = GetComponent<Animator>();
        GetComponent<CombatMovement>().safeToMove = true;
        GetComponent<CombatMovement>().hWeapon.SetActive(false);
        iniSpd = speed;
        }

    void Update()
    {


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.fixedDeltaTime);
        velocity.y += gravity * Time.fixedDeltaTime;
        cc.Move(velocity * Time.fixedDeltaTime);
        movement();
    }


    private void checkIfIsAttacking()
    {
        if (drawS && att == true)
        {
            speed = 0;
        }
       
    }

    private void assignSpdBasedOnStatus(float drawSpd, float notDrawnSpd)
    {
        if (drawS)
            speed = drawSpd;
        else
            speed = notDrawnSpd;

    }

    public void goForward()
    {
        if (Input.GetKeyDown(KeyCode.W) && GetComponent<CombatMovement>().safeToMove)
        {
     
            assignSpdBasedOnStatus(combatSpeed, iniSpd);
            notStatic = true;
            anim.SetBool("Front", true);

        }


        if (Input.GetKeyUp(KeyCode.W) && GetComponent<CombatMovement>().safeToMove)
        {
            Debug.Log("does it");
            assignSpdBasedOnStatus(combatSpeed, iniSpd);
            notStatic = false;
            anim.SetBool("Front", false);

        }
    }

    public void goLeft()
    {
        //Walk Left 

        if (Input.GetKeyDown(KeyCode.A) && GetComponent<CombatMovement>().safeToMove)
        {
            assignSpdBasedOnStatus(combatSpeed, iniSpd);

            anim.SetBool("Left", true);
        }

        if (Input.GetKeyUp(KeyCode.A) && GetComponent<CombatMovement>().safeToMove)
        {

            anim.SetBool("Left", false);

            if (notStatic && GetComponent<CombatMovement>().safeToMove)
            {
                assignSpdBasedOnStatus(combatSpeed, iniSpd);

            }

        }
    }

    public void goRight()
    {
        //Walk Right
        if (Input.GetKeyDown(KeyCode.D) && GetComponent<CombatMovement>().safeToMove)
        {
            assignSpdBasedOnStatus(combatSpeed, iniSpd);

            anim.SetBool("Right", true);
        }

        if (Input.GetKeyUp(KeyCode.D) && GetComponent<CombatMovement>().safeToMove)
        {

            anim.SetBool("Right", false);


            if (notStatic && GetComponent<CombatMovement>().safeToMove)
            {
                assignSpdBasedOnStatus(combatSpeed, iniSpd);

            }

        }
    }

    public void goBack()
    {
        //Walk Back
        if (Input.GetKeyDown(KeyCode.S) && GetComponent<CombatMovement>().safeToMove)
        {
            assignSpdBasedOnStatus(combatSpeed, iniSpd);

            anim.SetBool("Back", true);
        }
        if (Input.GetKeyUp(KeyCode.S) && GetComponent<CombatMovement>().safeToMove)
        {

            anim.SetBool("Back", false);


            if (notStatic && GetComponent<CombatMovement>().safeToMove)
            {
                assignSpdBasedOnStatus(combatSpeed, iniSpd);

            }

        }
    }



    private void movement()
    {

             checkIfIsAttacking();
        
     
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                anim.SetTrigger("Jump");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        goForward();
        goLeft();
        goRight();
        goBack();

        GetComponent<CombatMovement>().Combat();
           GetComponent<CombatMovement>().raiseHand();
    }

   


}
