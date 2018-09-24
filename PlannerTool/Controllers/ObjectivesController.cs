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
    public class ObjectivesController : ApiController
    {

        private PtDbContext db = new PtDbContext();

        //Get-all
        [HttpGet]
        [ActionName("List")]
        public JsonResponse List()
        {
            return new JsonResponse
            {
                Data = db.Objectives.ToList()
            };
        }

        //Get-one
        [HttpGet]
        [ActionName("Get")]
        public JsonResponse Objective(int? id)
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
                Data = db.Objectives.Find(id)
            };
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create(Objective objective)
        {
            if (objective == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of Objective"
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

            db.Objectives.Add(objective);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Create successful",
                Data = objective
            };
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change(Objective objective)
        {
            if (objective == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of Objective"
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

            db.Entry(objective).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Change successful.",
                Data = objective
            };
        }

        //DELETE
        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove(Objective objective)
        {
            if (objective == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires an instance of Objective"
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
            db.Entry(objective).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = objective
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
                    Message = "RemoveId requires a Objective.Id"
                };
            var objective = db.Objectives.Find(id);
            if (objective == null)
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = $"No Objectives have Id of {id}"
                };
            db.Objectives.Remove(objective);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = objective
            };

        }
    }
}
