using UnityEngine;

namespace DefaultNamespace.DataPersistence.Data
{
    [System.Serializable]
    public class BookData
    {
        public string bookName;
        public string bookAuthor;
        public Sprite bookIcon;
        public string ISBN { get; set; }
        
        public BookData()
        {
            bookName = "";
            bookAuthor = "";
            bookIcon = null;
            ISBN = System.Guid.NewGuid().ToString();
        }
    }
}