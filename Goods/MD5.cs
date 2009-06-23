using System.IO;
using System.Security.Cryptography;

namespace Goods {
  /// <summary>
  /// Calculates an MD5 digest.
  /// </summary>
  public static class MD5 {
    static readonly MD5CryptoServiceProvider m_MD5 = new MD5CryptoServiceProvider();

    /// <summary>
    /// Returns the MD5 digest of a specified file as a string.
    /// </summary>
    /// <param name="path">The full path of the file.</param>
    /// <returns>MD5 digest as a string.</returns>
    public static string Calculate(string path) {
      return DigestToString(CalculateDigest(path));
    }

    /// <summary>
    /// Returns the MD5 digest of an input stream as a string.
    /// </summary>
    /// <param name="stream">Input stream.</param>
    /// <returns>MD5 digest as a string.</returns>
    public static string Calculate(Stream stream) {
      return DigestToString(CalculateDigest(stream));
    }

    /// <summary>
    /// Returns the MD5 digest of a byte array as a string.
    /// </summary>
    /// <param name="data">The byte array.</param>
    /// <returns>MD5 digest as a string.</returns>
    public static string Calculate(byte[] data) {
      return DigestToString(CalculateDigest(data));
    }

    /// <summary>
    /// Returns the MD5 digest of a specified file as a byte array.
    /// </summary>
    /// <param name="path">The full path of the file.</param>
    /// <returns>MD5 digest as a byte array.</returns>
    public static byte[] CalculateDigest(string path) {
      using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        return CalculateDigest(fileStream);
    }

    /// <summary>
    /// Returns the MD5 digest of an input stream as a byte array.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>MD5 digest as a byte array.</returns>
    public static byte[] CalculateDigest(Stream stream) {
      stream.Position = 0;
      return m_MD5.ComputeHash(stream);
    }

    /// <summary>
    /// Returns the MD5 digest of a byte array as a byte array.
    /// </summary>
    /// <param name="data">The byte array.</param>
    /// <returns>MD5 digest as a byte array.</returns>
    public static byte[] CalculateDigest(byte[] data) {
      using (var memoryStream = new MemoryStream(data))
        return CalculateDigest(memoryStream);
    }

    static string DigestToString(byte[] digest) {
      string result = string.Empty;
      foreach (byte item in digest)
        result += string.Format("{0:x2}", item);
      return result;
    }
  }
}