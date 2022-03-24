using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class CombatMovement : MonoBehaviour
{
    public CharaterMovement cM;
    public float attackRate = 1f;
    float nextAttackTime = 0f;

    public float switchRate = 1f;
    float nextSwitchTime = 0f;

    private bool switchCam = true;
    public bool safeToMove = true;
    float raiseHandSeconds;
    float raiseHandSecondsAdj;
    float stopper;
  


    public GameObject hWeapon;
    public GameObject bWeapon;
    public GameObject cam1;
    public GameObject cam2;

    public Transform attackPointF;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    IEnumerator DrawnAndShealthWeapon()
    {
        yield return new WaitForSeconds(0.32f);
        hWeapon.SetActive(false);
        bWeapon.SetActive(true);
    }

    IEnumerator notDrawnAndShowWeapon()
    {
        yield return new WaitForSeconds(0.2f);
        hWeapon.SetActive(true);
        bWeapon.SetActive(false);

    }

    IEnumerator movingTabFreeze()
    {
        cM.anim.SetBool("Front", false) ;
        cM.anim.SetBool("Left", false);
        cM.anim.SetBool("Right", false);
        cM.anim.SetBool("Back", false);
        cM.notStatic = false;
        safeToMove = false;
        yield return new WaitForSeconds(0.7f);
        safeToMove = true;
      
        if (cM.drawS)
            cM.speed = 6;
        else
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                cM.speed = 5;
            }
            else
            {
                cM.speed = cM.iniSpd;
            }
        }

        if (Input.GetKey(KeyCode.W)){
            cM.anim.SetBool("Front", true);
            cM.goForward();

        }

        if (Input.GetKey(KeyCode.A))
        {
            cM.anim.SetBool("Left", true);
            cM.goForward();

        }

        if (Input.GetKey(KeyCode.D))
        {
            cM.anim.SetBool("Right", true);
            cM.goForward();

        }

        if (Input.GetKey(KeyCode.S))
        {
            cM.anim.SetBool("Back", true);
            cM.goForward();

        }

    }



    IEnumerator attFreeze()
    {

        yield return new WaitForSeconds(1.0f);

        cM.att = false;
        if (cM.drawS)
            cM.speed = 6;
        else
            cM.speed = cM.iniSpd;

    }

    public void raiseHand()
    {

        if (Time.time >= raiseHandSecondsAdj && Time.time <= raiseHandSeconds)
        {
           
            
            Collider[] hitEnemies = Physics.OverlapSphere(attackPointF.position, attackRange, enemyLayers);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().death();
             
            }
        }

    }


    private void attCommand()
    {
        if (Input.GetMouseButtonDown(0))
        {

            raiseHandSecondsAdj = Time.time + 0.3f;
            raiseHandSeconds = Time.time + 0.8f;
            cM.att = true;
            cM.anim.SetTrigger("Att");
            cM.speed = 0;
            StartCoroutine(attFreeze());

            nextAttackTime = Time.time + 1f / attackRate;
           
        }
    }

    private void drawCommand()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            switchCam = !switchCam;
            cam1.SetActive(switchCam);
            cam2.SetActive(!switchCam);
            cM.speed = 0;
            cM.drawS = !cM.drawS;
            cM.anim.SetBool("Draw", cM.drawS);
            if (cM.drawS)
                StartCoroutine(notDrawnAndShowWeapon());
            else if (!cM.drawS)
                StartCoroutine(DrawnAndShealthWeapon());
            stopper = Time.time + 0.8f;
            StartCoroutine(movingTabFreeze());
            nextSwitchTime = Time.time + 1f / switchRate;
        }
    }


    public void Combat()
    {
        if (Time.time >= nextSwitchTime)
            drawCommand();

        if (cM.drawS)
            if (Time.time >= nextAttackTime)
                attCommand();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointF.position, attackRange);

    }
}



