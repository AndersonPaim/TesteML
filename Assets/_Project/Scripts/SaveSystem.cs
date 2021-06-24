using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public static class SaveSystem
{
    public static SaveData localData { get; private set; }

    public static void Save() 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, localData);
        stream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/save.data";
        if (File.Exists(path)) //load from an existing save
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
             
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            localData = data; //store save data locally to dont need to load again
        }
        else //create new save file
        {
            localData = new SaveData();
        }

        return localData;
    }
}