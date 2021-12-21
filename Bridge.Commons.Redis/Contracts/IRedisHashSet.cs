using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Contracts
{
    /// <summary>
    ///     Redis Hash set
    /// </summary>
    public interface IRedisHashSet : IDisposable
    {
        #region TTL

        /// <summary>
        ///     Setar TTL
        /// </summary>
        /// <param name="key"></param>
        /// <param name="durantion"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool SetTtl(string key, TimeSpan durantion, int database = (int)EDataStructure.HASHSET);

        #endregion

        /// <summary>
        ///     Fechar
        /// </summary>
        void Close();

        #region COUNT

        /// <summary>
        ///     Contagem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> CountAsync(string key, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Conta
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Count(string key, int database = (int)EDataStructure.HASHSET);

        #endregion

        #region EXISTS

        /// <summary>
        ///     Existe (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Existe campo (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> FieldExistsAsync(string key, string field, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Existe
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Exists(string key, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Existe campo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool FieldExists(string key, string field, int database = (int)EDataStructure.HASHSET);

        #endregion

        #region GET

        /// <summary>
        ///     Buscar chaves por pesquisa
        /// </summary>
        /// <param name="search"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IEnumerable<RedisKey> GetHashKeysBySearch(string search, int database = (int)EDataStructure.HASHSET);

        //Key
        /// <summary>
        ///     Buscar as chaves hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IList<string> GetHashKeys(string key, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Buscar as chaves hash (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<IList<string>> GetHashKeysAsync(string key, int database = (int)EDataStructure.HASHSET);

        //Field
        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        string Get(string key, string field, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key, string field, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Buscar (genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(string key, string field, int database = (int)EDataStructure.HASHSET) where T : class;

        /// <summary>
        ///     Buscar (genérico/ assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, string field, int database = (int)EDataStructure.HASHSET) where T : class;

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IDictionary<string, byte[]> Get(string key, string[] fields, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<IDictionary<string, byte[]>> GetAsync(string key, string[] fields,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Pegar ou setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="func"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOrSet<T>(string key, string field, Func<T> func, int database = (int)EDataStructure.HASHSET)
            where T : class;

        /// <summary>
        ///     Pegar ou setar assíncrono
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="asyncFunc"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetOrSetAsync<T>(string key, string field, Func<Task<T>> asyncFunc,
            int database = (int)EDataStructure.HASHSET) where T : class;

        //All
        /// <summary>
        ///     Buscar todos
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDictionary<string, T> GetAll<T>(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET) where T : class;

        /// <summary>
        ///     Buscar todos (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IDictionary<string, T>> GetAllAsync<T>(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET) where T : class;

        /// <summary>
        ///     Buscar todos (Crú)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IDictionary<string, byte[]> GetAllRaw(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Buscar todos (crú / assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<IDictionary<string, byte[]>> GetAllRawAsync(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Buscar todos (string)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IDictionary<string, string> GetAllString(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Buscar todos (string / assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<IDictionary<string, string>> GetAllStringAsync(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET);

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remove campo da chave (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> RemoveKeyFieldAsync(string key, string field, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Remove campos de chave (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> RemoveKeyFieldsAsync(string key, IEnumerable<string> hashFields,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Remove chave (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> RemoveKeyAsync(string key, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Remove campo da chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool RemoveKeyField(string key, string field, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Remove campos da chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long RemoveKeyFields(string key, IEnumerable<string> hashFields, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Remove chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool RemoveKey(string key, int database = (int)EDataStructure.HASHSET);

        #endregion

        #region SET

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, string field, string value, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, IDictionary<string, string> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, IDictionary<string, int> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(string key, IDictionary<string, T> keyValues,
            int database = (int)EDataStructure.HASHSET)
            where T : class;

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(string key, string field, T value, int database = (int)EDataStructure.HASHSET)
            where T : class;

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<bool> SetAsync<T>(string key, IDictionary<string, T> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET) where T : class;

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Set(string key, string field, string value, int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Set(string key, IDictionary<string, string> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Set(string key, IDictionary<string, int> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET);

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(string key, IDictionary<string, T> keyValues, int database = (int)EDataStructure.HASHSET)
            where T : class;

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(string key, string field, T value, int database = (int)EDataStructure.HASHSET) where T : class;

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Set<T>(string key, IDictionary<string, T> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET) where T : class;

        #endregion
    }
}