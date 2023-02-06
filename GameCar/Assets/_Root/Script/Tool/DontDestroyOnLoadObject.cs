using UnityEngine;

namespace GameCarTool
{
    public sealed class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Awake()
        {
            if(enabled)
                DontDestroyOnLoad(gameObject);
        }
    }
}