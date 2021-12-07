using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RISTORANTIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RISTORANTIS.Data
{
	public class DbInitialiser
	{
		public static void Initialize(RistorantisContext context)
		{
            context.Database.EnsureCreated();

			if (context.TipoPratoDoDia.Any())
			{
				return;
			}

			//Tipos de prato do dia
			var tipoPratoDia = new TipoPratoDoDium[]
			{
				new TipoPratoDoDium
				{
					NomeTipoP = "Carne"
				},

				new TipoPratoDoDium
				{
					NomeTipoP = "Peixe"
				},

				new TipoPratoDoDium
				{
					NomeTipoP = "Vegan"
				},
				new TipoPratoDoDium
				{
					NomeTipoP = "Vegetariano"
				}
			};

			foreach (TipoPratoDoDium tipos in tipoPratoDia)
			{
				context.TipoPratoDoDia.Add(tipos);
			}
			context.SaveChanges();

			if (context.TipoServicos.AsNoTracking().Any())
			{
				return;
			}

			//Tipos de serviços
			var tipoServicos = new TipoServico[]
			{
				new TipoServico
				{
					NomeTipoS = "Takeway",
					Selecionado = false,
				},

				new TipoServico
				{
					NomeTipoS = "Local",
					Selecionado = false,
				},

				new TipoServico
				{
					NomeTipoS = "Entrega",
					Selecionado = false,
				}
			};


			foreach (TipoServico tipo in tipoServicos)
			{
				context.TipoServicos.Add(tipo);
			}
			context.SaveChanges();

			if (context.Administradors.Any())
			{
				return;
			}

			var admin = new Administrador[]
			{
				new Administrador
				{
					Username="XPTO",
					Password ="12345",
					Email ="xpto@hotmail.com",
					Nome = "Chiquinho"

					//IdCriador=null
				}
			};
			foreach (Administrador a in admin)
			{
				context.Administradors.Add(a);
			}
			context.SaveChanges();

			//retorna o id do administrador criado anteriormente
			var administradorID = context.Administradors.Where(a => a.Username == "XPTO").Select(a => a.IdAdministrador).First();
			if(administradorID != null)
			{
				//abre uma conexão à BD para atualizar o IdCriador do Administrador anterior
				context.Database.OpenConnection();
				try
				{
					
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Administrador ON");
					context.Administradors.Find(administradorID).IdCriador = administradorID;
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Administrador OFF");
				}
				finally
				{
					context.Database.CloseConnection();
					
				}

			}
			
		}
    }
}
