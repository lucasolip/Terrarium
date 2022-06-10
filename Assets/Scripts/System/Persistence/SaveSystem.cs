using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string petPath = "/pet.save";
    private static string terrainPath = "/terrain.save";

    public static void SavePet(PetController pet) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + petPath;
        FileStream file = new FileStream(path, FileMode.Create);

        PetData data = new PetData(pet);
        formatter.Serialize(file, data);
        file.Close();
        Debug.Log("Pet data saved.");
    }

    public static void SaveTerrain(TerrainController terrain) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + terrainPath;
        FileStream file = new FileStream(path, FileMode.Create);

        TerrainData data = new TerrainData(terrain);
        formatter.Serialize(file, data);
        file.Close();
        Debug.Log("Terrain data saved.");
    }

    public static PetData LoadPet() {
        string path = Application.persistentDataPath + petPath;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            PetData data = (PetData)formatter.Deserialize(file);
            file.Close();
            Debug.Log("Pet data loaded.");
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
            Debug.Log("Terrain data loaded.");
            return data;
        }
        return null;
    }

    public static void ResetPet() {
        string path = Application.persistentDataPath + petPath;
        if (File.Exists(path)) File.Delete(path);
    }

    public static void ResetTerrain() {
        string path = Application.persistentDataPath + terrainPath;
        if (File.Exists(path)) File.Delete(path);
    }

}
