using CFH.Data.Contracts;
using System.Web.Http;

namespace CFH.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        public BaseController(ICFHData data)
        {
            this.Data = data;
        }

        protected ICFHData Data { get; set; }

    }
}
