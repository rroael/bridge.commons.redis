using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Enums;

namespace Bridge.Commons.Redis.Contracts
{
    /// <summary>
    ///     Redis List
    /// </summary>
    public interface IRedisList : IDisposable
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
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task AddAsync(string key, string value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Adicionar (assíncrono / genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task AddAsync<T>(string key, T value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class;

        /// <summary>
        ///     Adicionar alcance (assíncrono / genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task AddRangeAsync<T>(string key, IEnumerable<T> values, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class;

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        void Add(string key, string value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Adicionar (genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        void Add<T>(string key, T value, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class;

        /// <summary>
        ///     Adicionar alcance (genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="maxItems"></param>
        /// <param name="duration"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        void AddRange<T>(string key, IEnumerable<T> values, int maxItems = 1000, TimeSpan? duration = null,
            int database = (int)EDataStructure.LIST) where T : class;

        #endregion

        #region CLEAR

        /// <summary>
        ///     Limpar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ClearAsync(string key, int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Limpar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Clear(string key, int database = (int)EDataStructure.LIST);

        #endregion

        #region COUNT

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> CountAsync(string key, int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Contagem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Count(string key, int database = (int)EDataStructure.LIST);

        #endregion

        #region EXISTS

        /// <summary>
        ///     Verificar existencia (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Verificar existencia
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Exists(string key, int database = (int)EDataStructure.LIST);

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
        Task<IList<string>> GetAsync(string key, int skip = 0, int take = 0, int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Buscar (assíncrono / genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IList<T>> GetAsync<T>(string key, int skip = 0, int take = 0, int database = (int)EDataStructure.LIST)
            where T : class;

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IList<string> Get(string key, int skip = 0, int take = 0, int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Buscar (genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> Get<T>(string key, int skip = 0, int take = 0, int database = (int)EDataStructure.LIST)
            where T : class;

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover (aasíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task RemoveAsync<T>(string key, T value, int database = (int)EDataStructure.LIST) where T : class;

        /// <summary>
        ///     Remover alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task RemoveRangeAsync(string key, long count, int database = (int)EDataStructure.LIST);

        /// <summary>
        ///     Remover (genérico)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        void Remove<T>(string key, T value, int database = (int)EDataStructure.LIST) where T : class;

        /// <summary>
        ///     Remover alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        void RemoveRange(string key, long count, int database = (int)EDataStructure.LIST);

        #endregion
    }
}