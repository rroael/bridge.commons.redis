using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Contracts
{
    /// <summary>
    ///     Redis set
    /// </summary>
    public interface IRedisSet : IDisposable
    {
        /// <summary>
        ///     Fechar
        /// </summary>
        void Close();

        #region ADD

        /// <summary>
        ///     Adicionar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, RedisValue value, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Adicionar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> AddAsync<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Adicionar alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<long> AddRangeAsync<T>(string key, IEnumerable<T> values, int database = (int)EDataStructure.SET)
            where T : class;

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Add(string key, RedisValue value, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Add<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Adicionar alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long AddRange<T>(string key, IEnumerable<T> values, int database = (int)EDataStructure.SET) where T : class;

        #endregion

        #region CLEAR

        /// <summary>
        ///     Limpar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ClearAsync(string key, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Limpar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Clear(string key, int database = (int)EDataStructure.SET);

        #endregion

        #region COMBINE

        /// <summary>
        ///     Combinar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<ISet<RedisValue>> CombineAsync(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<ISet<RedisValue>> CombineAsync(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ISet<T>> CombineAsync<T>(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Combinar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ISet<T>> CombineAsync<T>(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Combinar e guardar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync(SetOperation setOperation, string keyDestination, string firstKey,
            string secondKey, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar e guardar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync(SetOperation setOperation, string keyDestination,
            IEnumerable<string> keysToCombine, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar e guardar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync<T>(SetOperation setOperation, string keyDestination, string firstKey,
            string secondKey, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Combinar e guardar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<long> CombineAndStoreAsync<T>(SetOperation setOperation, string keyDestination,
            IEnumerable<string> keysToCombine, int database = (int)EDataStructure.SET) where T : class;


        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        ISet<RedisValue> Combine(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        ISet<RedisValue> Combine(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISet<T> Combine<T>(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISet<T> Combine<T>(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Combinar e guardar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long CombineAndStore(SetOperation setOperation, string keyDestination, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar e guardar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long CombineAndStore(SetOperation setOperation, string keyDestination, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Combinar e guardar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long CombineAndStore<T>(SetOperation setOperation, string keyDestination, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Combinar e guardar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long CombineAndStore<T>(SetOperation setOperation, string keyDestination, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET) where T : class;

        #endregion

        #region CONTAINS

        /// <summary>
        ///     Verifica se contém algo (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ContainsAsync(string key, RedisValue value, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Verifica se contém algo (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> ContainsAsync<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Verifica se contém algo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Contains(string key, RedisValue value, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Verifica se contém algo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Contains<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class;

        #endregion

        #region COUNT

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> CountAsync(string key, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Contagem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Count(string key, int database = (int)EDataStructure.SET);

        #endregion

        #region EXISTS

        /// <summary>
        ///     Verifica existencia (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Verifica existencia
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Exists(string key, int database = (int)EDataStructure.SET);

        #endregion

        #region GET

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<ISet<RedisValue>> GetAsync(string key, long count = 1, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ISet<T>> GetAsync<T>(string key, long count = 1, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Buscar todos (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<ISet<RedisValue>> GetAllAsync(string key, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Buscar todos (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ISet<T>> GetAllAsync<T>(string key, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        ISet<RedisValue> Get(string key, long count = 1, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISet<T> Get<T>(string key, long count = 1, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Buscar todos
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        ISet<RedisValue> GetAll(string key, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Buscar todos
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISet<T> GetAll<T>(string key, int database = (int)EDataStructure.SET) where T : class;

        #endregion

        #region MOVE

        /// <summary>
        ///     Mover (assíncrono)
        /// </summary>
        /// <param name="keySource"></param>
        /// <param name="keyDestination"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> MoveAsync(string keySource, string keyDestination, RedisValue value,
            int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Mover (assíncrono)
        /// </summary>
        /// <param name="keySource"></param>
        /// <param name="keyDestination"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> MoveAsync<T>(string keySource, string keyDestination, T value,
            int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Mover
        /// </summary>
        /// <param name="keySource"></param>
        /// <param name="keyDestination"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Move(string keySource, string keyDestination, RedisValue value, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Mover
        /// </summary>
        /// <param name="keySource"></param>
        /// <param name="keyDestination"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Move<T>(string keySource, string keyDestination, T value, int database = (int)EDataStructure.SET)
            where T : class;

        #endregion

        #region POP

        /// <summary>
        ///     Estourar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<RedisValue> PopAsync(string key, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Estourar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> PopAsync<T>(string key, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Estourar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        RedisValue Pop(string key, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Estourar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Pop<T>(string key, int database = (int)EDataStructure.SET) where T : class;

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key, RedisValue value, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> RemoveAsync<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Remover alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> RemoveRangeAsync(string key, IEnumerable<RedisValue> values, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Remover alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<long> RemoveRangeAsync<T>(string key, IEnumerable<T> values, int database = (int)EDataStructure.SET)
            where T : class;

        /// <summary>
        ///     Remover
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Remove(string key, RedisValue value, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Remover
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Remove<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class;

        /// <summary>
        ///     Remover alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long RemoveRange(string key, IEnumerable<RedisValue> values, int database = (int)EDataStructure.SET);

        /// <summary>
        ///     Remover alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long RemoveRange<T>(string key, IEnumerable<T> values, int database = (int)EDataStructure.SET) where T : class;

        #endregion
    }
}