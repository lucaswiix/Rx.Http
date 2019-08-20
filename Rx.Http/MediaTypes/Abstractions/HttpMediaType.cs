﻿using Rx.Http.Serializers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rx.Http.MediaTypes.Abstractions
{
    public abstract class HttpMediaType
    {
        public IBodySerializer Serializer { get; private set; }

        public HttpMediaType(IBodySerializer serializer)
        {
            Serializer = serializer;
        }

        public bool CanSerialize(string mediaType)
        {
            return !string.IsNullOrEmpty(mediaType) && 
                SupportedMimeTypes.Contains(mediaType.ToLower().Trim());
        }

        public abstract string[] SupportedMimeTypes { get; }
    }
}