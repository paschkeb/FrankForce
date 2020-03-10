using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FrankForce.Models;
using FrankForce.Functions;
using DataLibrary.BusinessLogic;

namespace FrankForce.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserProcessor _userProcessor;
        private readonly ICallEmailFunction _callEmailFunction;
        public UserController(IUserProcessor userProcessor, ICallEmailFunction callEmailFunction)
        {
            _userProcessor = userProcessor;
            _callEmailFunction = callEmailFunction;
        }

        // GET: User
        public ActionResult List(int? organizationId = null)
        {
            var data = _userProcessor.LoadUsers(organizationId);
            List<UserModel> users = new List<UserModel>();
            foreach (var row in data)
            {
                users.Add(new UserModel
                {
                    UserId = row.Id,
                    OrganizationId = row.OrganizationId,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    EmailAddress = row.EmailAddress,
                    ConfirmEmail = row.EmailAddress,
                    PhoneNumber = row.PhoneNumber
                });
            }
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create(int organizationId)
        {            
            return View(new UserModel { OrganizationId = organizationId });
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model, int organizationId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = _userProcessor.CreateUser(organizationId, model.FirstName, model.LastName, model.EmailAddress, model.PhoneNumber);
                    if (userId!=0) {
                        var userValues = new DataLibrary.Models.UserModel
                        {
                            Id = userId,
                            OrganizationId = model.OrganizationId,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            EmailAddress = model.EmailAddress,
                            PhoneNumber = model.PhoneNumber
                        };
                        _callEmailFunction.CallEmailFunctionApp(userValues).Wait();
                    }
                }

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(UserModel model)
        {
            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userProcessor.UpdateUser(model.UserId, model.OrganizationId, model.FirstName, model.LastName, model.EmailAddress, model.PhoneNumber);
                }
                return RedirectToAction("List", "Organization", model.OrganizationId);
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(UserModel model)
        {
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int UserId, UserModel model)
        {
            try
            {
                _userProcessor.DeleteUser(UserId);

                return RedirectToAction("List", "Organization", model.OrganizationId);
            }
            catch
            {
                return View();
            }
        }
    }
}