using System;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Contracts
{
    /// <summary>
    ///     Fila do Redis
    /// </summary>
    public interface IRedisQueue : IDisposable
    {
        /// <summary>
        ///     Fechar
        /// </summary>
        void Close();

        #region COUNT

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<long> CountAsync(string key, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Contagem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        long Count(string key, int database = (int)EDataStructure.QUEUE);

        #endregion

        #region DEQUEUE

        /// <summary>
        ///     Desenfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<RedisValue> DequeueAsync(string key, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Desenfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> DequeueAsync<T>(string key, int database = (int)EDataStructure.QUEUE) where T : class;

        /// <summary>
        ///     Desenfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        RedisValue Dequeue(string key, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Desenfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Dequeue<T>(string key, int database = (int)EDataStructure.QUEUE) where T : class;

        #endregion

        #region ENQUEUE

        /// <summary>
        ///     Enfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task EnqueueAsync(string key, string value, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Enfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task EnqueueAsync(string key, byte[] value, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Enfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task EnqueueAsync<T>(string key, T value, int database = (int)EDataStructure.QUEUE) where T : class;

        /// <summary>
        ///     Enfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        void Enqueue(string key, string value, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Enfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        void Enqueue(string key, byte[] value, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Enfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        void Enqueue<T>(string key, T value, int database = (int)EDataStructure.QUEUE) where T : class;

        #endregion

        #region EXISTS

        /// <summary>
        ///     Verificar existencia (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Verificar existencia
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool Exists(string key, int database = (int)EDataStructure.QUEUE);

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover fila (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Task<bool> RemoveQueueAsync(string key, int database = (int)EDataStructure.QUEUE);

        /// <summary>
        ///     Remover fila
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        bool RemoveQueue(string key, int database = (int)EDataStructure.QUEUE);

        #endregion
    }
}