﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webbandienthoai.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult XemGioHang()
        {
            return View();
        }
        public ActionResult GioHangIcon()
        {
            return PartialView();
        }
    }
}