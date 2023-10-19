using LibClienteMeteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLectorMeteo
{
	public static class EscritorMeteo
	{
		// Parte final de los ficheros donde se guardan los datos
		private static readonly string nombreFichero = ".txt";  // Los datos iniciales.
		private static readonly string nombreFichero2 = "Url.txt";  // La URL con los datos útiles
		private static readonly string nombreFichero3 = "Temperaturas.txt"; // Los datos útiles

		public async static Task EscribirPredicciones(string municipio, List<Datos> listaDatos,
			bool mostrar = true)
		{
			using (StreamWriter mifichero = new StreamWriter(municipio + nombreFichero3))
			{
				foreach (Datos dato in listaDatos)
				{
					string cadena = $"\n\tPredicciones de las temperaturas para {dato.Municipio}:";

					await Escribir(cadena, mifichero, mostrar);
					foreach (Medidas med in dato.Prediccion.Medidas)
					{
						 cadena =
							$"\n\tPredicciones para el día: {med.Fecha.Date:d}.\nMaxima:{med.Temperaturas.Maxima}. Mínima: {med.Temperaturas.Minima}.";

						await Escribir(cadena, mifichero, mostrar);
						foreach (ParHoraTemperatura par in med.Temperaturas.Datos)
						{
							cadena = $"Temperatura: {par.Temperatura}, Hora: {par.Hora}";

							await Escribir(cadena, mifichero, mostrar);
						}
					}
				}
				mifichero.Close();
			}
		}

		static async public Task EscribirCadena(string municipio, string cadena, bool mostrar = true)
		{
			using (StreamWriter mifichero = new StreamWriter(municipio + nombreFichero))
			{
				await Escribir(cadena, mifichero, mostrar);
				mifichero.Close();
			}
		}

		static async public Task EscribirURL(string municipio, DatosUrl datos, bool mostrar = true)
		{
			using (StreamWriter mifichero = new StreamWriter(municipio + nombreFichero2))
			{
				if (datos != null && !string.IsNullOrEmpty(datos.URL))
				{
					string cadena = $"Estado: {datos.Estado}, URL: {datos.URL}";

					await Escribir(cadena, mifichero, mostrar);
				}
				else
				{
					string cadena = "Sin URL asociada ...";

					await Escribir(cadena, mifichero, true);
				}
				mifichero.Close();
			}
		}

		static async private Task Escribir(string cadena, StreamWriter fichero, bool mostrar)
		{
			if (mostrar)
			{
				Console.WriteLine(cadena);
			}
			await fichero.WriteLineAsync(cadena);
		}
	}
}
