using Nethereum.Signer;
using Nethereum.Web3;   

namespace Novaxisgen.Services
{
    public class WalletService
    {
        public (string Address, string PrivateKey) TaoViMoi()
        {
            var key = EthECKey.GenerateKey();
            var privateKey = key.GetPrivateKey();
            var address = key.GetPublicAddress();
            return (address, privateKey);
        }
    }
}
