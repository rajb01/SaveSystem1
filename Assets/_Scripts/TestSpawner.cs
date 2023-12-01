using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestSpawner : MonoBehaviour
{
    public SaveSystem saveSystem;
    public GameObject prefab;
    public GameObject prefab2;
    public List<GameObject> createdPrefabs = new List<GameObject>();
    public List<GameObject> createdPrefabs2 = new List<GameObject>();

    public void Clear()
    {
        foreach (var item in createdPrefabs)
        {
            Destroy(item);
        }
        createdPrefabs.Clear();
        foreach (var item in createdPrefabs2)
        {
            Destroy(item);
        }
        createdPrefabs2.Clear();
    }
    public void SpawnPrefab()
    {
        var position = Random.insideUnitSphere * 5;
        createdPrefabs.Add(Instantiate(prefab, position, Quaternion.identity));
        
        //createdPrefabs.Add(Instantiate(prefab, transform.position, Quaternion.identity));
    }
    public void SpawnPrefab2()
    {
        var position = Random.insideUnitSphere * 5;
        createdPrefabs2.Add(Instantiate(prefab2, position, Quaternion.identity));
        //createdPrefabs.Add(Instantiate(prefab, transform.position, Quaternion.identity));
    }
    public void SaveGame()
    {
        SaveData data = new SaveData();
        foreach (var item in createdPrefabs)
        {
            data.Add(item);
        }
        foreach (var item in createdPrefabs2)
        {
            data.Add(item);
        }
        var dataToSave = JsonUtility.ToJson(data);
        saveSystem.SaveData(dataToSave);
    }
    public void LoadGame()
    {
        Clear();
        string dataToLoad = "";
        dataToLoad = saveSystem.LoadData();
        if (String.IsNullOrEmpty(dataToLoad) == false)
        {
            SaveData data = JsonUtility.FromJson<SaveData>(dataToLoad);
            //Debug.Log(data);
            foreach (var objData in data.objectData)
            {
                GameObject newObj = new GameObject();
                if (objData.type.StartsWith("Cube"))
                    newObj = Instantiate(prefab, objData.position.GetValue(), Quaternion.identity);
                if (objData.type.StartsWith("Sphere"))
                    newObj = Instantiate(prefab2, objData.position.GetValue(), Quaternion.identity);
                newObj.name = objData.type;
                createdPrefabs.Add(newObj);
            }
        }
    } 
    [Serializable]
    public class SaveData
    {
        public List<OjbectSerialization> objectData;
        public SaveData()
        {
            objectData = new List<OjbectSerialization>();
        }
        public void Add(GameObject obj)
        {
            objectData.Add(new OjbectSerialization(obj.name, obj.transform.position));
        }
    }
    [Serializable]
    public class OjbectSerialization
    {
        public Vector3Serialzation position;
        public string type;
        public OjbectSerialization(string t, Vector3 pos)
        {
            this.type = t;
            this.position = new Vector3Serialzation(pos);
        }
    }
    [Serializable]
    public class Vector3Serialzation
    {
        public float x, y, z;
        public Vector3Serialzation(Vector3 position)
        {
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;
        }
        public Vector3 GetValue()
        {
            return new Vector3(x,y,z);
        }
    }
}
