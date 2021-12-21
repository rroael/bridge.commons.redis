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
    ///     Redis Hash Set
    /// </summary>
    public class RedisHashSet : RedisCommons, IRedisHashSet
    {
        #region CONSTRUCTOR

        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="redisContext"></param>
        public RedisHashSet(IRedisContext redisContext) : base(redisContext)
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
        public bool SetTtl(string key, TimeSpan duration, int database = (int)EDataStructure.HASHSET)
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

        #region COUNT

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> CountAsync(string key, int database = (int)EDataStructure.HASHSET)
        {
            return await GetDatabase(database).HashLengthAsync(key, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Contagem (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long Count(string key, int database = (int)EDataStructure.HASHSET)
        {
            return GetDatabase(database).HashLength(key, CommandFlags.PreferReplica);
        }

        #endregion

        #region EXISTS

        /// <summary>
        ///     Verifica existência (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, int database = (int)EDataStructure.HASHSET)
        {
            return await KeyExistsAsync(key, database);
        }

        /// <summary>
        ///     Verifica existência de campo (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> FieldExistsAsync(string key, string field, int database = (int)EDataStructure.HASHSET)
        {
            return await GetDatabase(database).HashExistsAsync(key, field, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Verifica existência
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Exists(string key, int database = (int)EDataStructure.HASHSET)
        {
            return KeyExists(key, database);
        }

        /// <summary>
        ///     Verifica existência de campo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool FieldExists(string key, string field, int database = (int)EDataStructure.HASHSET)
        {
            return GetDatabase(database).HashExists(key, field, CommandFlags.PreferReplica);
        }

        #endregion

        #region GET

        /// <summary>
        ///     Buscar chaves por pesquisa
        /// </summary>
        /// <param name="search"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IEnumerable<RedisKey> GetHashKeysBySearch(string search, int database = (int)EDataStructure.HASHSET)
        {
            return GetKeys(search, database);
        }

        //Key
        /// <summary>
        ///     Busca as chaves hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IList<string> GetHashKeys(string key, int database = (int)EDataStructure.HASHSET)
        {
            return GetDatabase(database).HashKeys(key, CommandFlags.PreferReplica).Select(v => v.ToString()).ToList();
        }

        /// <summary>
        ///     Busca chaves hash (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<IList<string>> GetHashKeysAsync(string key, int database = (int)EDataStructure.HASHSET)
        {
            return (await GetDatabase(database).HashKeysAsync(key, CommandFlags.PreferReplica))
                .Select(v => v.ToString())
                .ToList();
        }

        //Field
        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public string Get(string key, string field, int database = (int)EDataStructure.HASHSET)
        {
            return GetDatabase(database).HashGet(key, field, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key, string field, int database = (int)EDataStructure.HASHSET)
        {
            return await GetDatabase(database).HashGetAsync(key, field, CommandFlags.PreferReplica);
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(string key, string field, int database = (int)EDataStructure.HASHSET) where T : class
        {
            return MsgPackUtil.Deserialize<T>(GetDatabase(database).HashGet(key, field, CommandFlags.PreferReplica));
        }

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, string field, int database = (int)EDataStructure.HASHSET)
            where T : class
        {
            return MsgPackUtil.Deserialize<T>(await GetDatabase(database)
                .HashGetAsync(key, field, CommandFlags.PreferReplica));
        }

        /// <summary>
        ///     Buscar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IDictionary<string, byte[]> Get(string key, string[] fields,
            int database = (int)EDataStructure.HASHSET)
        {
            IDictionary<string, byte[]> result = new Dictionary<string, byte[]>();

            var values = GetDatabase(database)
                .HashGet(key, fields.Select(v => (RedisValue)v).ToArray(), CommandFlags.PreferReplica);

            for (var i = 0; i < fields.Length; i++)
                result.Add(fields.ElementAt(i), values[i]);

            return result;
        }

        /// <summary>
        ///     Buscar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<IDictionary<string, byte[]>> GetAsync(string key, string[] fields,
            int database = (int)EDataStructure.HASHSET)
        {
            IDictionary<string, byte[]> result = new Dictionary<string, byte[]>();

            var values = await GetDatabase(database)
                .HashGetAsync(key, fields.Select(v => (RedisValue)v).ToArray(), CommandFlags.PreferReplica);

            for (var i = 0; i < fields.Length; i++)
                result.Add(fields.ElementAt(i), values[i]);

            return result;
        }

        /// <summary>
        ///     Pegar ou setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="func"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOrSet<T>(string key, string field, Func<T> func, int database = (int)EDataStructure.HASHSET)
            where T : class
        {
            var result = Get<T>(key, field, database);

            if (result != null) return result;

            result = func();
            Set(key, field, result, database);

            return result;
        }

        /// <summary>
        ///     Pegar ou setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="asyncFunc"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetOrSetAsync<T>(string key, string field, Func<Task<T>> asyncFunc,
            int database = (int)EDataStructure.HASHSET)
            where T : class
        {
            var result = await GetAsync<T>(key, field, database);

            if (result != null) return result;

            result = await asyncFunc();
            await SetAsync(key, field, result, database);

            return result;
        }

        //All
        /// <summary>
        ///     Buscar todos (crú)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IDictionary<string, byte[]> GetAllRaw(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET)
        {
            IDictionary<string, byte[]> result;

            if (take > 0)
                result = GetDatabase(database)
                    .HashScan(key, pageOffset: skip, pageSize: take, flags: CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (byte[])v.Value
                    );
            else
                result = GetDatabase(database)
                    .HashGetAll(key, CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (byte[])v.Value
                    );

            return result;
        }

        /// <summary>
        ///     Buscar todos (crú / assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<IDictionary<string, byte[]>> GetAllRawAsync(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET)
        {
            IDictionary<string, byte[]> result;

            if (take > 0)
                result = GetDatabase(database)
                    .HashScan(key, pageOffset: skip, pageSize: take, flags: CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (byte[])v.Value
                    );
            else
                result = (await GetDatabase(database)
                        .HashGetAllAsync(key, CommandFlags.PreferReplica))
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (byte[])v.Value
                    );

            return result;
        }

        /// <summary>
        ///     Buscar todos
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDictionary<string, T> GetAll<T>(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET) where T : class
        {
            IDictionary<string, T> result;

            if (take > 0)
                result = GetDatabase(database)
                    .HashScan(key, pageOffset: skip, pageSize: take, flags: CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => MsgPackUtil.Deserialize<T>(v.Value)
                    );
            else
                result = GetDatabase(database)
                    .HashGetAll(key, CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => MsgPackUtil.Deserialize<T>(v.Value)
                    );

            return result;
        }

        /// <summary>
        ///     Buscar todos (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IDictionary<string, T>> GetAllAsync<T>(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET) where T : class
        {
            IDictionary<string, T> result;

            if (take > 0)
                result = GetDatabase(database)
                    .HashScan(key, pageOffset: skip, pageSize: take, flags: CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => MsgPackUtil.Deserialize<T>(v.Value)
                    );
            else
                result = (await GetDatabase(database)
                        .HashGetAllAsync(key, CommandFlags.PreferReplica))
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => MsgPackUtil.Deserialize<T>(v.Value)
                    );

            return result;
        }

        /// <summary>
        ///     Buscar todos (string)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetAllString(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET)
        {
            IDictionary<string, string> result;

            if (take > 0)
                result = GetDatabase(database)
                    .HashScan(key, pageOffset: skip, pageSize: take, flags: CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (string)v.Value
                    );
            else
                result = GetDatabase(database)
                    .HashGetAll(key, CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (string)v.Value
                    );

            return result;
        }

        /// <summary>
        ///     Buscar todos (string / assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<IDictionary<string, string>> GetAllStringAsync(string key, int skip = 0, int take = 0,
            int database = (int)EDataStructure.HASHSET)
        {
            IDictionary<string, string> result;

            if (take > 0)
                result = GetDatabase(database)
                    .HashScan(key, pageOffset: skip, pageSize: take, flags: CommandFlags.PreferReplica)
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (string)v.Value
                    );
            else
                result = (await GetDatabase(database)
                        .HashGetAllAsync(key, CommandFlags.PreferReplica))
                    .ToDictionary
                    (
                        k => (string)k.Name,
                        v => (string)v.Value
                    );

            return result;
        }

        #endregion

        #region REMOVE

        /// <summary>
        ///     Remover campo chave (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> RemoveKeyFieldAsync(string key, string field,
            int database = (int)EDataStructure.HASHSET)
        {
            return await GetDatabase(database).HashDeleteAsync(key, field, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove campo chave (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<long> RemoveKeyFieldsAsync(string key, IEnumerable<string> hashFields,
            int database = (int)EDataStructure.HASHSET)
        {
            return await GetDatabase(database)
                .HashDeleteAsync(key, hashFields.Select(field => (RedisValue)field).ToArray(),
                    CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove chave (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> RemoveKeyAsync(string key, int database = (int)EDataStructure.HASHSET)
        {
            return await KeyDeleteAsync(key, database);
        }

        /// <summary>
        ///     Remove campo chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool RemoveKeyField(string key, string field, int database = (int)EDataStructure.HASHSET)
        {
            return GetDatabase(database).HashDelete(key, field, CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove campos chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long RemoveKeyFields(string key, IEnumerable<string> hashFields,
            int database = (int)EDataStructure.HASHSET)
        {
            return GetDatabase(database)
                .HashDelete(key, hashFields.Select(field => (RedisValue)field).ToArray(), CommandFlags.DemandMaster);
        }

        /// <summary>
        ///     Remove chave
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool RemoveKey(string key, int database = (int)EDataStructure.HASHSET)
        {
            return KeyDelete(key, database);
        }

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
        public async Task<bool> SetAsync(string key, string field, string value,
            int database = (int)EDataStructure.HASHSET)
        {
            await GetDatabase(database)
                .HashSetAsync(key, new[] { new HashEntry(field, value) }, CommandFlags.DemandMaster);
            return true;
        }

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, IDictionary<string, string> keyValues,
            bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET)
        {
            var newHashArray = keyValues.Select(hash => new HashEntry(hash.Key, hash.Value)).ToArray();

            if (removeExistingKeys)
            {
                var actualHashArray = await GetDatabase(database).HashGetAllAsync(key, CommandFlags.PreferReplica);

                var hashesToRemove = actualHashArray.Except(newHashArray).Select(x => x.Name).ToArray();
                var hashesToAdd = newHashArray.Except(actualHashArray).ToArray();

                if (hashesToRemove.Any())
                    await GetDatabase(database).HashDeleteAsync(key, hashesToRemove, CommandFlags.DemandMaster);

                if (hashesToAdd.Any())
                    await GetDatabase(database).HashSetAsync(key, newHashArray, CommandFlags.DemandMaster);
            }
            else
            {
                await GetDatabase(database).HashSetAsync(key, newHashArray, CommandFlags.DemandMaster);
            }

            return true;
        }

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, IDictionary<string, int> keyValues,
            bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET)
        {
            var newHashArray = keyValues.Select(hash => new HashEntry(hash.Key, hash.Value)).ToArray();

            if (removeExistingKeys)
            {
                var actualHashArray = await GetDatabase(database).HashGetAllAsync(key, CommandFlags.PreferReplica);

                var hashesToRemove = actualHashArray.Except(newHashArray).Select(x => x.Name).ToArray();
                var hashesToAdd = newHashArray.Except(actualHashArray).ToArray();

                if (hashesToRemove.Any())
                    await GetDatabase(database).HashDeleteAsync(key, hashesToRemove, CommandFlags.DemandMaster);

                if (hashesToAdd.Any())
                    await GetDatabase(database).HashSetAsync(key, newHashArray, CommandFlags.DemandMaster);
            }
            else
            {
                await GetDatabase(database).HashSetAsync(key, newHashArray, CommandFlags.DemandMaster);
            }

            return true;
        }

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, IDictionary<string, T> keyValues,
            int database = (int)EDataStructure.HASHSET)
            where T : class
        {
            await GetDatabase(database).HashSetAsync(key, keyValues
                    .Select(hash => new HashEntry(hash.Key, MsgPackUtil.Serialize(hash.Value))).ToArray(),
                CommandFlags.DemandMaster);

            return true;
        }

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, string field, T value,
            int database = (int)EDataStructure.HASHSET)
            where T : class
        {
            await GetDatabase(database)
                .HashSetAsync(key, new[] { new HashEntry(field, MsgPackUtil.Serialize(value)) },
                    CommandFlags.DemandMaster);

            return true;
        }

        /// <summary>
        ///     Setar (assíncrono)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, IDictionary<string, T> keyValues,
            bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET) where T : class
        {
            var newHashArray = keyValues
                .Select(v => new HashEntry(v.Key, MsgPackUtil.Serialize(v.Value))).ToArray();

            if (removeExistingKeys)
            {
                var actualHashArray = await GetDatabase(database).HashGetAllAsync(key, CommandFlags.PreferReplica);

                var hashesToRemove = actualHashArray.Except(newHashArray).Select(x => x.Name).ToArray();
                var hashesToAdd = newHashArray.Except(actualHashArray).ToArray();

                if (hashesToRemove.Any())
                    await GetDatabase(database).HashDeleteAsync(key, hashesToRemove, CommandFlags.DemandMaster);

                if (hashesToAdd.Any())
                    await GetDatabase(database).HashSetAsync(key, newHashArray, CommandFlags.DemandMaster);
            }
            else
            {
                await GetDatabase(database).HashSetAsync(key, newHashArray, CommandFlags.DemandMaster);
            }

            return true;
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Set(string key, string field, string value, int database = (int)EDataStructure.HASHSET)
        {
            GetDatabase(database).HashSet(key, new[] { new HashEntry(field, value) }, CommandFlags.DemandMaster);
            return true;
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Set(string key, IDictionary<string, string> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET)
        {
            var newHashArray = keyValues.Select(hash => new HashEntry(hash.Key, hash.Value)).ToArray();

            if (removeExistingKeys)
            {
                var actualHashArray = GetDatabase(database).HashGetAll(key, CommandFlags.PreferReplica);

                var hashesToRemove = actualHashArray.Except(newHashArray).Select(x => x.Name).ToArray();
                var hashesToAdd = newHashArray.Except(actualHashArray).ToArray();

                if (hashesToRemove.Any())
                    GetDatabase(database).HashDelete(key, hashesToRemove, CommandFlags.DemandMaster);

                if (hashesToAdd.Any())
                    GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);
            }
            else
            {
                GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);
            }

            return true;
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public bool Set(string key, IDictionary<string, int> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET)
        {
            var newHashArray = keyValues.Select(hash => new HashEntry(hash.Key, hash.Value)).ToArray();

            if (removeExistingKeys)
            {
                var actualHashArray = GetDatabase(database).HashGetAll(key, CommandFlags.PreferReplica);

                var hashesToRemove = actualHashArray.Except(newHashArray).Select(x => x.Name).ToArray();
                var hashesToAdd = newHashArray.Except(actualHashArray).ToArray();

                if (hashesToRemove.Any())
                    GetDatabase(database).HashDelete(key, hashesToRemove, CommandFlags.DemandMaster);

                if (hashesToAdd.Any())
                    GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);
            }
            else
            {
                GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);
            }

            return true;
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(string key, IDictionary<string, T> keyValues, int database = (int)EDataStructure.HASHSET)
            where T : class
        {
            var newHashArray = keyValues
                .Select(hash => new HashEntry(hash.Key, MsgPackUtil.Serialize(hash.Value))).ToArray();

            GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);

            return true;
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(string key, string field, T value, int database = (int)EDataStructure.HASHSET)
            where T : class
        {
            var newHashArray = new[] { new HashEntry(field, MsgPackUtil.Serialize(value)) };

            GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);

            return true;
        }

        /// <summary>
        ///     Setar
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValues"></param>
        /// <param name="removeExistingKeys"></param>
        /// <param name="database"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Set<T>(string key, IDictionary<string, T> keyValues, bool removeExistingKeys = false,
            int database = (int)EDataStructure.HASHSET) where T : class
        {
            var newHashArray = keyValues
                .Select(v => new HashEntry(v.Key, MsgPackUtil.Serialize(v.Value))).ToArray();

            if (removeExistingKeys)
            {
                var actualHashArray = GetDatabase(database).HashGetAll(key, CommandFlags.PreferReplica);

                var hashesToRemove = actualHashArray.Except(newHashArray).Select(x => x.Name).ToArray();
                var hashesToAdd = newHashArray.Except(actualHashArray).ToArray();

                if (hashesToRemove.Any())
                    GetDatabase(database).HashDelete(key, hashesToRemove, CommandFlags.DemandMaster);

                if (hashesToAdd.Any())
                    GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);
            }
            else
            {
                GetDatabase(database).HashSet(key, newHashArray, CommandFlags.DemandMaster);
            }

            return true;
        }

        #endregion
    }
}