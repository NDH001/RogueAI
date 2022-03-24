using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public GameObject NPC;
    public NavMeshAgent agent;
    private Transform choosenP;
    private Animator anim;
    private bool oneTime = true;
    private float forceAction = 14f;

    void Start()
    {
      
       agent = this.GetComponent<NavMeshAgent>();
       agent.enabled = true;      
       anim = GetComponent<Animator>();
       SetDestination();
  
    }


    private void SetDestination()
    {
        oneTime = true;
        choosenP = randomPoint();
        if (choosenP != null)
        {
            Vector3 targetVector = choosenP.transform.position;
            agent.SetDestination(targetVector);
        }
    }

     private Transform randomPoint()
    {
        int randomP = Random.Range(0, Destinations.destinationsCood.Count-1);
        Transform temp = Destinations.destinationsCood[randomP];
        Destinations.destinationsCood.RemoveAt(randomP);
        return temp;
    }


    IEnumerator randomAction()
    {
        forceAction = Time.time + 20f;
        oneTime = false;
        string[] allActions = new string[] { "a","b","c","d","e"};
        int randomA = Random.Range(0, allActions.Length);
        float randomB = Random.Range(7, 12);
        anim.SetTrigger(allActions[randomA]);
        anim.SetBool("walking", true);
               
        yield return new WaitForSeconds(randomB);
        Transform temp = choosenP.transform;
        Destinations.destinationsCood.Add(temp);
        anim.SetBool("walking", false);
        SetDestination();
        
    }

    IEnumerator randomAction2()
    {
        forceAction = Time.time + 20f;
        oneTime = false;
        yield return new WaitForSeconds(0f);
        Transform temp = choosenP.transform;
        Destinations.destinationsCood.Add(temp);
        SetDestination();

    }

    void Update()
    {

       if( Time.time < forceAction)
        {
            if ((NPC.transform.position.x == choosenP.transform.position.x) && (NPC.transform.position.z == choosenP.transform.position.z))

            {
                if (oneTime)
                {
                    StartCoroutine(randomAction());
                }
            }
        }
        else
        {
            if (oneTime)
            {
               
                StartCoroutine(randomAction2());
            }
        }
    }
}
