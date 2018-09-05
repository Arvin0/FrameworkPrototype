using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SYN.DBModel;
using SYN.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SYN.Repository.Extensions;

namespace SYN.Service.Impl
{
    public class SystemConfigurationService : ISystemConfigurationService
    {
        private readonly ILogger<SystemConfigurationService> _logger;
        private readonly IRepository<SystemDictionaryDBModel> _systemDictionaryRepository;

        public SystemConfigurationService(IUnitOfWork unitOfWork, ILogger<SystemConfigurationService> logger)
        {
            _systemDictionaryRepository = unitOfWork.GetRepository<SystemDictionaryDBModel>();
            _logger = logger;
        }

        public async Task<IList<SystemDictionaryModel>> Get()
        {
            var configuration = await _systemDictionaryRepository.Query().ToListAsync();
            var result = from c in configuration
                select new SystemDictionaryModel()
                {
                    Type = c.Type,
                    Code = c.Code,
                    Name = c.Name,
                    Order = c.Order,
                    Value = c.Value
                };

            return result.ToList();
        }

        public async Task<SystemDictionaryModel> Get(int id)
        {
            var data = _systemDictionaryRepository.Query();
            var sql = data.ToSql();
            _logger.LogInformation($"Exec  : {id}");
            var configuration = await _systemDictionaryRepository.Query(s =>s.Id == id).FirstOrDefaultAsync();
            if (configuration == null)
            {
                _logger.LogInformation($"No result for id : {id}");
                return null;
            }

            return new SystemDictionaryModel()
            {
                Id = configuration.Id,
                Type = configuration.Type,
                Code = configuration.Code,
                Name = configuration.Name,
                Order = configuration.Order,
                Value = configuration.Value
            };
        }
    }
}
