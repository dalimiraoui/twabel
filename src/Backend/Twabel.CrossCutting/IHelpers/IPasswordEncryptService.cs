
namespace Twabel.CrossCutting.IHelpers
{
    public interface IPasswordEncryptService
    {
        string Encrypt(string password);
        bool CheckIfMatch(string hash, string password);
    }
}
