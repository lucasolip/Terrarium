using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    public string recordString = "Récord: ";
    Text username, bestPet;

    private void Awake()
    {
        username = transform.Find("InnerPanel").Find("Username").GetComponent<Text>();
        bestPet = transform.Find("InnerPanel").Find("Best").GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void SetUsername(string username)
    {
        this.username.text = username;
    }

    public void SetBestPet(string best)
    {
        bestPet.text = recordString + best;
    }

}
