using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RISTORANTIS.Data;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static RISTORANTIS.Models.TipoServico;

namespace RISTORANTIS.Controllers
{
    public class SelecionarServicoController : Controller
    {
       
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RistorantisContext _context;
 
        public SelecionarServicoController(RistorantisContext context, IWebHostEnvironment e)
        {
            _context = context;
            webHostEnvironment = e;
        }

        [HttpGet]
        public IActionResult RegistarServico()
        {
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("ClientID") != null)
			{
				return View("Error403");
			}
			//if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			//{
			//	return View("Error403");
			//}

			TipoServico tipoServico = new TipoServico();
          
            tipoServico.ts = _context.TipoServicos.ToList<TipoServico>();
         
            return View(tipoServico);
        }

        [HttpPost]
        public ActionResult RegistarServico(TipoServico tp, SelecionarServico s)
        {
			var Selecao = tp.ts.Where(x => x.Selecionado == true).ToList<TipoServico>();
			var countSelecao = Selecao.Count();
			//vai buscar o id do ultimo restaurante registado
			var rest = _context.Restaurantes.OrderByDescending(p=>p.IdRestaurante).Select(p=>p.IdRestaurante).First();

			if (countSelecao == 0)
			{
				//ViewBag.Message = "Selecione pelo menos um serviço!";
				//função sleep
				ModelState.AddModelError("", "Selecione pelo menos um serviço!");
				Thread.Sleep(10000);
				return View("RegistarServico");
			}
			else if (countSelecao >= 1)
			{
				//vai buscar os id´s dos itens selecionados para um lista
				var selecao1 = tp.ts.Where(u => u.Selecionado == true).Select(u => u.IdServico).ToList();

					//para cada id de serviço escolhido vai registar na tabela juntamente com o id do restaurante que o escolheu
					foreach(var sel in selecao1)
					{
						var selecionar = new SelecionarServico()
						{
							IdServico = sel,
							IdRestaurante = rest
						};

						_context.Add(selecionar);
						_context.SaveChanges();
					}
				

				//ViewBag.Message = "Foi guardado com sucesso!";
				ModelState.AddModelError("", "Foi guardado com sucesso!");
				//função sleep
				Thread.Sleep(5000);
				return RedirectToAction("Login", "Accounts");
			}


			return View();
        }
    }

}



