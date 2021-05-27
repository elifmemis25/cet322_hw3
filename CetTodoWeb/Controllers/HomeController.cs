﻿using CetTodoWeb.Data;
using CetTodoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CetTodoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var query = dbContext.TodoItems // from todoItems
                .Include(t=>t.Category) //inner join categories c on t.categoryId = c.Id
                .Where(t => !t.IsCompleted) // where isCompleted=0
                .OrderBy(t => t.DueDate) // order by dueDate
                .Take(3); // top 3
                          //select top 3 * from todoItems t inner join categories c on t.categoryId = c.Id
                          //where isCompleted=0 order by dueDate
            List<TodoItem> result =await query.ToListAsync();
            //List<TodoItem> result2 =  query.ToList();

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
