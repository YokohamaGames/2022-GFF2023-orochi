using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Instantiate(player);
    }

    public void InstantiateCharacter()
    {
        PlayerInput.Instantiate(player);
    }
    /*
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        foreach(var device in playerInput.devices)
        {
            Debug.Log("����f�o�C�X�@+ device");
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
