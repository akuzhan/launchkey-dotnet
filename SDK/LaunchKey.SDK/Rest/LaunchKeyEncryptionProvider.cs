using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Utility class for performing common encryption/decryption/signing tasks.
	/// Uses RSA OAEP-SHA1 encoding for encryption tasks, and RSA SHA256 for signing
	/// </summary>
	public class LaunchKeyEncryptionProvider
	{
		private RsaEngine engine;
		private OaepEncoding encoding;
		private RsaDigestSigner signer;

		/// <summary>
		/// Default constructor
		/// </summary>
		public LaunchKeyEncryptionProvider()
		{
			this.engine = new RsaEngine();
			this.encoding = new OaepEncoding(this.engine, new Sha1Digest());
			this.signer = new RsaDigestSigner(new Sha256Digest());
		}

		/// <summary>
		/// Decrypt <paramref name="data"/> using <paramref name="key"/> and RSA OAEP-SHA1
		/// </summary>
		/// <param name="key">The key to use when decrypting</param>
		/// <param name="data">The data to decrypt</param>
		/// <returns>decrypted data</returns>
		public byte[] Decrypt(AsymmetricKeyParameter key, byte[] data)
		{
			encoding.Init(false, key);
			byte[] encodedBundle = encoding.ProcessBlock(data, 0, data.Length);
			return encodedBundle;
		}

		/// <summary>
		/// Encrypt <paramref name="data"/> using <paramref name="key"/> and RSA OAEP-SHA1
		/// </summary>
		/// <param name="key">The key to use when encrypting</param>
		/// <param name="data">The data to encrypt</param>
		/// <returns>encrypted data</returns>
		public byte[] Encrypt(AsymmetricKeyParameter key, byte[] data)
		{
			encoding.Init(true, key);
			byte[] encodedBundle = encoding.ProcessBlock(data, 0, data.Length);
			return encodedBundle;
		}

		/// <summary>
		/// Creates a RSA SHA256 signature of <paramref name="data"/> using <paramref name="key"/>
		/// </summary>
		/// <param name="key">The key to use when signing</param>
		/// <param name="data">The data to sign</param>
		/// <returns>digest signature</returns>
		public byte[] Sign(AsymmetricKeyParameter key, byte[] data)
		{
			signer.Init(true, key);
			signer.BlockUpdate(data, 0, data.Length);
			return signer.GenerateSignature();
		}

		/// <summary>
		/// Verifies an RSA SHA256 signature of <paramref name="data"/> using <paramref name="key"/>
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <param name="signature"></param>
		/// <returns></returns>
		public bool VerifySignature(RsaKeyParameters key, byte[] data, byte[] signature)
		{
			signer.Init(false, key);
			signer.BlockUpdate(data, 0, data.Length);
			return signer.VerifySignature(signature);
		}
	}
}
