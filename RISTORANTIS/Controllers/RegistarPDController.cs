using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RISTORANTIS.Data;
using RISTORANTIS.Migrations;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RISTORANTIS.Controllers
{
    public class RegistarPDController : Controller
    {
		private readonly RistorantisContext _context;
		private readonly IHostEnvironment _he;

		public RegistarPDController(RistorantisContext context, IHostEnvironment e)
		{
			_context = context;
			_he = e;
		}

		[HttpGet]
		public ActionResult Editar(int? id)
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
			var prato = _context.Registars.FirstOrDefault(t => t.IdPratoDoDiaNavigation.IdPrato == id);

			if (prato == null)
			{
				return NotFound();
			}
			return View(prato);
        }

        [HttpPost]
		public ActionResult Editar(int id, Registar registar)
		{
            if (id == null)
            {
                return NotFound();
            }

			if (ModelState.IsValid)
			{
				if (registar.DataDisponibilidade.Date < DateTime.Today)
				{
					
					ModelState.AddModelError("", "A data de disponibilidade tem que ser superior à atual.");
					return View("Editar");
				}
                else
                {
					//_context.Update(registar);
					_context.Registars.Where(p => p.IdPratoDoDia == id && p.IdRestaurante == HttpContext.Session.GetInt32("restID")).First().DataDisponibilidade = registar.DataDisponibilidade.Date;
					_context.SaveChanges();
					return RedirectToAction("ApresentarPD", "PratoDoDia");
				}
							
			}
			return View(registar);
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

			return View();
		}

        [HttpPost]
        public ActionResult RegistarNovoPrato([Bind("ID_Registo,DataDisponibilidade,IdPratoDia,IdRestaurante,Descricao,Preco")] Registar model, IFormFile Fotografia)
        {
           
			if (ModelState.IsValid)
            {
				model.IdPratoDoDia = (int)HttpContext.Session.GetInt32("pratoID");
				model.IdRestaurante = (int)HttpContext.Session.GetInt32("restID");
				if(Fotografia.FileName == null)
				{

					ModelState.AddModelError("", "O prato do dia necessita de um fotografia");
					return View("RegistarNovoPrato");
				}
				else
				{
					model.Fotografia = Fotografia.FileName;
					var errors = ModelState.Select(x => x.Value.Errors)
								   .Where(y => y.Count > 0)
								   .ToList();
				}
				if (model.DataDisponibilidade.Date < DateTime.Today)  // a data disponibilidade não pode menor que a data atual
				{
					
					ModelState.AddModelError("", "A data de disponibilidade tem que ser superior à atual.");
					return View("RegistarNovoPrato");
				}
                else
                {
					string destination = Path.Combine(_he.ContentRootPath, "wwwroot/Imagens", Path.GetFileName(model.Fotografia));

					FileStream fs = new FileStream(destination, FileMode.Create);

					Fotografia.CopyTo(fs);
					fs.Close();

					//retorna o numero de linhas da tabela
					var countRegistar = _context.Registars.Count();
					//_context.Database.OpenConnection();
					try
					{
						
						//_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Registar ON");
						if (countRegistar > 0)
						{
							//vai buscar o id do último elemento colocado na tabela
							var registoID = _context.Registars.OrderByDescending(r => r.ID_Registo).First();
							var novo_registo = new Registar()
							{
								ID_Registo = registoID.ID_Registo + 1,
								IdPratoDoDia = model.IdPratoDoDia,
								IdRestaurante = model.IdRestaurante,
								Fotografia = model.Fotografia,
								DataDisponibilidade = model.DataDisponibilidade.Date,
								Descricao = model.Descricao,
								Preco = model.Preco
							};
							_context.Add(novo_registo);
							_context.SaveChanges();
						}
						else if (countRegistar == 0)
						{
							var novo_registo = new Registar()
							{
								IdPratoDoDia = model.IdPratoDoDia,
								IdRestaurante = model.IdRestaurante,
								Fotografia = model.Fotografia,
								DataDisponibilidade = model.DataDisponibilidade.Date,
								Descricao = model.Descricao,
								Preco = model.Preco
							};
							_context.Add(novo_registo);
							_context.SaveChanges();
						}

						//_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Registar OFF");
					}
					catch(Exception ex)
					{
						throw ex;
						//_context.Database.CloseConnection();
					}


					return RedirectToAction("Painel", "Restaurante");
				}	
			}

			ModelState.AddModelError("", "Todos os campos são de preenchimeno obrigatótio!");
			return View("RegistarNovoPrato");
        }
    }
}
