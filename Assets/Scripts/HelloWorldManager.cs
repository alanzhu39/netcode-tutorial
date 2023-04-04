using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Client"))
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                    "169.229.103.126",
                    (ushort)12345
                );
                NetworkManager.Singleton.StartClient();
            }
            if (GUILayout.Button("Host"))
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                    "169.229.103.126",
                    (ushort)12345,
                    "0.0.0.0"
                );
                NetworkManager.Singleton.StartHost();
            }
            if (GUILayout.Button("Server"))
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                    "169.229.103.126",
                    (ushort)12345,
                    "0.0.0.0"
                );
                NetworkManager.Singleton.StartServer();
            }
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        static void SubmitNewPosition()
        {
            if (GUILayout.Button("Reset Position"))
            {
                if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient )
                {
                    // Do nothing
                }
                else
                {
                    var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                    var player = playerObject.GetComponent<HelloWorldPlayer>();
                    player.ResetPosition();
                }
            }
        }
    }
}
