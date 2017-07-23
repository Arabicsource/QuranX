public static class IntegerExtensions
{
	public static int Modulo(this int source, int m)
	{
		return (source % m + m) % m;
	}
}
