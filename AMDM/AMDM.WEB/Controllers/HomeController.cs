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
        IAmdmService amdmService;
        public HomeController(IAmdmService serv)
        {
            amdmService = serv;
        }

        public ActionResult Index()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PerformerDTO, PerformerViewModel>().MaxDepth(3);
                cfg.CreateMap<SongDTO, SongViewModel>().MaxDepth(3);
                cfg.CreateMap<ChordDTO, ChordViewModel>().MaxDepth(3);
            });
            var mapper = config.CreateMapper();
            IEnumerable<PerformerDTO> performerDtos = amdmService.GetPerformers();
            List<PerformerViewModel> performers = mapper.Map<IEnumerable<PerformerDTO>, List<PerformerViewModel>>(performerDtos);
            return View(performers);
        }
        [HttpGet]
        public ActionResult Parse()
        {
            amdmService.ParseAmdm();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAllData()
        {
            amdmService.DeleteAllData();
            return RedirectToAction("Index");
        }

    }
}