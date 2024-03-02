using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerAuthenticator : MonoBehaviour
{
    private string apiKey = "NjVjNjA0MGY0Njc3MGQ1YzY2MTcyMmM4OjY1YzYwNDBmNDY3NzBkNWM2NjE3MjJiZQ";
    private string baseUrl = "http://20.15.114.131:8080/api/login";

    public string gamingSceneName;
    public Text tokenTextBox;
    public GameObject welcomePage;
    
    public float speed = 20.0f;
    public float turnSpeed = 10.0f;
    public float horizontalInput;
    public float forwardInput;

    public void AuthenticatePlayer()
    {
        StartCoroutine(AuthenticatePlayerCoroutine());
    }

    /*public void CamController()
    {
        Debug.Log("In the CamController");

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //transform.Translate(0, 0, 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }*/
    private IEnumerator AuthenticatePlayerCoroutine()
    {
        string requestBody = "{\"apiKey\": \"" + apiKey + "\"}";

        var request = new UnityWebRequest(baseUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("Response Code: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            AuthResponse authResponse = JsonUtility.FromJson<AuthResponse>(jsonResponse);
            string token = authResponse.token;
            Debug.Log("JWT Token: " + token);
            

            if (tokenTextBox != null)
            {
                tokenTextBox.text = "JWT Token: " + token;
                Debug.Log("Token Text Box updated with the token!");
            }
            else
            {
                Debug.LogError("Token Text Box reference is not set!");
            }

            FindObjectOfType<CamController>().EnableCameraControl();
            //OpenWelcomePage();
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    /*private void OpenWelcomePage()
    {
        if (welcomePage != null)
        {
            welcomePage.SetActive(true);
            Debug.Log("Welcome page opened!");
        }
        else
        {
            Debug.LogError("Welcome page reference is not set!");
        }

       // FindObjectOfType<CamController>().EnableCameraControl();
        LoadGamingScene();
    }*/

    private void LoadGamingScene()
    {
        if (!string.IsNullOrEmpty(gamingSceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gamingSceneName);
        }
        else
        {
            Debug.LogError("Gaming scene name is not specified!");
        }
    }

    [System.Serializable]
    public class AuthResponse
    {
        public string token;
    }
}