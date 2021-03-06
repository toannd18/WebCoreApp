﻿namespace WebCoreApp.Data
{
    public partial class Permissions
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FunctionId { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanCreate { get; set; }
        public bool CanDelete { get; set; }

        public Functions Function { get; set; }
        public AppRole Role { get; set; }
    }
}