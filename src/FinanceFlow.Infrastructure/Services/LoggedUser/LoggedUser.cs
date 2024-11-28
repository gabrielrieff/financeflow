using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Infrastructure.DataAccess;
using FinanceFlow.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceFlow.Domain.Security.Tokens;

namespace FinanceFlow.Infrastructure.Services.LoggedUser;

internal class LoggedUser : ILoggedUser
{
    private readonly FinanceFlowDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(FinanceFlowDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
