using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject messageObject;

    [SerializeField] 
    private GameObject messageText;

    [SerializeField]
    private string message = "";

    public void ShowMessage()
    {
        messageObject.SetActive(true);
        messageText.GetComponent<TextMeshProUGUI>().SetText(message);
        if(Input.GetKeyDown(KeyCode.Escape) && messageObject.activeSelf)
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
