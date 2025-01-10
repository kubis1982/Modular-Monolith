namespace ModularMonolith.Modules.ReadModel {
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Query;

    [ApiController]
    [Produces("application/json")]
    [EnableQuery]
    [Authorize]
    public abstract class ModuleController : ControllerBase {
    }
}
