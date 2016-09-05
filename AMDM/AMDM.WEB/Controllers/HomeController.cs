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
using PagedList;
using System.Threading.Tasks;

namespace AMDM.WEB.Controllers
{
    public class HomeController : Controller
    {
        IDataService dataService;
        IPerformerService performerService;
        ISongService songService;
        public HomeController(IDataService dServ, IPerformerService pServ, ISongService sServ)
        {
            dataService = dServ;
            performerService = pServ;
            songService = sServ;
        }

        public ActionResult Index()
        {
            IEnumerable<PerformerDTO> performerDtos = performerService.GetAll();
            IEnumerable<PerformerViewModel> performers = Mapper.Map<IEnumerable<PerformerDTO>, IEnumerable<PerformerViewModel>>(performerDtos);
            return View(performers);
        }

        public ActionResult Performer(int? id, string sortOption, int pageSize = 10, int page = 1)
        {            
            PerformerDTO performerDto = performerService.Get(id);
            PerformerViewModel performer = Mapper.Map<PerformerDTO, PerformerViewModel>(performerDto);
            if (sortOption == null)
            {
                sortOption = "default";
            }
            IEnumerable<SongDTO> songDtos = songService.GetSongsChunkWithOrder(performer.Id, sortOption, page, pageSize);
            IEnumerable<SongViewModel> songs = Mapper.Map<IEnumerable<SongDTO>, IEnumerable<SongViewModel>>(songDtos);
            int songsCount = songService.GetSongsCount(performer.Id);
            var pagedList = new StaticPagedList<SongViewModel>(songs, page, pageSize, songsCount);
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            ViewBag.pagedList = pagedList;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("PartialSongList", pagedList)
                : View(performer);
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

        public ActionResult ClearCache()
        {
            dataService.ClearCache();
                return RedirectToAction("Index");
        }

    }
}