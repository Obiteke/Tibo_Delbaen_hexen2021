using UnityEngine;

namespace Hexen.Utils
{
    public class DontDestroyOnLoad : MonoBehaviour
    {

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}

