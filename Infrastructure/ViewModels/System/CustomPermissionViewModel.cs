namespace WebCoreApp.Infrastructure.ViewModels.System
{
    public class CustomPermissionViewModel
    {
        public string Role { get; set; }

        public string Id { get; set; }

        public bool View { get; set; }

        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }
}