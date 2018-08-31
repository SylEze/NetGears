using NetGears.Game.Data.TransferObject;

namespace NetGears.Game.Data.AccessLayer
{
    public interface IAccountDal
    {
        AccountDto GetByUsername(string username);
    }
}