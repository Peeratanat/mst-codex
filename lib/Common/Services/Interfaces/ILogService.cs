
using Common.Helper.Logging;
using System.Threading.Tasks;

namespace Common.Services.Interfaces
{
    public interface ILogService
    {
        Task WriteLog(LogModel logModel);
    }
}