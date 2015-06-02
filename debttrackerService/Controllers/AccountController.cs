using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using debttrackerService.DataObjects;
using debttrackerService.Models;

namespace debttrackerService.Controllers
{
    public class AccountController : TableController<Account>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            debttrackerContext context = new debttrackerContext();
            DomainManager = new EntityDomainManager<Account>(context, Request, Services);
        }

        // GET tables/Account
        public IQueryable<Account> GetAllAccount()
        {
            return Query(); 
        }

        // GET tables/Account/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Account> GetAccount(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Account/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Account> PatchAccount(string id, Delta<Account> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Account
        public async Task<IHttpActionResult> PostAccount(Account item)
        {
            Account current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Account/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAccount(string id)
        {
             return DeleteAsync(id);
        }

    }
}