using NetGears.Game.Data.AccessLayer;
using NetGears.Game.Data.TransferObject;

namespace NetGears.Database.DataAccessLayer
{
    public class AccountDal : GenericDal<AccountDto>, IAccountDal
    {
        public AccountDto GetByUsername(string username)
        {
            return Get(x => x.Username == username);
        }
    }
}
