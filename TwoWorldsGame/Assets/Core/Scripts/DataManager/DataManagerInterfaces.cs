using UnityEngine;

namespace Core.Scripts.DataManager
{
    public interface ISaveDataStorage
    {
        public void SaveData(SaveDataType dataType, string data);
        public void SaveData(SaveDataType dataType, int data);
        public void SaveData(SaveDataType dataType, float data);
    }
    
    public interface ILoaderDataStorage
    {
        public string GetDataString(SaveDataType dataType);
        public int GetDataInt(SaveDataType dataType);
        public float GetDataFloat(SaveDataType dataType);
    }
    
}
