using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bridge.Commons.Compress;
using Bridge.Commons.Redis.Commons;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.DataStructures
{
    /// <summary>
    ///     Redis List
    /// </summary>
    public class RedisList : RedisCommons, IRedisList
    {
        #region CONSTRUCTOR

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="redisContext"></param>
        public RedisList(IRedisContext redisContext) : base(redisContext)
        {
        }

        #endregion

        /// <summary>
        ///     Fechar
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        #region ADD

        /// <summary>
        ///     Adicionar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task AddAsync(string key, string value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST)
        {
            if (maxItems > 0)
            {
                var length = await CountAsync(key);

                if (length >= maxItems)
                    await GetDatabase(database).ListLeftPopAsync(key, CommandFlags.DemandMaster);
            }

            await GetDatabase(database).ListRightPushAsync(key, value, flags: CommandFlags.DemandMaster);

            if (duration.HasValue)
                await GetDatabase(database).KeyExpireAsync(key, duration, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task AddAsync<T>(string key, T value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class
        {
            if (maxItems > 0)
            {
                var length = await CountAsync(key);

                if (length >= maxItems)
                    await GetDatabase(database).ListLeftPopAsync(key, CommandFlags.DemandMaster);
            }

            await GetDatabase(database)
                .ListRightPushAsync(key, MsgPackUtil.Serialize(value), flags: CommandFlags.DemandMaster);

            if (duration.HasValue)
                await GetDatabase(database).KeyExpireAsync(key, duration, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task AddRangeAsync<T>(string key, IEnumerable<T> values, int maxItems = 1000,
            TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class
        {
            if (maxItems > 0)
            {
                var length = await CountAsync(key);

                if (length >= maxItems)
                    await GetDatabase(database).ListLeftPopAsync(key, CommandFlags.DemandMaster);
            }

            var listValues = Array.ConvertAll(values.Select(MsgPackUtil.Serialize).ToArray(),
                item => (RedisValue)item);

            await GetDatabase(database).ListRightPushAsync(key, listValues, CommandFlags.DemandMaster);

            if (duration.HasValue)
                await GetDatabase(database).KeyExpireAsync(key, duration, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        public void Add(string key, string value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST)
        {
            if (maxItems > 0)
            {
                var length = Count(key);

                if (length >= maxItems)
                    GetDatabase(database).ListLeftPop(key, CommandFlags.DemandMaster);
            }

            GetDatabase(database).ListRightPush(key, value, flags: CommandFlags.DemandMaster);

            if (duration.HasValue)
                GetDatabase(database).KeyExpire(key, duration, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        public void Add<T>(string key, T value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class
        {
            if (maxItems > 0)
            {
                var length = Count(key);

                if (length >= maxItems) GetDatabase(database).ListLeftPop(key, CommandFlags.DemandMaster);
            }

            GetDatabase(database).ListRightPush(key, MsgPackUtil.Serialize(value), flags: CommandFlags.DemandMaster);

            if (duration.HasValue) GetDatabase(database).KeyExpire(key, duration, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        public void AddRange<T>(string key, IEnumerable<T> values, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class
        {
            if (maxItems > 0)
            {
                var length = Count(key);

                if (length >= maxItems)
                    GetDatabase(database).ListLeftPop(key, CommandFlags.DemandMaster);
            }

            var listValues = Array.ConvertAll(values.Select(MsgPackUtil.Serialize).ToArray(),
                item => (RedisValue)item);

            GetDatabase(database).ListRightPush(key, listValues, CommandFlags.DemandMaster);

            if (duration.HasValue)
                GetDatabase(database).KeyExpire(key, duration, CommandFlags.DemandMaster);
        }

        #endregion

        #region CLEAR

        /// <summary>
        ///     Limpar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ClearAsync(string key, int database = (int)EDataStructure.LIST)
        {
            return await KeyDeleteAsync(key, database);
        }

        /// <summary>
        ///     Limpar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Clear(string key, int database = (int)EDataStructure.LIST)
        {
            return KeyDelete(key, database);
        }

        #endregion

        #region COUNT

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> CountAsync(string key, int database = (int)EDataStructure.LIST)
        {
            return await GetDatabase(database).ListLengthAsync(key, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Contagem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Count(string key, int database = (int)EDataStructure.LIST)
        {
            return GetDatabase(database).ListLength(key, CommandFlags.PreferReplica);
        }

        #endregion

        #region EXISTS

        /// <summary>
        ///     Verifica existência (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.LIST)
        {
            return await KeyExistsAsync(key, database);
        }

        /// <summary>
        ///     Verifica existência
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Exists(string key, int database = (int)EDataStructure.LIST)
        {
            return KeyExists(key, database);
        }

        #endregion

        #region GET

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetAsync(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.LIST)
        {
            List<string> result = null;

            RedisValue[] items;

            if (take > 0)
            {
                var start = skip == 0 ? 0 : skip - 1;
                var stop = start + take - 1;

                items = await GetDatabase(database).ListRangeAsync(key, start, stop, CommandFlags.PreferReplica);
            }
            else
            {
                items = await GetDatabase(database).ListRangeAsync(key, flags: CommandFlags.PreferReplica);
            }

            if (items != null)
                result = items.Select(item => (string)item).ToList();

            return result;
        }

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IList<T>> GetAsync<T>(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.LIST) where T : class
        {
            List<T> result = null;

            RedisValue[] items;

            if (take > 0)
            {
                var start = skip == 0 ? 0 : skip - 1;
                var stop = start + take - 1;

                items = await GetDatabase(database).ListRangeAsync(key, start, stop, CommandFlags.PreferReplica);
            }
            else
            {
                items = await GetDatabase(database).ListRangeAsync(key, flags: CommandFlags.PreferReplica);
            }

            if (items != null)
                result = items.Select(item => MsgPackUtil.Deserialize<T>(item)).ToList();

            return result;
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IList<string> Get(string key, int skip = 0, int take = 0, int database = (int)EDataStructure.LIST)
        {
            IList<string> result = null;

            RedisValue[] items;

            if (take > 0)
            {
                var start = skip == 0 ? 0 : skip - 1;
                var stop = start + take - 1;

                items = GetDatabase(database).ListRange(key, start, stop, CommandFlags.PreferReplica);
            }
            else
            {
                items = GetDatabase(database).ListRange(key, flags: CommandFlags.PreferReplica);
            }

            if (items != null)
                result = items.Select(item => (string)item).ToList();

            return result;
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> Get<T>(string key, int skip = 0, int take = 0, int database = (int)EDataStructure.LIST)
            where T : class
        {
            IList<T> result = null;

            RedisValue[] items;

            if (take > 0)
            {
                var start = skip == 0 ? 0 : skip - 1;
                var stop = start + take - 1;

                items = GetDatabase(database).ListRange(key, start, stop, CommandFlags.PreferReplica);
            }
            else
            {
                items = GetDatabase(database).ListRange(key, flags: CommandFlags.PreferReplica);
            }

            if (items != null)
                result = items.Select(item => MsgPackUtil.Deserialize<T>(item)).ToList();

            return result;
        }

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remove (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task RemoveAsync<T>(string key, T value, int database = (int)EDataStructure.LIST)
            where T : class
        {
            await GetDatabase(database)
                .ListRemoveAsync(key, MsgPackUtil.Serialize(value), flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task RemoveRangeAsync(string key, long count, int database = (int)EDataStructure.LIST)
        {
            await GetDatabase(database).ListTrimAsync(key, count, await CountAsync(key), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>(string key, T value, int database = (int)EDataStructure.LIST) where T : class
        {
            GetDatabase(database).ListRemove(key, MsgPackUtil.Serialize(value), flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        public void RemoveRange(string key, long count, int database = (int)EDataStructure.LIST)
        {
            GetDatabase(database).ListTrim(key, count, Count(key), CommandFlags.DemandMaster);
        }

        #endregion
    }
}