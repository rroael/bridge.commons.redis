using System.Threading.Tasks;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Contracts
{
    /// <summary>
    ///     Redis Context
    /// </summary>
    public interface IRedisContext
    {
        /// <summary>
        ///     URL do servidor
        /// </summary>
        string[] RedisServers { get; set; }

        /// <summary>
        ///     Conexão com o servidor
        /// </summary>
        ConnectionMultiplexer Connection { get; set; }

        /// <summary>
        ///     Fecha a conexão
        /// </summary>
        void Close();

        /// <summary>
        ///     Se está conectado ao servidor do Redis
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        ///     Checa se está conectado (Checagem real)
        /// </summary>
        /// <returns></returns>
        bool CheckConnection();

        /// <summary>
        ///     Checa se está conectado (Checagem real) - Assíncrono
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckConnectionAsync();

        /// <summary>
        ///     Referência à database especificada
        /// </summary>
        /// <param name="databaseIndex">Índice da database a buscar</param>
        /// <returns></returns>
        IDatabase Database(int databaseIndex);

        /// <summary>
        ///     Referência ao servidor do Redis (busca de chaves etc)
        /// </summary>
        /// <returns></returns>
        IServer Server();
    }
}