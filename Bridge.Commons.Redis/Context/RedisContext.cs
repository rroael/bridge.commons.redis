using System;
using System.Threading.Tasks;
using Bridge.Commons.Redis.Contracts;
using Bridge.Commons.Redis.Enums;
using Bridge.Commons.Redis.Exceptions;
using StackExchange.Redis;

namespace Bridge.Commons.Redis.Context
{
    /// <summary>
    ///     Contexto do Redis
    /// </summary>
    public class RedisContext : IRedisContext
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="redisServersUrls">Aceita mais de uma URL</param>
        /// <example>https://master.domain.com;https://slave.domain.com</example>
        public RedisContext(params string[] redisServersUrls)
        {
            RedisServers = redisServersUrls;
            Connect();
        }

        private ConfigurationOptions Configuration { get; set; }

        /// <summary>
        ///     Servidor do Redis
        /// </summary>
        public string[] RedisServers { get; set; }

        /// <summary>
        ///     Conexão
        /// </summary>
        public ConnectionMultiplexer Connection { get; set; }

        /// <summary>
        ///     Checar conexão
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            var keyCheckConnection = "TST_CHECK_CONNECTION_" + DateTime.UtcNow.Ticks;

            var inserted = Database((int)EDataStructure.SINGLE).StringSet(keyCheckConnection, keyCheckConnection);

            var deleted = Database((int)EDataStructure.SINGLE).KeyDelete(keyCheckConnection);

            return inserted && deleted;
        }

        /// <summary>
        ///     Checar conexão (assíncrono)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckConnectionAsync()
        {
            var keyCheckConnection = "TST_CHECK_CONNECTION_" + DateTime.UtcNow.Ticks;

            var inserted = await Database((int)EDataStructure.SINGLE)
                .StringSetAsync(keyCheckConnection, keyCheckConnection);

            var deleted = await Database((int)EDataStructure.SINGLE).KeyDeleteAsync(keyCheckConnection);

            return inserted && deleted;
        }

        /// <summary>
        ///     Fechar
        /// </summary>
        public void Close()
        {
            if (Connection != null && Connection.IsConnected)
                Connection.Close();
        }

        /// <summary>
        ///     Checa se há conexão
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return Connection != null && Connection.IsConnected;
        }

        /// <summary>
        ///     Banco de dados
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <returns></returns>
        public IDatabase Database(int databaseIndex)
        {
            return Connection.GetDatabase(databaseIndex);
        }

        /// <summary>
        ///     Servidor
        /// </summary>
        /// <returns></returns>
        public IServer Server()
        {
            var endpoints = Connection.GetEndPoints();

            return Connection.GetServer(endpoints.Length > 1 ? endpoints[1] : endpoints[0]);
        }

        /// <summary>
        ///     Dispor
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        private void Connect()
        {
            if (RedisServers == null || RedisServers.Length == 0)
                throw new RedisServerNullException();

            SetConfiguration();

            if (Connection == null)
                Connection = ConnectionMultiplexer.Connect(Configuration);
            else if (!Connection.IsConnected)
                Connection = ConnectionMultiplexer.Connect(Configuration);
        }

        private void SetConfiguration()
        {
            Configuration = new ConfigurationOptions
            {
                ConnectTimeout = 300000,
                SyncTimeout = 300000
            };

            foreach (var rs in RedisServers)
                Configuration.EndPoints.Add(rs);
        }

        /// <summary>
        ///     Fechar
        /// </summary>
        ~RedisContext()
        {
            Close();
        }
    }
}