namespace EBTCO.Core.Contract
{
    public interface IAESEncryptor
    {
        String Encrypt(String plainText);
        String Decrypt(String cipher);
    }
}
