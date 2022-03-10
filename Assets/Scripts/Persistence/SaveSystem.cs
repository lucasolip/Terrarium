using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePet(PetController pet) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pet.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PetData data = new PetData(pet);
    }
}
