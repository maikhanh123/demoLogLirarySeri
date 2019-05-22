using System;

namespace Klogger
{
    public class KloggerDetail
    {
        public KloggerDetail()
        {
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; private set; }
        public string Message { get; set; }
    }
}
