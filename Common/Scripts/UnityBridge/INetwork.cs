using UnityEngine;
using System.Collections;

namespace BuildingBlocks
{
    /// <summary>
    /// INetwork interface used to enable implementing a mock network.
    /// </summary>
    public interface INetwork
    {
        /// <summary>
        /// Instantiate the specified prefab at location with the rotation for the group.
        /// </summary>
        /// <returns>The instantiated Object</returns>
        object Instantiate(UnityEngine.Object prefab, Vector3 location, Quaternion rotation, int group);

        /// <summary>
        /// Initializes the server.
        /// </summary>
        /// <returns>The server.</returns>
        /// <param name="maxConnection">Max connection.</param>
        /// <param name="portnumber">Portnumber.</param>
        /// <param name="NATPunchthrough">If set to <c>true</c> NAT punchthrough.</param>
        NetworkConnectionError InitializeServer(int maxConnection, int portnumber, bool NATPunchthrough);

        NetworkConnectionError Connect(string ip, int port);

        void Disconnect();

        void SetSendingEnabled(int group, bool enabled);

        void RemoveRPCs(NetworkViewID viewId);
        void Destroy(NetworkViewID viewId);

        bool isClient { get; }
        bool isServer { get; }
        bool isMessageQueueRunning { get; set; }
    }
}
