using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string PlayerCharacter = "Mouse";
    private bool anyKeyPressed = false;
    
    void Start()
    {
        GameObject playerPrefab = Resources.Load<GameObject>(PlayerCharacter);
        GameObject player = Instantiate(playerPrefab);
        Time.timeScale = 0;
    }

    void Update(){
        if(Input.anyKey && !anyKeyPressed)
        {
            Time.timeScale = 1.0f;
            anyKeyPressed = true;
        }
    }

}
