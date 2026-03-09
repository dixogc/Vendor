using Npgsql;

public class ConexionBD
{
    private static string cadenaConexion;

    public ConexionBD(IConfiguration configuration)
    {
        cadenaConexion = configuration.GetConnectionString("DefaultConnection");
    }

    public static NpgsqlConnection ObtenerConexion()
    {
        return new NpgsqlConnection(cadenaConexion);
    }
}

