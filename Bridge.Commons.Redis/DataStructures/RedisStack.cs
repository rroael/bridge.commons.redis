using System.Threading.Tasks;
using Bridge.Commons.Compress;
using Bridge.Commons.Redis.Commons;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.Redis.Enums;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.DataStructures
{
    /// <summary>
    /// </summary>
    public class RedisStack : RedisCommons, IRedisStack
    {
        #region CONSTRUCTOR

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="redisContext"></param>
        public RedisStack(IRedisContext redisContext) : base(redisContext)
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

        #region EXISTS

        /// <summary>
        ///     Verifica existência (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.STACK)
        {
            return await KeyExistsAsync(key, database);
        }

        /// <summary>
        ///     Verifica existência
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Exists(string key, int database = (int)EDataStructure.STACK)
        {
            return KeyExists(key, database);
        }

        #endregion

        #region POP

        /// <summary>
        ///     Remove item (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<RedisValue> PopAsync(string key, int database = (int)EDataStructure.STACK)
        {
            return await GetDatabase(database).ListRightPopAsync(key, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove item (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> PopAsync<T>(string key, int database = (int)EDataStructure.STACK) where T : class
        {
            return MsgPackUtil.Deserialize<T>(await PopAsync(key, database));
        }

        /// <summary>
        ///     Remove item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public RedisValue Pop(string key, int database = (int)EDataStructure.STACK)
        {
            return GetDatabase(database).ListRightPop(key, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Pop<T>(string key, int database = (int)EDataStructure.STACK) where T : class
        {
            return MsgPackUtil.Deserialize<T>(Pop(key, database));
        }

        #endregion

        #region PUSH

        /// <summary>
        ///     Adiciona item (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task PushAsync(string key, string value, int database = (int)EDataStructure.STACK)
        {
            await GetDatabase(database).ListRightPushAsync(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adiciona item (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task PushAsync(string key, byte[] value, int database = (int)EDataStructure.STACK)
        {
            await GetDatabase(database).ListRightPushAsync(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adiciona item (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task PushAsync<T>(string key, T value, int database = (int)EDataStructure.STACK)
            where T : class
        {
            await PushAsync(key, MsgPackUtil.Serialize(value), database);
        }

        /// <summary>
        ///     Adiciona item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        public void Push(string key, string value, int database = (int)EDataStructure.STACK)
        {
            GetDatabase(database).ListRightPush(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adiciona item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        public void Push(string key, byte[] value, int database = (int)EDataStructure.STACK)
        {
            GetDatabase(database).ListRightPush(key, value, flags: CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adiciona item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        public void Push<T>(string key, T value, int database = (int)EDataStructure.STACK) where T : class
        {
            Push(key, MsgPackUtil.Serialize(value), database);
        }

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key, int database = (int)EDataStructure.STACK)
        {
            return await KeyDeleteAsync(key, database);
        }

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Remove(string key, int database = (int)EDataStructure.STACK)
        {
            return KeyDelete(key, database);
        }

        #endregion
    }
}