using Core.Scripts.LevelController;
using UnityEngine;
using Zenject;

namespace Core.Scripts.DataManager
{
    public class DataStorage : ILoaderDataStorage, ISaveDataStorage
    {

        #region Save Data

        public void SaveData(SaveDataType dataType, string data)
        {
            PlayerPrefs.SetString(dataType.ToString(), data);
            Save();
        }

        public void SaveData(SaveDataType dataType, int data)
        {
            PlayerPrefs.SetInt(dataType.ToString(), data);
            Save();
        }

        public void SaveData(SaveDataType dataType, float data)
        {
            PlayerPrefs.SetFloat(dataType.ToString(), data);
            Save();
        }

        #endregion


        #region Get Data

        public string GetDataString(SaveDataType dataType)
        {
            if (!PlayerPrefs.HasKey(dataType.ToString()))
            {
                return default;
            }
            
            return PlayerPrefs.GetString(dataType.ToString());
        }

        public int GetDataInt(SaveDataType dataType)
        {
            if (!PlayerPrefs.HasKey(dataType.ToString()))
            {
                return default;
            }

            return PlayerPrefs.GetInt(dataType.ToString());
        }

        public float GetDataFloat(SaveDataType dataType)
        {
            if (!PlayerPrefs.HasKey(dataType.ToString()))
            {
                return default;
            }
            
            return PlayerPrefs.GetInt(dataType.ToString());
        }

        #endregion

        private void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
