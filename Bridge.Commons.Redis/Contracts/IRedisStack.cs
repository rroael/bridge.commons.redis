using System;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Contracts
{
    /// <summary>
    ///     Redis Stack
    /// </summary>
    public interface IRedisStack : IDisposable
    {
        /// <summary>
        ///     Fechar
        /// </summary>
        void Close();

        #region EXISTS

        /// <summary>
        ///     Verifica existência (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Verifica existência
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Exists(string key, int database = (int)EDataStructure.STACK);

        #endregion

        #region POP

        /// <summary>
        ///     Estourar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<RedisValue> PopAsync(string key, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Estourar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> PopAsync<T>(string key, int database = (int)EDataStructure.STACK) where T : class;

        /// <summary>
        ///     Estourar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        RedisValue Pop(string key, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Estourar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Pop<T>(string key, int database = (int)EDataStructure.STACK) where T : class;

        #endregion

        #region PUSH

        /// <summary>
        ///     Enviar informações (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task PushAsync(string key, string value, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Enviar informações (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task PushAsync(string key, byte[] value, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Enviar informações (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task PushAsync<T>(string key, T value, int database = (int)EDataStructure.STACK) where T : class;

        /// <summary>
        ///     Enviar informações (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        void Push(string key, string value, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Enviar informações (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        void Push(string key, byte[] value, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Enviar informações
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        void Push<T>(string key, T value, int database = (int)EDataStructure.STACK) where T : class;

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remove (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key, int database = (int)EDataStructure.STACK);

        /// <summary>
        ///     Remove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Remove(string key, int database = (int)EDataStructure.STACK);

        #endregion
    }
}