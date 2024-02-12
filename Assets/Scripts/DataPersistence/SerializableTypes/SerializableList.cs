using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.DataPersistence.SerializableTypes
{
    [System.Serializable]
    public class SerializableList<T> : ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<T> items = new List<T>();

        [SerializeField]
        private List<T> serializedItems = new List<T>();

        public List<T> Items { get { return items; } }

        public void OnBeforeSerialize()
        {
            serializedItems.Clear();
            serializedItems.AddRange(items);
        }

        public void OnAfterDeserialize()
        {
            items.Clear();
            items.AddRange(serializedItems);
        }
    }
}