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
    /// </summary>
    public class RedisSet : RedisCommons, IRedisSet
    {
        #region CONSTRUCTOR

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="redisContext"></param>
        public RedisSet(IRedisContext redisContext) : base(redisContext)
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
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, RedisValue value, int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database).SetAddAsync(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> AddAsync<T>(string key, T value, int database = (int)EDataStructure.SET)
            where T : class
        {
            return await GetDatabase(database)
                .SetAddAsync(key, MsgPackUtil.Serialize(value), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<long> AddRangeAsync<T>(string key, IEnumerable<T> values,
            int database = (int)EDataStructure.SET) where T : class
        {
            var setValues = Array.ConvertAll(values.Select(MsgPackUtil.Serialize).ToArray(),
                item => (RedisValue)item);

            return await GetDatabase(database).SetAddAsync(key, setValues, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Add(string key, RedisValue value, int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database).SetAdd(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Add<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class
        {
            return GetDatabase(database).SetAdd(key, MsgPackUtil.Serialize(value), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Adicionar alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public long AddRange<T>(string key, IEnumerable<T> values, int database = (int)EDataStructure.SET)
            where T : class
        {
            var setValues = Array.ConvertAll(values.Select(MsgPackUtil.Serialize).ToArray(),
                item => (RedisValue)item);

            return GetDatabase(database).SetAdd(key, setValues, CommandFlags.DemandMaster);
        }

        #endregion

        #region CLEAR

        /// <summary>
        ///     Limpar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ClearAsync(string key, int database = (int)EDataStructure.SET)
        {
            return await KeyDeleteAsync(key, database);
        }

        /// <summary>
        ///     Limpar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Clear(string key, int database = (int)EDataStructure.SET)
        {
            return KeyDelete(key, database);
        }

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
        public async Task<ISet<RedisValue>> CombineAsync(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(await GetDatabase(database)
                .SetCombineAsync(setOperation, firstKey, secondKey, CommandFlags.DemandMaster));
        }

        /// <summary>
        ///     Combinar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<ISet<RedisValue>> CombineAsync(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(await GetDatabase(database).SetCombineAsync(setOperation,
                Array.ConvertAll(keysToCombine.ToArray(), item => (RedisKey)item), CommandFlags.DemandMaster));
        }

        /// <summary>
        ///     Combinar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ISet<T>> CombineAsync<T>(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET) where T : class
        {
            var values = await GetDatabase(database)
                .SetCombineAsync(setOperation, firstKey, secondKey, CommandFlags.DemandMaster);
            return new HashSet<T>(from v in values select MsgPackUtil.Deserialize<T>(v));
        }

        /// <summary>
        ///     Combinar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ISet<T>> CombineAsync<T>(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET) where T : class
        {
            var values = await GetDatabase(database).SetCombineAsync(setOperation,
                Array.ConvertAll(keysToCombine.ToArray(), k => (RedisKey)k), CommandFlags.DemandMaster);
            return new HashSet<T>(values.Select(v => MsgPackUtil.Deserialize<T>(v)));
        }

        /// <summary>
        ///     Combinar e guardar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync(SetOperation setOperation, string keyDestination, string firstKey,
            string secondKey,
            int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database)
                .SetCombineAndStoreAsync(setOperation, keyDestination, firstKey, secondKey, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Combinar e guardar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync(SetOperation setOperation, string keyDestination,
            IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database).SetCombineAndStoreAsync(setOperation, keyDestination,
                Array.ConvertAll(keysToCombine.ToArray(), item => (RedisKey)item), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Combinar e guardar (assícrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync<T>(SetOperation setOperation, string keyDestination,
            string firstKey, string secondKey,
            int database = (int)EDataStructure.SET) where T : class
        {
            return await GetDatabase(database).SetCombineAndStoreAsync(setOperation, keyDestination, firstKey,
                secondKey,
                CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Combinar e guardar (assíncrono)
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<long> CombineAndStoreAsync<T>(SetOperation setOperation, string keyDestination,
            IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET) where T : class
        {
            return await GetDatabase(database).SetCombineAndStoreAsync(setOperation, keyDestination,
                Array.ConvertAll(keysToCombine.ToArray(), k => (RedisKey)k), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public ISet<RedisValue> Combine(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(GetDatabase(database)
                .SetCombine(setOperation, firstKey, secondKey, CommandFlags.DemandMaster));
        }

        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public ISet<RedisValue> Combine(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(GetDatabase(database).SetCombine(setOperation,
                Array.ConvertAll(keysToCombine.ToArray(), item => (RedisKey)item), CommandFlags.DemandMaster));
        }

        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ISet<T> Combine<T>(SetOperation setOperation, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET) where T : class
        {
            var values = GetDatabase(database).SetCombine(setOperation, firstKey, secondKey, CommandFlags.DemandMaster);
            return new HashSet<T>(from v in values select MsgPackUtil.Deserialize<T>(v));
        }

        /// <summary>
        ///     Combinar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ISet<T> Combine<T>(SetOperation setOperation, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET) where T : class
        {
            var values = GetDatabase(database)
                .SetCombine(setOperation, Array.ConvertAll(keysToCombine.ToArray(), k => (RedisKey)k),
                    CommandFlags.DemandMaster);
            return new HashSet<T>(values.Select(v => MsgPackUtil.Deserialize<T>(v)));
        }

        /// <summary>
        ///     Combinar e guardar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="firstKey"></param>
        /// <param name="secondKey"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long CombineAndStore(SetOperation setOperation, string keyDestination, string firstKey, string secondKey,
            int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database)
                .SetCombineAndStore(setOperation, keyDestination, firstKey, secondKey, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Combinar e guardar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long CombineAndStore(SetOperation setOperation, string keyDestination, IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database).SetCombineAndStore(setOperation, keyDestination,
                Array.ConvertAll(keysToCombine.ToArray(), item => (RedisKey)item), CommandFlags.DemandMaster);
        }

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
        public long CombineAndStore<T>(SetOperation setOperation, string keyDestination, string firstKey,
            string secondKey,
            int database = (int)EDataStructure.SET) where T : class
        {
            return GetDatabase(database).SetCombineAndStore(setOperation, keyDestination, firstKey, secondKey,
                CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Combinar e guardar
        /// </summary>
        /// <param name="setOperation"></param>
        /// <param name="keyDestination"></param>
        /// <param name="keysToCombine"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public long CombineAndStore<T>(SetOperation setOperation, string keyDestination,
            IEnumerable<string> keysToCombine,
            int database = (int)EDataStructure.SET) where T : class
        {
            return GetDatabase(database).SetCombineAndStore(setOperation, keyDestination,
                Array.ConvertAll(keysToCombine.ToArray(), k => (RedisKey)k), CommandFlags.DemandMaster);
        }

        #endregion

        #region CONTAINS

        /// <summary>
        ///     Verifica se contém algo (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ContainsAsync(string key, RedisValue value, int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database).SetContainsAsync(key, value, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Verifica se contém algo (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> ContainsAsync<T>(string key, T value, int database = (int)EDataStructure.SET)
            where T : class
        {
            return await GetDatabase(database)
                .SetContainsAsync(key, MsgPackUtil.Serialize(value), CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Verifica se contém algo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Contains(string key, RedisValue value, int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database).SetContains(key, value, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Verifica se contém algo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>(string key, T value, int database = (int)EDataStructure.SET) where T : class
        {
            return GetDatabase(database).SetContains(key, MsgPackUtil.Serialize(value), CommandFlags.PreferReplica);
        }

        #endregion

        #region COUNT

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> CountAsync(string key, int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database).SetLengthAsync(key, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Contagem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Count(string key, int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database).SetLength(key, CommandFlags.PreferReplica);
        }

        #endregion

        #region EXISTS

        /// <summary>
        ///     Verifica existência (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.SET)
        {
            return await KeyExistsAsync(key, database);
        }

        /// <summary>
        ///     Verifica existência
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Exists(string key, int database = (int)EDataStructure.SET)
        {
            return KeyExists(key, database);
        }

        #endregion

        #region GET

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<ISet<RedisValue>> GetAsync(string key, long count = 1,
            int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(await GetDatabase(database)
                .SetRandomMembersAsync(key, count, CommandFlags.PreferReplica));
        }

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ISet<T>> GetAsync<T>(string key, long count = 1, int database = (int)EDataStructure.SET)
            where T : class
        {
            var values = await GetDatabase(database).SetRandomMembersAsync(key, count, CommandFlags.PreferReplica);
            return new HashSet<T>(values.Select(v => MsgPackUtil.Deserialize<T>(v)));
        }

        /// <summary>
        ///     Buscar todos (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<ISet<RedisValue>> GetAllAsync(string key, int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(await GetDatabase(database)
                .SetMembersAsync(key, CommandFlags.PreferReplica));
        }

        /// <summary>
        ///     Buscar todos (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ISet<T>> GetAllAsync<T>(string key, int database = (int)EDataStructure.SET)
            where T : class
        {
            var values = await GetDatabase(database).SetMembersAsync(key, CommandFlags.PreferReplica);
            return new HashSet<T>(values.Select(v => MsgPackUtil.Deserialize<T>(v)));
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public ISet<RedisValue> Get(string key, long count = 1, int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(GetDatabase(database)
                .SetRandomMembers(key, count, CommandFlags.PreferReplica));
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ISet<T> Get<T>(string key, long count = 1, int database = (int)EDataStructure.SET) where T : class
        {
            var values = GetDatabase(database).SetRandomMembers(key, count, CommandFlags.PreferReplica);
            return new HashSet<T>(values.Select(v => MsgPackUtil.Deserialize<T>(v)));
        }

        /// <summary>
        ///     Buscar todos
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public ISet<RedisValue> GetAll(string key, int database = (int)EDataStructure.SET)
        {
            return new HashSet<RedisValue>(GetDatabase(database).SetMembers(key, CommandFlags.PreferReplica));
        }

        /// <summary>
        ///     Buscar todos
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ISet<T> GetAll<T>(string key, int database = (int)EDataStructure.SET) where T : class
        {
            var values = GetDatabase(database).SetMembers(key, CommandFlags.PreferReplica);
            return new HashSet<T>(values.Select(v => MsgPackUtil.Deserialize<T>(v)));
        }

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
        public async Task<bool> MoveAsync(string keySource, string keyDestination, RedisValue value,
            int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database)
                .SetMoveAsync(keySource, keyDestination, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Mover (assíncrono)
        /// </summary>
        /// <param name="keySource"></param>
        /// <param name="keyDestination"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> MoveAsync<T>(string keySource, string keyDestination, T value,
            int database = (int)EDataStructure.SET) where T : class
        {
            return await GetDatabase(database)
                .SetMoveAsync(keySource, keyDestination, MsgPackUtil.Serialize(value), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Mover
        /// </summary>
        /// <param name="keySource"></param>
        /// <param name="keyDestination"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Move(string keySource, string keyDestination, RedisValue value,
            int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database)
                .SetMove(keySource, keyDestination, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Mover
        /// </summary>
        /// <param name="keySource"></param>
        /// <param name="keyDestination"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Move<T>(string keySource, string keyDestination, T value,
            int database = (int)EDataStructure.SET) where T : class
        {
            return GetDatabase(database)
                .SetMove(keySource, keyDestination, MsgPackUtil.Serialize(value), CommandFlags.DemandMaster);
        }

        #endregion

        #region POP

        /// <summary>
        ///     Remove item aleatório do SET (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<RedisValue> PopAsync(string key, int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database).SetPopAsync(key, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove item aleatório do SET (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> PopAsync<T>(string key, int database = (int)EDataStructure.SET) where T : class
        {
            return MsgPackUtil.Deserialize<T>(await GetDatabase(database).SetPopAsync(key, CommandFlags.DemandMaster));
        }

        /// <summary>
        ///     Remove item aleatório do SET
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public RedisValue Pop(string key, int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database).SetPop(key, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove item aleatório do SET
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Pop<T>(string key, int database = (int)EDataStructure.SET) where T : class
        {
            return MsgPackUtil.Deserialize<T>(GetDatabase(database).SetPop(key, CommandFlags.DemandMaster));
        }

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key, RedisValue value, int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database).SetRemoveAsync(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remover (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> RemoveAsync<T>(string key, T value, int database = (int)EDataStructure.SET)
            where T : class
        {
            return await GetDatabase(database)
                .SetRemoveAsync(key, MsgPackUtil.Serialize(value), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remover alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> RemoveRangeAsync(string key, IEnumerable<RedisValue> values,
            int database = (int)EDataStructure.SET)
        {
            return await GetDatabase(database).SetRemoveAsync(key, values.ToArray(), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remover alcance (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<long> RemoveRangeAsync<T>(string key, IEnumerable<T> values,
            int database = (int)EDataStructure.SET) where T : class
        {
            var removeValues = Array.ConvertAll(values.Select(MsgPackUtil.Serialize).ToArray(),
                item => (RedisValue)item);

            return await GetDatabase(database).SetRemoveAsync(key, removeValues, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remover
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Remove(string key, RedisValue value, int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database).SetRemove(key, value, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remover
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Remove<T>(string key, T value, int database = (int)EDataStructure.SET)
            where T : class
        {
            return GetDatabase(database).SetRemove(key, MsgPackUtil.Serialize(value), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remover alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long RemoveRange(string key, IEnumerable<RedisValue> values,
            int database = (int)EDataStructure.SET)
        {
            return GetDatabase(database).SetRemove(key, values.ToArray(), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remover alcance
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public long RemoveRange<T>(string key, IEnumerable<T> values,
            int database = (int)EDataStructure.SET) where T : class
        {
            var removeValues = Array.ConvertAll(values.Select(MsgPackUtil.Serialize).ToArray(),
                item => (RedisValue)item);

            return GetDatabase(database).SetRemove(key, removeValues, CommandFlags.DemandMaster);
        }

        #endregion
    }
}