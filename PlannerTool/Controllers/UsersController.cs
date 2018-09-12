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
    public class UsersController : ApiController
    {
        private PtDbContext db = new PtDbContext();

        //Get-all
        [HttpGet]
        [ActionName("List")]
        public JsonResponse List()
        {
            return new JsonResponse
            {
                Data = db.Users.ToList()
            };
        }

        //Get-one
        [HttpGet]
        [ActionName("Get")]
        public JsonResponse User(int? id) {
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
                Data = db.Users.Find(id)
            };
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create(User user)
        {
            if (user == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of User"
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

            db.Users.Add(user);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Create successful",
                Data = user
            };
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change(User user)
        {
            if (user == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires instance of User"
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

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Change successful.",
                Data = user
            };
        }

        //DELETE
        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove(User user)
        {
            if (user == null)
            {
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = "Create requires an instance of User"
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
            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = user
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
                    Message = "RemoveId requires a User.Id"
                };
            var user = db.Users.Find(id);
            if (user == null)
                return new JsonResponse
                {
                    Result = "Failed",
                    Message = $"No Users have Id of {id}"
                };
            db.Users.Remove(user);
            db.SaveChanges();
            return new JsonResponse
            {
                Message = "Remove successful.",
                Data = user
            };
        }
    }
}
