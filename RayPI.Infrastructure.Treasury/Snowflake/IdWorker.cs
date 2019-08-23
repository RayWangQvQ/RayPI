using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Treasury.Snowflake
{
    public class IdWorker
    {
        /// <summary>The _last timestamp</summary>
        private long _lastTimestamp = -1;
        /// <summary>The _lock</summary>
        private readonly object _lock = new object();
        /// <summary>The twepoch</summary>
        public const long Twepoch = 1288834974657;
        /// <summary>The worker identifier bits</summary>
        private const int WorkerIdBits = 5;
        /// <summary>The datacenter identifier bits</summary>
        private const int DatacenterIdBits = 5;
        /// <summary>The sequence bits</summary>
        private const int SequenceBits = 12;
        /// <summary>The maximum worker identifier</summary>
        private const long MaxWorkerId = 31;
        /// <summary>The maximum datacenter identifier</summary>
        private const long MaxDatacenterId = 31;
        /// <summary>The worker identifier shift</summary>
        private const int WorkerIdShift = 12;
        /// <summary>The datacenter identifier shift</summary>
        private const int DatacenterIdShift = 17;
        /// <summary>The timestamp left shift</summary>
        public const int TimestampLeftShift = 22;
        /// <summary>The sequence mask</summary>
        private const long SequenceMask = 4095;
        /// <summary>The _sequence</summary>
        private long _sequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLC.Treasury.Snowflake.IdWorker" /> class.
        /// </summary>
        /// <param name="workerId">The worker identifier.</param>
        /// <param name="datacenterId">The datacenter identifier.</param>
        /// <param name="sequence">The sequence.</param>
        /// <exception cref="!:System.ArgumentException">
        /// </exception>
        public IdWorker(long workerId, long datacenterId, long sequence = 0)
        {
            this.WorkerId = workerId;
            this.DatacenterId = datacenterId;
            this._sequence = sequence;
            if (workerId > 31L || workerId < 0L)
                throw new ArgumentException(string.Format("worker Id can't be greater than {0} or less than 0", (object)31L));
            if (datacenterId > 31L || datacenterId < 0L)
                throw new ArgumentException(string.Format("datacenter Id can't be greater than {0} or less than 0", (object)31L));
        }

        /// <summary>Gets or sets the worker identifier.</summary>
        /// <value>The worker identifier.</value>
        public long WorkerId { get; protected set; }

        /// <summary>Gets or sets the datacenter identifier.</summary>
        /// <value>The datacenter identifier.</value>
        public long DatacenterId { get; protected set; }

        /// <summary>Gets the sequence.</summary>
        /// <value>The sequence.</value>
        public long Sequence
        {
            get
            {
                return this._sequence;
            }
            internal set
            {
                this._sequence = value;
            }
        }

        /// <summary>Nexts the identifier.</summary>
        /// <returns>System.Int64.</returns>
        /// <exception cref="T:NLC.Treasury.Snowflake.InvalidSystemClock"></exception>
        public virtual long NextId()
        {
            lock (this._lock)
            {
                long num = this.TimeGen();
                if (num < this._lastTimestamp)
                    throw new InvalidSystemClockException(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds", (object)(this._lastTimestamp - num)));
                if (this._lastTimestamp == num)
                {
                    this._sequence = this._sequence + 1L & 4095L;
                    if (this._sequence == 0L)
                        num = this.TilNextMillis(this._lastTimestamp);
                }
                else
                    this._sequence = 0L;
                this._lastTimestamp = num;
                return num - 1288834974657L << 22 | this.DatacenterId << 17 | this.WorkerId << 12 | this._sequence;
            }
        }

        /// <summary>Tils the next millis.</summary>
        /// <param name="lastTimestamp">The last timestamp.</param>
        /// <returns>System.Int64.</returns>
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            long num = this.TimeGen();
            while (num <= lastTimestamp)
                num = this.TimeGen();
            return num;
        }

        /// <summary>Times the gen.</summary>
        /// <returns>System.Int64.</returns>
        protected virtual long TimeGen()
        {
            return SnowflakeHelper.CurrentTimeMillis();
        }
    }
}
