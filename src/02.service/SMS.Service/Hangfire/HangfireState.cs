using System.Collections.Generic;
using Hangfire.States;

namespace SMS.Service.Hangfire
{
    public class HangfireState : IState
    {
        public HangfireState(IState state)
        {
            if (state != null)
            {
                Name = state.Name;
                Reason = state.Reason;
                IsFinal = state.IsFinal;
                IgnoreJobLoadException = state.IgnoreJobLoadException;

                Data = state.SerializeData();
            }
        }

        public Dictionary<string, string> Data { get; set; }

        public Dictionary<string, string> SerializeData()
        {
            return Data;
        }

        public string Name { get; set; }
        public string Reason { get; set; }
        public bool IsFinal { get; set; }
        public bool IgnoreJobLoadException { get; set; }
    }
}