using NoSolo.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using NoSolo.Abstractions.Data;

namespace NoSolo.Presentation.Authorization;

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
            var user = await unitOfWork.UserRepository.GetUserByUsernameWithAllIncludesAsync(context.User.GetUsername());
            if (user.UserProfile != null)
            {
                context.Succeed(requirement);
            }
        }
    }
}