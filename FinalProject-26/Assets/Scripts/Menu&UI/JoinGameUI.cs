
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JoinGameUI : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("Join Lobby Menu")]
    [SerializeField] private GameObject LobbyPage = null;
    [SerializeField] private TMP_InputField ipAddressText = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable()
    {
        //By Client here -> only ourselves, not the server
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }
    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAddress = ipAddressText.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }
    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        gameObject.SetActive(false);
        LobbyPage.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}