using UnityEngine;

namespace Core.Scripts.DataManager
{
    public class DataStorage : MonoBehaviour, ILoaderDataStorage
    {
        

        public void LoaderData(string dataName, string data)
        {
            PlayerPrefs.SetString(dataName, data);
        }

        public void LoaderData(string dataName, int data)
        {
            PlayerPrefs.SetInt(dataName, data);
        }

        public void LoaderData(string dataName, float data)
        {
            PlayerPrefs.SetFloat(dataName, data);
        }
    }
}
