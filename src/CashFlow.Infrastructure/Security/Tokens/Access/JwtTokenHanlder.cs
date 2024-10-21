using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CashFlow.Infrastructure.Security.Tokens.Access;

public abstract class JwtTokenHanlder
{

    // Método privado que cria a chave simétrica a partir da string de assinatura
    protected static SymmetricSecurityKey CreateSecurityKey(string signingKey)
    {
        // Converte a chave de string para um array de bytes usando codificação UTF-8
        var toBytes = Encoding.UTF8.GetBytes(signingKey);

        // Retorna a chave simétrica gerada a partir dos bytes
        return new SymmetricSecurityKey(toBytes);
    }
}
