using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using debttrackerService.DataObjects;
using debttrackerService.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace debttrackerService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class DebtController : TableController<Debt>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            debttrackerContext context = new debttrackerContext();
            DomainManager = new EntityDomainManager<Debt>(context, Request, Services);
        }

        // GET tables/Debt
        public IQueryable<Debt> GetAllDebt()
        {
            return Query(); 
        }

        // GET tables/Debt/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Debt> GetDebt(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Debt/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Debt> PatchDebt(string id, Delta<Debt> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Debt
        public async Task<IHttpActionResult> PostDebt(Debt item)
        {
            Debt current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Debt/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDebt(string id)
        {
             return DeleteAsync(id);
        }

    }
}