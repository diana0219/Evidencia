using System;
using System.Collections.Generic;

using System.IO;

using System.Xml.Serialization;


namespace evidencia_desarrollo
{
	class Program
	{

		static List<estudiante> ListaEstudiantes = new List<estudiante>();
		static Validaciones Validar = new Validaciones();
		static void Main(string[] args)
		{


			int opcion;
			string temporal;
			bool EntValida = false;
			do
			{
				Console.WriteLine("1.Agregar estudiantes");
				Console.WriteLine("2.Listar estudiantes");
				Console.WriteLine("3.Buscar estudiantes");
				Console.WriteLine("4.Guardar archivo");
				Console.WriteLine("5.Caragar archivo xml");
				Console.WriteLine("0. para salir");
				opcion = int.Parse(Console.ReadLine());


				do
				{
					Console.WriteLine("Digite una opcion");
					temporal = Console.ReadLine();
					if (!Validar.Vacio(temporal))
						if (Validar.TipoNumero(temporal))
							EntValida = true;

				} while (!EntValida);




				switch (opcion)
				{
					case 1:
						CrearEstudiante();
						break;
					case 2:
						ListarEstudiantes();
						break;
					case 3:
						BuscarEstudiante();
						break;
					case 4:
						GuardarArchivo();
						break;
					case 5:
						CargarXml();
						break;

					case 0:
						Console.WriteLine(" gracias por usar el aplicativo");
						Console.WriteLine(" ******************************");
						Console.ReadKey();
						break;
				}
			} while (opcion != 0);
		}

		static void CrearEstudiante()

		{
			Console.Clear();
			double promedio = 0;

			string nom ="";
			string	email, cod;
			bool CodigoValido = false;
			bool NombreValido = false;
			bool CorreoValido = false;
			
			do
			{ 
				Console.Write(" Digite Codigo del Estudiante: ");
				cod = Console.ReadLine();
				if (!Validar.Vacio(cod))
					if (Validar.TipoNumero(cod))
						CodigoValido = true;
			} while (!CodigoValido);


			if (Existe(Convert.ToInt32(cod)))
				Console.WriteLine("El codigo " + cod + " Ya existe en el sistema");
			else

				do
				{ 
				Console.Write(" Digite nombre del Estudiante : ");
					nom = Console.ReadLine();
					if (!Validar.Vacio(nom))
						if (Validar.TipoTexto(nom))
							NombreValido = true; 
				} while (!NombreValido);



			do
			{
				Console.Write(" Digite correo del Estudiante : ");
				email = Console.ReadLine();
				if (!Validar.Vacio(email))
					if (Validar.emailValido(email))
						CorreoValido = true;
				Console.Write("paso");
			} while (!CorreoValido);

			Console.WriteLine("Ingrese los valores con comas ");
			Console.Write("Digite la nota # 1  ");
			double not1 = double.Parse(Console.ReadLine());
			Console.Write("Digite la nota # 2 ");
			double not2 = double.Parse(Console.ReadLine());
			Console.Write("Digite la nota # 3 ");
			double not3 = double.Parse(Console.ReadLine());
			promedio = (not1 + not2 + not3) / 3;
			Console.WriteLine("su promedio es: " + Math.Round (promedio));


			if (promedio >= 3.5)
			{
				Console.WriteLine("Aprobado");

			}
			else Console.WriteLine("Reprobado");

			Console.ReadLine();

			estudiante myestudiante = new estudiante();
			myestudiante.Codigo = Convert.ToInt32(cod);
			myestudiante.Nombre = nom;
			myestudiante.Correo = email;
			myestudiante.Promedio = promedio;


			ListaEstudiantes.Add(myestudiante);

		}

		static void ListarEstudiantes()
		{
			Console.Clear();

			foreach (estudiante itemEstudiante in ListaEstudiantes)
			{
				Console.WriteLine(" Codigo = " + itemEstudiante.Codigo);
				Console.WriteLine(" Nombre = " + itemEstudiante.Nombre);
				Console.WriteLine(" Correo = " + itemEstudiante.Correo);
				Console.WriteLine(" prmedio =" + itemEstudiante.Promedio);
				Console.ReadKey();
			}
		}

		static void BuscarEstudiante()
		{
			string cod;

			Console.Clear();
			Console.WriteLine(" ...... Buscar Estudiante");
			Console.WriteLine(" ......................");
			Console.Write(" Digite Codigo del estudiante que desea buscar: ");
			cod = Console.ReadLine();



			if (Existe(Convert.ToInt32(cod)))
			{
				estudiante myEstudiante = ObtenerDatos(Convert.ToInt32(cod));
				Console.WriteLine("Codigo: " + myEstudiante.Codigo + "\t Nombre: " + myEstudiante.Nombre + "\t Correo: " + myEstudiante.Correo + "\t  Promedio: " + myEstudiante.Promedio);
			}

			else
				Console.WriteLine(" El Usuario " + cod + " NO  existe en el sistema");


		}

		static bool Existe(int cod)
		{
			bool aux = false;
			foreach (estudiante objetoestudiante in ListaEstudiantes)
			{
				if (objetoestudiante.Codigo == cod)
					aux = true;
			}
			return aux;
		}

		static estudiante ObtenerDatos(int cod)
		{
			foreach (estudiante objetoestudiante in ListaEstudiantes)
			{
				if (objetoestudiante.Codigo == cod)
					return objetoestudiante;
			}
			return null;
		}
		static void GuardarArchivo()
		{
			XmlSerializer codificador = new XmlSerializer(typeof(List<estudiante>));
			TextWriter GuardarArchivo = new StreamWriter("C:/Users/sebas/OneDrive/Documentos/diana/.net/listaestudiantes.xml");
			codificador.Serialize(GuardarArchivo, ListaEstudiantes);
			GuardarArchivo.Close();

			Console.WriteLine("--- Archivo Guardado --- ");
		}

		static void CargarXml()
		{
			Console.Clear();
			ListaEstudiantes.Clear();
			if (File.Exists("C:/Users/sebas/OneDrive/Documentos/diana/.net/listaestudiantes.xml"))
			{
				XmlSerializer codificador = new XmlSerializer(typeof(List<estudiante>));
				FileStream CargarXml = File.OpenRead("C:/Users/sebas/OneDrive/Documentos/diana/.net/listaestudiantes.xml");
				ListaEstudiantes = (List<estudiante>)codificador.Deserialize(CargarXml);
				CargarXml.Close();
			}
			Console.WriteLine("--- Archivo Cargado ---- ");

		}
	}
}

