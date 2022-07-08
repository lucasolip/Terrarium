using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using Exception = System.Exception;

public static class SaveSystem
{
    [DllImport("__Internal")]
    private static extern void Save();
    private static string petPath = "/pet.save";
    private static string terrainPath = "/terrain.save";
    private static string inventoryPath = "/inventory.save";
    private static string userPath = "/user.save";

    public static void SavePet(PetController pet, bool dead) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + petPath;
        FileStream file = new FileStream(path, FileMode.Create);

        PetData data = new PetData(pet, dead);
        formatter.Serialize(file, data);
        file.Close();
        try {
            Save();
        } catch (Exception) {}
        //Debug.Log("Pet data saved.");
    }

    public static void SaveTerrain(TerrainController terrain) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + terrainPath;
        FileStream file = new FileStream(path, FileMode.Create);

        TerrainData data = new TerrainData(terrain);
        formatter.Serialize(file, data);
        file.Close();

        try {Save();} catch (Exception) {}

        //Debug.Log("Terrain data saved.");
    }

    public static void SaveInventory(InventoryController inventory) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + inventoryPath;
        FileStream file = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory);
        formatter.Serialize(file, data);
        file.Close();

        try {Save();} catch (Exception) {}

        //Debug.Log("Terrain data saved.");
    }

    public static void SaveUser(UserManager userManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + userPath;
        FileStream file = new FileStream(path, FileMode.Create);

        UserData data = new UserData(userManager.username, userManager.bestBornTime, userManager.bestDiedTime);
        formatter.Serialize(file, data);
        file.Close();

        try {Save();} catch (Exception) {}

        //Debug.Log("User data saved.");
    }

    public static PetData LoadPet() {
        string path = Application.persistentDataPath + petPath;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            PetData data = (PetData)formatter.Deserialize(file);
            file.Close();
            //Debug.Log("Pet data loaded.");
            return data;
        }
        return null;
    }

    public static TerrainData LoadTerrain() {
        string path = Application.persistentDataPath + terrainPath;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            TerrainData data = (TerrainData)formatter.Deserialize(file);
            file.Close();
            //Debug.Log("Terrain data loaded.");
            return data;
        }
        return null;
    }

    public static InventoryData LoadInventory() {
        string path = Application.persistentDataPath + inventoryPath;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            InventoryData data = (InventoryData)formatter.Deserialize(file);
            file.Close();
            //Debug.Log("Inventory data loaded.");
            return data;
        }
        return null;
    }

    public static UserData LoadUser()
    {
        string path = Application.persistentDataPath + userPath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            UserData data = (UserData)formatter.Deserialize(file);
            file.Close();
            //Debug.Log("User data loaded.");
            return data;
        }
        return null;
    }

    public static void ResetPet() {
        string path = Application.persistentDataPath + petPath;
        if (File.Exists(path)) File.Delete(path);

        try {Save();} catch (Exception) {}

    }

    public static void ResetTerrain() {
        string path = Application.persistentDataPath + terrainPath;
        if (File.Exists(path)) File.Delete(path);

        try {Save();} catch (Exception) {}

    }

    public static void ResetInventory() {
        string path = Application.persistentDataPath + inventoryPath;
        if (File.Exists(path)) File.Delete(path);

        try {Save();} catch (Exception) {}

    }

    public static void ResetUser()
    {
        string path = Application.persistentDataPath + userPath;
        if (File.Exists(path)) File.Delete(path);

        try {Save();} catch (Exception) {}

    }

}
