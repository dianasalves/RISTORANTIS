using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RISTORANTIS.Data;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RISTORANTIS.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly RistorantisContext _context;
		public HomeController(RistorantisContext context)
		{
			_context = context;
		}

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Inicio()
		{
			return View();
		}

		public async Task<IActionResult> Pesquisa(string searchString)
		{
			var restaurantes = _context.Utilizadors.Include(e => e.Restaurante).Where(e => e.IdUtilizador == e.Restaurante.IdRestaurante && e.Restaurante.EstadoR == "Registado");

			if (!String.IsNullOrEmpty(searchString))
			{
				restaurantes = restaurantes.Where(s => s.Nome.Contains(searchString));
			}

			return View(await restaurantes.ToListAsync());
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
