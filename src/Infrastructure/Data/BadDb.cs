using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Data;

public static class BadDb
{
    // Corregido: Inicializado sin contraseña hardcodeada - se configura desde Program.cs
    public static string ConnectionString { get; set; } = string.Empty;

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
