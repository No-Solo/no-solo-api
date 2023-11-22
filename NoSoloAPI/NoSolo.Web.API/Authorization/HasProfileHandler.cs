using Microsoft.AspNetCore.Authorization;
using NoSolo.Abstractions.Data.Data;
using NoSolo.Web.API.Extensions;

namespace NoSolo.Web.API.Authorization;

public class HasProfileHandler : AuthorizationHandler<HasProfileRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public HasProfileHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HasProfileRequirement requirement)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
            var user = await unitOfWork.UserRepository.GetUserByEmailWithAllIncludesAsync(context.User.GetEmail());
            if (user.UserProfile != null)
            {
                context.Succeed(requirement);
            }
        }
    }
}