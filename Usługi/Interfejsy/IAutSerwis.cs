using Transf;
using System.Threading.Tasks;
using System.Collections.Generic;
using Modele;

namespace WymianaWaluty.Usługi.Interfejsy
{
    public interface IAutSerwis
    {
        Task<string> LoginAsync(LoginRcs request);
        Task<bool> RegisterAsync(RegisterRqs request);

    }
}