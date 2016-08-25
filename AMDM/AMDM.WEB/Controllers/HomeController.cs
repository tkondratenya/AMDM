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
            return View();
        }

        public ActionResult Parse()
        {
            amdmService.ParseAmdm();
            return RedirectToAction("Index");
        }

        public ActionResult ShowSongs()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<SongDTO, SongViewModel>();
            });
            var songs = Mapper.Map<IEnumerable<SongDTO>, List<SongViewModel>>(amdmService.GetSongs());
            return View(songs);
        }

    }
}