using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RISTORANTIS.Data;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace RISTORANTIS.Controllers
{
	public class RestauranteController : Controller
	{
		private readonly RistorantisContext _context;
		private readonly IHostEnvironment _he;

		public RestauranteController(RistorantisContext context, IHostEnvironment e)
		{
			_context = context;
			_he = e;
		}

		public ActionResult Painel()
		{
			//só o restaurante consegue aceder ao seu proprio menu
			if(HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("adminID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			return View();
		}

		[HttpGet]
		public ActionResult Registar()
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("adminID") != null)
			{
				return View("Error403");
			}
			//if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			//{
			//	return View("Error403");
			//}
			return View();
		}

		//Função para registar um novo Restauarente
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Registar([Bind("Telefone,IdRestaurante,DiaDescanso,Horario,EnderecoLocalidade,EnderecoMorada, LocalizacaoGps, EnderecoCodigoPostal, EstadoR")] Restaurante model, int id, IFormFile Fotografia)
		{
			
			if (ModelState.IsValid)
			{
				/*
				string destination = Path.Combine(
					_he.ContentRootPath, "wwwroot/Imagens", Path.GetFileName(model.Fotografia)
					);

				FileStream fs = new FileStream(destination, FileMode.Create);


				Fotografia.CopyTo(fs);
				fs.Close();
				*/

				if(Fotografia.FileName == null)
				{
					ModelState.AddModelError("", "Insira um fotografia!");
					return View("Registar");
				}
				else
				{
					// Para converter a foto para base64
					var ms = new MemoryStream();
					Fotografia.CopyTo(ms);
					var fileBytes = ms.ToArray();
					string foto = Convert.ToBase64String(fileBytes);

					model.Fotografia = foto;

					var errors = ModelState.Select(x => x.Value.Errors)
								   .Where(y => y.Count > 0)
								   .ToList();
				}

				model.IdRestaurante = id;
				_context.Add(model);
				_context.SaveChanges();

				//retorna o numero de linhas da tabela
				var countPedido = _context.PedirRegistos.Count();
				//_context.Database.OpenConnection();
				try
				{
					//_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Pedir_Registo ON");
					//vai acrescentar os dados à tabela do pedidos do registo
					if(countPedido > 0)
					{
						var pedidoID = _context.PedirRegistos.OrderByDescending(r => r.IdPedirRegisto).First();
						var novo_pedidoR = new PedirRegisto()
						{
							IdPedirRegisto = pedidoID.IdPedirRegisto + 1,
							IdRestaurante = model.IdRestaurante,
							DataPedido = DateTime.Now
						};
						_context.Add(novo_pedidoR);
						_context.SaveChanges();
					}
					else if(countPedido == 0)
					{
						var novo_pedidoR = new PedirRegisto()
						{
							IdRestaurante = model.IdRestaurante,
							DataPedido = DateTime.Now
						};
						_context.Add(novo_pedidoR);
						_context.SaveChanges();
					}
					//_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Pedir_Registo OFF");
				}
				catch(Exception ex)
				{

					throw ex;
					//_context.Database.CloseConnection();
				}

				return RedirectToAction("RegistarServico", "SelecionarServico");

			}
			ModelState.AddModelError("", "Todos os campos são de preenchimeno obrigatótio!");
			return View("Registar");
		}

		

		public ActionResult NovoPrato()
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("adminID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			return	RedirectToAction("RegistarNovoPrato", "PratoDoDia");
		}
		
		public ActionResult SelecionarPD()
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("adminID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			return RedirectToAction("ApresentarPD", "PratoDoDia");
		}

		[HttpGet]
		public ActionResult AlterarDadosC()
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("adminID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (HttpContext.Session.GetInt32("restID") == null)
			{
				return NotFound();
			}

			var rest = _context.Utilizadors.Find(HttpContext.Session.GetInt32("restID"));
			if (rest == null)
			{
				return NotFound();
			}
			return View(rest);
		}

		[HttpPost]
		public ActionResult AlterarDadosC(Utilizador utilizador)
		{
			if (HttpContext.Session.GetInt32("restID") != utilizador.IdUtilizador)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					// alteração de dados do restaurante onde se pode alterar um dado caso deseje
					var userName = _context.Utilizadors.Where(u => u.Username == utilizador.Username).Any();
					var password = _context.Utilizadors.Where(u => u.Password == utilizador.Password && u.Username==utilizador.Username).Any();
					var nome = _context.Utilizadors.Where(u => u.Nome == utilizador.Nome).Any();
                    var email = _context.Utilizadors.Where(u => u.Email == utilizador.Email && u.Username == utilizador.Username).Any();

					if (userName == false || password == false || nome == false || email == false)
					{
						_context.Update(utilizador);
						_context.SaveChanges();
						//Utilizador novo = _context.Utilizadors.Where(u => u.Username == utilizador.Username).Single();
						//HttpContext.Session.SetString("novouser", novo.Username);
						HttpContext.Session.SetString("nome", utilizador.Nome);
					}
					else if (userName == true || password == true || nome == true || email == true)
                    {
						return RedirectToAction("Painel", "Restaurante");
					}					
				}
				catch (DbUpdateConcurrencyException)
				{

					if (!UtilizadorExists(utilizador.IdUtilizador))
					{
						return NotFound();
					}
					else
					{
						throw;
					}

				}
				return RedirectToAction("Painel", "Restaurante");
			}
			return View(utilizador);
		}

		//função para editar os dados de utilizador do Tipo Restaurante
		
		//!!!Falta meter a autorizaçao do Admin para as alterações!!!! 
		[HttpGet]
		public ActionResult AlterarDadosR()
		{
			//só o restaurante consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("adminID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (HttpContext.Session.GetInt32("restID") == null)
			{
				return NotFound();
			}

			var rest = _context.Restaurantes.Find(HttpContext.Session.GetInt32("restID"));
			if (rest == null)
			{
				return NotFound();
			}
			return View(rest);
		}

		[HttpPost]
		public ActionResult AlterarDadosR([Bind("Telefone,IdRestaurante,DiaDescanso,Horario,EnderecoLocalidade,EnderecoMorada, LocalizacaoGps, EnderecoCodigoPostal")] Restaurante restaurante, IFormFile Fotografia)
		{
			// Para converter a foto para base64
			var ms = new MemoryStream();
			Fotografia.CopyTo(ms);
			var fileBytes = ms.ToArray();
			string foto = Convert.ToBase64String(fileBytes);

			restaurante.Fotografia = foto;
			//restaurante.Fotografia = Fotografia.FileName;

			if (HttpContext.Session.GetInt32("restID") != restaurante.IdRestaurante)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					/*
						string destination = Path.Combine(
							_he.ContentRootPath, "wwwroot/Imagens", Path.GetFileName(restaurante.Fotografia)
							);

						FileStream fs = new FileStream(destination, FileMode.Create);

						Fotografia.CopyTo(fs);
						fs.Close();
					*/
						_context.Update(restaurante);
						_context.SaveChanges();
				}
				catch (DbUpdateConcurrencyException)
				{

					if (!RestauranteExists(restaurante.IdRestaurante))
					{
						return NotFound();
					}
					else
					{
						throw;
					}

				}
				return RedirectToAction(nameof(Painel));
			}
			return View(restaurante);
		}

		private bool RestauranteExists(int id)
		{
			return _context.Restaurantes.Any(e => e.IdRestaurante == id);
		}

		public bool UtilizadorExists(int id)
		{
			return _context.Utilizadors.Any(e => e.IdUtilizador== id);
		}
	}
}
