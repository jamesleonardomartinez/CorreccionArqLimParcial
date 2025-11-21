using System;
using Application.Interfaces;

namespace Infrastructure.Logging;

public class Logger : IAppLogger
{
    public bool Enabled { get; set; } = true;

    public void Log(string message)
    {
        if (!Enabled) return;
        Console.WriteLine("[LOG] " + DateTime.Now + " - " + message);
    }

    // Corregido: Agregado manejo explícito de excepciones con logging
    public void Try(Action action)
    {
        try 
        { 
            action(); 
        } 
        catch (Exception ex) 
        {
            // Intencionalmente se ignora la excepción para este caso de uso
            Console.WriteLine($"[ERROR IGNORED] {ex.Message}");
        }
    }
}
