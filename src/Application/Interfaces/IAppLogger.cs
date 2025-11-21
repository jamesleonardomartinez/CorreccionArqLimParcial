using System;

namespace Application.Interfaces;

public interface IAppLogger
{
    void Log(string message);
    void Try(Action action);
}
