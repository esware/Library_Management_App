using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DefaultNamespace.DataPersistence;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;
    
    public AppData appData;
    
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance!=null)
        {
            Debug.Log("$ Found more than Data Persistence Manager in the scene");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath,fileName,useEncryption);
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.appData = new AppData();
    }

    public void LoadGame()
    {
        // GetCrop any saved data from a  file using the data handler
        appData = _dataHandler.Load();
        
        // if no data can be loaded , initialize to a new game
        
        if (appData==null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        
        // TODO -  push the loaded data to all other scripts that need it
        foreach (var dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(appData);
        }
    }

    public void SaveGame()
    {
        // TODO - pass the data to other scripts so they can update it
        
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref appData);
        }
        
        // save that data to a file using the data handler
        
        _dataHandler.Save(appData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> persistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(persistenceObjects);
    }
}
