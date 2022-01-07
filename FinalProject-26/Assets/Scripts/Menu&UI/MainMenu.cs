
using UnityEngine;

public class MainMenu : MonoBehaviour
{
 
     [SerializeField] private NetworkManagerLobby networkManager = null;

     [Header("MAIN UI")]
     [SerializeField] private GameObject landingPagePanel = null;

      public void HostLobby()
      {
            networkManager.StartHost();

            landingPagePanel.SetActive(false);
      }
    
}
