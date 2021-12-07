using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RISTORANTIS.Data;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RISTORANTIS.Controllers
{
	public class ClientesController : Controller
	{
		public virtual List<SelecionarRFavorito> fav { get; set; }
		private readonly RistorantisContext _context;
		private readonly IHostEnvironment _he;

		public ClientesController(RistorantisContext context, IHostEnvironment e)
		{
			_context = context;
			_he = e;
		}

		//função que irá apresentar o Menu inicial 
		public ActionResult Painel()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			return View();
		}

		public ActionResult Favoritos()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			return View();
		}


		//função que irá apresentar os Favoritos 
		//[HttpGet]
		//public ActionResult Favoritos()
		//{
		//	return View();
		//}

		//[HttpPost]
		//public ActionResult Favoritos()
		//{
		//          SelecionarRFavorito fav = _context.SelecionarRFavoritos.FirstOrDefault(f => f.IdRestaurante == (int)HttpContext.Session.GetInt32("restID"));

		//          if (fav != null)
		//          {
		//              SelecionarRFavorito favorite = _context.SelecionarRFavoritos.FirstOrDefault(u => u.IdRestaurante == u.IdRestauranteNavigation.IdRestaurante && u.IdCliente == u.IdClienteNavigation.IdCliente);

		//              if (fav != null)
		//              {
		//                  var favoritinho = new SelecionarRFavorito
		//                  {
		//                      IdCliente = (int)HttpContext.Session.GetInt32("client"),
		//                      IdRestaurante = (int)HttpContext.Session.GetInt32("rest"),
		//                  };
		//                  _context.Add(favoritinho);
		//                  _context.SaveChanges();
		//              }
		//              else
		//              {
		//                  // if this is already favorited, unfavorite it (delete)
		//                  _context.Remove(favorite);
		//                  _context.SaveChanges();
		//              }

		//              //once done favorite or unfave, redirect
		//              return RedirectToAction("Favoritos");
		//          }
		//          else
		//          {
		//              return NotFound();
		//          }


		//}

		public ActionResult Servicos(int? id)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			//mostra os tipos serviços que o restaurante tem:
			var tiposS = _context.SelecionarServicos.Include(s => s.IdRestauranteNavigation).Include(s => s.IdServicoNavigation)
													.Where(s => s.IdRestaurante == id);
			return View(tiposS.ToList());
		}


		//função que apresenta os restaurantes selecionados como favoritos
		public ActionResult FavoritosR()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			////condiçoes onde só aparece os utilizadores que sao restaurantes
			//////var restaurantes = _context.Utilizadors.Include(e => e.Restaurante).Where(e => e.IdUtilizador == e.Restaurante.IdRestaurante && e.Restaurante.EstadoR == "Registado" && e.Estado == "Registado");
			////var localidades = 
			//return View(restaurantes.ToList());
			//vai buscar os restaurantes selecionados como favoritos do restaurante
			var selecionadoFavoritoR = _context.SelecionarRFavoritos.Include(s=>s.IdRestauranteNavigation.IdRestauranteNavigation).Include(s=>s.IdClienteNavigation.IdClienteNavigation).Where(e => e.IdCliente == (int)HttpContext.Session.GetInt32("ClientID"));

            return View(selecionadoFavoritoR.ToList());
		}

		//função que apresenta os pratos do dia selecionados como favoritos
		public ActionResult FavoritosPD()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			//vai buscar os pratos do dia selecionados como favoritos do restaurante
			var selecionadoFavoritoPD = _context.SelecionarPFavoritos.Include(s => s.IdClienteNavigation.IdClienteNavigation).Include(s => s.IdPratoNavigation).Where(s => s.IdCliente == (int)HttpContext.Session.GetInt32("ClientID"));

			return View(selecionadoFavoritoPD.ToList());
		}

        public ActionResult SelecionarFavoritoR(int id)
        {
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (ModelState.IsValid)
            {
                var restFavorito = _context.SelecionarRFavoritos.Include(e => e.IdRestauranteNavigation).Include(e => e.IdClienteNavigation)
                                        .Where(s => s.IdClienteNavigation.IdClienteNavigation.IdUtilizador == (int)HttpContext.Session.GetInt32("ClientID") && s.IdRestauranteNavigation.IdRestauranteNavigation.Restaurante.IdRestaurante == id).Any();
                var myStream = new byte[0];

                if (restFavorito == false)  // para  favoritar
                {
                    var novo_favorito = new SelecionarRFavorito()
                    {
                        IdCliente = (int)HttpContext.Session.GetInt32("ClientID"),
                        IdRestaurante = id,
                        NotificacaoR = true,
                    };
                    _context.AddRange(novo_favorito);
                    _context.SaveChanges();


                    //string favourite_yes = Path.Combine(_he.ContentRootPath, "wwwroot/Icon/favourite_yes.png");

                    //myStream = System.IO.File.ReadAllBytes(favourite_yes);

                    //return File(myStream, "image/png");

                    return RedirectToAction("VerRestaurantes", "Clientes");
                }
                else if (restFavorito == true)  //para desfavoritar
                {

                    var favoritos = _context.SelecionarRFavoritos.Include(e => e.IdRestauranteNavigation.IdRestauranteNavigation.Restaurante).Include(e => e.IdClienteNavigation.IdClienteNavigation)
                                        .Where(s => s.IdClienteNavigation.IdClienteNavigation.IdUtilizador == (int)HttpContext.Session.GetInt32("ClientID") && s.IdRestauranteNavigation.IdRestauranteNavigation.Restaurante.IdRestaurante == id);

                    _context.RemoveRange(favoritos);
                    _context.SaveChanges();

                    //string favourite_yes = Path.Combine(_he.ContentRootPath, "wwwroot/Icon/favourite_no.png");

                    //myStream = System.IO.File.ReadAllBytes(favourite_yes);

                    //return File(myStream, "image/png");

                    return RedirectToAction("VerRestaurantes", "Clientes");

                }

            }
            return View("VerRestaurantes");
        }

		public ActionResult SelecionarFavoritoPD(int id)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}

			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (ModelState.IsValid)
			{
				var pdFavorito = _context.SelecionarPFavoritos.Include(e => e.IdPratoNavigation).Include(e => e.IdClienteNavigation)
										.Where(s => s.IdClienteNavigation.IdClienteNavigation.IdUtilizador == (int)HttpContext.Session.GetInt32("ClientID") && s.IdPratoNavigation.IdPrato == id).Any();
				var myStream = new byte[0];

				if (pdFavorito == false)  // para  favoritar
				{
					var novo_favorito = new SelecionarPFavorito()
					{
						IdCliente = (int)HttpContext.Session.GetInt32("ClientID"),
						IdPrato = id,
						NotificacaoP = true,
						//RegistarDataDisponibilidade = new DateTime(2021,01,01,00,00,00),
						//RegistarIdPratoDoDia = 0,
						//RegistarIdRestaurante = 0
					};
					_context.AddRange(novo_favorito);
					_context.SaveChanges();


					//string favourite_yes = Path.Combine(_he.ContentRootPath, "wwwroot/Icon/favourite_yes.png");

					//myStream = System.IO.File.ReadAllBytes(favourite_yes);

					//return File(myStream, "image/png");

					return RedirectToAction("VerPratosDoDia", "Clientes");
				}
				else if (pdFavorito == true)  //para desfavoritar
				{

					var favoritos = _context.SelecionarPFavoritos.Include(e => e.IdPratoNavigation.Registars).Include(e => e.IdClienteNavigation.IdClienteNavigation)
										.Where(s => s.IdClienteNavigation.IdClienteNavigation.IdUtilizador == (int)HttpContext.Session.GetInt32("ClientID") && s.IdPratoNavigation.IdPrato == id);

					_context.RemoveRange(favoritos);
					_context.SaveChanges();

					//string favourite_yes = Path.Combine(_he.ContentRootPath, "wwwroot/Icon/favourite_no.png");

					//myStream = System.IO.File.ReadAllBytes(favourite_yes);

					//return File(myStream, "image/png");

					return RedirectToAction("VerPratosDoDia", "Clientes");

				}

			}
			return View("VerPratosDoDia");
		}


		//função que irá apresentar as notificacoes 
		public ActionResult Notificacoes()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			return View();
		}

		//funçao onde apresenta todos os restaurantes
		public ActionResult VerRestaurantes()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			//condiçoes onde só aparece os utilizadores que sao restaurantes
			var restaurantes = _context.Restaurantes.Include(e => e.SelecionarRFavoritos.Where(x => x.IdClienteNavigation.IdCliente == HttpContext.Session.GetInt32("ClientID"))).Where(x => x.EstadoR == "Registado").Include(y=>y.IdRestauranteNavigation);

			return View(restaurantes);
		}

		//funçao onde apresenta todos os pratos do dia
		public ActionResult VerPratosDoDia()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			//condiçoes onde só aparece os utilizadores que sao restaurantes
			var pratosRegistados = _context.Registars.Include(e=>e.IdRestauranteNavigation).Include(e => e.IdPratoDoDiaNavigation).Where(r => r.IdPratoDoDia == r.IdPratoDoDiaNavigation.IdPrato).Include(d => d.SelecionarPFavoritos.Where(c => c.IdClienteNavigation.IdCliente == HttpContext.Session.GetInt32("ClientID")));

			return View(pratosRegistados);
		}

		//função que lista todos os detalhes de um prato do dia
		public ActionResult Detalhes1(int? id)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
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

			var pratododia = _context.Registars.Include(e=>e.IdRestauranteNavigation).Include(e=>e.IdRestauranteNavigation.IdRestauranteNavigation).FirstOrDefault(e => e.IdPratoDoDia == id);
			if (pratododia == null)
			{
				return NotFound();
			}

			return View(pratododia);
		}

		//função que lista todos os detalhes de um prato do dia nos favoritos
		public ActionResult Detalhes2(int? id)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
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

			var pratododia = _context.Registars.Include(e => e.IdRestauranteNavigation).Include(e => e.IdRestauranteNavigation.IdRestauranteNavigation).FirstOrDefault(e => e.IdPratoDoDia == id);
			if (pratododia == null)
			{
				return NotFound();
			}

			return View(pratododia);
		}

		//função que lista todos os detalhes de um prato do dia
		public ActionResult Detalhes3(int? id)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
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

			var pratododia = _context.Registars.Include(e => e.IdRestauranteNavigation).Include(e => e.IdRestauranteNavigation.IdRestauranteNavigation).FirstOrDefault(e => e.IdPratoDoDia == id);
			if (pratododia == null)
			{
				return NotFound();
			}

			return View(pratododia);
		}

		//função que lista todos os detalhes de um restaurante
		public ActionResult Detalhes4(int? id)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
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

			var restaurante = _context.Restaurantes.FirstOrDefault(e => e.IdRestaurante == id);
			if (restaurante == null)
			{
				return NotFound();
			}

			return View(restaurante);

		}

		//função que lista todos os detalhes de um restaurante
		public ActionResult Detalhes(int? id)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
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

			var restaurante = _context.Restaurantes.FirstOrDefault(e => e.IdRestaurante == id);
			if (restaurante == null)
			{
				return NotFound();
			}

			return View(restaurante);
		}

		public ActionResult Pesquisa(string searchString)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			var restaurantes = _context.Restaurantes.Include(e => e.SelecionarRFavoritos.Where(x => x.IdClienteNavigation.IdCliente == HttpContext.Session.GetInt32("ClientID"))).Where(x => x.EstadoR == "Registado").Include(y => y.IdRestauranteNavigation); ;

			if (!String.IsNullOrEmpty(searchString))
			{
				return View("VerRestaurantes");
			}

			return View("VerRestaurantes", restaurantes.ToList());
		}

		public ActionResult PesquisaPD(string searchString)
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			var pratos = _context.Registars.Include(e=>e.IdPratoDoDiaNavigation).Where(r => r.IdPratoDoDiaNavigation.Nome == searchString);

			if (!String.IsNullOrEmpty(searchString))
			{
				return View("VerPratosDoDia");
			}

			return View("VerPratosDoDia", pratos.ToList());
		}

		//função para editar os dados de utilizador
		[HttpGet]
		public ActionResult AlterarDados()
		{
			//só o cliente consegue aceder a esta página
			if (HttpContext.Session.GetInt32("adminID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (HttpContext.Session.GetInt32("ClientID") == null)
			{
				return NotFound();
			}

			//vai procurar na BD pelos dados do id do utilizador
			var client = _context.Utilizadors.Find(HttpContext.Session.GetInt32("ClientID"));
			if(client == null)
			{
				return NotFound();
			}
			return View(client);
		}

		[HttpPost]
		public ActionResult AlterarDados(Utilizador utilizador)
		{
			if(HttpContext.Session.GetInt32("ClientID") != utilizador.IdUtilizador)
			{
				return NotFound();
			}

			if(ModelState.IsValid)
			{
				try
				{
					// alteração de dados do cliente onde se pode alterar um dado caso deseje
					var userName = _context.Utilizadors.Where(u => u.Username == utilizador.Username).Any();
					var password = _context.Utilizadors.Where(u => u.Password == utilizador.Password && u.Username == utilizador.Username).Any();
					var nome = _context.Utilizadors.Where(u => u.Nome == utilizador.Nome).Any();
					var email = _context.Utilizadors.Where(u => u.Email == utilizador.Email).Any();

					if (userName == false || password == false || nome == false || email == false)
					{
						_context.Update(utilizador);
						_context.SaveChanges();
						HttpContext.Session.SetString("client", utilizador.Username);
						HttpContext.Session.SetString("nome", utilizador.Nome);
					}
					else if (userName == true || password == true || nome == true || email == true)
					{
					
						ModelState.AddModelError("Username", "Dados já existentes.");
						return View("AlterarDados");
						//return RedirectToAction("AlterarDados", "Clientes");
					}
				}
				catch(DbUpdateConcurrencyException)
				{

					if(!UtilizadorExists(utilizador.IdUtilizador))
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
			return View(utilizador);
		}

		private bool UtilizadorExists(int id)
		{
			return _context.Utilizadors.Any(e => e.IdUtilizador == id);
		}

		
	}
}
