using System;
using System.Threading;
using System.Threading.Tasks;
using Blockexplorer.Core.Domain;
using Blockexplorer.Core.Interfaces;
using Blockexplorer.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Blockexplorer.Services
{
	public class ApiService : IApiService
	{
        private readonly IBlockchainDataProvider _blockchainProvider;
		private readonly ILogger _log;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly Dictionary<Api, object> _caches = new Dictionary<Api, object>()
        {
            { Api.Info, new Info { Errors = "Not Initialzed" } }
        };

		public ApiService(IBlockchainDataProvider blockchainProvider, ILoggerFactory loggerFactory)
		{
			_blockchainProvider = blockchainProvider;
			_log = loggerFactory.CreateLogger(GetType());
		}

		public async Task<Info> GetInfo()
		{
            var cache = Get<Info>("info");
            if (UseCache(cache.LastQuery))
                return cache.Model;


			await _semaphore.WaitAsync();

			try
			{
				_info = await _blockchainProvider.GetInfo();
				_lastQuery = DateTime.UtcNow;
				return _info;
			}
			catch (Exception e)
			{
				_log.LogError(e.Message);

				return new Info { Errors = "Exception" };
			}
			finally
			{
                _semaphore.Release();
			}
		}

		private bool UseCache(DateTime lastQuery)
		{
			var now = DateTime.UtcNow;
			var last = lastQuery;

			var elapsed = now - last;
			if (elapsed.TotalSeconds > 60)
				return false;

			return true;
		}

        private T Get<T>(string key)
        {
            return (T)_caches[key];
        }
    }
}
