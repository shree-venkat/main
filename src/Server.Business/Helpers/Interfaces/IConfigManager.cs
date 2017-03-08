namespace Server.Business.Helpers.Interfaces
{
    using System;

    public interface IConfigManager
    {
        T Get<T>(string key) where T : IConvertible;
    }
}
