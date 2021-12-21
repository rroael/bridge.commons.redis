using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Contracts
{
    /// <summary>
    ///     Redis Single
    /// </summary>
    public interface IRedisSingle : IDisposable
    {
        #region TTL

        /// <summary>
        ///     Setar TTL
        /// </summary>
        /// <param name="key"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool SetTtl(string key, TimeSpan? duration, int database = (int)EDataStructure.SINGLE);

        #endregion

        /// <summary>
        ///     Fechar
        /// </summary>
        void Close();

        #region EXISTS

        /// <summary>
        ///     Verifica existencia (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Verifica existencia
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Exists(string key, int database = (int)EDataStructure.SINGLE);

        #endregion

        #region INCREMENT AND DECREMENT

        /// <summary>
        ///     Incrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> IncrementAsync(string key,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Incrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> IncrementAsync(string key, long value,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Incrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<double> IncrementAsync(string key, double value,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Decrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> DecrementAsync(string key,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Decrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> DecrementAsync(string key, long value,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Decrementar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<double> DecrementAsync(string key, double value,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Incrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Increment(string key, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Incrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Increment(string key, long value,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Incrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        double Increment(string key, double value,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Decrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Decrement(string key, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Decrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Decrement(string key, long value,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Decrementar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        double Decrement(string key, double value,
            int database = (int)EDataStructure.SINGLE);

        #endregion

        #region GET

        /// <summary>
        ///     Buscar chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<TimeSpan?> GetKeyTimeToLive(string key, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<RedisValue> GetAsync(string key, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, int database = (int)EDataStructure.SINGLE) where T : class;

        /// <summary>
        ///     Buscar chaves por pesquisa
        /// </summary>
        /// <param name="search"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IEnumerable<RedisKey> GetKeysBySearch(string search, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        RedisValue Get(string key, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(string key, int database = (int)EDataStructure.SINGLE) where T : class;

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
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> geFunction, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE) where T : class;

        /// <summary>
        ///     Pegar ou setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="getFunction"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOrSetAsync<T>(string key, Func<T> getFunction, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE) where T : class;

        /// <summary>
        ///     Pegar ou setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="getFunction"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOrSet<T>(string key, Func<T> getFunction, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE)
            where T : class;

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Remover
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Remove(string key, int database = (int)EDataStructure.SINGLE);

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
        Task<bool> SetAsync(string key, string value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, byte[] value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? duration = null,
            int database = (int)EDataStructure.SINGLE) where T : class;

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Set(string key, string value, TimeSpan? duration = null, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Set(string key, byte[] value, TimeSpan? duration = null, int database = (int)EDataStructure.SINGLE);

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(string key, T value, TimeSpan? duration = null, int database = (int)EDataStructure.SINGLE)
            where T : class;

        #endregion
    }
}