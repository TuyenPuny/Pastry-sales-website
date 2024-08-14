using Nhom4_DeTai7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_DeTai7.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            SanPham data = new SanPham();
            data.loadSPM_KM();
            return View(data);
        }
        public ActionResult LienHe()
        {
            return View();
        }

        public ActionResult PhanHoi()
        {
            return View();
        }
    }
}