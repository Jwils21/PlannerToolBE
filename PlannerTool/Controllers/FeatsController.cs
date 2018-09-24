using PlannerTool.Models;
using PlannerTool.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlannerTool.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FeatsController : ApiController
    {

        private PtDbContext db = new PtDbContext();

        //Get-all
        [HttpGet]
        [ActionName("List")]
        public JsonResponse List()
        {
            return new JsonResponse
            {
                Data = db.Feats.ToList()
            };
        }

        //Get-one
        [HttpGet]
        [ActionName("Get")]
        public JsonResponse Feat(int? id)
        {
            if (id == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Id does not exist"
                };
            }
            return new JsonResponse
            {
                Data = db.Feats.Find(id)
            };
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create(Feat feat)
        {
            if (feat == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of Feat"
                };
            }
            if (!ModelState.IsValid)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Model state is Invalid. See Data",
                    Error = ModelState
                };
            }

            db.Feats.Add(feat);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Create successful",
                Data = feat
            };
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change(Feat feat)
        {
            if (feat == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of Feat"
                };
            }
            if (!ModelState.IsValid)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Model state is Invalid. See Data",
                    Error = ModelState
                };
            }

            db.Entry(feat).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Change successful.",
                Data = feat
            };
        }

        //DELETE
        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove(Feat feat)
        {
            if (feat == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires an instance of Feat"
                };
            }
            if (!ModelState.IsValid)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Model state is invalid. See data.",
                    Error = ModelState
                };
            }
            db.Entry(feat).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = feat
            };
        }

        //REMOVE/ID
        [HttpPost]
        [ActionName("RemoveId")]
        public JsonResponse Remove(int? id)
        {
            if (id == null)
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "RemoveId requires a Feat.Id"
                };
            var feat = db.Feats.Find(id);
            if (feat == null)
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = $"No Feats have Id of {id}"
                };
            db.Feats.Remove(feat);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = feat
            };
        }

    }
}
