using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;

public class UserManager : MonoBehaviour, TickEventListener, PetDiedEventListener
{
    public TickEvent tickEvent;
    public PetDiedEvent petDiedEvent;
    public PetManager petManager;
    public NameUI inputField;
    public DatabaseController database;
    public UserInfoUI userUI;
    public GameObject[] onlineUI;
    public string nameFieldTitle;
    public string username;
    public DateTime? bestBornTime;
    public DateTime? bestDiedTime;
    private PetController bestPet;

    void Start()
    {
        UserData data = SaveSystem.LoadUser();
        if (data == null) {
            HideOnlineUI();
            inputField.SetTitle(nameFieldTitle);
            inputField.Show();
            inputField.inputSent += OnInputSent;
        } else {
            username = data.username;
            bestBornTime = data.bestBornTime;
            bestDiedTime = data.bestDiedTime;
            database.UpdateUser(username, bestBornTime, bestDiedTime);
            userUI.SetUsername(username);
            userUI.SetBestPet(TimeFormatter.Format(bestBornTime, bestDiedTime));
            ShowOnlineUI();
        }
    }

    public void ShowOnlineUI()
    {
        userUI.gameObject.SetActive(true);
        foreach (GameObject ui in onlineUI) {
            ui.SetActive(true);
        }
    }

    public void HideOnlineUI()
    {
        userUI.gameObject.SetActive(false);
        foreach (GameObject ui in onlineUI)
        {
            ui.SetActive(false);
        }
    }

    private int GetRecord()
    {
        return bestBornTime == null ? 0 :
                    bestDiedTime == null ?
                        (int)(DateTime.Now - bestBornTime.Value).TotalSeconds :
                        (int)(bestDiedTime.Value - bestBornTime.Value).TotalSeconds;
    }

    public void ResetUser()
    {
        database.errorEvent += OnErrorDelete;
        database.receivedResponseEvent += OnReceivedResponseDelete;
        database.DeleteUser(username);
    }

    public void OnInputSent()
    {
        inputField.inputSent -= OnInputSent;
        username = inputField.Dispose();
        database.receivedResponseEvent += OnReceivedResponseInput;
        database.errorEvent += OnErrorInput;
        database.ValidName(username);
    }

    public void OnReceivedResponseDelete(bool acknowledged)
    {
        database.errorEvent -= OnErrorDelete;
        database.receivedResponseEvent -= OnReceivedResponseDelete;
        bestBornTime = null;
        bestDiedTime = null;
        username = "";
        HideOnlineUI();
        SaveSystem.ResetUser();
        
        inputField.SetTitle(nameFieldTitle);
        inputField.Show();
        inputField.inputSent += OnInputSent;
    }

    public void OnErrorDelete(string error)
    {
        database.errorEvent -= OnErrorDelete;
        database.receivedResponseEvent -= OnReceivedResponseDelete;
    }

    public void OnReceivedResponseInput(bool valid) {
        database.errorEvent -= OnErrorInput;
        database.receivedResponseEvent -= OnReceivedResponseInput;
        if (valid) {
            Debug.Log("Valid name");
            database.InitializeUser(username);
            userUI.SetUsername(username);
            SaveSystem.SaveUser(this);
            tickEvent.tickEvent += OnTick;
            petDiedEvent.petDiedEvent += OnPetDied;
            
            ShowOnlineUI();
        }
        else {
            Debug.Log("Invalid name");
            inputField.SetTitle("El nombre ya está en uso\n" + nameFieldTitle);
            inputField.Show();
            inputField.inputSent += OnInputSent;
        }
    }

    public void OnErrorInput(string error) {
        database.errorEvent -= OnErrorInput;
        database.receivedResponseEvent -= OnReceivedResponseInput;
        inputField.SetTitle("Hubo un error de conexión\n" + nameFieldTitle);
        inputField.Show();
        inputField.inputSent += OnInputSent;
    }

    public void OnTick()
    {
        PetController pet = petManager.CheckBestPet(GetRecord(), bestPet);
        if (pet != null) {
            bestPet = pet;
            bestDiedTime = null;
            bestBornTime = pet.bornTime;
            SaveSystem.SaveUser(this);
            database.UpdateUser(username, bestBornTime, bestDiedTime);
        }
        if (bestBornTime != null && bestDiedTime == null) {
            userUI.SetBestPet(TimeFormatter.Format(DateTime.Now - bestBornTime.Value));
        }
    }

    public void OnPetDied(PetController pet)
    {
        if (bestBornTime.Value.Equals(pet.bornTime)) {
            bestDiedTime = DateTime.Now;
            SaveSystem.SaveUser(this);
            database.UpdateUser(username, bestBornTime, bestDiedTime);
        }
    }
}