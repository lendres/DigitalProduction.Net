namespace DigitalProduction.Reflection;

/// <summary>
/// Summary not provided for the class Assembly.
/// </summary>
public static class Assembly
{
	#region Properties

	/// <summary>
	/// Location of this library assembly (including the name of the library).
	/// </summary>
	/// <remarks>
	/// This is the same as System.Reflection.Assembly.GetExecutingAssembly().Location called from within this library.
	/// Note that this will NOT return the location of an executable that references this library.  To get that use
	/// the System version or use the Location() function in this library and provide the executables assembly as input.
	/// </remarks>
	public static string LibraryLocation { get => IO.Path.RemoveDosDevicePaths(System.Reflection.Assembly.GetExecutingAssembly().Location); }

	/// <summary>
	/// Path of the library (does not include the name of the library).
	/// </summary>
	/// <remarks>
	/// If the library and any executable that calls it are installed in the same directory, this can be used
	/// as a shortcut to get the path of the running executable.
	/// </remarks>
	public static string? LibraryPath { get => System.IO.Path.GetDirectoryName(Assembly.LibraryLocation); }

	/// <summary>
	/// Path of the executable (does not include the name of the assembly).
	/// </summary>
	public static string? ExecutablePath { get => System.IO.Path.GetDirectoryName(IO.Path.RemoveDosDevicePaths(AppDomain.CurrentDomain.BaseDirectory)); }

	#endregion

	#region Methods

	/// <summary>
	/// Find an assembly by its name.
	/// </summary>
	/// <param name="name">Name of the assembly to find.</param>
	public static System.Reflection.Assembly? GetAssemblyByName(string name)
	{
		return AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == name);
	}

	/// <summary>
	/// Location of the executing assembly (including the name of the assembly).
	/// </summary>
	/// <remarks>This is the same as System.Reflection.Assembly.GetExecutingAssembly().Location.</remarks>
	public static string Location()
	{
		return Location(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Location of the assembly (including the name of the assembly).
	/// </summary>
	/// <remarks>This is the same as System.Reflection.Assembly.GetExecutingAssembly().Location.</remarks>
	public static string Location(System.Reflection.Assembly assembly)
	{
		return DigitalProduction.IO.Path.RemoveDosDevicePaths(assembly.Location);
	}

	/// <summary>
	/// Path of the calling assembly (does not include the name of the assembly).
	/// </summary>
	public static string? Path()
	{
		return Path(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Path of the assembly (does not include the name of the assembly).
	/// </summary>
	public static string? Path(System.Reflection.Assembly assembly)
	{
		return System.IO.Path.GetDirectoryName(Assembly.Location(assembly));
	}

	/// <summary>
	/// Get the calling assembly's title.
	/// </summary>
	public static string Title()
	{
		return Title(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly title.
	/// </summary>
	public static string Title(System.Reflection.Assembly assembly)
	{
		// Get all Title attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false);

		// If there is at least one Title attribute.
		if (attributes.Length > 0)
		{
			// Select the first one.
			System.Reflection.AssemblyTitleAttribute titleAttribute = (System.Reflection.AssemblyTitleAttribute)attributes[0];

			// If it is not an empty string, return it.
			if (titleAttribute.Title != "")
			{
				return titleAttribute.Title;
			}
		}

		// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name.
		return System.IO.Path.GetFileNameWithoutExtension(assembly.Location);
	}

	/// <summary>
	/// Get the calling assembly's authors.
	/// </summary>
	public static string Authors()
	{
		return Authors(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly's authors.
	/// </summary>
	public static string Authors(System.Reflection.Assembly assembly)
	{
		// Get all Authors attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(AuthorsAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((AuthorsAttribute)attributes[0]).Authors;
	}

	/// <summary>
	/// Get the calling assembly's version.
	/// </summary>
	public static string Version(bool threeDigit = false)
	{
		return Version(System.Reflection.Assembly.GetCallingAssembly(), threeDigit);
	}

	/// <summary>
	/// Get the assembly version.
	/// </summary>
	public static string Version(System.Reflection.Assembly assembly, bool threeDigit = false)
	{
		string version = assembly.GetName().Version?.ToString() ?? "";
		if (threeDigit)
		{ 
			version = version.Substring(0, 5);
		}
		return version;
	}

	/// <summary>
	/// Get the calling assembly's description.
	/// </summary>
	public static string Description()
	{
		return Description(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly description.
	/// </summary>
	public static string Description(System.Reflection.Assembly assembly)
	{
		// Get all Description attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyDescriptionAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((System.Reflection.AssemblyDescriptionAttribute)attributes[0]).Description;
	}

	/// <summary>
	/// Get the calling assembly's product name.
	/// </summary>
	public static string Product()
	{
		return Product(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly product name.
	/// </summary>
	public static string Product(System.Reflection.Assembly assembly)
	{
		// Get all Product attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((System.Reflection.AssemblyProductAttribute)attributes[0]).Product;
	}

	/// <summary>
	/// Get the calling assembly's copyright.
	/// </summary>
	public static string Copyright()
	{
		return Copyright(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly copyright.
	/// </summary>
	public static string Copyright(System.Reflection.Assembly assembly)
	{
		// Get all Copyright attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyCopyrightAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((System.Reflection.AssemblyCopyrightAttribute)attributes[0]).Copyright;
	}

	/// <summary>
	/// Get the calling assembly's company.
	/// </summary>
	public static string Company()
	{
		return Company(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly company.
	/// </summary>
	public static string Company(System.Reflection.Assembly assembly)
	{
		// Get all Company attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyCompanyAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((System.Reflection.AssemblyCompanyAttribute)attributes[0]).Company;
	}

	/// <summary>
	/// Get the calling assembly's website.
	/// </summary>
	public static string Website()
	{
		return Website(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly's website.
	/// </summary>
	public static string Website(System.Reflection.Assembly assembly)
	{
		// Get all the specific attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(WebsiteAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((WebsiteAttribute)attributes[0]).Url;
	}

	/// <summary>
	/// Get the calling assembly's location to report issues.
	/// </summary>
	public static string IssuesAddress()
	{
		return IssuesAddress(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly location to report issues..
	/// </summary>
	public static string IssuesAddress(System.Reflection.Assembly assembly)
	{
		// Get all the specific attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(IssuesAddressAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((IssuesAddressAttribute)attributes[0]).Url;
	}

	/// <summary>
	/// Get the calling assembly's documentation address.
	/// </summary>
	public static string DocumentationAddress()
	{
		return DocumentationAddress(System.Reflection.Assembly.GetCallingAssembly());
	}

	/// <summary>
	/// Get the assembly location to report issues..
	/// </summary>
	public static string DocumentationAddress(System.Reflection.Assembly assembly)
	{
		// Get all the specific attributes on this assembly.
		object[] attributes = assembly.GetCustomAttributes(typeof(DocumentationAddressAttribute), false);

		// If there aren't any attributes, return an empty string.
		if (attributes.Length == 0)
		{
			return "";
		}

		// If there is an attribute, return its value.
		return ((DocumentationAddressAttribute)attributes[0]).Url;
	}

	/// <summary>
	/// Get a List of Types that are subclasses of the superclass.  Searches the entire assembly the superclass is defined in.
	/// </summary>
	/// <param name="superclassType">Superclass/base class to search for subclass/derived class types for.</param>
	public static List<Type> GetSubclassTypesOf(Type superclassType)
	{
		System.Reflection.Assembly assembly = superclassType.Assembly;
		return assembly.GetTypes().Where(type => type != superclassType && type.IsSubclassOf(superclassType)).ToList();
	}

	/// <summary>
	/// Get a List of Types that are concrete (non-abstract) subclasses of the superclass.  Searches the entire assembly the superclass is defined in.
	/// </summary>
	/// <param name="superclassType">Superclass/base class to search for subclass/derived class types for.</param>
	public static List<Type> GetConcreteSubclassTypesOf(Type superclassType)
	{
		System.Reflection.Assembly assembly = superclassType.Assembly;
		return assembly.GetTypes().Where(type => type != superclassType && type.IsSubclassOf(superclassType) && !type.IsAbstract).ToList();
	}

	/// <summary>
	/// Get a List of Types that are implementations of the interface.  Searches the entire assembly the interface is defined in.
	/// </summary>
	/// <param name="superclassType">Interface to search for implementations of.</param>
	public static List<Type> GetInterfaceImplementationsTypesOf(Type superclassType)
	{
		System.Reflection.Assembly assembly = superclassType.Assembly;
		return assembly.GetTypes().Where(type => type != superclassType && type.IsAssignableFrom(superclassType)).ToList();
	}

	#endregion

} // End class.