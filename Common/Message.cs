using System;
using Newtonsoft.Json;

namespace Common
{
    [Serializable]
    public class Message
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool Processed { get; set; }
    }
}
