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
        SaveData data2 = new SaveData();
        foreach (var item in createdPrefabs)
        {
            data.Add(item.transform.position);
        }
        var dataToSave1 = JsonUtility.ToJson(data);
        //saveSystem.SaveData(dataToSave);

        //SaveData2 data2 = new SaveData2();
        foreach (var item in createdPrefabs2)
        {
            data2.Add(item.transform.position);
        }
        var dataToSave2 = JsonUtility.ToJson(data2);
        var dataToSave = (dataToSave1+dataToSave2);
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
            foreach (var positionData in data.positionData)
            {
                createdPrefabs.Add(Instantiate(prefab, positionData.GetValue(), Quaternion.identity));
            }
        }
    } 
    [Serializable]
    public class SaveData
    {
        public List<Vector3Serialzation> positionData;
        public List<Vector3Serialzation> positionData2;
        public SaveData()
        {
            //Clear();
            positionData = new List<Vector3Serialzation>();
            //positionData2 = new List<Vector3Serialzation>();
        }
        public void Add(Vector3 position)
        {
            //Clear();
            positionData.Add(new Vector3Serialzation(position));
            //positionData2.Add(new Vector3Serialzation(position));
        }
        /*
        public SaveData2()
        {
            //Clear();
            //positionData = new List<Vector3Serialzation>();
            positionData2 = new List<Vector3Serialzation>();
        }

        public void Add2(Vector3 position)
        {
            //Clear();
            //positionData.Add(new Vector3Serialzation(position));
            positionData2.Add(new Vector3Serialzation(position));
        }*/

      
        
        
    }
    /*

    [Serializable]
    public class SaveData2
    {
        public List<Vector3Serialzation> positionData2;
        public SaveData2()
        {
            positionData2 = new List<Vector3Serialzation>();
        }
        public void Add2(Vector3 position)
        {
            positionData2.Add2(new Vector3Serialzation(position));
        }
    }*/

   
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
