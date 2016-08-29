﻿using AMDM.BLL.Interfaces;
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
            amdmService.DatabaseCheck();
            IEnumerable<PerformerDTO> performerDtos = amdmService.GetPerformers();
            List<PerformerViewModel> performers = Mapper.Map<IEnumerable<PerformerDTO>, List<PerformerViewModel>>(performerDtos);
            return View(performers);
        }

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