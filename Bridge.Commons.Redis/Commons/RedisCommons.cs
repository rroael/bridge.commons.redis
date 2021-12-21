using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Contracts;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Commons
{
    /// <summary>
    ///     Redis Bridge.Commons
    /// </summary>
    public class RedisCommons
    {
    private IRedisContext RedisConnectionContext;

    /// <summary>
    ///     Construtor
    /// </summary>
    /// <param name="redisContext"></param>
    public RedisCommons(IRedisContext redisContext) => RedisConnectionContext = redisContext;

    /// <summary>
    ///     Retorna uma referência do database indicado pelo índice
    /// </summary>
    /// <param name="databaseIndex">Tipo de estrutura a ser referenciada</param>
    /// <returns></returns>
    protected IDatabase GetDatabase(int databaseIndex)
    {
        return RedisConnectionContext.Database(databaseIndex);
    }

    /// <summary>
    ///     Fecha a conexão com o Redis
    /// </summary>
    public void Close()
    {
        RedisConnectionContext.Close();
    }

    /// <summary>
    ///     Retorna uma referência do server do Redis
    /// </summary>
    /// <returns></returns>
    protected IServer GetServer()
    {
        return RedisConnectionContext.Server();
    }

    /// <summary>
    ///     Remove a estrutura
    /// </summary>
    /// <param name="key">Key da estrutura</param>
    /// <param name="databaseIndex">Tipo de estrutura a ser removida</param>
    /// <returns>True em caso positivo</returns>
    protected bool KeyDelete(string key, int databaseIndex)
    {
        return GetDatabase(databaseIndex).KeyDelete(key, CommandFlags.DemandMaster);
    }

    /// <summary>
    ///     Remove a estrutura, assincronamente
    /// </summary>
    /// <param name="key">Key da estrutura</param>
    /// <param name="databaseIndex">Tipo de estrutura a ser removida</param>
    /// <returns>True em caso positivo</returns>
    protected async Task<bool> KeyDeleteAsync(string key, int databaseIndex)
    {
        return await GetDatabase(databaseIndex).KeyDeleteAsync(key, CommandFlags.DemandMaster);
    }

    /// <summary>
    ///     Verifica se a estrutura existe
    /// </summary>
    /// <param name="key">Key da estrutura</param>
    /// <param name="databaseIndex">Tipo de estrutura a ser verificada</param>
    /// <returns>True em caso positivo</returns>
    protected bool KeyExists(string key, int databaseIndex)
    {
        return GetDatabase(databaseIndex).KeyExists(key, CommandFlags.PreferReplica);
    }

    /// <summary>
    ///     Retorna o Time To Live da chave em TimeSpan?
    /// </summary>
    /// <param name="key">Key da estrutura</param>
    /// <param name="databaseIndex">Tipo de estrutura a ser verificada</param>
    /// <returns>Timespan de tempo de vida restante</returns>
    protected async Task<TimeSpan?> KeyTimeToLive(string key, int databaseIndex)
    {
        return await GetDatabase(databaseIndex).KeyTimeToLiveAsync(key, CommandFlags.PreferReplica);
    }

    /// <summary>
    ///     Retorna chaves com o padrão selecionado
    /// </summary>
    /// <param name="search">Trecho da chave</param>
    /// <param name="databaseIndex">Tipo da estrutura a ser verificada</param>
    /// <returns>Lista de chaves Redis</returns>
    protected IEnumerable<RedisKey> GetKeys(string search, int databaseIndex)
    {
        return RedisConnectionContext.Server()
            .Keys(database: (int)databaseIndex, pattern: "*" + search + "*", flags: CommandFlags.PreferReplica);
    }

    /// <summary>
    ///     Verifica se a estrutura existe, assincronamente
    /// </summary>
    /// <param name="key">Key da estrutura</param>
    /// <param name="databaseIndex">Tipo de estrutura a ser verificada</param>
    /// <returns>True em caso positivo</returns>
    protected async Task<bool> KeyExistsAsync(string key, int databaseIndex)
    {
        return await GetDatabase(databaseIndex).KeyExistsAsync(key, CommandFlags.PreferReplica);
    }

    /// <summary>
    ///     Seta o tempo definido para uma chave existente no Redis
    /// </summary>
    /// <param name="key">Key da estrutura</param>
    /// <param name="databaseIndex">Tipo de estrutura a ser verificada</param>
    /// <param name="hoursDuration">Duração do tempo de vida da key</param>
    /// <returns></returns>
    protected bool KeySetTtl(string key, int databaseIndex, int hoursDuration)
    {
        return hoursDuration != 0 && KeySetTtl(key, databaseIndex, TimeSpan.FromHours(hoursDuration));
    }

    /// <summary>
    ///     Seta o tempo definido para uma chave existente no Redis
    /// </summary>
    /// <param name="key">Key da estrutura</param>
    /// <param name="databaseIndex">Tipo de estrutura a ser verificada</param>
    /// <param name="duration">Duração do tempo de vida da key</param>
    /// <returns></returns>
    protected bool KeySetTtl(string key, int databaseIndex, TimeSpan? duration)
    {
        return GetDatabase(databaseIndex).KeyExpire(key, duration, CommandFlags.DemandMaster);
    }
    }
}