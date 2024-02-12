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
    
    public LibraryData appData;
    
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance!=null)
        {
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
        LoadData();
    }

    public void DefaultAppData()
    {
        this.appData = new LibraryData();
    }

    private void LoadData()
    {
        appData = _dataHandler.Load();
        
        if (appData==null)
        {
            DefaultAppData();
        }
        
        foreach (var dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(appData);
        }
    }

    private void SaveData()
    {
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref appData);
        }
        
        _dataHandler.Save(appData);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> persistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(persistenceObjects);
    }
}
