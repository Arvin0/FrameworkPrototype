using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SYN.Cache;
using SYN.DBModel;
using SYN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SYN.Common.Exceptions;

namespace SYN.Service.Impl
{
    public class SystemConfigurationService : ISystemConfigurationService
    {
        private readonly ILogger<SystemConfigurationService> _logger;
        private readonly ICacheProvider _cache;
        private readonly IRepository<SystemDictionaryDBModel> _systemDictionaryRepository;

        public SystemConfigurationService(IUnitOfWork unitOfWork, ILogger<SystemConfigurationService> logger,
            ICacheProvider cache)
        {
            _systemDictionaryRepository = unitOfWork.GetRepository<SystemDictionaryDBModel>();
            _cache = cache;
            _logger = logger;
        }

        public async Task<IList<SystemDictionaryModel>> Get()
        {
            // 从缓存中获取数据
            var config = await _cache.GetOrAddAsync<IList<SystemDictionaryModel>>("Config:All",
                () =>
                {
                    var configuration = _systemDictionaryRepository.Query().ToListAsync().Result;
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
                },
                TimeSpan.FromMinutes(5));

            return config;
        }

        public async Task<SystemDictionaryModel> Get(int id)
        {
            // 从缓存中获取数据
            var config = await _cache.GetOrAddAsync($"Config:{id}",
                () =>
                {
                    var configuration = _systemDictionaryRepository.Query(s => s.Id == id).FirstOrDefaultAsync().Result;
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
                },
                TimeSpan.FromMinutes(5) );

            return config;
        }

        public bool Add(SystemDictionaryModel model)
        {
            var dbModel = new SystemDictionaryDBModel()
            {
                Code = model.Code,
                Name = model.Name,
                Type = model.Type,
                Value = model.Value,
                Order = model.Order
            };

            var entry = _systemDictionaryRepository.DbSet.Add(dbModel);

            entry.State = EntityState.Added;
            var saveResult = entry.Context.SaveChanges();

            var result = saveResult > 0;
            if (result)
            {
                _cache.RemoveAsync("Config:All");
            }

            return result;
        }

        public bool Edit(SystemDictionaryModel model)
        {
            var dbModel = _systemDictionaryRepository.Query(s => s.Id == model.Id).FirstOrDefault();
            if (dbModel == null)
            {
                throw new BusinessLogicException($"ID: {model.Id} 不存在");
            }

            dbModel.Value = model.Value;

            var entry = _systemDictionaryRepository.DbSet.Update(dbModel);

            entry.State = EntityState.Modified;
            var saveResult = entry.Context.SaveChanges();

            var result = saveResult > 0;
            if (result)
            {
                _cache.RemoveAsync($"Config:{dbModel.Id}");
                _cache.RemoveAsync("Config:All");
            }

            return result;
        }

        public bool Remove(int id)
        {
            var model = _systemDictionaryRepository.Query(s => s.Id == id).FirstOrDefault();
            if (model == null)
            {
                return true;
            }
            var entry = _systemDictionaryRepository.DbSet.Remove(model);

            entry.State = EntityState.Deleted;
            var saveResult = entry.Context.SaveChanges();

            var result = saveResult > 0;
            if (result)
            {
                _cache.RemoveAsync($"Config:{id}");
                _cache.RemoveAsync("Config:All");
            }

            return result;
        }
    }
}
