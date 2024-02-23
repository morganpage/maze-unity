using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public static Action<int> OnGathered;
    private static int Gathered; //All the gatherables that have been gathered so far

    void Awake(){
        Gathered = 0;
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        Debug.Log($"Collided with: {collider2D.name}");
        gameObject.SetActive(false);
        Gathered++;
        OnGathered?.Invoke(Gathered);
    }

}
