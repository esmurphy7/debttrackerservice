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
    public class FriendController : TableController<Friend>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            debttrackerContext context = new debttrackerContext();
            DomainManager = new EntityDomainManager<Friend>(context, Request, Services);
        }

        // GET tables/Friend
        public IQueryable<Friend> GetAllFriend()
        {
            return Query(); 
        }

        // GET tables/Friend/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Friend> GetFriend(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Friend/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Friend> PatchFriend(string id, Delta<Friend> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Friend
        public async Task<IHttpActionResult> PostFriend(Friend item)
        {
            Friend current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Friend/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFriend(string id)
        {
             return DeleteAsync(id);
        }

    }
}