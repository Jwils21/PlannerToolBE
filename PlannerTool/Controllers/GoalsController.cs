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
    public class GoalsController : ApiController
    {

        private PtDbContext db = new PtDbContext();

        //Get-all
        [HttpGet]
        [ActionName("List")]
        public JsonResponse List()
        {
            return new JsonResponse
            {
                Data = db.Goals.ToList()
            };
        }

        //Get-one
        [HttpGet]
        [ActionName("Get")]
        public JsonResponse Goal (int? id)
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
                Data = db.Goals.Find(id)
            };
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create(Goal goal)
        {
            if (goal == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of Goal"
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

            db.Goals.Add(goal);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Create successful",
                Data = goal
            };
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change(Goal goal)
        {
            if (goal == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of Goal"
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

            db.Entry(goal).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Change successful.",
                Data = goal
            };
        }

        //DELETE
        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove(Goal goal)
        {
            if (goal == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires an instance of Goal"
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
            db.Entry(goal).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = goal
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
                    Message = "RemoveId requires a Goal.Id"
                };
            var goal = db.Goals.Find(id);
            if (goal == null)
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = $"No Goals have Id of {id}"
                };
            db.Goals.Remove(goal);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = goal
            };
        }


    }
}
