using System;
using MvvmCross.Plugins.Messenger;

namespace Orbi.Messages
{
    public class DoneBtnMessage : MvxMessage
    {
        public bool Enabled { get; private set; }

        public DoneBtnMessage(object sender, bool enabled) : base(sender)
        {
            Enabled = enabled;
        }
    }
}
