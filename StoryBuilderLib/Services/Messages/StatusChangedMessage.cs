﻿using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;

namespace StoryBuilder.Services.Messages
{
    public class StatusChangedMessage : ValueChangedMessage<StatusMessage>
    {
        public StatusChangedMessage(StatusMessage value) : base(value)
        {
        }
    }
}
