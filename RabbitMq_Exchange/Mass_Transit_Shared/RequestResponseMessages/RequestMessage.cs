﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Transit_Shared.RequestResponseMessages
{
    public  record RequestMessage
    {
        public int MessageNo { get; set; }
        public string Text { get; set; }
    }
}