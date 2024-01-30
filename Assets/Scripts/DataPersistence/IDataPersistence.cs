
namespace DefaultNamespace.DataPersistence
{
    public interface IDataPersistence
    {
        void LoadData(AppData gameData);
        void SaveData(ref AppData gameData);
    }
}