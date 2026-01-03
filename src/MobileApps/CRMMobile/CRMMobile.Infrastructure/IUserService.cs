using System;
namespace CRMMobile.Infrastructure
{
    public interface IUserService
    {
        void Get();
        void Save();
        bool IsLogged();
        bool Login();
    }
}
