using SYN.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SYN.Service
{
    public interface ISystemConfigurationService
    {
        Task<IList<SystemDictionaryModel>> Get();

        Task<SystemDictionaryModel> Get(int id);

        bool Add(SystemDictionaryModel model);

        bool Edit(SystemDictionaryModel model);

        bool Remove(int id);
    }
}
