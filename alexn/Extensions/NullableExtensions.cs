namespace alexn.Extensions {
    public static class NullableExtensions {
        public static T ValueOr<T>(this T? instance, T @default) where T : struct {
            return instance.GetValueOrDefault(@default);
        }
    }
}
