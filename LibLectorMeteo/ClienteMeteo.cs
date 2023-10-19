using LibClienteHttpUTF8;
using LibClienteMeteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibLectorMeteo
{
	/*
     * 
     * Cliente Http para realizar una consulta a Opendata de la Aemet.
     * 
     */
	public static class ClienteMeteo
	{

		private static readonly HttpClient cliente = new HttpClient();
		// Partes de la URL inicial
		private static readonly string servidor = "https://opendata.aemet.es/opendata";
		private static readonly string api = "/api/prediccion/especifica/municipio/diaria";
		//static private string municipio="/{municipio}";
		private static readonly string enlace = "/?api_key=";
		private static readonly string clave = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJmcmFuX3NvcG9AaG90bWFpbC5jb20iLCJqdGkiOiJkODQ0ZTkwNi1jZTdkLTQ4ODEtYmMxZS1jYTU3NjBmNDJmYjQiLCJpc3MiOiJBRU1FVCIsImlhdCI6MTY5NzAyMTgxMiwidXNlcklkIjoiZDg0NGU5MDYtY2U3ZC00ODgxLWJjMWUtY2E1NzYwZjQyZmI0Iiwicm9sZSI6IiJ9.AD1fEU0fFqHJgvynw9r1_KrawxQx5j0LZ5z4b5THlgM";
		// Cliente y URL para obtener los datos útiles
		static private string urlFinal = "";

		/*
         * 
         * Ejemplo de petición a Opendata.
         * var client = new RestClient("https://opendata.aemet.es/opendata/api/valores/climatologicos/inventarioestaciones/todasestaciones/?api_key=jyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJqbW9udGVyb2dAYWVtZXQuZXMiLCJqdGkiOiI3NDRiYmVhMy02NDEyLTQxYWMtYmYzOC01MjhlZWJlM2FhMWEiLCJleHAiOjE0NzUwNTg3ODcsImlzcyI6IkFFTUVUIiwiaWF0IjoxNDc0NjI2Nzg3LCJ1c2VySWQiOiI3NDRiYmVhMy02NDEyLTQxYWMtYmYzOC01MjhlZWJlM2FhMWEiLCJyb2xlIjoiIn0.xh3LstTlsP9h5cxz3TLmYF4uJwhOKzA0B6-vH8lPGGw");
         *
         */

		/*
         * 
         * Inicialización de algunos de los componentes.
         * 
         */
		static ClienteMeteo()
		{
			cliente.DefaultRequestHeaders.Accept.Clear();
			cliente.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json")
			);
		}

		/*
         *
         *  Petición del json que contiene la URL fuente de los datos a leer.
         *  No se leen directmente los datos útiles.
         *  
         */
		static async public Task<string> ObtenerCadenaURL(string municipio)
		{
			string totalUrl = servidor + api + $"/{municipio}{enlace}{clave}";
			var json = await ClienteHttpUTF8.PrepararGetUTF8(cliente, totalUrl);

			return json;
		}

		/*
         * 
         * Obtención de la verdadera URL fuente del json de los datos útiles.
         * 
         */
		static async public Task<DatosUrl> ObtenerDatosURL(string municipio)
		{
			var json = await ObtenerCadenaURL(municipio);
			var datos = JsonSerializer.Deserialize<DatosUrl>(json);

			if (datos == null)
			{
				return new DatosUrl(404, "");
			}
			if (string.IsNullOrEmpty(datos.URL))
			{
				return new DatosUrl(datos.Estado, "");

			}

			return datos;
		}

		///
		/// <summary>
		/// Obtiene los datos realmente útiles.
		/// </summary>
		///<param name="datos">
		///Variable que almacena la URL fuente de los datos.
		///</param>
		///<returns>
		///Una lista con los datos.<br/>
		///Los datos son válidos si la lista tiene al menos un elemento.
		///</returns>
		///
		public async static Task<List<Datos>> ObtenerDatosUtiles(DatosUrl datos)
		{
			CrearDatosFinales(datos);

			var jsonFinal = await ClienteHttpUTF8.PrepararGetUTF8(cliente, urlFinal); ;
			var listaDatos = JsonSerializer.Deserialize<List<Datos>>(jsonFinal);

			return listaDatos ?? new();
		}

		private static void CrearDatosFinales(DatosUrl datos)
		{
			urlFinal = datos.URL;
		}
	}
}
