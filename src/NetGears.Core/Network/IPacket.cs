namespace NetGears.Core.Network
{
    public interface IPacket
    {
        void Deserialize(byte[] buffer);

        byte[] Serialize();
    }
}