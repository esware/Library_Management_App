
namespace DefaultNamespace.DataPersistence
{
    public interface IDataPersistence
    {
        void LoadData(LibraryData data);
        void SaveData(ref LibraryData data);
    }
}