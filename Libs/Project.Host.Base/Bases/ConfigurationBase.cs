namespace Project.Host.Base.Bases
{
    public static class ConfigurationBase
    {
        // Phương thức này giờ sẽ trả về IConfigurationBuilder
        public static IConfigurationBuilder AddBaseConfiguration(this IConfigurationBuilder builder, List<string> fileConfigs)
        {
            var pathConfigs = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Configs");

            builder.SetBasePath(pathConfigs);

            foreach (var file in fileConfigs)
            {
                builder.AddJsonFile(file, optional: true, reloadOnChange: true);
            }

            return builder;
        }
    }
}
