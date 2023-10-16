using LectorMeteo.Modelos;

namespace LectorMeteo
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("\n\t\tIniciando la lectura de la API ...");

			DateTime inicial = DateTime.Now;
			string municipio = "46017"; // Código del INE de Alzira
			string cadena = await ClienteMeteo.ObtenerCadenaURL(municipio);
			var datos = await ClienteMeteo.ObtenerDatosURL(municipio);

			await ClienteMeteo.EscribirCadena(municipio, cadena);
			await ClienteMeteo.EscribirURL(municipio, datos);
			if (!string.IsNullOrEmpty(datos.URL))
			{
				var listaDatos = await ClienteMeteo.ObtenerDatosUtiles(datos);
				if (listaDatos.Count > 0)
				{
					await ClienteMeteo.EscribirPredicciones(municipio, listaDatos);
				}
			}
			Console.WriteLine("\n\t\tSalida exitosa ...");

			DateTime final = DateTime.Now;
			TimeSpan intervalo = final - inicial;

			Console.WriteLine($"\n\tTiempo transcurrido: {intervalo.TotalSeconds:F2} segundos ...");
		}
	}
}