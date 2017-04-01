﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NetCoreCMS.Framework.Core.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCMS.Framework.Core.Models
{
    public class NccRole : IdentityRole<long, NccUserRole, IdentityRoleClaim<long>>, IBaseModel<long>
    {
        public int VersionNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long CreateBy { get; set; }
        public long ModifyBy { get; set; }
        public int Status { get; set; }
    }
}
