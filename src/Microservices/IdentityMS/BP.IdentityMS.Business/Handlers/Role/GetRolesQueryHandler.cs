using BP.IdentityMS.Business.Queries.Role;
using BP.Utils.Helpers;
using BP.Utils.Models.EnumOption;
using MediatR;

namespace BP.IdentityMS.Business.Handlers.Role
{
    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<EnumOptionViewModel>>
    {
        public Task<IEnumerable<EnumOptionViewModel>> Handle(GetRolesQuery query, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            return Task.FromResult(EnumHelper.GetEnumOptions<BP.IdentityMS.Business.Enums.Role>());
        }
    }
}
