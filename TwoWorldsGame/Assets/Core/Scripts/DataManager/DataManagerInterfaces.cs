using UnityEngine;

namespace Core.Scripts.DataManager
{
    public interface ILoaderDataStorage
    {
        public void LoaderData(string dataName, string data);
        public void LoaderData(string dataName, int data);
        public void LoaderData(string dataName, float data);
    }
}
