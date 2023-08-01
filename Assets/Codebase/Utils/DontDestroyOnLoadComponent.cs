using UnityEngine;

namespace Assets.Codebase.Utils
{
    public class DontDestroyOnLoadComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}