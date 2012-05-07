﻿using System.Web.Mvc;
using DomainModels;
using Microsoft.Practices.ServiceLocation;
using Zephyr.Data.Repository.Contract;
using Zephyr.Data.UnitOfWork;
using Zephyr.Web.Mvc.Controllers;
using Zephyr.Web.Mvc.ViewModels;

namespace StoreModule.Controllers
{
    public class PublisherController : ZephyrCRUDController<Publisher>
    {
        public PublisherController(IRepository<Publisher> repository) : base(repository)
        {

        }        

        [HttpPost]
        public ActionResult Edit(Publisher publisher)
        { 
            if(ModelState.IsValid)
            {
                //always use Unit of work for save/update
                using (UnitOfWorkScope.Start())
                {
                    var repo = ServiceLocator.Current.GetInstance<IRepository<Publisher>>();

                    repo.SaveOrUpdate(publisher);
                    return RedirectToAction("List");
                } 
            }

            return View("Edit", new EditViewModel<Publisher>(){Model = publisher});
        }        
    }
}
