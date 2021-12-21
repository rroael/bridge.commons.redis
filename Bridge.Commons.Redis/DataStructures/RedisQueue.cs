using System.Threading.Tasks;
using Bridge.Commons.Compress;
using Bridge.Commons.Redis.Commons;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.DataStructures
{
    /// <summary>
    ///     Redis Queue
    /// </summary>
    public class RedisQueue : RedisCommons, IRedisQueue
    {
        #region CONSTRUCTOR

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="redisContext"></param>
        public RedisQueue(IRedisContext redisContext) : base(redisContext)
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

        #region COUNT

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> CountAsync(string key, int database = (int)EDataStructure.QUEUE)
        {
            return await GetDatabase(database).ListLengthAsync(key, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Contagem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Count(string key, int database = (int)EDataStructure.QUEUE)
        {
            return GetDatabase(database).ListLength(key, CommandFlags.PreferReplica);
        }

        #endregion

        #region DEQUEUE

        /// <summary>
        ///     Desenfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<RedisValue> DequeueAsync(string key, int database = (int)EDataStructure.QUEUE)
        {
            return await GetDatabase(database).ListLeftPopAsync(key, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Desenfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> DequeueAsync<T>(string key, int database = (int)EDataStructure.QUEUE)
            where T : class
        {
            return MsgPackUtil.Deserialize<T>(await DequeueAsync(key, database));
        }

        /// <summary>
        ///     Desenfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public RedisValue Dequeue(string key, int database = (int)EDataStructure.QUEUE)
        {
            return GetDatabase(database).ListLeftPop(key, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Desenfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Dequeue<T>(string key, int database = (int)EDataStructure.QUEUE) where T : class
        {
            return MsgPackUtil.Deserialize<T>(Dequeue(key, database));
        }

        #endregion

        #region ENQUEUE

        /// <summary>
        ///     Enfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task EnqueueAsync(string key, string value, int database = (int)EDataStructure.QUEUE)
        {
            await GetDatabase(database).ListRightPushAsync(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Enfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task EnqueueAsync(string key, byte[] value, int database = (int)EDataStructure.QUEUE)
        {
            await GetDatabase(database).ListRightPushAsync(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Enfileirar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task EnqueueAsync<T>(string key, T value, int database = (int)EDataStructure.QUEUE)
            where T : class
        {
            await EnqueueAsync(key, MsgPackUtil.Serialize(value), database);
        }

        /// <summary>
        ///     Enfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        public void Enqueue(string key, string value, int database = (int)EDataStructure.QUEUE)
        {
            GetDatabase(database).ListRightPush(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Enfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        public void Enqueue(string key, byte[] value, int database = (int)EDataStructure.QUEUE)
        {
            GetDatabase(database).ListRightPush(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Enfileirar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        public void Enqueue<T>(string key, T value, int database = (int)EDataStructure.QUEUE) where T : class
        {
            Enqueue(key, MsgPackUtil.Serialize(value), database);
        }

        #endregion

        #region EXISTS

        /// <summary>
        ///     Verifica existência (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.QUEUE)
        {
            return await KeyExistsAsync(key, database);
        }

        /// <summary>
        ///     Verifica existência
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Exists(string key, int database = (int)EDataStructure.QUEUE)
        {
            return KeyExists(key, database);
        }

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover fila (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> RemoveQueueAsync(string key, int database = (int)EDataStructure.QUEUE)
        {
            return await KeyDeleteAsync(key, database);
        }

        /// <summary>
        ///     Remover fila
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool RemoveQueue(string key, int database = (int)EDataStructure.QUEUE)
        {
            return KeyDelete(key, database);
        }

        #endregion
    }
}