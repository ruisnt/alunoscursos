﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class AlunosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}