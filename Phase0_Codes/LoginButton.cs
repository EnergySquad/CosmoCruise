using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    public PlayerAuthenticator playerAuthenticator; // Reference to the PlayerAuthenticator script
    
    // Start is called before the first frame update
    void Start()
    {
        playerAuthenticator = FindObjectOfType<PlayerAuthenticator>();
    }

    public void OnLoginButtonClick()
    {
        // Call the AuthenticatePlayer method when the login button is clicked
        if (playerAuthenticator != null)
        {
            playerAuthenticator.AuthenticatePlayer();
        }
        else
        {
            Debug.LogError("PlayerAuthenticator script not found!");
        }
    }

    public void HideLoginButton()
    {
        gameObject.SetActive(false);
    }
}
