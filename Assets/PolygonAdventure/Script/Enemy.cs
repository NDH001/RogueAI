using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Animator anim;
   
            public void death()
    {
        anim.SetTrigger("death");
        Debug.Log("hit");
        GetComponent<CharacterController>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        this.enabled = false;
    }

}
