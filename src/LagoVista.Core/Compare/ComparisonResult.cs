﻿using LagoVista.Core.Interfaces;
using LagoVista.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LagoVista.Core.Compare
{
    public class AuditTrail : IIDEntity
    {
        [JsonProperty("id")]
        public String Id { get; set; }
        public String DateStamp { get; set; }
        public String EntityName { get; set; }
        public String EntityId { get; set; }
        
        public EntityHeader User { get; set; }
        public EntityHeader Organization { get; set; }

        public List<Change> Changes { get; set; }

        [JsonIgnore()]
        public bool IsDirty { get { return Changes.Count > 0; } }
    }

    public class Change
    {
        public String Name { get; set; }
        public String OldValue { get; set; }
        public String NewValue { get; set; }
        public String Message { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
