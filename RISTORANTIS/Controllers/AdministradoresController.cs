using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RISTORANTIS.Data;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RISTORANTIS.Controllers
{
	public class AdministradoresController : Controller
	{
		private readonly RistorantisContext _context;
		public AdministradoresController(RistorantisContext context)
		{
			_context = context;
		}

		//função que apresenta o Menu Inicial
		public ActionResult Painel()
		{
			//só o admin consegue aceder ao seu proprio menu
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if(HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			return View();
		}

		//funçáo que mostra os administradores criados pelo próprio administradores
		public ActionResult VerAdministradores()
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			var administradores = _context.Administradors.Where(a => a.IdCriador == (int)HttpContext.Session.GetInt32("adminID"));
			return View(administradores.ToList());
		}
		

		//funçáo que mostra a lista de clientes
		public ActionResult Clientes()
		{

			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			var clientes = _context.Utilizadors.Include(p =>p.Cliente).Where(p => p.Cliente.IdCliente == p.IdUtilizador);
			return View(clientes.ToList());
		}

		//função para registar um Administrador
		public ActionResult RegistarAdministrador(int id)
		{
			//procura na BD pelo utilizador com o id que foi recebido como parametro
			Utilizador user = _context.Utilizadors.Where(u => u.IdUtilizador == id).Single();
			//verifca se já não é um administrador (por precaução!!)
			var admin = _context.Administradors.Where(p => p.Email == user.Email && p.Username == user.Username).Any();
			

			if (admin == false && user != null)
			{
				//adiciona um novo administradores aproveitando os seu dados anteriores que tinha de utilizador
				var novo_admnistrador = new Administrador()
				{
					Username = user.Username,
					Nome = user.Nome,
					Email = user.Email,
					Password = user.Password,
					IdCriador =(int)HttpContext.Session.GetInt32("adminID") // id do admnistrador que o criou
				};
				_context.Administradors.Add(novo_admnistrador);

				//remove o utilizador da BD de utilizadores
				ApagarUtilizador(id);
				_context.SaveChanges();
				return RedirectToAction("VerAdministradores");

			}
			else
			{
				return View("Clientes");
			}


			return View();
		}


		//função para apagar Utilizador da BD
		public void  ApagarUtilizador(int id)
		{
			//Procura o utilizador na BD
			var utilizador = _context.Utilizadors.Find(id);
			var cliente = _context.Clientes.Find(id);
			if (utilizador != null && cliente != null)
			{
				//Apaga a linha na BD
				_context.Clientes.Remove(cliente);
				_context.Utilizadors.RemoveRange(utilizador);
				_context.SaveChanges();
				
			}
	
		}

		//função para mostrar os pedidos dos restaurantes
		public ActionResult PedidosRestaurante()
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}
			//vai buscar os dados de pedidos que ainda não foram aceites!
			var pedidos = _context.PedirRegistos.Include(e => e.IdRestauranteNavigation.IdRestauranteNavigation).Include(e => e.IdRestauranteNavigation.IdRestauranteNavigation.Restaurante)
													.Where(e=>e.IdAdministrador == null);
			return View(pedidos.ToList());
		}

		//função para aceitar pedidos
		[HttpGet]
		public ActionResult AceitarPedidos(int? id)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
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
			var pedido = _context.PedirRegistos.Find(id);
			if(pedido == null)
			{
				return NotFound();
			}

			return View(pedido);
		}
		
		[HttpPost]
		public ActionResult AceitarPedidos(PedirRegisto pedido ,int id)
		{
			if(HttpContext.Session.GetInt32("adminID") == null)
			{
				return NotFound();
			}
			else
			{
				pedido.IdPedirRegisto = id;

				//retorna o id do restaurante que está a fazer o pedido
				var restaurantes = _context.PedirRegistos.Include(e=>e.IdRestauranteNavigation)
										.Where(r => r.IdRestaurante == r.IdRestauranteNavigation.IdRestaurante && r.IdPedirRegisto == id).Select(r => r.IdRestauranteNavigation.IdRestaurante).First();
				pedido.IdRestaurante = restaurantes;

				if(ModelState.IsValid)
				{
					//atauliza os valores da tabela Pedir Registos necessários
					_context.PedirRegistos.Where(r => r.IdPedirRegisto == id).First().IdAdministrador = (int)HttpContext.Session.GetInt32("adminID");
					_context.PedirRegistos.Where(r => r.IdPedirRegisto == id).First().Resultado = pedido.Resultado;
					_context.PedirRegistos.Where(r => r.IdPedirRegisto == id).First().DataAceitacao = DateTime.Now;
					//atualiza o estado da tabela restaurantes para depois o restaurante se puder registar
					_context.Restaurantes.Where(r => r.IdRestaurante == pedido.IdRestaurante).First().EstadoR = "Registado";
					_context.SaveChanges();

					return RedirectToAction("PedidosRestaurante");
				}
			}

			return RedirectToAction("PedidosRestaurante");
		}

		//função qpara rejitar o pedido
		[HttpGet]
		public ActionResult RejeitarPedidos(int? id)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
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
			var pedido = _context.PedirRegistos.Find(id);
			if (pedido == null)
			{
				return NotFound();
			}

			return View(pedido);
		}

		[HttpPost]
		public ActionResult RejeitarPedidos(PedirRegisto pedido, int id)
		{
			if (HttpContext.Session.GetInt32("adminID") == null)
			{
				return NotFound();
			}
			else
			{
				pedido.IdRestaurante = id;

				var restaurantes = _context.PedirRegistos.Include(e => e.IdRestauranteNavigation)
										.Where(r => r.IdRestaurante == r.IdRestauranteNavigation.IdRestaurante).Select(r => r.IdRestauranteNavigation.IdRestaurante).First();
				pedido.IdRestaurante = restaurantes;


				if (ModelState.IsValid)
				{
					if(pedido.MotivoRejeicao == null)
					{
						ModelState.AddModelError("", "O campo Motivo de Rejeição é de preenchimento obrigatório!");
						return View("RejeitarPedidos");
					}
					//atauliza os valores da tabela Pedir Registos necessários
					_context.PedirRegistos.Where(r => r.IdPedirRegisto == id).First().IdAdministrador = (int)HttpContext.Session.GetInt32("adminID");
					_context.PedirRegistos.Where(r => r.IdPedirRegisto == id).First().Resultado = pedido.Resultado;
					_context.PedirRegistos.Where(r => r.IdPedirRegisto == id).First().MotivoRejeicao = pedido.MotivoRejeicao;
					//atualiza o estado da tabela restaurantes para depois o restaurante se puder registar
					_context.Restaurantes.Where(r => r.IdRestaurante == pedido.IdRestaurante).First().EstadoR = "Rejeitado";
					_context.SaveChanges();

					return RedirectToAction("PedidosRestaurante");
				}
			}

			return RedirectToAction("PedidosRestaurante");

			return View();
		}


		//função para alterar dados do utilizador
		[HttpGet]
		public ActionResult AlterarDadosA()
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			if (HttpContext.Session.GetInt32("adminID") == null)
			{
				return NotFound();
			}

			//vai procurar na BD pelos dados do id do utilizador
			var admin = _context.Administradors.Find(HttpContext.Session.GetInt32("adminID"));
			if (admin == null)
			{
				return NotFound();
			}
			return View(admin);
		}

		[HttpPost]
		public ActionResult AlterarDadosA(Administrador admin)
		{
			if (HttpContext.Session.GetInt32("adminID") == null)
			{
				return NotFound();
			}



			if (ModelState.IsValid)
			{
				try
				{
					// alteração de dados do administrador onde se pode alterar um dado caso deseje
					var userName = _context.Administradors.Where(u => u.Username == admin.Username).Any();
					var userName1 = _context.Utilizadors.Where(u => u.Username == admin.Username).Any();

					var password = _context.Administradors.Where(u => u.Password == admin.Password && u.Username == admin.Username).Any();

					var nome = _context.Administradors.Where(u => u.Nome == admin.Nome && u.Username == admin.Username).Any();

					var email = _context.Administradors.Where(u => u.Email == admin.Email).Any();
					var email1 = _context.Utilizadors.Where(u => u.Email == admin.Email).Any();

					// se nao encontrar em nenhuma base de dados guarda a info
					if((userName == false && userName1 == false) || (email==false && email1 == false))
					{
						_context.Update(admin);
						_context.SaveChanges();
						HttpContext.Session.SetString("admin", admin.Username);
						HttpContext.Session.SetString("nome", admin.Nome);

					}
					else if (userName==true && (password == false || nome == false) && userName1== false && email== true && email1 == false)
                    {
						_context.Update(admin);
						_context.SaveChanges();
						HttpContext.Session.SetString("admin", admin.Username);
						HttpContext.Session.SetString("nome", admin.Nome);
					}
					else if (userName == true || email == true || userName1 == false || email1 == false)
					{
						ModelState.AddModelError("", "Dados já utilizados.");
						return View("AlterarDadosA");
					}
				}
				catch (DbUpdateConcurrencyException)
				{

					if (!AdministradorExists(admin.IdAdministrador))
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
			return View(admin);
		}

		private bool AdministradorExists(int id)
		{
			return _context.Utilizadors.Any(e => e.IdUtilizador == id);
		}

		
		[HttpGet]
		public ActionResult GerirUtilizadores()
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			var users = _context.Utilizadors.Include(e => e.Restaurante).Include(e=>e.Cliente).Where(e => e.IdUtilizador == e.Restaurante.IdRestaurante && e.Restaurante.EstadoR == "Registado" || e.IdUtilizador == e.Cliente.IdCliente);
			return View(users.ToList());
		}

		public ActionResult ProcurarRestaurantes(string searchString)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			var restaurantes = _context.Utilizadors.Include(e => e.Restaurante).Where(e => e.IdUtilizador == e.Restaurante.IdRestaurante && e.Restaurante.EstadoR == "Registado");

			if (!String.IsNullOrEmpty(searchString))
			{
				restaurantes = restaurantes.Where(s => s.Nome.Contains(searchString));
			}

			return View("GerirUtilizadores", restaurantes.ToList());
			//return RedirectToAction("PageInicial", "Visitante");
		}
		public ActionResult ProcurarClientes(string searchString)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}


			var clientes = _context.Utilizadors.Include(e => e.Cliente).Where(e => e.IdUtilizador == e.Cliente.IdCliente);

			if (!String.IsNullOrEmpty(searchString))
			{
				clientes = clientes.Where(s => s.Nome.Contains(searchString));
			}

			return View("GerirUtilizadores", clientes.ToList());
		}

		//função que permite bloquear utilizadores
		[HttpGet]
		public ActionResult Bloquear()
		{
			//var user = _context.Bloquears.Include(e=>e.IdUtilizadorNavigation.IdUtilizador).Where(b=>b.IdUtilizadorNavigation.IdUtilizador == id).Select(r=>r.IdUtilizadorNavigation.IdUtilizador).First();
			//if (user == null)
			//{
			//	return NotFound();
			//}

			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
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
		public ActionResult Bloquear(Bloquear bq, int id)
        {
			if(HttpContext.Session.GetInt32("adminID")== null)
            {
				return NotFound();
            }
			

			if (ModelState.IsValid)
            {
				if(bq.MotivoBloqueio == null)
				{
					ModelState.AddModelError("", "O campo_ Motivo de Bloqueio é de preenchimento obrigatório!");
					return View("Bloquear");
				}

				var user = _context.Utilizadors.Where(r => r.IdUtilizador == id).Select(r => r.IdUtilizador).Any();
				if(user == true)
                {
					var bloqueio = new Bloquear()
					{
						MotivoBloqueio = bq.MotivoBloqueio,
						DataBloquear = DateTime.Now,
						DataDesbloqueio = bq.DataDesbloqueio,
						IdAdministrador = (int)HttpContext.Session.GetInt32("adminID"),
						IdUtilizador = id
					};
					_context.Utilizadors.Find(id).Estado = "Bloqueado";
					_context.AddRange(bloqueio);
					_context.SaveChanges();

					////vai buscar o motivo de bloqueio do utilizado bloquado anteriormente para mais tarde ser mostrada na View
					//var motivo = _context.Bloquears.Where(r => r.IdUtilizador == id).Select(r => r.MotivoBloqueio).First();
					//HttpContext.Session.SetString("motivoB", motivo);
					return RedirectToAction("GerirUtilizadores");
				}
				else
                {
					return NotFound();
                }
				
			}
            else
            {
				return View();
			}
        }

		[HttpGet]
		public ActionResult Desbloquear(int? id)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}

			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			var user = _context.Bloquears.Where(b => b.IdUtilizador == id).First();
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		[HttpPost]
		public ActionResult Desbloquear(Bloquear bq, int id)
		{
			

			if (id == null)
            {
				return NotFound();
            }
            else 
            {
				var user = _context.Utilizadors.Find(id);
				if (user == null)
				{
					return NotFound();
				}
				else if(bq.DataDesbloqueio.Value.Date == DateTime.Now.Date)
                {
					
					//atualiza a data de desbloqueio
					_context.Bloquears.Where(r=>r.IdUtilizador== id).First().DataDesbloqueio = bq.DataDesbloqueio;
					//atualiza o estado do utilizador para que posso efetuar login
					_context.Utilizadors.Find(id).Estado = "Registado";
					_context.SaveChanges();
                }
				else if(bq.DataDesbloqueio.Value.Date > DateTime.Now.Date || bq.DataDesbloqueio.Value.Date < DateTime.Now.Date)
				{
					ModelState.AddModelError("", "A Data escolhida não é compatível com a Data Atual!");
					return View("Desbloquear");
				}

			}
		
				return RedirectToAction("Painel");
		}

		//função para ver os detalhes do restaurante
		public ActionResult Detalhes(int? id)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
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

		//função para ver os detalhes dos pratos do dia no Gerir Restaurantes
		public ActionResult DetalhesPR(int? id)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
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
			
			var pratosDia = _context.Registars.Include(p => p.IdRestauranteNavigation).Include(p => p.IdPratoDoDiaNavigation).Where(p => p.IdRestaurante == id).OrderBy(p=>p.IdPratoDoDia);
			return View(pratosDia.ToList());
		}

		//função para apresentar os tipos de serviço
		public ActionResult Servicos(int? id)
		{
			//só o admin consegue aceder a esta página
			if (HttpContext.Session.GetInt32("ClientID") != null || HttpContext.Session.GetInt32("restID") != null)
			{
				return View("Error403");
			}
			if (HttpContext.Session.GetInt32("ClientID") == null && HttpContext.Session.GetInt32("restID") == null && HttpContext.Session.GetInt32("adminID") == null)  // o visitante não pode ter acesso a esta pagina
			{
				return View("Error403");
			}

			//mostra os tipos serviços que o restaurante tem:
			var tiposS = _context.SelecionarServicos.Include(s=>s.IdRestauranteNavigation).Include(s=>s.IdServicoNavigation)
													.Where(s=>s.IdRestaurante == id);
			return View(tiposS.ToList());
		}
	}
}
