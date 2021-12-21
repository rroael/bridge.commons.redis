using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Compress;
using Bridge.Commons.Redis.Commons;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.DataStructures
{
    /// <summary>
    ///     Redis Single
    /// </summary>
    public class RedisSingle : RedisCommons, IRedisSingle
    {
        #region CONSTRUCTOR

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="redisContext"></param>
        public RedisSingle(IRedisContext redisContext) : base(redisContext)
        {
        }

        #endregion

        #region TTL

        /// <summary>
        ///     Setar TTL
        /// </summary>
        /// <param name="key"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool SetTtl(string key, TimeSpan? duration, int database = (int)EDataStructure.SINGLE)
        {
            return KeyExists(key, database) && KeySetTtl(key, database, duration);
        }

        #endregion

        /// <summary>
        ///     Fechar
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        #region EXISTS

        /// <summary>
        ///     Verifica existência (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.SINGLE)
        {
            return await KeyExistsAsync(key, database);
        }

        /// <summary>
        ///     Verifica existência
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Exists(string key, int database = (int)EDataStructure.SINGLE)
        {
            return KeyExists(key, database);
        }

        #endregion

        #region INCREMENT AND DECREMENT

        /// <summary>
        ///     Incrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> IncrementAsync(string key, int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringIncrementAsync(key, 1, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Incrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> IncrementAsync(string key, long value, int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringIncrementAsync(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Incrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<double> IncrementAsync(string key, double value, int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringIncrementAsync(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Decrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> DecrementAsync(string key, int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringDecrementAsync(key, 1, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Decrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> DecrementAsync(string key, long value, int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringDecrementAsync(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Decrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<double> DecrementAsync(string key, double value, int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringDecrementAsync(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Incrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Increment(string key, int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringIncrement(key, 1, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Incrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Increment(string key, long value, int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringIncrement(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Incrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public double Increment(string key, double value, int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringIncrement(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Decrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Decrement(string key, int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringDecrement(key, 1, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Decrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Decrement(string key, long value, int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringDecrement(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Decrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public double Decrement(string key, double value, int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringDecrement(key, value, CommandFlags.DemandMaster);
        }

        #endregion

        #region GET

        /// <summary>
        ///     Buscar chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<TimeSpan?> GetKeyTimeToLive(string key, int database = (int)EDataStructure.SINGLE)
        {
            return await KeyTimeToLive(key, database);
        }

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<RedisValue> GetAsync(string key, int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringGetAsync(key, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, int database = (int)EDataStructure.SINGLE) where T : class
        {
            return MsgPackUtil.Deserialize<T>(await GetAsync(key, database));
        }

        /// <summary>
        ///     Buscar chava por pesquisa
        /// </summary>
        /// <param name="search"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IEnumerable<RedisKey> GetKeysBySearch(string search, int database = (int)EDataStructure.SINGLE)
        {
            return GetKeys(search, database);
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public RedisValue Get(string key, int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringGet(key, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(string key, int database = (int)EDataStructure.SINGLE) where T : class
        {
            return MsgPackUtil.Deserialize<T>(Get(key, database));
        }

        #endregion

        #region GET OR SET

        /// <summary>
        ///     Pegar ou setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="geFunction"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> geFunction, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE) where T : class
        {
            var item = await GetAsync<T>(key, database);

            if (item != null)
                return item;

            var itemCallback = await geFunction();

            await SetAsync(key, itemCallback, duration, database);

            return itemCallback;
        }

        /// <summary>
        ///     Pegar ou setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="getFunction"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetOrSetAsync<T>(string key, Func<T> getFunction, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE) where T : class
        {
            var item = await GetAsync<T>(key, database);

            if (item != null)
                return item;

            var itemCallback = getFunction();

            await SetAsync(key, itemCallback, duration, database);

            return itemCallback;
        }

        /// <summary>
        ///     Pegar ou setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="getFunction"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOrSet<T>(string key, Func<T> getFunction, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE) where T : class
        {
            var item = Get<T>(key, database);

            if (item != null)
                return item;

            var itemCallback = getFunction();

            Set(key, itemCallback, duration, database);

            return itemCallback;
        }

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key, int database = (int)EDataStructure.SINGLE)
        {
            return await KeyDeleteAsync(key, database);
        }

        /// <summary>
        ///     Remover
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Remove(string key, int database = (int)EDataStructure.SINGLE)
        {
            return KeyDelete(key, database);
        }

        #endregion

        #region SET

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringSetAsync(key, value, duration, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, byte[] value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE)
        {
            return await GetDatabase(database).StringSetAsync(key, value, duration, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE) where T : class
        {
            return await SetAsync(key, MsgPackUtil.Serialize(value), duration, database);
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Set(string key, string value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringSet(key, value, duration, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Set(string key, byte[] value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE)
        {
            return GetDatabase(database).StringSet(key, value, duration, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan? duration = null, int database = (int)EDataStructure.SINGLE)
            where T : class
        {
            return Set(key, MsgPackUtil.Serialize(value), duration, database);
        }

        #endregion
    }
}