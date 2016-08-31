using AMDM.BLL.Interfaces;
using AMDM.BLL.DTO;
using AMDM.WEB.Models;
using AMDM.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;

namespace AMDM.WEB.Controllers
{
    public class HomeController : Controller
    {
        IDataService dataService;
        IModelService<PerformerDTO> performerService;
        public HomeController(IDataService dServ, IModelService<PerformerDTO> pServ)
        {
            dataService = dServ;
            performerService = pServ;
        }

        public ActionResult Index()
        {
            IEnumerable<PerformerDTO> performerDtos = performerService.GetAll();
            IEnumerable<PerformerViewModel> performers = Mapper.Map<IEnumerable<PerformerDTO>, IEnumerable<PerformerViewModel>>(performerDtos);
            return View(performers);
            return View();
        }

        [HttpGet]
        public ActionResult Performer(int? id)
        { 
            PerformerDTO performerDto = performerService.Get(id);
            PerformerViewModel performer = Mapper.Map<PerformerDTO, PerformerViewModel>(performerDto);
            return View(performer);
        }

        [HttpGet]
        public ActionResult ParseSongs()
        {
            dataService.StoreParsedSongs();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ParsePerformers()
        {
            dataService.StoreParsedPerformers();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAllData()
        {
            dataService.DeleteAllData();
            return RedirectToAction("Index");
        }

    }
}