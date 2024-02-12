using System.Collections.Generic;
using DefaultNamespace.DataPersistence.Data;
using DefaultNamespace.DataPersistence.SerializableTypes;
using UnityEngine.Serialization;

[System.Serializable]
public class LibraryData
{
    public SerializableDictionary<string ,BookData> books;
    public LibraryData()
    {
        books = new SerializableDictionary<string, BookData>();
    }
}