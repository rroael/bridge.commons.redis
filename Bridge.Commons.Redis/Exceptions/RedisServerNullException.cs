using System;

namespace Bridge.Commons.Redis.Exceptions
{
    /// <summary>
    ///     Exceção de null no servidor do Redis
    /// </summary>
    public class RedisServerNullException : Exception
    {
        /// <summary>
        ///     Contrutor
        /// </summary>
        public RedisServerNullException() : base("Could not connect to Redis. Redis server is null.")
        {
        }

        /// <summary>
        ///     Contrutor
        /// </summary>
        /// <param name="message"></param>
        public RedisServerNullException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Contrutor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public RedisServerNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}