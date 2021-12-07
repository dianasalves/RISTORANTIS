using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RISTORANTIS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RISTORANTIS.Controllers
{
	public class VisitanteController : Controller
	{
		private readonly RistorantisContext _context;
		public VisitanteController(RistorantisContext context)
		{
			_context = context;
		}

		//funçao onde no inicio aparece os restaurantes registados
		public ActionResult PageInicial()
		{
			//condiçoes onde só aparece os utilizadores que sao restaurantes
			var restaurantes = _context.Utilizadors.Include(e => e.Restaurante).Where(e => e.IdUtilizador == e.Restaurante.IdRestaurante && e.Restaurante.EstadoR == "Registado" && e.Estado=="Registado" );
			//var localidades = 
			return View(restaurantes.ToList());
		}

		//funçao onde é listado todos os detalhes de um restaurante
		public ActionResult Detalhes(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var restaurante = _context.Restaurantes.FirstOrDefault(e => e.IdRestaurante == id);
			if (restaurante == null)
			{
				return NotFound();
			}

			return View(restaurante);

		}

		public ActionResult Pesquisa(string searchString)
		{
			var restaurantes = _context.Utilizadors.Include(e => e.Restaurante).Where(e => e.IdUtilizador == e.Restaurante.IdRestaurante && e.Restaurante.EstadoR == "Registado");
			
			if (!String.IsNullOrEmpty(searchString))
			{
				restaurantes = restaurantes.Where(s => s.Nome.Contains(searchString));
			}

			return View("PageInicial", restaurantes.ToList());
			//return RedirectToAction("PageInicial", "Visitante");
		}

		public ActionResult SelecionarFavoritoR()
		{
			return RedirectToAction("Login", "Accounts");
		}

		public ActionResult SelecionarFavoritoP()
		{
			return RedirectToAction("Login", "Accounts");
		}

		public ActionResult PratoDoDia(int? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			//var restaurante = _context.Restaurantes.FirstOrDefault(e => e.IdRestaurante == id);
			var pratosRegistados = _context.Registars.Include(e => e.IdPratoDoDiaNavigation).Where(r => r.IdPratoDoDia == r.IdPratoDoDiaNavigation.IdPrato && r.IdRestaurante == id && r.DataDisponibilidade.Date==DateTime.Today);
			return View(pratosRegistados.ToList());
        }

		public ActionResult Servicos(int? id)
		{
			//mostra os tipos serviços que o restaurante tem:
			var tiposS = _context.SelecionarServicos.Include(s => s.IdRestauranteNavigation).Include(s => s.IdServicoNavigation)
													.Where(s => s.IdRestaurante == id);
			return View(tiposS.ToList());
		}
	}
}
