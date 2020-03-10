using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FrankForce.Models;
using DataLibrary.BusinessLogic;

namespace FrankForce.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationProcessor _organizationProcessor;
        public OrganizationController(IOrganizationProcessor organizationProcessor)
        {
            _organizationProcessor = organizationProcessor;
        }

        // GET: Organization
        public ActionResult List()
        {
            var data = _organizationProcessor.LoadOrganizations();
            List<OrganizationModel> organizations = new List<OrganizationModel>();
            foreach (var row in data)
            {
                organizations.Add(new OrganizationModel
                {
                    OrganizationId = row.Id,
                    Name = row.Name,
                    Description = row.Description
                });
            }
            return View(organizations);
        }

        // GET: Organization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Organization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Organization/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrganizationModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    int orgId = _organizationProcessor.CreateOrganization(model.Name, model.Description);
                }
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: Organization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Organization/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Organization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Organization/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}