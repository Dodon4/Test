using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    GameObject sqrPrefab;
    public static bool isLoaded = false;
    const string SQUARE_SUB = "/square";
    const string SQUARE_COUNT_SUB = "/square";
    private void Awake()
    {
       // Load();
    }
    private void OnApplicationQuit()
    {
       // Save();
    }
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SQUARE_SUB;
        string countPath = Application.persistentDataPath + SQUARE_COUNT_SUB;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, BoardManager.instance.squares.Count);
        for (int i = 0; i < BoardManager.instance.squares.Count; i++)
        {
            FileStream stream = new FileStream(path + i,FileMode.Create);
            Data data = new Data(BoardManager.instance.squares[i]);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        
       
    }
    public void Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SQUARE_SUB;
        string countPath = Application.persistentDataPath + SQUARE_COUNT_SUB;
        int sqrCount = 0;
        if(File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);
            sqrCount = (int)formatter.Deserialize(countStream);
            countStream.Close();
            isLoaded = true;
        }
        else
        {
            Debug.LogError("path not found");
        }

        for (int i = 0; i < sqrCount; i++)
        {
            if(File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                Data data = formatter.Deserialize(stream) as Data;

                stream.Close();
                Vector3 pos = new Vector3(data.position[0], data.position[1], data.position[2]);
                GameObject square = Instantiate(sqrPrefab, pos, Quaternion.identity);
            }
            else
            {
                Debug.LogError("path not found");
            }
        }
    }
}
