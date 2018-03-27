using NetGears.Database.Entities;

namespace NetGears.Database.Repositories
{
    public class AccountRepository : Repository<NetGearsContext, Account>
    {
        public AccountRepository(NetGearsContext context) : base(context) { }
    }
}