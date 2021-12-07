using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using RISTORANTIS.Data;
using RISTORANTIS.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace RISTORANTIS.Controllers
{
	
    public class AccountsController : Controller
    {
		private readonly RistorantisContext _context;
		public AccountsController(RistorantisContext context)
		{
			_context = context;
		}

		//Função para fazer o registo de um Cliente ou redirecionar para completar um registo de um restaurante
		[HttpGet]
		
		public ActionResult Registar()
		{
			

			return View();
		}

		[HttpPost]
		public ActionResult Registar(Utilizador utilizador)
		{
			
			if (ModelState.IsValid)
			{
				//Procurar na base de dados pelos dados inseridos
				var userName = _context.Utilizadors.Where(u => u.Username == utilizador.Username).Any();
				var userEmail = _context.Utilizadors.Where(u => u.Email == utilizador.Email).Any();

				//Procurar na base de dados dos administradores pelos dados inseridos
				var userNameAdmin = _context.Administradors.Where(u => u.Username == utilizador.Username).Any();
				var userEmailAdmin = _context.Administradors.Where(u => u.Email == utilizador.Email).Any();

				//caso os dados (Email, Username) já existam na base de dados
				if ((userName == true && userEmail == true) || (userNameAdmin == true && userEmailAdmin == true))
				{
					ModelState.AddModelError("Username", "Esse Username já está a ser utilizado!");
					ModelState.AddModelError("Email", "Esse Email já está a ser utilizado!");
					return View("Registar");
				}
				else if ((userName == false && userEmail == true) || (userNameAdmin == false && userEmailAdmin == true))
				{
					ModelState.AddModelError("Email", "Esse Email já está a ser utilizado!");
					return View("Registar");
				}
				else if ((userName == true && userEmail == false) || (userNameAdmin == true && userEmailAdmin == false))
				{
					ModelState.AddModelError("Username", "Esse Username já está a ser utilizado!");
					return View("Registar");
				}
				//caso os dados (Email, Username) ainda não existam na base de dados
				else if ((userName == false && userEmail == false) || (userNameAdmin == false && userEmailAdmin == false))
				{
					//Gera string aleatória para confirmar o email
					utilizador.ConfEmail = GenerateString(50);

					//Adicionar à tabela do Utilizador
					_context.Utilizadors.Add(utilizador);
					_context.SaveChanges();

					//Adicionar à tabela do Cliente
					var novo_cliente = new Cliente
					{
						IdCliente = utilizador.IdUtilizador
					};
					_context.Clientes.Add(novo_cliente);
					_context.SaveChanges();

					//Trata da confirmação de email
					var token = utilizador.ConfEmail;
					var confirmationLink = Url.Action("ConfirmarEmail", "Accounts", new { token, email = utilizador.Email }, Request.Scheme);
					bool emailResponse = SendEmail(utilizador.Email, confirmationLink);
					
					return RedirectToAction(nameof(Sucesso));
				}
			}
			return View(utilizador);
		}

		public static string GenerateString(int length)
		{
			string AllowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

			var bytes = new byte[length];

			using (var random = RandomNumberGenerator.Create())
			{
				random.GetBytes(bytes);
			}

			return new string(bytes.Select(x => AllowableCharacters[x % AllowableCharacters.Length]).ToArray());
		}

		public bool SendEmail(string userEmail, string confirmationLink)
		{
			//MailMessage mailMessage = new MailMessage();
			//mailMessage.From = new MailAddress("ristorantis@ruii.uu.me");
			//mailMessage.To.Add(new MailAddress(userEmail));

			//mailMessage.Subject = "RISTORANTIS - Confirme o seu email";
			//mailMessage.IsBodyHtml = true;
			//mailMessage.Body = confirmationLink;

			var apiKey = "SG.B - cvydqmR6uKAJoeJVC1sw.1z5vI_1cYJcrzMAQMe3mabIUxsIEVyYllRQu1OPz_D0";
			var client = new SendGridClient(apiKey);
			var from = new EmailAddress("ristorantis@ruii.uu.me");
			var to = new EmailAddress(userEmail);
			var subject = "RISTORANTIS - Confirme o seu email";
			var plainTextContent = confirmationLink;
			var htmlContent = confirmationLink;
			var msg = MailHelper.CreateSingleEmail(
				from,
				to,
				subject,
				plainTextContent,
				htmlContent);

			var response = client.SendEmailAsync(msg);

			//SmtpClient client = new SmtpClient();
			//client.Credentials = new System.Net.NetworkCredential("apikey", "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");
			//client.Host = "smtp.sendgrid.net";
			//client.Port = 465;

			//try
			//{
			//	client.Send(mailMessage);
			//	return true;
			//}
			//catch (Exception ex)
			//{
			//	//exceção
			//}
			return false;
		}

		public async Task<IActionResult> ConfirmarEmail(string token, string email)
		{

				return View("Error");

		}

		public ActionResult Sucesso()
		{
			return View("Login");
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult Login()
		{
			return View();
		}


		//função para fazer login :: !!!!!!VER MELHOR PORQUE SÓ PODE FAZER LOGIN QUANDO O ESTADO FOR: RESGISTADO (CLIENTE ou RESTAURANTE) 
		[HttpPost]
		[AllowAnonymous]
		public ActionResult Login(string username, string password)
		{
			if (ModelState.IsValid)
			{
				//Procura os dados inseridos na base de dados:
				var user = _context.Utilizadors.Where(u => u.Username == username && u.Password == password).Any();
				var admin = _context.Administradors.Where(a => a.Username == username && a.Password == password).Any();

				
				if(user == false && admin == false )
				{
					ModelState.AddModelError("", "As credenciais estão erradas! Tente Novamente!");
					return View("Login");
				}
				else if((user == true) && (admin == false)) // login de um utilizador (Cliente, Restaurante)
				{
					Utilizador utilizador = _context.Utilizadors.Where(u => u.Username == username && u.Password == password).Single();
					var restaurante = _context.Restaurantes.Where(u => u.IdRestaurante == utilizador.IdUtilizador).Any();
					var cliente = _context.Clientes.Where(u => u.IdCliente == utilizador.IdUtilizador).Any();

					if(restaurante == true && cliente == false) //Restaurante
					{
						Restaurante rest = _context.Restaurantes.Where(u => u.IdRestaurante == utilizador.IdUtilizador).Single();
						if (rest.EstadoR == "Registado" && utilizador.Estado == "Registado") // estado que indica que o pedido de registo foi aceite pelo administrador
						{
							HttpContext.Session.SetString("rest", username);
							//retorna o id do restaurante que fez login
							var restaurant = _context.Utilizadors.Where(u => u.Username == username).Select(c => c.IdUtilizador).First();
							HttpContext.Session.SetInt32("restID", restaurant);
							var nome = _context.Utilizadors.Where(u => u.Username == username).Select(c => c.Nome).First();
							HttpContext.Session.SetString("nome", nome);
							HttpContext.Session.SetString("RoleR", "restaurante");

							//login efetuado com sucesso!
							return RedirectToAction("Painel", "Restaurante");
						}
						else if(rest.EstadoR == "Em_Espera" && utilizador.Estado == "Registado") // ainda está á espera da confirmação do pedido
						{
							ModelState.AddModelError("", "O pedido de registo desta conta ainda não foi aceite pelo Administrador! Aguarde!");
							return View("Login");
						}
						else if(utilizador.Estado == "Bloqueado")   // conta bloqueada
						{
							//vai buscar o motivo de bloqueio
							var motivoB = _context.Bloquears.Where(r => r.IdUtilizador == utilizador.IdUtilizador).Select(r => r.MotivoBloqueio).First();

							ModelState.AddModelError("", "Esta conta está bloqueada! Motivo do Bloqueio:" + motivoB);
							//ModelState.AddModelError("", motivoB);
							return View("Login");
						}
						else if(rest.EstadoR == "Rejeitado" && utilizador.Estado == "Registado")  // o pedido foi rejeitado
						{
							var motivoR = _context.PedirRegistos.Where(r => r.IdRestaurante == utilizador.Restaurante.IdRestaurante).Select(r => r.MotivoRejeicao).First();
							ModelState.AddModelError("", "O seu pedido foi rejeitado pelo administrador! Motivo de Rejeição:" + motivoR);
							//ModelState.AddModelError("", motivoR);
							return View("Login");
						}
					
					}
					else if(cliente == true && restaurante == false)  //Cliente
					{
						if(utilizador.Estado == "Registado")  //estado que permite que se efetue login
						{
							HttpContext.Session.SetString("client", username);
							//retorn o id do cliente que fez login
							var client = _context.Utilizadors.Where(u => u.Username == username).Select(c => c.IdUtilizador).First();
							HttpContext.Session.SetInt32("ClientID", client);
							var nome = _context.Utilizadors.Where(u => u.Username == username).Select(c => c.Nome).First();
							HttpContext.Session.SetString("nome", nome);
							HttpContext.Session.SetString("RoleC", "cliente");

							//login efetuado com sucesso
							return RedirectToAction("Painel", "Clientes");
						}
						else if(utilizador.Estado == "Em_Espera") // caso ainda não tenha confirmado o email
						{
							ModelState.AddModelError("", "Confirme o seu Email!");
							return View("Login");
						}
						else if(utilizador.Estado == "Bloqueado") //caso o cliente esteja bloqueado
						{
							//vai buscar o motivo de bloqueio
							var motivoB = _context.Bloquears.Where(r => r.IdUtilizador == utilizador.IdUtilizador).Select(r => r.MotivoBloqueio).First();
							ModelState.AddModelError("", "Esta conta está bloqueada! Motivo do Bloqueio:" + motivoB);
							//ModelState.AddModelError("", motivoB);
							return View("Login");
						}

						
					}
					else if(restaurante == false && cliente == false)
					{
						ModelState.AddModelError("", "Não foi encontrado nenhum tipo de utilizador! Tente Novamente");
						return View("Login");
					}
				}
				else if((user == false) && (admin == true))  //login do administrador
				{
					HttpContext.Session.SetString("admin", username);
					//retorna o id do administrador que fez login
					var administrador = _context.Administradors.Where(u => u.Username == username).Select(c => c.IdAdministrador).First();
					HttpContext.Session.SetInt32("adminID", administrador);
					var nome = _context.Administradors.Where(u => u.Username == username).Select(c => c.Nome).First();
					HttpContext.Session.SetString("nome", nome);
					HttpContext.Session.SetString("RoleA", "administrador");

					//login efetuado com sucesso!
					return RedirectToAction("Painel", "Administradores");
				}
			}
			return View();
		}

		public ActionResult Logout()
		{
			HttpContext.Response.Cookies.Delete(".Ristorantis.Session");
			return RedirectToAction("PageInicial", "Visitante");
		}

		[HttpGet]
		public ActionResult Painel()
		{
			if (HttpContext.Session.GetString("client") != null)
            {
				return RedirectToAction("Painel", "Clientes");
			}
			else if (HttpContext.Session.GetString("rest") != null)
			{
				return RedirectToAction("Painel", "Restaurante");
			}
			else if (HttpContext.Session.GetString("admin") != null)
			{
				return RedirectToAction("Painel", "Administradores");
			}
			else
            {
				return RedirectToAction("PageInicial", "Visitante");
			}
			////string username = HttpContext.Session.GetString("client");
			//if (ModelState.IsValid)
			//{
			//	//Procura os dados inseridos na base de dados:
			//	var user = _context.Utilizadors.Where(u => u.Username == username).Any();
			//	var admin = _context.Administradors.Where(a => a.Username == username).Any();

			//	if ((user == true) && (admin == false)) // painel de um utilizador (Cliente, Restaurante)
			//	{
			//		Utilizador utilizador = _context.Utilizadors.Where(u => u.Username == username).Single();
			//		var restaurante = _context.Restaurantes.Where(u => u.IdRestaurante == utilizador.IdUtilizador).Any();
			//		var cliente = _context.Clientes.Where(u => u.IdCliente == utilizador.IdUtilizador).Any();

			//		if (restaurante == true && cliente == false) //painel de um restaurante
			//		{
			//			return RedirectToAction("Painel", "Restaurante");
			//		}
			//		else if (cliente == true && restaurante == false) //painel de um cliente
			//		{
			//			return RedirectToAction("Painel", "Clientes");
			//		}
			//	}
			//	else if ((user == false) && (admin == true))  //painel de um administrador
			//	{
			//		return RedirectToAction("Painel", "Administradores");
			//	}
			//}
			//return RedirectToAction("PageInicial", "Visitante");
		}

		[HttpGet]
		public ActionResult Redirecionar()
		{
			return View();
		}


		//Função para concluir o registo de um Restaurante
		[HttpPost]
		public ActionResult Redirecionar(Utilizador utilizador)
		{
			if (ModelState.IsValid)
			{
				//Procurar na base de dados dos utilizadores pelos dados inseridos
				var userName = _context.Utilizadors.Where(u => u.Username == utilizador.Username).Any();
				var userEmail = _context.Utilizadors.Where(u => u.Email == utilizador.Email).Any();

				//Procurar na base de dados dos administradores pelos dados inseridos
				var userNameAdmin = _context.Administradors.Where(u => u.Username == utilizador.Username).Any();
				var userEmailAdmin = _context.Administradors.Where(u => u.Email == utilizador.Email).Any();

				//caso os dados (Email, Username) já existam na base de dados
				if ((userName == true && userEmail == true) || (userNameAdmin == true && userEmailAdmin == true))
				{
					ModelState.AddModelError("Username", "Esse Username já está a ser utilizado!");
					ModelState.AddModelError("Email", "Esse Email já está a ser utilizado!");
					return View("Registar");
				}
				else if (userName == false && userEmail == true || (userNameAdmin == false && userEmailAdmin == true))
				{
					ModelState.AddModelError("Email", "Esse Email já está a ser utilizado!");
					return View("Registar");
				}
				else if ((userName == true && userEmail == false) || (userNameAdmin == true && userEmailAdmin == false))
				{
					ModelState.AddModelError("Username", "Esse Username já está a ser utilizado!");
					return View("Registar");
				}
				//caso os dados (Email, Username) ainda não existam na base de dados
				else if (userName == false && userEmail == false)
				{
					_context.Utilizadors.Add(utilizador);
					_context.SaveChanges();
					//é redirecionado para uma outra página para registar o utilizador criado como restaurante, levando o id como parametro
					return RedirectToAction("Registar", "Restaurante", new { id = utilizador.IdUtilizador });
				}

			}

			return RedirectToAction("Registar");
		}

		//[HttpGet]
		//public ActionResult ConfirmEmail(int userId, string token)
  //      {
		//	if (token == null)
  //          {
		//		return RedirectToAction("PageInicial", "Visitante");
  //          }

		//	var user = _context.Utilizadors.Where(u => u.IdUtilizador == userId).Any();

		
		//}
	}
}
