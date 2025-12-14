namespace Project.Domain.Share.Enums
{
    /// <summary>
    /// Enum các provider đăng nhập
    /// </summary>
    public enum LoginProvider
    {
        /// <summary>
        /// Provider nội bộ
        /// </summary>
        Internal = 0,

        /// <summary>
        /// provider Google
        /// </summary>
        Google = 1,

        /// <summary>
        /// provider Facebook
        /// </summary>
        Facebook = 2,

        /// <summary>
        /// povider Apple
        /// </summary>
        Apple = 3,
    }
}
