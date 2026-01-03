using Prism.Commands;

namespace CRMMobile.Helper
{
    public interface IFormLifeCycle
    {
        void InitializeForm();

        void SetupForm(params object[] arr);

        void Save();

        DelegateCommand SaveCommad { get; set; }
    }
}