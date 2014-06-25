using UnityEngine;

namespace BuildingBlocks.Client
{
    public class QuitBehaviour : MonoBehaviour
    {
        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                Network.Disconnect();

                switch (Application.loadedLevel)
                {
                    case 1: // QRCodeScene
                        Application.Quit();
                        break;
                    case 2: // StartScreenScene
                    case 3: // Client
                        Application.LoadLevel(Application.loadedLevel - 1);
                        break;
                }
            }
        }
    }
}
