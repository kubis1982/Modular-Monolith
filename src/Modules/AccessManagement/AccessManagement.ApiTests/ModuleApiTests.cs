namespace ModularMonolith.Modules.AccessManagement
{
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
    using ModularMonolith.Shared;
    using System.IdentityModel.Tokens.Jwt;
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    [Collection(nameof(WebApplicationFixtureCollection))]
    public class ModuleApiTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ApiTests<ReadDbContext>(webApplicationFixture, testOutputHelper) {
        public UserEntity Administrator { get; set; } = default!;
        
        public override async Task InitializeAsync() {
            await base.InitializeAsync();

            // Użytkownicy
            Administrator = new UserEntity {
                TypeId = User.Administrator.Id.TypeId,
                Email = User.Administrator.Email,
                Id = User.Administrator.Id.Id,
                FirstName = User.Administrator.FullName?.FirstName,
                LastName = User.Administrator.FullName?.LastName,
                MiddleName = User.Administrator.FullName?.MiddleName,
                Password = User.Administrator.Password.Hash,
                IsActive = true
            };
            DbContext.Users.Add(Administrator);
            DbContext.SaveChanges();
            DbContext.Database.ExecuteSqlRaw($"""ALTER SEQUENCE "{ModuleDefinition.MODULE_CODE}"."Users_Id_seq" RESTART WITH 2;""");
        }

        protected static string? GetClaim(string token, string claimName) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
            return claimValue;
        }
    }
}
