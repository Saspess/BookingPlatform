using BP.Utils.Models.EnumOption;
using MediatR;

namespace BP.IdentityMS.Business.Queries.Role
{
    public class GetRolesQuery : IRequest<IEnumerable<EnumOptionViewModel>>
    {
    }
}
