
namespace BuildingBlocks.CubeFinger
{
    public enum CubeFingerMode
    {
        None,
        Delete,
        Build
    };

    public delegate void CubeFingerModeChangedHandler(object sender, CubeFingerMode mode);

    public interface ICubeFinger : IBuildingBlocksBehaviour
    {
        event CubeFingerModeChangedHandler OnModeChanged;

        bool Hide { get; }
        CubeFingerMode Mode { get; set; }
        ICubeFingerRenderer Renderer { get; }

        bool IsMine { get; }

        void OnPlayerConnected(INetworkPlayer player);
        void Update();
        void Destroy();
    }
}
