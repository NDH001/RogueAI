using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Destinations : MonoBehaviour
{

    public static GameObject[] destinations;
    public static List<Transform> destinationsCood;
    private GameObject[] NPC;
    private Transform choosenP;

    private void Start()
    {
        destinations = GameObject.FindGameObjectsWithTag("Respawn");
        destinationsCood = new List<Transform>(new Transform[destinations.Length]);
        Debug.Log(destinationsCood.Count);

        for (int i = 0; i < destinations.Length; i++)
        {
            destinationsCood[i] = destinations[i].transform;
        }

        NPC = GameObject.FindGameObjectsWithTag("NPC");

        for ( int i = 0; i < NPC.Length; i++)
        {

            NPC[i].transform.position = initialSetUp();
          
        }

    }

    private Vector3 initialSetUp()
    {
        choosenP = randomPoint();
        if (choosenP != null)
        {
            Vector3 targetVector = choosenP.transform.position;

            return targetVector;
        }
        else
        {
            
            return new Vector3(0, 0, 0);
           
        }
    }

    private Transform randomPoint()
    {
        int randomP = Random.Range(0, destinationsCood.Count - 1);
        Transform temp = destinationsCood[randomP];
        destinationsCood.RemoveAt(randomP);
        return temp;
    }

  }
