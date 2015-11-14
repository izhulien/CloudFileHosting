using CFH.Data;
using CFH.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
