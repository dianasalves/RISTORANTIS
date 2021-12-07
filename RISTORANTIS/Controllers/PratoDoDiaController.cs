using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RISTORANTIS.Data;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RISTORANTIS.Controllers
{
	public class PratoDoDiaController : Controller
	{
		private readonly RistorantisContext _context;
		private readonly IWebHostEnvironment webHostEnvironment;

		public PratoDoDiaController(RistorantisContext context, IWebHostEnvironment e)
		{
			_context = context;
			webHostEnvironment = e;
		}

		[HttpGet]
		public ActionResult RegistarNovoPrato()
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("ClientID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			//Passo 1 para a criação da ViewBag.
			ViewBag.TipoPratoDoDia = new SelectList(_context.TipoPratoDoDia, "IdTipoP", "NomeTipoP");
			return View();
		}

		[HttpPost]
		public ActionResult RegistarNovoPrato(PratoDoDium prato)
		{
			if (ModelState.IsValid)
			{
				//criar um novo prato
				var novo_prato = new PratoDoDium
				{
					Nome = prato.Nome,
					Tipo = prato.Tipo,
				};
				_context.Add(novo_prato);
				_context.SaveChanges();
				HttpContext.Session.SetInt32("pratoID", novo_prato.IdPrato);
				return RedirectToAction("RegistarNovoPrato", "RegistarPD");
			}
			return View(prato);
		}

		
		public ActionResult ApresentarPD()
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("ClientID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			//vai buscar os id dos pratos que foram registado pelo restaurante que tiver a sessão iniciada
			var pratosRegistados = _context.Registars.Include(e=>e.IdPratoDoDiaNavigation).Where(r => r.IdPratoDoDia == r.IdPratoDoDiaNavigation.IdPrato && r.IdRestaurante == (int)HttpContext.Session.GetInt32("restID"));
			return View(pratosRegistados.ToList());
		}
				

		//funcao que mostra os detalhes dos pratos do dia
		public ActionResult Detalhes(int? id)
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("ClientID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (id == null)
			{
				return NotFound();
			}

			var prato = _context.Registars.FirstOrDefault(e => e.IdPratoDoDia == id);
			if (prato == null)
			{
				return NotFound();
			}

			return View(prato);

			

		}

		[HttpGet]
		public ActionResult Apagar(int? id)
        {
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("ClientID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (id == null)
            {
				return NotFound();
            }
			var prato = _context.PratoDoDia.FirstOrDefault(t=>t.IdPrato == id);

			if(prato == null)
            {
				return NotFound();
            }
			return View(prato);
        }

		[HttpPost, ActionName("Apagar")]
		[ValidateAntiForgeryToken]
		public ActionResult ApagarConfirmacao(int id)
        {
			Registar pratoRegisto = _context.Registars.Where(e => e.IdPratoDoDia == id).Single();
			var pratodoDia = _context.PratoDoDia.Find(id);
			_context.Registars.Remove(pratoRegisto);
			_context.PratoDoDia.Remove(pratodoDia);
			_context.SaveChanges();

			return RedirectToAction("ApresentarPD", "PratoDoDia");
        }
	}
}
