using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data;

public static class BadDb
{
    // Corregido: Campo convertido a propiedad auto-implementada
    public static string ConnectionString { get; set; } = "Server=localhost;Database=master;User Id=sa;Password=SuperSecret123!;TrustServerCertificate=True";

    // Corregido: Agregado try-catch y cierre de conexión
    public static int ExecuteNonQueryUnsafe(string sql)
    {
        try
        {
            var conn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand(sql, conn);
            conn.Open();
            var result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DB ERROR] {ex.Message}");
            return -1;
        }
    }

    // Corregido: Agregado manejo de excepciones
    public static IDataReader ExecuteReaderUnsafe(string sql)
    {
        try
        {
            var conn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand(sql, conn);
            conn.Open();
            return cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DB ERROR] {ex.Message}");
            return null;
        }
    }
}
